using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Broadcasts
{
    public class Episodes : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Broadcasts);

        protected override Func<IDocument, bool> Predicate =>
            doc => doc.ContainsKey(SiteKeys.FeedItems) && doc.GetString(SiteKeys.Language) == "English";

        protected override Func<IDocument, object> Data => doc => doc[SiteKeys.FeedItems];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(doc => (NormalizedPath)$"data/episodes/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
