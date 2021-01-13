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
    public class HelpWantedPages : IssuePages
    {
        public HelpWantedPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<Issue, bool> IssuePredicate => i => i.HelpWanted;

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.HelpWanted = count;

        protected override Config<FilePath> Destination => Config.FromDocument(doc => (FilePath)$"data/issues/help-wanted/{doc.GetInt("Page")}.json");
    }
}
