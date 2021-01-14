using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Resources
{
    public class ResourcesData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Resources);

        protected override Func<IDocument, object> Data => doc => doc[SiteKeys.CardData];

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/resources.json";
    }
}
