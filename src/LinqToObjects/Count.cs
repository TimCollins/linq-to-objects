using System;
using System.Collections;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static int Count<T>(this IEnumerable<T> input)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input cannot be null");
            }

            var count = 0;

            if (TryFastCount(input, out count))
            {
                return count;
            }

            // Do it the slow way making sure to overflow appropriately.
            checked
            {
                using (var iterator = input.GetEnumerator())
                {
                    while (iterator.MoveNext())
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        public static int Count<T>(this IEnumerable<T> input, Func<T, bool> predicate)
        {
            if (input == null)
            {
                throw new ArgumentNullException("input cannot be null");
            }

            var count = 0;
            checked
            {
                foreach (T item in input)
                {
                    if (predicate(item))
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static bool TryFastCount<TSource>(IEnumerable<TSource> source, out int count)
        {
            // Optimisation for ICollection<T>
            ICollection<TSource> genericCollection = source as ICollection<TSource>;

            if (genericCollection != null)
            {
                count = genericCollection.Count;
                return true;
            }

            // Optimisation for ICollection
            var nonGenericCollection = source as ICollection;

            if (nonGenericCollection != null)
            {
                count = nonGenericCollection.Count;
                return true;
            }

            count = 0;
            return false;
        }
    }
}
