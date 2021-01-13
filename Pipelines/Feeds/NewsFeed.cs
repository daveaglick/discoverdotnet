using System;
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
                new FilterDocuments(Config.FromDocument(doc => doc.Get<FeedItem>("FeedItems").Recent)),
                new OrderDocuments(Config.FromDocument(doc => doc.Get<FeedItem>("FeedItems").Published)).Descending(),
                new GenerateFeeds()
                    .WithAtomPath("feeds/news.atom")
                    .WithRssPath("feeds/news.rss")
                    .WithFeedTitle("Recent News From Discover .NET")
                    .WithFeedDescription("A roundup of recent blog posts, podcasts, and more.")
                    .WithItemTitle(Config.FromDocument(doc => doc.Get<FeedItem>("FeedItems").Title))
                    .WithItemDescription(Config.FromDocument(doc => doc.Get<FeedItem>("FeedItems").Description))
                    .WithItemPublished(Config.FromDocument(doc => (DateTime?)doc.Get<FeedItem>("FeedItems").Published.DateTime))
                    .WithItemLink(Config.FromDocument(doc => TypeHelper.Convert<Uri>(doc.Get<FeedItem>("FeedItems").Link)))
                    .WithItemId(Config.FromDocument(doc => TypeHelper.Convert<Uri>(doc.Get<FeedItem>("FeedItems").Link).ToString()))
                    .WithItemAuthor(Config.FromDocument(doc =>
                        doc.Get<FeedItem>("FeedItems").Author
                        ?? doc.GetString("Author")
                        ?? doc.GetString("Title")))
                    .WithItemImageLink(null)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
