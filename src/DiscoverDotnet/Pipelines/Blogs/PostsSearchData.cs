using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscoverDotnet.Models;
using DiscoverDotnet.Modules;
using Microsoft.Extensions.Configuration;
using Statiq.Common;
using Statiq.Core;
using Statiq.Json;
using Statiq.Razor;
using Statiq.Yaml;

namespace DiscoverDotnet.Pipelines.Blogs
{
    public class PostsSearchData : SelectManyDataPipeline
    {
        protected override string SourcePipeline => nameof(Blogs);

        protected override Func<IDocument, bool> Predicate =>
            doc => doc.ContainsKey("FeedItems") && doc.GetString("Language") == "English";

        protected override Func<IDocument, IEnumerable<object>> Data =>
            doc => doc
                .Get<IEnumerable<FeedItem>>("FeedItems")
                .Select(i => new
                {
                    i.Title,
                    i.Link,
                    i.Published
                });

        protected override Config<FilePath> Destination => (FilePath)"data/search/posts.json";
    }
}
