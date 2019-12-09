using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Netlify;
using Statiq.Sass;

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
                new ReplaceDocuments(nameof(Projects.Projects)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "projects",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Key",
                        "Title",
                        "Description",
                        "StargazersCount",
                        "Tags"
                    })),

                // Blogs
                new ReplaceDocuments(nameof(Blogs.Blogs)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "blogs",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Key",
                        "Title",
                        "Description"
                    })),

                // Posts
                new ReplaceDocuments(nameof(Posts)),
                new FilterDocuments(Config.FromDocument(doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English")),
                new ExecuteConfig(Config.FromDocument((doc, ctx) => doc
                    .Get<IEnumerable<FeedItem>>("FeedItems")
                    .Select(x => ctx.CreateDocument(new MetadataItems
                    {
                        { "Title", x.Title },
                        { "Link", x.Link },
                        { "Published", x.Published }
                    })))),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "posts",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Title",
                        "Link",
                        "Published"
                    })),

                // Broadcasts
                new ReplaceDocuments(nameof(Broadcasts.Broadcasts)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "broadcasts",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Key",
                        "Title",
                        "Description"
                    })),

                // Episodes
                new ReplaceDocuments(nameof(Episodes)),
                new FilterDocuments(Config.FromDocument(doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English")),
                new ExecuteConfig(Config.FromDocument((doc, ctx) => doc
                    .Get<IEnumerable<FeedItem>>("FeedItems")
                    .Select(x => ctx.CreateDocument(new MetadataItems
                    {
                        { "Title", x.Title },
                        { "Link", x.Link },
                        { "Published", x.Published }
                    })))),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "episodes",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Title",
                        "Link",
                        "Published"
                    })),

                // Resources
                new ReplaceDocuments(nameof(Resources.Resources)),
                new UpdateSearchIndex(
                    ApplicationId,
                    Config.FromSetting<string>("ALGOLIA_TOKEN"),
                    "resources",
                    Config.FromValue<IEnumerable<string>>(new[]
                    {
                        "Website",
                        "Title",
                        "Description",
                        "Tags"
                    })),
            };
        }
    }
}
