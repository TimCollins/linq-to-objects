using System;
using NUnit.Framework;
using LinqToObjects;

namespace UnitTests
{
    [TestFixture]
    public class UnionTests
    {
        [Test]
        public void NullFirstNoComparer()
        {
            string[] first = null;
            string[] second = {};

            Assert.Throws<ArgumentNullException>(() => first.Union(second));
        }

        [Test]
        public void NullSecondNoComparer()
        {
            string[] first = {};
            string[] second = null;

            Assert.Throws<ArgumentNullException>(() => first.Union(second));
        }

        [Test]
        public void NullFirstWithComparer()
        {
            string[] first = null;
            string[] second = { };

            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
        }

        [Test]
        public void NullSecondWithComparer()
        {
            string[] first = { };
            string[] second = null;

            Assert.Throws<ArgumentNullException>(() => first.Union(second, StringComparer.Ordinal));
        }

        [Test]
        public void UnionWithoutComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };

            first.Union(second).AssertSequenceEqual("a", "b", "B", "c", "d", "e");
        }

        [Test]
        public void UnionWithNullComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };

            first.Union(second, null).AssertSequenceEqual("a", "b", "B", "c", "d", "e");
        }

        [Test]
        public void UnionWithCaseInsensitiveComparer()
        {
            string[] first = { "a", "b", "B", "c", "b" };
            string[] second = { "d", "e", "d", "a" };

            first.Union(second, StringComparer.OrdinalIgnoreCase).AssertSequenceEqual("a", "b", "c", "d", "e");
        }

        [Test]
        public void UnionWithEmptyFirstSequence()
        {
            string[] first = { };
            string[] second = { "d", "e", "d", "a" };

            first.Union(second).AssertSequenceEqual("d", "e", "a");
        }

        [Test]
        public void UnionWithEmptySecondSequence()
        {
            string[] first = { "d", "e", "d", "a" };
            string[] second = { };

            first.Union(second).AssertSequenceEqual("d", "e", "a");
        }

        [Test]
        public void UnionWithTwoEmptySequences()
        {
            string[] first = { };
            string[] second = { };

            first.Union(second).AssertSequenceEqual();
        }

        [Test]
        public void FirstSequenceNotUsedUntilQueryIterated()
        {
            var first = new ThrowingEnumerable();
            int[] second = { 2 };
            var query = first.Union(second);

            using (var iterator = query.GetEnumerator())
            {
                // No exception thrown until here.
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [Test]
        public void SecondSequenceNotUsedUniltFirstIsExhausted()
        {
            int[] first = { 3, 5, 3 };
            var second = new ThrowingEnumerable();

            using (var iterator = first.Union(second).GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(3, iterator.Current);

                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [Test]
        [ExpectedException("System.InvalidOperationException")]
        public void AlternateExceptionExpectation()
        {
            int[] first = { 3, 5, 3 };
            var second = new ThrowingEnumerable();

            using (var iterator = first.Union(second).GetEnumerator())
            {
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(3, iterator.Current);

                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                iterator.MoveNext();
            }
        }
    }
}
