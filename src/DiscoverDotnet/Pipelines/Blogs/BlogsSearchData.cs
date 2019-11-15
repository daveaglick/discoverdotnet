using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Modules;
using Microsoft.Extensions.Configuration;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class BlogsSearchData : SelectDataPipeline
    {
        protected override string SourcePipeline => nameof(Blogs);

        protected override Func<IDocument, object> Data => doc => doc["SearchData"];

        protected override Config<FilePath> Destination => (FilePath)"data/search/blogs.json";
    }
}
