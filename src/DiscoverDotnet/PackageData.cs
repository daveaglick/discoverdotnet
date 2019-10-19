using System.Collections.Generic;
using System.Linq;

namespace DiscoverDotnet
{
    public class PackageData
    {
        public string Id { get; set; }
        public string TotalDownloads { get; set; }
        public string PerDayDownloads { get; set; }
        public List<VersionData> Versions { get; set; } // In descending order

        public VersionData LatestVersion => Versions.FirstOrDefault();
    }
}
