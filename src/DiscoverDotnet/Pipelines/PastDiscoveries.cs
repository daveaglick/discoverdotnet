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
    public class PastDiscoveries : Pipeline
    {
        public PastDiscoveries()
        {
            Dependencies.Add(nameof(Discoveries));

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(c => c.Outputs
                        .FromPipeline(nameof(Discoveries))
                        .Skip(1).Take(4)
                        .Select(x => x["CardData"])))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((FilePath)"data/past-discoveries.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
