using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Broadcasts
{
    public class Episodes : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Broadcasts);

        protected override Func<IDocument, bool> Predicate =>
            doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English";

        protected override Func<IDocument, object> Data => doc => doc["FeedItems"];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(doc => (NormalizedPath)$"data/episodes/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
