using System;
using AngleSharp.Dom;

namespace DiscoverDotnet.Models
{
    public class PackageVersion
    {
        public string Version { get; }
        public string Downloads { get; }
        public DateTime LastUpdated { get; }

        public PackageVersion(IElement element)
        {
            Version = element.Children[1].TextContent.Replace("(current)", string.Empty).Trim();
            Downloads = element.Children[2].TextContent.Trim();
            LastUpdated = DateTime.Parse(element.QuerySelector("*[data-datetime]").GetAttribute("data-datetime"));
        }
    }
}
