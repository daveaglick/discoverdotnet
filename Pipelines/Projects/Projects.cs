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
                new SetMetadata(SiteKeys.Key, Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata(SiteKeys.Link, Config.FromDocument((d, c) => c.GetLink(d))),
                new SetMetadata(SiteKeys.DonationsData, Config.FromDocument(x => x.FilterMetadata(
                    SiteKeys.Website,
                    SiteKeys.NuGet,
                    SiteKeys.SourceCode,
                    SiteKeys.Destination))),
                new SetMetadata(SiteKeys.CardData, Config.FromDocument(x => x.FilterMetadata(
                    SiteKeys.Key,
                    SiteKeys.Title,
                    SiteKeys.Link,
                    SiteKeys.Image,
                    SiteKeys.NuGet,
                    SiteKeys.SourceCode,
                    SiteKeys.Description,
                    SiteKeys.StargazersCount,
                    SiteKeys.ForksCount,
                    SiteKeys.OpenIssuesCount,
                    SiteKeys.PushedAt,
                    SiteKeys.Website,
                    SiteKeys.Donations,
                    SiteKeys.Language,
                    SiteKeys.Tags,
                    SiteKeys.Comment,
                    SiteKeys.Platform,
                    SiteKeys.Microsoft,
                    SiteKeys.Foundation)))
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
