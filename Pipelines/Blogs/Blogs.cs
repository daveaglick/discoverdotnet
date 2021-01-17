using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Html;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class Blogs : Pipeline
    {
        public Blogs()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(Config.FromContext(ctx => ctx.FileSystem.RootPath.Combine("data/blogs/*.yml").FullPath)),
                new ExecuteIf(Config.FromContext(ctx => ctx.ApplicationState.IsCommand("preview") || ctx.Settings.GetBool("dev")))
                {
                    new OrderDocuments(Config.FromDocument(x => x.Source)),
                    new TakeDocuments(5)
                }
            };

            ProcessModules = new ModuleList
            {
                new LogMessage(Config.FromContext(ctx => $"Getting blog data for {ctx.Inputs.Length} blogs...")),
                new ParseYaml(),
                new SetContent(string.Empty),
                new GetFeedData(true),
                new SetDestination(Config.FromDocument(x => (NormalizedPath)$"blogs/{x.Source.FileName.ChangeExtension("html")}")),
                new SetMetadata(SiteKeys.Key, Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata(SiteKeys.Link, Config.FromDocument((d, c) => c.GetLink(d))),
                new SetMetadata(SiteKeys.Language, Config.FromDocument(x => x.GetString(SiteKeys.Language, "English"))),
                new SetMetadata(SiteKeys.CardData, Config.FromDocument(x => x.FilterMetadata(
                    SiteKeys.Key,
                    SiteKeys.Title,
                    SiteKeys.Image,
                    SiteKeys.Link,
                    SiteKeys.Description,
                    SiteKeys.Author,
                    SiteKeys.Website,
                    SiteKeys.Feed,
                    SiteKeys.Comment,
                    SiteKeys.Language,
                    SiteKeys.LastPublished,
                    SiteKeys.NewestFeedItem)))
            };

            PostProcessModules = new ModuleList
            {
                new RenderRazor().WithLayout((NormalizedPath)"/blogs/_layout.cshtml"),
                new MirrorResources()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
