using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;

namespace DiscoverDotnet
{
    public class FeedItemData
    {
        public string Title { get; }
        public string Link { get; }
        public string Description { get; }
        public DateTimeOffset Published { get; }
        public bool Recent { get; }
        public IDictionary<string, string> Links { get; }
        public string Author { get; }

        public FeedItemData(ISyndicationItem item, DateTimeOffset recent)
        {
            Title = item.Title;
            Link = item.Links.FirstOrDefault(x => x.RelationshipType == RssLinkTypes.Alternate)?.Uri.ToString() ?? item.Id;
            Published = item.Published != default(DateTimeOffset) ? item.Published : item.LastUpdated;
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
