using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class ProjectKeys : Pipeline
    {
        public ProjectKeys()
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
                        .Select(x => x.GetMetadata(
                            "Key",
                            "Title",
                            "Link"))))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination("data/project-keys.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
