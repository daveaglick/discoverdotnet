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
    public class AllIssuePages : IssuePages
    {
        public AllIssuePages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.All = count;

        protected override Config<FilePath> Destination => Config.FromDocument(doc => (FilePath)$"data/issues/all/{doc.GetInt("Page")}.json");
    }
}
