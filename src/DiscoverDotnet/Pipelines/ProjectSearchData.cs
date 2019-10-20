using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public class ProjectSearchData : Pipeline
    {
        public ProjectSearchData()
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
                        .Select(x => x["SearchData"])))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((FilePath)"data/search/projects.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
