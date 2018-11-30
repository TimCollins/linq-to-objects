using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            using (var iterator = source.GetEnumerator())
            {
                //var current = iterator.Current;
                TSource current = default(TSource);

                while (iterator.MoveNext())
                {
                    current = func(current, iterator.Current);
                }

                return current;
            }
        }

        public static TSource Aggregate<TSource>(this IEnumerable<TSource> source, TSource val, Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            return default(TSource);
        }
    }
}
