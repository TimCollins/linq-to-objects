using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using LinqToObjects;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class DistinctTests
    {
        private static readonly string TestString1 = "test";
        private static readonly string TestString2 = new string(TestString1.ToCharArray());

        [Test]
        public void NullSourceNoComparer()
        {
            string[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Distinct());
        }

        [Test]
        public void NullSourceWithComparer()
        {
            string[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Distinct(StringComparer.Ordinal));
        }

        [Test]
        public void NullElementsArePassedToComparer()
        {
            IEqualityComparer<object> comparer = new SimpleEqualityComparer();

            Assert.AreEqual(0, comparer.GetHashCode(null));
            Assert.IsFalse(comparer.Equals(null, "xyz"));

            string[] source = { "xyz", null, "abc", null };
            var distinct = source.Distinct(comparer);

            Assert.AreEqual(3, distinct.Count());
        }

        [Test]
        public void HashSetCopesWithNullElementsIfComparerDoes()
        {
            IEqualityComparer<string> comparer = EqualityComparer<string>.Default;

            Assert.AreEqual(comparer.GetHashCode(null), comparer.GetHashCode(null));
            Assert.IsTrue(comparer.Equals(null, null));

            string[] source = { "xyz", null, "xyz", null, "abc" };
            source.Distinct(comparer).AssertSequenceEqual("xyz", null, "abc");
        }

        [Test]
        public void NoComparerSpecifiedUsesDefault()
        {
            string[] source = { "xyz", TestString1, "XYZ", TestString2, "def" };
            source.Distinct().AssertSequenceEqual("xyz", TestString1, "XYZ", "def");
        }

        [Test]
        public void NullComparerUsesDefault()
        {
            string[] source = { "xyz", TestString1, "XYZ", TestString2, "def" };
            source.Distinct(null).AssertSequenceEqual("xyz", TestString1, "XYZ", "def");
        }

        [Test]
        public void DistinctStringsWithCaseInsensitiveComparer()
        {
            string[] source = { "xyz", TestString1, "XYZ", TestString2, "def" };
            source.Distinct(StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("xyz", TestString1, "def");
        }

        [Test]
        public void DistinctStringsCustomComparer()
        {
            string[] source = { "xyz", TestString1, "XYZ", TestString2, TestString1 };
            source.Distinct(new ReferenceEqualityComparer())
                .AssertSequenceEqual("xyz", TestString1, "XYZ", TestString2);
        }

        private class SimpleEqualityComparer : IEqualityComparer<object>
        {
            // Use explicit interface implementation to avoid a warning about things being hidden.
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(object obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }

        private class ReferenceEqualityComparer : IEqualityComparer<object>
        {
            // Use explicit interface implementation to avoid a warning about things being hidden.
            bool IEqualityComparer<object>.Equals(object x, object y)
            {
                return ReferenceEquals(x, y);
            }

            public int GetHashCode(object obj)
            {
                return RuntimeHelpers.GetHashCode(obj);
            }
        }
    }    
}
