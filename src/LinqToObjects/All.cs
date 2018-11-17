using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static bool All<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
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
                return true;
            }

            while (iterator.MoveNext())
            {
                if (!predicate(iterator.Current))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
