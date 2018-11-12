using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static int Count<T>(this IEnumerable<T> input)
        {
            var count = 0;
            var enumerator = input.GetEnumerator();

            while (enumerator.MoveNext())
            {
                count++;
            }

            return count;
        }

        public static int Count<T>(this IEnumerable<T> input, Func<T, bool> predicate)
        {
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
