using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource Last<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var list = new List<TSource>(source);

            if (list.Count == 0)
            {
                throw new InvalidOperationException("source");
            }


            return list[list.Count - 1];
        }

        public static TSource Last<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var list = new List<TSource>(source);

            if (list.Count == 0)
            {
                throw new InvalidOperationException("source");
            }

            for (var i = list.Count - 1; i >= 0; i--)
            {
                var item = list[i];
                if (predicate(list[i]))
                {
                    return item;
                }
            }

            throw new InvalidOperationException("source");
        }
    }
}
