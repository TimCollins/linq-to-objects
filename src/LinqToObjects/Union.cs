using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second)
        {
            //if (first == null)
            //{
            //    throw new ArgumentNullException("source");
            //}

            //if (second == null)
            //{
            //    throw new ArgumentNullException("source");
            //}

            //return UnionImpl(first, second, EqualityComparer<TSource>.Default);
            // The duplicate validation can be avoided done by simply calling the overloaded method with the default 
            // comparer and doing the validation in that function
            return first.Union(second, EqualityComparer<TSource>.Default);
        }

        public static IEnumerable<TSource> Union<TSource>(this IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            if (first == null)
            {
                throw new ArgumentNullException("source");
            }

            if (second == null)
            {
                throw new ArgumentNullException("source");
            }

            return UnionImpl(first, second, comparer);
        }

        private static IEnumerable<TSource> UnionImpl<TSource>(IEnumerable<TSource> first, IEnumerable<TSource> second, IEqualityComparer<TSource> comparer)
        {
            var seen = new HashSet<TSource>(comparer);

            foreach (var item in first)
            {
                if (seen.Add(item))
                {
                    yield return item;
                }
            }

            foreach (var item in second)
            {
                if (seen.Add(item))
                {
                    yield return item;
                }
            }
        }
    }
}
