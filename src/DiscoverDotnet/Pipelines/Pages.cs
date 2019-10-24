using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
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
                        Config.FromDocument(doc => doc.Source.Extension.Equals(".md", StringComparison.OrdinalIgnoreCase)),
                        new RenderMarkdown().UseExtensions()),
                    new SetDestination(".html")
                }
            };

            TransformModules = new ModuleList
            {
                new ExecuteIf(
                    Config.FromDocument(doc => doc.Source.Extension.Equals(".md", StringComparison.OrdinalIgnoreCase)),
                    new RenderRazor().WithLayout((FilePath)"/_MarkdownLayout.cshtml"))
                    .Else(new RenderRazor().WithLayout((FilePath)"/_Layout.cshtml"))
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
