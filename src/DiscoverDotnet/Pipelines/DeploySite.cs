using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Netlify;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class DeploySite : Pipeline
    {
        public DeploySite()
        {
            Deployment = true;

            OutputModules = new ModuleList
            {
                new ThrowException(Config.FromSettings(x => x.ContainsKey("DISCOVERDOTNET_NETLIFY_TOKEN") ? null : "The setting NETLIFY_TOKEN must be defined")),
                new DeployNetlifySite("discoverdotnet-test", Config.FromSetting<string>("DISCOVERDOTNET_NETLIFY_TOKEN"))
            };
        }
    }
}
