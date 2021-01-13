using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectKeys : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data =>
            doc => doc.FilterMetadata(
                "Key",
                "Title",
                "Link");

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/project-keys.json";
    }
}
