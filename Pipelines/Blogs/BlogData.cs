using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class BlogData : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Blogs);

        protected override Func<IDocument, object> Data => x => x["CardData"];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(doc => (NormalizedPath)$"data/blogs/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
