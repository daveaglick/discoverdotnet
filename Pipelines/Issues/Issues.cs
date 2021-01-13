using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class Issues : Pipeline
    {
        public Issues()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(Config.FromContext(ctx => ctx.FileSystem.RootPath.Combine("data/projects/*.yml").FullPath)),
                new ExecuteIf(Config.FromContext(ctx => ctx.ApplicationState.IsCommand("preview") || ctx.Settings.GetBool("dev")))
                {
                    new OrderDocuments(Config.FromDocument(x => x.Source)),
                    new TakeDocuments(10)
                }
            };

            ProcessModules = new ModuleList
            {
                new LogMessage(Config.FromContext(ctx => $"Getting issue data for {ctx.Inputs.Length} issues...")),
                new ParseYaml(),
                new SetContent(string.Empty),
                new GetIssueGitHubData(),
                new FilterDocuments(Config.FromDocument(doc => doc.ContainsKey("Issues"))),
                new SetMetadata("ProjectKey", Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new GenerateJson(Config.FromDocument(doc => doc["Issues"]))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination(Config.FromDocument(x => (FilePath)$"data/issues/projects/{x.Source.FileName.ChangeExtension(".json")}"))
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
