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

        public static int Count<T>(this IEnumerable<T> input, Func<int, bool> predicate)
        {
            var count = 0;
            var enumerator = input.GetEnumerator();

            while (enumerator.MoveNext())
            {
                if (predicate(Convert.ToInt32(enumerator.Current)))
                {
                    count++;
                }
            }

            return count;
        }
    }
}
