using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource First<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return default(TSource);
        }

        public static TSource First<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return default(TSource);
        }
    }
}
