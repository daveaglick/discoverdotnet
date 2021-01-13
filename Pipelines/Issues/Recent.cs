using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class Recent : Pipeline
    {
        public Recent()
        {
            Dependencies.Add(nameof(Issues));

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(ctx => ctx.Outputs
                        .FromPipeline(nameof(Issues))
                        .SelectMany(doc => doc
                            .GetList<Issue>("Issues")
                            .Where(i => i.Recent)
                            .Select(i => new
                            {
                                ProjectKey = doc.GetString("ProjectKey"),
                                CreatedAt = i.CreatedAt,
                                Link = i.Link,
                                Title = i.Title
                            }))
                        .OrderByDescending(x => x.CreatedAt)))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((FilePath)"data/issues/recent/all.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
