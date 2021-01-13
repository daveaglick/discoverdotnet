using System;
using System.Collections.Generic;
using System.Linq;
using DiscoverDotnet.Modules;
using Statiq.Common;
using Statiq.Core;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public abstract class SelectManyDataPipeline : Pipeline
    {
        protected SelectManyDataPipeline()
        {
            Dependencies.Add(SourcePipeline);

            ProcessModules = new ModuleList
            {
                new GenerateJson(
                    Config.FromContext(ctx => ctx.Outputs
                        .FromPipeline(SourcePipeline)
                        .Where(doc => Predicate == null || Predicate(doc))
                        .SelectMany(doc => Data(doc))))
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

        protected abstract Func<IDocument, IEnumerable<object>> Data { get; }

        protected abstract Config<NormalizedPath> Destination { get; }
    }
}
