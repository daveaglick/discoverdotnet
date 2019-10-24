using System;
using System.Collections.Generic;
using System.Text;
using Statiq.Common;
using Statiq.Core;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Resources
{
    public class ResourcesData : AggregateDataPipeline
    {
        protected override string SourcePipeline => nameof(Resources);

        protected override Func<IDocument, object> Data => x => x["CardData"];

        protected override Config<FilePath> Destination => (FilePath)"data/resources.json";
    }
}
