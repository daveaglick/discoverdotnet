using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data => doc => doc["CardData"];

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/projects.json";
    }
}
