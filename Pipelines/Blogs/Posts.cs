using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class Posts : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Blogs);

        protected override Func<IDocument, bool> Predicate =>
            doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English";

        protected override Func<IDocument, object> Data => doc => doc["FeedItems"];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(doc => (NormalizedPath)$"data/posts/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
