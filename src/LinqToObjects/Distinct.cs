using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return DistinctImpl(source, null);
        }

        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return DistinctImpl(source, comparer);
        }

        private static IEnumerable<TSource> DistinctImpl<TSource>(IEnumerable<TSource> source, IEqualityComparer<TSource> comparer)
        {
            var seenElements = new HashSet<TSource>(comparer);

            foreach (var item in source)
            {
                if (seenElements.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}
