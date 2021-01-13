using Statiq.Common;
using Statiq.Core;
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

            PostProcessModules = new ModuleList
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
