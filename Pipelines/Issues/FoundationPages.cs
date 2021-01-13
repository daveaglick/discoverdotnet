using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class FoundationPages : IssuePages
    {
        public FoundationPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<IDocument, bool> DocumentPredicate => doc => doc.GetBool("Foundation");

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.Foundation = count;

        protected override Config<NormalizedPath> Destination => Config.FromDocument(doc => (NormalizedPath)$"data/issues/foundation/{doc.GetInt("Page")}.json");
    }
}
