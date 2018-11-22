using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource Single<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var iterator = source.GetEnumerator();

            if (!iterator.MoveNext())
            {
                throw new InvalidOperationException("source");
            }

            if (predicate(iterator.Current))
            {
                return iterator.Current;
            }

            while (iterator.MoveNext())
            {
                if (predicate(iterator.Current))
                {
                    return iterator.Current;
                }
            }

            throw new InvalidOperationException("source");
        }

        public static TSource Single<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            var iterator = source.GetEnumerator();

            if (!iterator.MoveNext())
            {
                throw new InvalidOperationException("source");  
            }

            return default(TSource);
        }
    }
}
