using Statiq.Common;
using Statiq.Core;
using Statiq.Web.Netlify;

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
                new DeployNetlifySite("1b1625b7-f7ad-46a4-bd5f-0e02e8b0f37c", Config.FromSetting<string>("NETLIFY_TOKEN"))
            };
        }
    }
}
