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

            var iterator = source.GetEnumerator();

            return iterator.MoveNext();
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

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
