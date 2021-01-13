using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class MicrosoftPages : IssuePages
    {
        public MicrosoftPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<IDocument, bool> DocumentPredicate => doc => doc.GetBool("Microsoft");

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.Microsoft = count;

        protected override Config<NormalizedPath> Destination => Config.FromDocument(doc => (NormalizedPath)$"data/issues/microsoft/{doc.GetInt("Page")}.json");
    }
}
