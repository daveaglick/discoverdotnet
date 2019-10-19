using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AngleSharp;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace DiscoverDotnet.Modules
{
    public class GetProjectNuGetData : Module
    {
        private static readonly IConfiguration AngleSharpConfig = Configuration.Default.WithDefaultLoader();

        protected override async Task<IEnumerable<IDocument>> ExecuteInputAsync(IDocument input, IExecutionContext context)
        {
            IDocument output = input;
            List<PackageData> packageData = new List<PackageData>();
            IReadOnlyList<string> packages = input.GetList("NuGet", Array.Empty<string>());
            foreach (string package in packages.Where(x => !string.IsNullOrWhiteSpace(x)))
            {
                context.LogInformation($"Getting NuGet data for {package}");
                try
                {
                    IBrowsingContext browsingContext = BrowsingContext.New(AngleSharpConfig);
                    AngleSharp.Dom.IDocument document = await browsingContext.OpenAsync($"https://www.nuget.org/packages/{package}");
                    if (document.StatusCode != System.Net.HttpStatusCode.OK)
                    {
                        context.LogWarning($"Bad status code for {package}: {document.StatusCode}");
                    }
                    else if (document == null)
                    {
                        context.LogWarning($"Could not get document for {package}");
                    }
                    else
                    {
                        PackageData data = new PackageData
                        {
                            Id = package
                        };

                        // Get statistics
                        AngleSharp.Dom.IElement statistics = document
                            .QuerySelectorAll(".package-details-info h2")
                            .First(x => x.TextContent == "Statistics")
                            .NextElementSibling;
                        data.TotalDownloads = statistics.Children
                            .First(x => x.TextContent.Contains("total downloads"))
                            .TextContent.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];
                        data.PerDayDownloads = statistics.Children
                            .First(x => x.TextContent.Contains("per day"))
                            .TextContent.Trim().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[0];

                        // Get versions
                        data.Versions = document
                            .QuerySelectorAll("#version-history table tbody tr")
                            .Select(x => new VersionData(x))
                            .ToList();

                        // Add the data
                        packageData.Add(data);
                    }
                }
                catch (Exception ex)
                {
                   context.LogWarning($"Error getting NuGet data for {package}: {ex.Message}");
                }
            }

            if (packageData.Count > 0)
            {
                // Return a new document with package data
                output = input.Clone(new MetadataItems
                {
                    { "NuGetPackages", packageData }
                });
            }

            return output.Yield();
        }
    }
}
