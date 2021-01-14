using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Issues
{
    public class ProjectCounts : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Issues);

        protected override Func<IDocument, object> Data =>
            doc => doc.FilterMetadata(
                SiteKeys.ProjectKey,
                SiteKeys.IssuesCount,
                SiteKeys.RecentIssuesCount,
                SiteKeys.HelpWantedIssuesCount);

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/issues/project-counts.json";
    }
}
