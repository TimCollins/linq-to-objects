using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    return default(TSource);
                }

                return iterator.Current;
            }
        }

        public static TSource FirstOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return item;
                }
            }

            return default(TSource);
        }
    }
}
