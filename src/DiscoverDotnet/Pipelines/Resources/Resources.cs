using System;
using System.Collections.Generic;
using System.Text;
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
                new ReadFiles(Config.FromContext(ctx => ctx.FileSystem.RootPath.Combine("../../data/resources/*.yml").FullPath)),
                new ExecuteIf(Config.FromContext(ctx => ctx.ApplicationState.IsCommand("preview") || ctx.Settings.GetBool("limited")))
                {
                    new OrderDocuments(Config.FromDocument(x => x.Source)),
                    new TakeDocuments(10)
                }
            };

            ProcessModules = new ModuleList
            {
                new ParseYaml(),
                new SetContent(string.Empty),
                new SetMetadata("Key", Config.FromDocument(x => x.Source.FileNameWithoutExtension.FullPath)),
                new SetMetadata("Link", Config.FromDocument(d => d.GetString("Website"))),
                new SetMetadata("CardType", "Resource"), // TODO: Do we still need this without groups/events?
                new SetMetadata("CardData", Config.FromDocument(x => x.GetMetadata(
                    "Website",
                    "Title",
                    "Image",
                    "Description",
                    "CardType",
                    "Comment",
                    "Tags",
                    "DiscoveryDate")))
            };
        }
    }
}
