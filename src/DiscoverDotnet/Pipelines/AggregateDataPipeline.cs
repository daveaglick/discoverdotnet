using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public abstract class AggregateDataPipeline : Pipeline
    {
        protected AggregateDataPipeline()
        {
            Dependencies.Add(SourcePipeline);

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(c => c.Outputs
                        .FromPipeline(SourcePipeline)
                        .Where(x => Predicate == null || Predicate(x))
                        .Select(x => Data(x))))
                    .WithCamelCase(),
                new MinifyJs(),
                new SetDestination(Destination)
            };

            OutputModules = new ModuleList
            {
                new WriteFiles()
            };
        }

        protected abstract string SourcePipeline { get; }

        protected virtual Func<IDocument, bool> Predicate { get; }

        protected abstract Func<IDocument, object> Data { get; }

        protected abstract Config<FilePath> Destination { get; }
    }
}
