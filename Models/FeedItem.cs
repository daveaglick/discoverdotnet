using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AngleSharp.Dom;
using AngleSharp.Html.Dom;
using AngleSharp.Html.Parser;
using Microsoft.Extensions.Logging;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;
using Statiq.Common;

namespace DiscoverDotnet.Models
{
    public class FeedItem
    {
        public string Title { get; }
        public string Link { get; }
        public string Description { get; private set; }
        public DateTimeOffset Published { get; }
        public bool Recent { get; }
        public IDictionary<string, string> Links { get; }
        public string Author { get; }

        public FeedItem(ISyndicationItem item, DateTimeOffset recent, Uri website, bool truncateDescription, IExecutionContext context)
        {
            Title = item.Title;
            ISyndicationLink firstLink = item.Links.FirstOrDefault(x => x.RelationshipType == RssLinkTypes.Alternate);
            if (firstLink != null)
            {
                Link = firstLink.Uri.IsAbsoluteUri ? firstLink.Uri.AbsoluteUri : new Uri(website, firstLink.Uri).AbsoluteUri;
            }
            else
            {
                Link = item.Id;
            }

            Published = item.Published != default ? item.Published : item.LastUpdated;
            Recent = Published > recent;
            Links = item.Links
                .Where(x => !string.IsNullOrEmpty(x.MediaType))
                .GroupBy(x => x.MediaType)
                .Select(x => x.First())
                .ToDictionary(x => x.MediaType, x => x.Uri.ToString());

            ISyndicationPerson person = item.Contributors.FirstOrDefault(x => x.RelationshipType == "author");
            if (person != null)
            {
                Author = person.Name ?? person.Email;
            }

            Description = item.Description;
            AtomEntry atom = item as AtomEntry;
            if (atom != null && !string.IsNullOrEmpty(atom.Summary))
            {
                Description = atom.Summary;
            }

            if (truncateDescription)
            {
                try
                {
                    // Use the first <p> if one is found
                    HtmlParser htmlParser = new HtmlParser();
                    IHtmlDocument htmlDocument = htmlParser.ParseDocument(Description);
                    IElement element = htmlDocument.QuerySelector("p");
                    if (element is object && !string.IsNullOrWhiteSpace(element.OuterHtml))
                    {
                        Description = element.OuterHtml;
                    }
                }
                catch (Exception ex)
                {
                    context.LogWarning($"Error parsing HTML description for feed {Title}: {ex.Message}");
                }
            }
        }
    }
}
