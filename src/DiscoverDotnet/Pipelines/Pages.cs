using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
using Statiq.Html;
using Statiq.Json;
using Statiq.Markdown;
using Statiq.Minification;
using Statiq.Razor;
using Statiq.Sass;
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

            TransformModules = new ModuleList
            {
                new ExecuteIf(
                    Config.FromDocument(doc => doc.MediaTypeEquals("text/markdown")),
                    new RenderRazor().WithLayout((FilePath)"/_markdown.cshtml"))
                    .Else(new RenderRazor().WithLayout((FilePath)"/_layout.cshtml")),
                new MirrorResources()
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
