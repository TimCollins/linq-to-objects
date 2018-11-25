using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return DefaultIfEmpty(source, default(TSource));
        }

        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source, TSource defaultValue)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return DefaultIfEmptyImpl(source, defaultValue);
        }

        private static IEnumerable<TSource> DefaultIfEmptyImpl<TSource>(IEnumerable<TSource> source, TSource defaultValue)
        {
            var iterator = source.GetEnumerator();

            if (!iterator.MoveNext())
            {
                yield return defaultValue;
            }

            while (iterator.MoveNext())
            {
                yield return iterator.Current;
            }
        }
    }
}
