using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Concat<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            if (first == null)
            {
                throw new ArgumentNullException("first");
            }

            if (second == null)
            {
                throw new ArgumentNullException("second");
            }

            // The deferred execution of the concatenation can't exist in the same scope as the 
            // immediate execution of the argument validation.
            return ConcatImpl(first, second);
        }

        private static IEnumerable<TSource> ConcatImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            // The concatenation is basically just returning all of the first 
            // set and then all of the second set.
            foreach (TSource item in first)
            {
                yield return item;
            }

            // Avoid hanging on to a reference that isn't used any more.
            first = null;

            foreach (TSource item in second)
            {
                yield return item;
            }
        }
    }
}
