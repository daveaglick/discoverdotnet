using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public class IndividualProjectData : Pipeline
    {
        public IndividualProjectData()
        {
            Dependencies = new HashSet<string>
            {
                nameof(Projects)
            };

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(nameof(Projects)),
                new GenerateJson(Config.FromDocument(x => x["CardData"]))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination(Config.FromDocument(x => (FilePath)$"data/projects/{x.Source.FileName.ChangeExtension("json")}"))
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
