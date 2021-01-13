using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Broadcasts
{
    public class BroadcastData : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Broadcasts);

        protected override Func<IDocument, object> Data => x => x["CardData"];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(doc => (NormalizedPath)$"data/broadcasts/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
