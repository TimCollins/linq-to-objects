using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> DefaultIfEmpty<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return null;
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
            yield return defaultValue;
        }
    }
}
