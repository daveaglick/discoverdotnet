using System;
using DiscoverDotnet.Models;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class HelpWantedPages : IssuePages
    {
        public HelpWantedPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<Issue, bool> IssuePredicate => i => i.HelpWanted;

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.HelpWanted = count;

        protected override Config<NormalizedPath> Destination => Config.FromDocument(doc => (NormalizedPath)$"data/issues/help-wanted/{doc.GetInt("Page")}.json");
    }
}
