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
    public class PlatformPages : IssuePages
    {
        public PlatformPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<IDocument, bool> DocumentPredicate => doc => doc.GetBool("Platform");

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.Platform = count;

        protected override Config<FilePath> Destination => Config.FromDocument(doc => (FilePath)$"data/issues/platform/{doc.GetInt("Page")}.json");
    }
}
