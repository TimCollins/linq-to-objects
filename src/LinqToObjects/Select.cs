using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        // Returns an enumerable collection of the TResult type (since the TSource type is getting projected to that type)
        // Takes an enumerable collection of TSource as input (as an extension method)
        // Takes a delegate which takes a TSource element and returns a TResult element
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source cannot be null");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector cannot be null");
            }

            return SelectImpl(source, selector);
        }

        private static IEnumerable<TResult> SelectImpl<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            foreach (var s in source)
            {
                yield return selector(s);
            }
        }

        // Here the delegate takes a second int parameter representing the element index
        public static IEnumerable<TResult> Select<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source cannot be null");
            }

            if (selector == null)
            {
                throw new ArgumentNullException("selector cannot be null");
            }

            return SelectImpl(source, selector);
        }

        private static IEnumerable<TResult> SelectImpl<TSource, TResult>(IEnumerable<TSource> source, Func<TSource, int, TResult> selector)
        {
            var index = 0;
            foreach (var s in source)
            {
                yield return selector(s, index);
                index++;
            }
        }
    }
}
