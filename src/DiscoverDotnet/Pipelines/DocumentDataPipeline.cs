using System;
using System.Collections.Generic;
using System.Linq;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Minification;

namespace DiscoverDotnet.Pipelines
{
    public abstract class DocumentDataPipeline : Pipeline
    {
        protected DocumentDataPipeline()
        {
            Dependencies.Add(SourcePipeline);

            ProcessModules = new ModuleList
            {
                new ReplaceDocuments(SourcePipeline),
                new FilterDocuments(Config.FromDocument(x => Predicate == null || Predicate(x))),
                new GenerateJson(
                    Config.FromDocument(x => Data(x)))
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
