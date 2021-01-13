using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.SyndicationFeed;
using Microsoft.SyndicationFeed.Atom;
using Microsoft.SyndicationFeed.Rss;

namespace DiscoverDotnet
{
    // See https://github.com/dotnet/SyndicationFeedReaderWriter/issues/31
    public class DiscoverAtomParser : AtomParser
    {
        public override IAtomEntry CreateEntry(ISyndicationContent content)
        {
            // Remove author and contributor entries if they don't contain an email or name
            ICollection<ISyndicationContent> children = (ICollection<ISyndicationContent>)content.Fields;
            ISyndicationContent author = children.FirstOrDefault(x => x.Name == AtomContributorTypes.Author);
            if (author != null
                && author.Fields.FirstOrDefault(x => x.Name == "name")?.Value == null
                && author.Fields.FirstOrDefault(x => x.Name == "email")?.Value == null)
            {
                children.Remove(author);
            }
            ISyndicationContent contributor = children.FirstOrDefault(x => x.Name == AtomContributorTypes.Contributor);
            if (contributor != null
                && contributor.Fields.FirstOrDefault(x => x.Name == "name")?.Value == null
                && contributor.Fields.FirstOrDefault(x => x.Name == "email")?.Value == null)
            {
                children.Remove(contributor);
            }
            return base.CreateEntry(content);
        }
    }
}
