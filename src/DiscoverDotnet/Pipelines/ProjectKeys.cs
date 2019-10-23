using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class ProjectKeys : AggregateDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data =>
            x => x.GetMetadata(
                "Key",
                "Title",
                "Link");

        protected override Config<FilePath> Destination => (FilePath)"data/project-keys.json";
    }
}
