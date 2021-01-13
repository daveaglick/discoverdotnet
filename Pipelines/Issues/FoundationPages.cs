using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Yaml;

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

        protected override Config<FilePath> Destination => Config.FromDocument(doc => (FilePath)$"data/issues/foundation/{doc.GetInt("Page")}.json");
    }
}
