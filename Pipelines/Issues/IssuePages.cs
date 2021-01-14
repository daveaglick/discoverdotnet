using System;
using System.Linq;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines.Issues
{
    public abstract class IssuePages : Pipeline
    {
        protected IssuePages(TotalIssueCounts totals)
        {
            Dependencies.Add(nameof(Issues));

            ProcessModules = new ModuleList
            {
                new ExecuteConfig(
                    Config.FromContext(ctx => ctx.Outputs
                        .FromPipeline(nameof(Issues))
                        .Where(doc => DocumentPredicate == null || DocumentPredicate(doc))
                        .SelectMany(doc => doc
                            .GetList<Issue>(SiteKeys.Issues)
                            .Where(i => IssuePredicate == null || IssuePredicate(i))
                            .Select(i => new PagedIssue(i, doc)))
                        .OrderByDescending(x => x.CreatedAt)
                        .Partition(24, count => SetTotal(count, totals))
                        .Select(x => ctx.CreateDocument(new MetadataItems
                        {
                            { SiteKeys.Page, x.Key },
                            { SiteKeys.Issues, x.ToList() }
                        })))),
                new GenerateJson(Config.FromDocument(doc => doc[SiteKeys.Issues]))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination(Destination)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }

        protected virtual Func<IDocument, bool> DocumentPredicate { get; }

        protected virtual Func<Issue, bool> IssuePredicate { get; }

        protected abstract Action<int, TotalIssueCounts> SetTotal { get; }

        protected abstract Config<NormalizedPath> Destination { get; }
    }
}
