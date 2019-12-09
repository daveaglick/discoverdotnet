using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;
using Statiq.Sass;

namespace DiscoverDotnet.Pipelines.Projects
{
    public class DonationsData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Projects);

        protected override Func<IDocument, bool> Predicate => x => x.ContainsKey("Donations");

        protected override Func<IDocument, object> Data =>
            doc => doc.GetMetadata(
                "Title",
                "Website",
                "NuGet",
                "SourceCode",
                "Donations");

        protected override Config<FilePath> Destination => (FilePath)"data/donations.json";
    }
}
