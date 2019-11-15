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
    public class MicrosoftPages : IssuePages
    {
        public MicrosoftPages(TotalIssueCounts totals)
            : base(totals)
        {
        }

        protected override Func<IDocument, bool> DocumentPredicate => doc => doc.GetBool("Microsoft");

        protected override Action<int, TotalIssueCounts> SetTotal => (count, totals) => totals.Microsoft = count;

        protected override Config<FilePath> Destination => Config.FromDocument(doc => (FilePath)$"data/issues/microsoft/{doc.GetInt("Page")}.json");
    }
}
