using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using AngleSharp;
using DiscoverDotnet.Models;
using Microsoft.Extensions.Logging;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class GetFeedData : ParallelModule
    {
        private DateTimeOffset _recent;

        protected override void BeforeExecution(IExecutionContext context)
        {
            _recent = new DateTimeOffset(
                DateTime.Today.AddHours(context.ApplicationState.IsCommand("preview") || context.Settings.GetBool("limited") ? -168 : -48));
        }

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            string feed = input.GetString("Feed");
            if (!string.IsNullOrEmpty(feed))
            {
                try
                {
                    // Download the feed
                    context.LogInformation($"Getting feed for {feed}");
                    string website = null;
                    string title = null;
                    string author = null;
                    string description = null;
                    string image = null;
                    List<ISyndicationItem> items = new List<ISyndicationItem>();
                    using (HttpClient httpClient = context.CreateHttpClient())
                    {
                        httpClient.DefaultRequestHeaders.Add("User-Agent", "Wyam");

                        using (Stream stream = await httpClient.GetStreamAsync(feed))
                        {
                            using (StreamReader streamReader = new XmlStreamReader(stream))
                            {
                                using (XmlReader xmlReader = XmlReader.Create(streamReader, new XmlReaderSettings { Async = true, DtdProcessing = DtdProcessing.Ignore }))
                                {
                                    xmlReader.MoveToContent();
                                    bool atom = xmlReader.Name == "feed";
                                    context.LogInformation($"Reading {feed} as " + (atom ? "Atom" : "RSS"));
                                    XmlFeedReader feedReader = atom
                                        ? (XmlFeedReader)new AtomFeedReader(xmlReader, new DiscoverAtomParser())
                                        : new RssFeedReader(xmlReader);
                                    while (await feedReader.Read())
                                    {
                                        try
                                        {
                                            switch (feedReader.ElementType)
                                            {
                                                case SyndicationElementType.Person:
                                                    ISyndicationPerson person = await feedReader.ReadPerson();
                                                    if (person.RelationshipType == "author")
                                                    {
                                                        author = person.Name ?? person.Email;
                                                    }
                                                    break;

                                                case SyndicationElementType.Image:
                                                    ISyndicationImage img = await feedReader.ReadImage();
                                                    image = img.Url.ToString();
                                                    break;

                                                case SyndicationElementType.Link:
                                                    ISyndicationLink link = await feedReader.ReadLink();
                                                    website = link.Uri.ToString();
                                                    break;

                                                case SyndicationElementType.Item:
                                                    ISyndicationItem item = await feedReader.ReadItem();
                                                    items.Add(item);
                                                    break;

                                                case SyndicationElementType.None:
                                                    break;

                                                default:
                                                    ISyndicationContent content = await feedReader.ReadContent();
                                                    if (string.Equals(content.Name, "title", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        title = content.Value;
                                                    }
                                                    else if (string.Equals(content.Name, "description", StringComparison.OrdinalIgnoreCase)
                                                        || string.Equals(content.Name, "subtitle", StringComparison.OrdinalIgnoreCase))
                                                    {
                                                        description = content.Value;
                                                    }
                                                    break;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            context.LogWarning($"Exception while processing {feedReader.ElementType} in {feed}: {ex.Message}");
                                        }
                                    }
                                }
                            }
                        }
                    }

                    // Get a new document with feed metadata
                    MetadataItems metadata = new MetadataItems();
                    if (items.Count > 0)
                    {
                        FeedItem[] feedItems = items
                            .Select(x => new FeedItem(x, _recent))
                            .OrderByDescending(x => x.Published)
                            .Take(50) // Only take the 50 most recent items
                            .ToArray();
                        metadata.Add("FeedItems", feedItems);
                        metadata.Add("LastPublished", feedItems.First().Published);
                        metadata.Add("NewestFeedItem", feedItems[0]);
                    }
                    if (!input.ContainsKey("Website") && !string.IsNullOrEmpty(website))
                    {
                        metadata.Add("Website", website);
                    }
                    if (!input.ContainsKey("Title"))
                    {
                        if (!string.IsNullOrEmpty(title))
                        {
                            metadata.Add("Title", title);
                        }
                        else
                        {
                            string generatedTitle = GenerateTitleFromFeedName(feed);
                            metadata.Add("Title", generatedTitle);
                        }
                    }
                    if (!input.ContainsKey("Author") && !string.IsNullOrEmpty(author))
                    {
                        metadata.Add("Author", author);
                    }
                    if (!input.ContainsKey("Description") && !string.IsNullOrEmpty(description))
                    {
                        metadata.Add("Description", description);
                    }
                    if (!input.ContainsKey("Image") && !string.IsNullOrEmpty(image))
                    {
                        metadata.Add("Image", image);
                    }
                    return input.Clone(metadata).Yield();
                }
                catch (Exception ex)
                {
                    context.LogWarning($"Error getting feed for {feed}: {ex.Message}");
                }
            }

            return null;
        }

        private static string GenerateTitleFromFeedName(string feed)
        {
            const string slashes = "//";
            int from = feed.IndexOf(slashes) + slashes.Length;
            int to = feed.IndexOf("/", from);
            return feed.Substring(from, to - from);
        }
    }
}
