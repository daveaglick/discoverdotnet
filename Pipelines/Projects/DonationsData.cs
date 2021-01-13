using System;
using Statiq.Common;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class DonationsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, bool> Predicate => x => x.ContainsKey("Donations");

        protected override Func<IDocument, object> Data =>
            doc => doc.FilterMetadata(
                "Title",
                "Website",
                "NuGet",
                "SourceCode",
                "Donations");

        protected override Config<NormalizedPath> Destination => (NormalizedPath)"data/donations.json";
    }
}
