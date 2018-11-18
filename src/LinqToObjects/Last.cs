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

            var foundAny = false;
            var last = default(TSource);

            foreach (TSource item in source)
            {
                if (predicate(item))
                {
                    foundAny = true;
                    last = item;
                }
            }

            if (!foundAny)
            {
                throw new InvalidOperationException("No items matched the predicate");
            }

            return last;
        }
    }
}
