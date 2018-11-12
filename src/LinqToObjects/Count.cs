using System;
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

            foreach (T item in input)
            {
                count++;
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

            foreach (T item in input)
            {
                if (predicate(item))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
