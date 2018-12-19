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

            return null;
        }

        // IEqualityComparer<TSource>
        public static IEnumerable<TSource> Distinct<TSource>(this IEnumerable<TSource> source, StringComparer comparer)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return null;
        }
    }
}
