using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Broadcasts
{
    public class BroadcastsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Broadcasts);

        protected override Func<IDocument, object> Data => doc => doc["CardData"];

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/broadcasts.json";
    }
}
