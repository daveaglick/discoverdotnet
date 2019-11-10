using System.Collections.Generic;
using System.Linq;

namespace DiscoverDotnet.Models
{
    public class Package
    {
        public string Id { get; set; }
        public string TotalDownloads { get; set; }
        public string PerDayDownloads { get; set; }
        public List<PackageVersion> Versions { get; set; } // In descending order

        public PackageVersion LatestVersion => Versions.FirstOrDefault();
    }
}
