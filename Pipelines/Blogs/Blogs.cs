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
                new GetFeedData(),
                new SetDestination(Config.FromDocument(x => (NormalizedPath)$"blogs/{x.Source.FileName.ChangeExtension("html")}")),
                new SetMetadata("Key", Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata("Link", Config.FromDocument((d, c) => c.GetLink(d))),
                new SetMetadata("Language", Config.FromDocument(x => x.GetString("Language", "English"))),
                new SetMetadata("CardData", Config.FromDocument(x => x.FilterMetadata(
                    "Key",
                    "Title",
                    "Image",
                    "Link",
                    "Description",
                    "Author",
                    "Website",
                    "Feed",
                    "Comment",
                    "Language",
                    "LastPublished",
                    "NewestFeedItem")))
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
