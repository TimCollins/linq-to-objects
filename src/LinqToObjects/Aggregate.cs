using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource Aggregate<TSource>(
            this IEnumerable<TSource> source, 
            Func<TSource, TSource, TSource> func)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Source sequence was empty");
                }

                TSource current = iterator.Current;

                while (iterator.MoveNext())
                {
                    current = func(current, iterator.Current);
                }

                return current;
            }
        }

        public static TAccumulate Aggregate<TSource, TAccumulate>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func)
        {
            return source.Aggregate(seed, func, x => x);
        }

        public static TResult Aggregate<TSource, TAccumulate, TResult>(
            this IEnumerable<TSource> source,
            TAccumulate seed,
            Func<TAccumulate, TSource, TAccumulate> func,
            Func<TAccumulate, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (func == null)
            {
                throw new ArgumentNullException("func");
            }

            using (var iterator = source.GetEnumerator())
            {
                TAccumulate current = seed;

                while (iterator.MoveNext())
                {
                    current = func(current, iterator.Current);
                }

                return resultSelector(current);
            }
        }
    }
}
