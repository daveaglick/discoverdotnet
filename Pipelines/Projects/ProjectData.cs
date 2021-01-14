using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectData : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data => x => x[SiteKeys.CardData];

        protected override Config<NormalizedPath> Destination =>
            Config.FromDocument(x => (NormalizedPath)$"data/projects/{x.Source.FileName.ChangeExtension("json")}");
    }
}
