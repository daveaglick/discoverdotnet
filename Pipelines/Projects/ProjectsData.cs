using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data => doc => doc["CardData"];

        protected override Config<FilePath> Destination => (FilePath)"data/projects.json";
    }
}
