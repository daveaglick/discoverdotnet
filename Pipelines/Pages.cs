using Statiq.Common;
using Statiq.Core;
using Statiq.Html;
using Statiq.Markdown;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines
{
    public class Pages : Pipeline
    {
        public Pages()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("**/{!_,}*.{cshtml,md}")
            };

            ProcessModules = new ModuleList
            {
                new CacheDocuments
                {
                    new ExtractFrontMatter(new ParseYaml()),
                    new ExecuteIf(
                        Config.FromDocument(doc => doc.MediaTypeEquals("text/markdown")),
                        new RenderMarkdown().UseExtensions()),
                    new SetDestination(".html")
                }
            };

            PostProcessModules = new ModuleList
            {
                new ExecuteIf(
                    Config.FromDocument(doc => doc.MediaTypeEquals("text/markdown")),
                    new RenderRazor().WithLayout((NormalizedPath)"/_markdown.cshtml"))
                    .Else(new RenderRazor().WithLayout((NormalizedPath)"/_layout.cshtml")),
                new MirrorResources()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
