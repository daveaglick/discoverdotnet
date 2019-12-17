using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Feeds;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Feeds
{
    public class DiscoveriesFeed : Pipeline
    {
        public DiscoveriesFeed()
        {
            Dependencies.Add(nameof(Discoveries));

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(nameof(Discoveries)),
                new TakeDocuments(10),
                new GenerateFeeds()
                    .WithAtomPath("feeds/discoveries.atom")
                    .WithRssPath("feeds/discoveries.rss")
                    .WithFeedTitle("Recent Discoveries From Discover .NET")
                    .WithFeedDescription("A new discovery (almost) every day.")
                    .WithItemTitle(Config.FromDocument(doc => doc.GetString("Title")))
                    .WithItemDescription(Config.FromDocument(doc => doc.GetString("Description")))
                    .WithItemPublished(Config.FromDocument(doc => (DateTime?)doc.GetDateTime("DiscoveryDate")))
                    .WithItemLink(Config.FromDocument((doc, ctx) => TypeHelper.Convert<Uri>(ctx.GetLink(doc.GetString("Link"), true))))
                    .WithItemId(Config.FromDocument((doc, ctx) => TypeHelper.Convert<Uri>(ctx.GetLink(doc.GetString("Link"), true))))
                    .WithItemImageLink(null)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
