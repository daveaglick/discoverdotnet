using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Models;
using Statiq.Common;
using Statiq.Core;
using Statiq.Feeds;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Razor;

namespace DiscoverDotnet.Pipelines.Feeds
{
    public class OpmlFeed : Pipeline
    {
        public OpmlFeed()
        {
            InputModules = new ModuleList
            {
                new ReadFiles("_feeds.cshtml")
            };

            ProcessModules = new ModuleList
            {
                new SetDestination("feeds.opml")
            };

            TransformModules = new ModuleList
            {
                new RenderRazor().IgnorePrefix(null)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
