using System;
using System.Linq;
using DiscoverDotnet.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Feeds;

namespace DiscoverDotnet.Pipelines.Feeds
{
    public class NewsFeed : Pipeline
    {
        public NewsFeed()
        {
            Dependencies.AddRange(
                nameof(Blogs.Posts),
                nameof(Broadcasts.Episodes));

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(
                    nameof(Blogs.Posts),
                    nameof(Broadcasts.Episodes)),
                new ExecuteConfig(Config.FromDocument((doc, ctx) =>
                {
                    // Expand the feed items into documents (I.e. SelectMany)
                    return doc.Get<FeedItem[]>(SiteKeys.FeedItems)
                        .Select(x => ctx.CreateDocument(new MetadataItems
                        {
                            { SiteKeys.FeedItem, x }
                        }));
                })),
                new FilterDocuments(Config.FromDocument(doc => doc.Get<FeedItem>(SiteKeys.FeedItem).Recent)),
                new OrderDocuments(Config.FromDocument(doc => doc.Get<FeedItem>(SiteKeys.FeedItem).Published)).Descending(),
                new GenerateFeeds()
                    .WithAtomPath("feeds/news.atom")
                    .WithRssPath("feeds/news.rss")
                    .WithFeedTitle("Recent News From Discover .NET")
                    .WithFeedDescription("A roundup of recent blog posts, podcasts, and more.")
                    .WithItemTitle(Config.FromDocument(doc => doc.Get<FeedItem>(SiteKeys.FeedItem).Title))
                    .WithItemDescription(Config.FromDocument(doc => doc.Get<FeedItem>(SiteKeys.FeedItem).Description))
                    .WithItemPublished(Config.FromDocument(doc => (DateTime?)doc.Get<FeedItem>(SiteKeys.FeedItem).Published.DateTime))
                    .WithItemLink(Config.FromDocument(doc => TypeHelper.Convert<Uri>(doc.Get<FeedItem>(SiteKeys.FeedItem).Link)))
                    .WithItemId(Config.FromDocument(doc => TypeHelper.Convert<Uri>(doc.Get<FeedItem>(SiteKeys.FeedItem).Link).ToString()))
                    .WithItemAuthor(Config.FromDocument(doc =>
                        doc.Get<FeedItem>(SiteKeys.FeedItem).Author
                        ?? doc.GetString(SiteKeys.Author)
                        ?? doc.GetString(SiteKeys.Title)))
                    .WithItemImageLink(null)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
