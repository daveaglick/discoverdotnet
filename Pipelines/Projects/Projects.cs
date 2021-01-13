using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Html;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class Projects : Pipeline
    {
        public Projects()
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
                new LogMessage(Config.FromContext(ctx => $"Getting project data for {ctx.Inputs.Length} projects...")),
                new ParseYaml(),
                new SetContent(string.Empty),
                new GetProjectData(),
                new SetDestination(Config.FromDocument(x => (NormalizedPath)$"projects/{x.Source.FileName.ChangeExtension("html")}")),
                new SetMetadata("Key", Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata("Link", Config.FromDocument((d, c) => c.GetLink(d))),
                new SetMetadata("DonationsData", Config.FromDocument(x => x.FilterMetadata(
                    "Website",
                    "NuGet",
                    "SourceCode",
                    "Destination"))),
                new SetMetadata("CardData", Config.FromDocument(x => x.FilterMetadata(
                    "Key",
                    "Title",
                    "Link",
                    "Image",
                    "NuGet",
                    "SourceCode",
                    "Description",
                    "StargazersCount",
                    "ForksCount",
                    "OpenIssuesCount",
                    "PushedAt",
                    "Website",
                    "Donations",
                    "Language",
                    "Tags",
                    "Comment",
                    "Platform",
                    "Microsoft",
                    "Foundation")))
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithLayout((NormalizedPath)"/projects/_layout.cshtml"),
                new MirrorResources()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
