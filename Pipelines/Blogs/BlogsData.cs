using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class BlogsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Blogs);

        protected override Func<IDocument, object> Data => doc => doc[SiteKeys.CardData];

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/blogs.json";
    }
}
