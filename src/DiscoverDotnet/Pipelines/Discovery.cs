using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class Discovery : Pipeline
    {
        public Discovery()
        {
            Dependencies.Add(nameof(Discoveries));

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(nameof(Discoveries)),
                new TakeDocuments(1),
                new GenerateJson(Config.FromDocument(x => x["CardData"]))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((FilePath)"data/discovery.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
