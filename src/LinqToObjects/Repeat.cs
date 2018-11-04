using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Repeat<TResult>(TResult element, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count cannot be negative");
            }

            return RepeatImpl(element, count);
        }

        private static IEnumerable<TResult> RepeatImpl<TResult>(TResult element, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return element;
            }
        }
    }
}
