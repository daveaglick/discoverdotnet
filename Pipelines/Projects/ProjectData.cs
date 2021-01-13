using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class ProjectData : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, object> Data => x => x["CardData"];

        protected override Config<FilePath> Destination =>
            Config.FromDocument(x => (FilePath)$"data/projects/{x.Source.FileName.ChangeExtension("json")}");
    }
}
