using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;

namespace DiscoverDotnet.Models
{
    public class FeedItem
    {
        public string Title { get; }
        public string Link { get; }
        public string Description { get; }
        public DateTimeOffset Published { get; }
        public bool Recent { get; }
        public IDictionary<string, string> Links { get; }
        public string Author { get; }

        public FeedItem(ISyndicationItem item, DateTimeOffset recent, Uri website)
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
            Description = item.Description;
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

            AtomEntry atom = item as AtomEntry;
            if (atom != null && !string.IsNullOrEmpty(atom.Summary))
            {
                Description = atom.Summary;
            }
        }
    }
}
