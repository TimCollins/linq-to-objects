using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source cannot be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate cannot be null");
            }

            return WhereImpl(source, predicate);
        }

        private static IEnumerable<TSource> WhereImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    yield return item;
                }
            }
        }

        public static IEnumerable<TSource> Where<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source cannot be null");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate cannot be null");
            }

            return WhereImpl(source, predicate);
        }

        private static IEnumerable<TSource> WhereImpl<TSource>(this IEnumerable<TSource> source, Func<TSource, int, bool> predicate)
        {
            var index = 0;
            foreach (TSource item in source)
            {
                if (predicate(item, index))
                {
                    yield return item;
                }

                index++;
            }
        }
    }
}
