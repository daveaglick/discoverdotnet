using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Algolia.Search.Clients;
using Algolia.Search.Http;
using Algolia.Search.Models.Common;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class UpdateSearchIndex : MultiConfigModule
    {
        // Config keys
        private const string ApplicationId = nameof(ApplicationId);
        private const string ApiKey = nameof(ApiKey);
        private const string IndexName = nameof(IndexName);
        private const string IndexKeys = nameof(IndexKeys);

        public UpdateSearchIndex(
            Config<string> applicationId,
            Config<string> apiKey,
            Config<string> indexName,
            Config<IEnumerable<string>> indexKeys)
            : base(
                new Dictionary<string, IConfig>
                {
                    { ApplicationId, applicationId.EnsureNonDocument(nameof(applicationId)) },
                    { ApiKey, apiKey.EnsureNonDocument(nameof(apiKey)) },
                    { IndexName, indexName.EnsureNonDocument(nameof(indexName)) },
                    { IndexKeys, indexKeys.EnsureNonDocument(nameof(indexKeys)) }
                },
                false)
        {
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteConfigAsync(IDocument input, IExecutionContext context, IMetadata values)
        {
            string applicationId = values.GetString(ApplicationId) ?? throw new ExecutionException("Invalid application ID");
            string apiKey = values.GetString(ApiKey) ?? throw new ExecutionException("Invalid search API key");
            string indexName = values.GetString(IndexName) ?? throw new ExecutionException("Invalid search index name");
            IReadOnlyList<string> indexKeys = values.GetList<string>(IndexKeys) ?? throw new ExecutionException("Invalid index keys");

            SearchClient client = new SearchClient(applicationId, apiKey);
            SearchIndex index = client.InitIndex(indexName);

            // Get the entire index
            HashSet<JObject> existing = new HashSet<JObject>(new SearchIndexItemEqualityComparer());
            BrowseIndexResponse<JObject> results;
            int page = 0;
            do
            {
                RequestOptions options = new RequestOptions
                {
                    QueryParameters = new Dictionary<string, string>
                    {
                        { "page", page.ToString() }
                    }
                };
                results = await index.BrowseFromAsync<JObject>(new BrowseIndexQuery(), options, context.CancellationToken);
                existing.AddRange(results.Hits);
                page++;
            }
            while (page < results.NbPages);

            // Figure out what we need to add and remove
            List<JObject> adds = new List<JObject>();
            foreach (IDocument document in context.Inputs)
            {
                // Create a JObject for the item
                JObject item = new JObject();
                foreach (string indexKey in indexKeys)
                {
                    if (document.ContainsKey(indexKey))
                    {
                        string key = indexKey.Substring(0, 1).ToLower() + indexKey.Substring(1);
                        string value = document.GetString(indexKey);
                        item.Add(key, value);
                    }
                }

                // Is it already in the index?
                if (!existing.Remove(item))
                {
                    // It wasn't matched, so this is a new one
                    adds.Add(item);
                }
            }

            context.LogInformation($"Search index \"{indexName}\": deleting {existing.Count}, adding {adds.Count}");

            // Update the search index
            if (existing.Count > 0)
            {
                await index.DeleteObjectsAsync(existing.Select(x => x.Property("objectID").Value.ToString()), ct: context.CancellationToken);
            }
            if (adds.Count > 0)
            {
                await index.SaveObjectsAsync(
                    adds,
                    ct: context.CancellationToken,
                    autoGenerateObjectId: true);
            }

            return context.Inputs;
        }

        private class SearchIndexItemEqualityComparer : IEqualityComparer<JObject>
        {
            public bool Equals(JObject a, JObject b)
            {
                return a.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name)
                    .SequenceEqual(b.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name));
            }

            public int GetHashCode(JObject obj)
            {
                int hash = 27;
                foreach ((string Name, string Value) prop in obj.Properties().Where(x => x.Name != "objectID").Select(x => (x.Name, x.Value.ToString())).OrderBy(x => x.Name))
                {
                    hash = (13 * hash) + prop.Name.GetHashCode();
                    hash = (13 * hash) + prop.Value.GetHashCode();
                }
                return hash;
            }
        }
    }
}
