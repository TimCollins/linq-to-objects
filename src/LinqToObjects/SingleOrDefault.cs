using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (predicate == null)
            {
                throw new ArgumentNullException("predicate");
            }

            var retVal = default(TSource);
            var foundAny = false;

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    if (foundAny)
                    {
                        throw new InvalidOperationException("Sequence contained multiple matching elements.");
                    }

                    foundAny = true;
                    retVal = item;
                }
            }

            if (!foundAny)
            {
                throw new InvalidOperationException("No item found for predicate.");
            }

            return retVal;
        }

        public static TSource SingleOrDefault<TSource>(this IEnumerable<TSource> source)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            using (IEnumerator<TSource> iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence was empty.");
                }

                var retVal = iterator.Current;

                if (iterator.MoveNext())
                {
                    throw new InvalidOperationException("Sequence contained multiple elements.");
                }

                return retVal;
            }
        }
    }
}
