using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return default(TSource);
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            return default(TSource);
        }
    }
}
