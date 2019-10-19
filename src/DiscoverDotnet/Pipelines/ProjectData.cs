using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public class ProjectData : Pipeline
    {
        public ProjectData()
        {
            Dependencies = new HashSet<string>
            {
                nameof(Projects)
            };

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(c => c.Outputs
                        .FromPipeline(nameof(Projects))
                        .Select(x => x["CardData"])))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((FilePath)"data/projects.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
