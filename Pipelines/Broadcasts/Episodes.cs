using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Modules;
using Microsoft.Extensions.Configuration;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Broadcasts
{
    public class Episodes : DocumentDataPipeline
    {
        protected override string SourcePipeline => nameof(Broadcasts);

        protected override Func<IDocument, bool> Predicate =>
            doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English";

        protected override Func<IDocument, object> Data => doc => doc["FeedItems"];

        protected override Config<FilePath> Destination =>
            Config.FromDocument(doc => (FilePath)$"data/episodes/{doc.Source.FileName.ChangeExtension(".json")}");
    }
}
