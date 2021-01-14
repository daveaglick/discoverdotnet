using Statiq.Common;
using Statiq.Core;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Resources
{
    public class Resources : Pipeline
    {
        public Resources()
        {
            InputModules = new ModuleList
            {
                new ReadFiles(Config.FromContext(ctx => ctx.FileSystem.RootPath.Combine("data/resources/*.yml").FullPath)),
                new ExecuteIf(Config.FromContext(ctx => ctx.ApplicationState.IsCommand("preview") || ctx.Settings.GetBool("dev")))
                {
                    new OrderDocuments(Config.FromDocument(x => x.Source)),
                    new TakeDocuments(10)
                }
            };

            ProcessModules = new ModuleList
            {
                new ParseYaml(),
                new SetContent(string.Empty),
                new SetMetadata(SiteKeys.Key, Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata(SiteKeys.Link, Config.FromDocument(d => d.GetString(SiteKeys.Website))),
                new SetMetadata(SiteKeys.CardType, SiteKeys.Resource), // TODO: Do we still need this without groups/events?
                new SetMetadata(SiteKeys.CardData, Config.FromDocument(x => x.FilterMetadata(
                    SiteKeys.Website,
                    SiteKeys.Title,
                    SiteKeys.Image,
                    SiteKeys.Description,
                    SiteKeys.CardType,
                    SiteKeys.Comment,
                    SiteKeys.Tags)))
            };
        }
    }
}
