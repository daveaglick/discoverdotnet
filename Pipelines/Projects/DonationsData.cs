using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class DonationsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, bool> Predicate => x => x.ContainsKey(SiteKeys.Donations);

        protected override Func<IDocument, object> Data =>
            doc => doc.FilterMetadata(
                SiteKeys.Title,
                SiteKeys.Website,
                SiteKeys.NuGet,
                SiteKeys.SourceCode,
                SiteKeys.Donations);

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/donations.json";
    }
}
