using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector");
            }

            return SelectManyImpl(source, selector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource item in source)
            {
                foreach (TResult result in selector(item))
                {
                    yield return result;
                }
            }
        }
    }
}
