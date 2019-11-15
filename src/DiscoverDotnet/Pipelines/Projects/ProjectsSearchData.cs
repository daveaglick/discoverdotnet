using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectsSearchData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data => doc => doc["SearchData"];

        protected override Config<FilePath> Destination => (FilePath)"data/search/projects.json";
    }
}
