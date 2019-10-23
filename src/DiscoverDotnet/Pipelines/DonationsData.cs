using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines
{
    public class DonationsData : AggregateDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, bool> Predicate => x => x.ContainsKey("Donations");

        protected override Func<IDocument, object> Data =>
            x => x.GetMetadata(
                "Title",
                "Website",
                "NuGet",
                "Source",
                "Donations");

        protected override Config<FilePath> Destination => (FilePath)"data/donations.json";
    }
}
