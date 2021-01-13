using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class TotalCounts : Pipeline
    {
        public TotalCounts(TotalIssueCounts totals)
        {
            Dependencies.AddRange(
                nameof(AllIssuePages),
                nameof(FoundationPages),
                nameof(HelpWantedPages),
                nameof(MicrosoftPages),
                nameof(PlatformPages));

            ProcessModules = new ModuleList
            {
                new GenerateJson(Config.FromValue(totals))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination((NormalizedPath)"data/issues/total-counts.json")
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }
    }
}
