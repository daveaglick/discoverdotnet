using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectKeys : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data =>
            doc => doc.GetMetadata(
                "Key",
                "Title",
                "Link");

        protected override Config<FilePath> Destination => (FilePath)"data/project-keys.json";
    }
}
