using Statiq.Common;
using Statiq.Core;

namespace DiscoverDotnet.Pipelines
{
    public class SiteResources : Pipeline
    {
        public SiteResources()
        {
            ProcessModules = new ModuleList
            {
                new CopyFiles("**/*{!.cshtml,!.md,!.less,!.yml,!.scss,}")
            };
        }
    }
}
