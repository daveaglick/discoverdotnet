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
                new ThrowException(Config.FromSettings(x => x.ContainsKey("NETLIFY_TOKEN") ? null : "The setting NETLIFY_TOKEN must be defined")),
                new DeployNetlifySite("f3900c88-7859-4b37-a7fc-628bb0d168fa", Config.FromSetting<string>("NETLIFY_TOKEN"))
            };
        }
    }
}
