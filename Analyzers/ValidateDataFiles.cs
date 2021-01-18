using System.Linq;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace DiscoverDotnet.Analyzers
{
    /// <summary>
    /// Validates that all files in the data directory are *.yml files and are all-lowercase with only dashes.
    /// </summary>
    public class ValidateDataFiles : SyncAnalyzer
    {
        public override LogLevel LogLevel { get; set; } = LogLevel.Error;

        public ValidateDataFiles()
        {
            PipelinePhases.Add(nameof(Pipelines.SiteResources), Phase.Input);
        }

        protected override void Analyze(IAnalyzerContext context)
        {
            IDirectory dataDirectory = context.FileSystem.GetRootDirectory("data");
            foreach (IFile dataFile in dataDirectory.GetFiles(System.IO.SearchOption.AllDirectories))
            {
                if (!dataFile.Path.Extension.Equals(".yml"))
                {
                    context.AddAnalyzerResult(null, $"File {dataDirectory.Parent.Path.GetRelativePath(dataFile.Path.FullPath)} should have a .yml extension");
                }
                if (dataFile.Path.FileName.FullPath.ToLower() != dataFile.Path.FileName || dataFile.Path.FileNameWithoutExtension.FullPath.Any(c => !char.IsLetterOrDigit(c) && c != '-'))
                {
                    context.AddAnalyzerResult(null, $"File {dataDirectory.Parent.Path.GetRelativePath(dataFile.Path.FullPath)} should only contain lowercase letters, digits, and dashes");
                }
            }
        }
    }
}
