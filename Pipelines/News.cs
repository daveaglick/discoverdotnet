using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public class News : Pipeline
    {
        public News()
        {
            Dependencies.AddRange(new[]
            {
                nameof(Posts),
                nameof(Episodes)
            });

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(c => c.Outputs
                        .FromPipelines(nameof(Posts), nameof(Episodes))
                        .SelectMany(d => d.Get<IEnumerable<FeedItem>>(SiteKeys.FeedItems)
                            .Where(p => p.Recent)
                            .Select(p => new
                            {
                                FeedLink = d.GetString(SiteKeys.Link),
                                FeedTitle = d.GetString(SiteKeys.Title),
                                p.Published,
                                p.Link,
                                p.Title,
                                p.Links,
                                p.Author
                            }))
                        .OrderByDescending(x => x.Published)))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((NormalizedPath)"data/news.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
