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
    public class ProjectCounts : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Issues);

        protected override Func<IDocument, object> Data =>
            doc => doc.GetMetadata(
                "ProjectKey",
                "IssuesCount",
                "RecentIssuesCount",
                "HelpWantedIssuesCount");

        protected override Config<FilePath> Destination => (FilePath)"data/issues/project-counts.json";
    }
}
