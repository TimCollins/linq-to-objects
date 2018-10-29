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

            return null;
        }
    }
}
