using System;
using System.Collections.Generic;
using System.Linq;

namespace DiscoverDotnet
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<IGrouping<int, TSource>> Partition<TSource>(
            this IEnumerable<TSource> source,
            int groupSize,
            Action<int> setTotal)
        {
            int c = 0;
            IEnumerable<IGrouping<int, TSource>> result = source
                .Select(x => new KeyValuePair<int, TSource>(c++ / groupSize, x))
                .GroupBy(x => x.Key, x => x.Value)
                .ToList();  // Materialize the list since we need an accurate count to pass out
            setTotal(c);
            return result;
        }
    }
}