﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace UnitTests
{
    /// <summary>
    /// This is originally from MoreLINQ:
    /// http://morelinq.googlecode.com
    /// 
    /// It may end up being expanded a bit though :)
    /// </summary>
    internal static class TestExtensions
    {
        /// <summary>
        /// Just to make our testing easier, allow ourselves to use the real SequenceEquals
        /// call from LINQ to Obects.
        /// </summary>
        internal static void AssertSequenceEqual<T>(this IEnumerable<T> actual, IEnumerable<T> expected)
        {
            Assert.IsTrue(actual.SequenceEqual(expected));
        }

        /// <summary>
        /// Make testing even easier - a params array makes for readable tests :)
        /// The sequence is evaluated exactly once.
        /// </summary>
        internal static void AssertSequenceEqual<T>(this IEnumerable<T> actual, params T[] expected)
        {
            // Working with a copy means we can look over it more than once.
            // We're safe to do that with the array anyway.
            List<T> copy = actual.ToList();
            bool result = copy.SequenceEqual(expected);
            // Looks nicer than Assert.IsTrue or Assert.That, unfortunately.
            if (!result)
            {
                Assert.Fail("Expected: " +
                    ",".InsertBetween(expected.Select(x => Convert.ToString(x))) + "; was: " +
                    ",".InsertBetween(copy.Select(x => Convert.ToString(x))));
            }
        }

        internal static string InsertBetween(this string delimiter, IEnumerable<string> items)
        {
            StringBuilder builder = new StringBuilder();
            foreach (string item in items)
            {
                if (builder.Length != 0)
                {
                    builder.Append(delimiter);
                }
                builder.Append(item);
            }
            return builder.ToString();
        }

        //internal static IEnumerable<string> GenerateSplits(this string str, params char[] separators)
        //{
        //    foreach (var split in str.Split(separators))
        //        yield return split;
        //}

    }

}
