using System;
using System.Collections.Generic;
using System.Text;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines
{
    public class Projects : Pipeline
    {
        public Projects()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(Config.FromContext(x => (IEnumerable<string>)new string[]
                {
                    x.FileSystem.RootPath.Combine("../../data/projects/*.yml").FullPath
                })),
                new ExecuteIf(Config.FromContext(x => x.ApplicationState.CommandName.Equals("preview", StringComparison.OrdinalIgnoreCase)))
                {
                    new OrderDocuments(Config.FromDocument(x => x.Source)),
                    new TakeDocuments(10)
                }
            };

            ProcessModules = new ModuleList
            {
                new ParseYaml(),
                new ReplaceContent(string.Empty),
                new GetProjectGitHubData(),
                new GetProjectNuGetData()
            };
        }
    }
}
