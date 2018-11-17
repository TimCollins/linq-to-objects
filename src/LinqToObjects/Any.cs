using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static bool Any<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            return AnyImpl(source);
        }

        private static bool AnyImpl<TSource>(IEnumerable<TSource> source)
        {
            var count = 0;
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    count++;
                }
            }

            return count > 0;
        }

        public static bool Any<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            return AnyImpl(source, predicate);
        }

        private static bool AnyImpl<TSource>(IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            var count = 0;
            using (var iterator = source.GetEnumerator())
            {
                while (iterator.MoveNext())
                {
                    if (predicate(iterator.Current))
                    {
                        count++;
                    }
                }
            }

            return count > 0;
        }
    }
}
