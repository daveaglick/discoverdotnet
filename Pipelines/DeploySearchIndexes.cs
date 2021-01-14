using Statiq.Common;
using Statiq.Core;

namespace DiscoverDotnet.Pipelines
{
    public class DeploySearchIndexes : Pipeline
    {
        private const string ApplicationId = "7TKEQH0O12";

        public DeploySearchIndexes()
        {
            Deployment = true;

            OutputModules = new ModuleList
            {
                new ThrowException(Config.FromSettings(x => x.ContainsKey("ALGOLIA_TOKEN") ? null : "The setting ALGOLIA_TOKEN must be defined")),

                // Projects
                /*
                new ReplaceDocuments(nameof(Projects.Projects)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "projects",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Key,
                        SiteKeys.Title,
                        SiteKeys.Description,
                        SiteKeys.StargazersCount,
                        SiteKeys.Tags
                    })),

                // Blogs
                new ReplaceDocuments(nameof(Blogs.Blogs)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "blogs",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Key,
                        SiteKeys.Title,
                        SiteKeys.Description
                    })),

                // Posts
                new ReplaceDocuments(nameof(Posts)),
                new FilterDocuments(Config.FromDocument(doc => doc.ContainsKey(SiteKeys.FeedItems) && doc.GetString(SiteKeys.Language) == "English")),
                new ExecuteConfig(Config.FromDocument((doc, ctx) => doc
                    .Get<IEnumerable<FeedItem>>(SiteKeys.FeedItems)
                    .Select(x => ctx.CreateDocument(new MetadataItems
                    {
                        { SiteKeys.Title, x.Title },
                        { SiteKeys.Link, x.Link },
                        { SiteKeys.Published, x.Published }
                    })))),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "posts",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Title,
                        SiteKeys.Link,
                        SiteKeys.Published
                    })),

                // Broadcasts
                new ReplaceDocuments(nameof(Broadcasts.Broadcasts)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "broadcasts",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Key,
                        SiteKeys.Title,
                        SiteKeys.Description
                    })),

                // Episodes
                new ReplaceDocuments(nameof(Episodes)),
                new FilterDocuments(Config.FromDocument(doc => doc.ContainsKey(SiteKeys.FeedItems) && doc.GetString(SiteKeys.Language) == "English")),
                new ExecuteConfig(Config.FromDocument((doc, ctx) => doc
                    .Get<IEnumerable<FeedItem>>(SiteKeys.FeedItems)
                    .Select(x => ctx.CreateDocument(new MetadataItems
                    {
                        { SiteKeys.Title, x.Title },
                        { SiteKeys.Link, x.Link },
                        { SiteKeys.Published, x.Published }
                    })))),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "episodes",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Title,
                        SiteKeys.Link,
                        SiteKeys.Published
                    })),

                // Resources
                new ReplaceDocuments(nameof(Resources.Resources)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "resources",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        SiteKeys.Website,
                        SiteKeys.Title,
                        SiteKeys.Description,
                        SiteKeys.Tags
                    })),
            */
            };
        }
    }
}
