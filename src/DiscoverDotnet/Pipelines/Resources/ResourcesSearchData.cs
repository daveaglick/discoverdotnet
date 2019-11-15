using System;
using System.Collections.Generic;
using System.Text;
using Statiq.Common;
using Statiq.Core;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Resources
{
    public class ResourcesSearchData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Resources);

        protected override Func<IDocument, object> Data => doc => doc["SearchData"];

        protected override Config<FilePath> Destination => (FilePath)"data/search/resources.json";
    }
}
