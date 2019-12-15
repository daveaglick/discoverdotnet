using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Pipelines.Blogs;
using DiscoverDotnet.Pipelines.Broadcasts;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class Discoveries : Pipeline
    {
        public Discoveries()
        {
            Dependencies.AddRange(new[]
            {
                nameof(Projects.Projects),
                nameof(Blogs.Blogs),
                nameof(Broadcasts.Broadcasts),
                nameof(Resources.Resources)
            });

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(
                    nameof(Projects.Projects),
                    nameof(Blogs.Blogs),
                    nameof(Broadcasts.Broadcasts),
                    nameof(Resources.Resources)),
                new FilterDocuments(Config.FromDocument(x =>
                    x.ContainsKey("DiscoveryDate") && x.GetDateTime("DiscoveryDate") <= DateTime.Today)),
                new OrderDocuments(Config.FromDocument(doc => doc.GetDateTime("DiscoveryDate")).Descending()
            };
        }
    }
}
