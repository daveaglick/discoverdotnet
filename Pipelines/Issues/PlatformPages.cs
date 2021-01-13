using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class PlatformPages : IssuePages
    {
        public PlatformPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<IDocument, bool> DocumentPredicate => doc => doc.GetBool("Platform");

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.Platform = count;

        protected override Config<NormalizedPath> Destination => Config.FromDocument(doc => (NormalizedPath)$"data/issues/platform/{doc.GetInt("Page")}.json");
    }
}
