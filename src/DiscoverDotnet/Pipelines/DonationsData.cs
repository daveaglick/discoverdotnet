using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class DonationsData : Pipeline
    {
        public DonationsData()
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
                        .Where(x => x.ContainsKey("Donations"))
                        .Select(x => x.GetMetadata(
                            "Title",
                            "Website",
                            "NuGet",
                            "Source",
                            "Donations"))))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination("data/donations.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
