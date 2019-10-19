using System;
using System.Collections.Generic;
using System.Text;
using DiscoverDotnet.Modules;
using Microsoft.Extensions.Configuration;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Razor;
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
                new GetProjectNuGetData(),
                new AddMetadata("Key", Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new AddMetadata("Link", Config.FromDocument((d, c) => c.GetLink(d))),
                new AddMetadata("DonationsData", Config.FromDocument(x => x.GetMetadata(
                    "Website",
                    "NuGet",
                    "Source",
                    "Destination"))),
                new AddMetadata("SearchData", Config.FromDocument(x => x.GetMetadata(
                    "Key",
                    "Title",
                    "Description",
                    "StargazersCount",
                    "Tags"))),
                new AddMetadata("CardData", Config.FromDocument(x => x.GetMetadata(
                    "Key",
                    "Title",
                    "Link",
                    "Image",
                    "NuGet",
                    "Source",
                    "Description",
                    "StargazersCount",
                    "ForksCount",
                    "OpenIssuesCount",
                    "PushedAt",
                    "Website",
                    "Donations",
                    "Language",
                    "Tags",
                    "DiscoveryDate",
                    "Comment",
                    "Platform",
                    "Microsoft",
                    "Foundation"))),
                new SetDestination(Config.FromDocument(x => (FilePath)$"projects/{x.Source.FileName.ChangeExtension("html")}"))
            };

            TransformModules = new ModuleList
            {
                new RenderRazor().WithLayout((FilePath)"/projects/_Layout.cshtml")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
