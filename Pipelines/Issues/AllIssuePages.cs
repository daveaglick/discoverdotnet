using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class AllIssuePages : IssuePages
    {
        public AllIssuePages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.All = count;

        protected override Config<NormalizedPath> Destination => Config.FromDocument(doc => (NormalizedPath)$"data/issues/all/{doc.GetInt(SiteKeys.Page)}.json");
    }
}
