using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
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

        private static IEnumerable<TResult> SelectManyImpl<TSource, TResult>(
            IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TResult>> selector)
        {
            foreach (TSource item in source)
            {
                foreach (TResult result in selector(item))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TResult>> selector)
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

        private static IEnumerable<TResult> SelectManyImpl<TSource, TResult>(
                    IEnumerable<TSource> source,
                    Func<TSource, int, IEnumerable<TResult>> selector)
        {
            var index = 0;

            foreach (TSource item in source)
            {
                foreach (TResult result in selector(item, index++))
                {
                    yield return result;
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (collectionSelector == null)
            {
                throw new ArgumentNullException("collectionSelector");
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException("resultSelector");
            }

            return SelectManyImpl(source, collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(IEnumerable<TSource> source, Func<TSource, IEnumerable<TCollection>> collectionSelector, Func<TSource, TCollection, TResult> resultSelector)
        {
            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }

        public static IEnumerable<TResult> SelectMany<TSource, TCollection, TResult>(
            this IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }

            if (collectionSelector == null)
            {
                throw new ArgumentNullException("collectionSelector");
            }

            if (resultSelector == null)
            {
                throw new ArgumentNullException("resultSelector");
            }

            return SelectManyImpl(source, collectionSelector, resultSelector);
        }

        private static IEnumerable<TResult> SelectManyImpl<TSource, TCollection, TResult>(
            IEnumerable<TSource> source,
            Func<TSource, int, IEnumerable<TCollection>> collectionSelector,
            Func<TSource, TCollection, TResult> resultSelector)
        {
            var index = 0;

            foreach (TSource item in source)
            {
                foreach (TCollection collectionItem in collectionSelector(item, index++))
                {
                    yield return resultSelector(item, collectionItem);
                }
            }
        }
    }
}
