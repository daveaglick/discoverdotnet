using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class SiteResources : Pipeline
    {
        public SiteResources()
        {
            Isolated = true;

            OutputModules = new ModuleList
            {
                new CopyFiles("**/*{!.cshtml,!.md,!.less,!.yml,!.scss,}")
            };
        }
    }
}
