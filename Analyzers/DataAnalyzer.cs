using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscoverDotnet.Pipelines.Projects;
using Microsoft.Extensions.Logging;
using Statiq.Common;

namespace DiscoverDotnet.Analyzers
{
    public abstract class DataAnalyzer : SyncAnalyzer
    {
        public override LogLevel LogLevel { get; set; } = LogLevel.Error;

        protected DataAnalyzer(string dataPipeline)
        {
            PipelinePhases.Add(dataPipeline, Phase.Process);
        }

        protected override sealed void Analyze(IAnalyzerContext context)
        {
            foreach (IDocument document in context.Inputs)
            {
                AnalyzeData(document, context);
            }
        }

        protected abstract void AnalyzeData(IDocument document, IAnalyzerContext context);
    }
}
