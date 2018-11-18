using LinqToObjects;
using NUnit.Framework;
using System;
//using System.Linq;
using UnitTests.TestSupport;

namespace UnitTests
{
    [TestFixture]
    public class LastOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.LastOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(n => n > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.LastOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };

            Assert.AreEqual(0, source.LastOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.LastOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };

            Assert.AreEqual(10, source.LastOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };

            Assert.AreEqual(0, source.LastOrDefault(n => n < 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.LastOrDefault(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(0, source.LastOrDefault(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };

            Assert.AreEqual(0, source.LastOrDefault(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 3, 4 };

            Assert.AreEqual(4, source.LastOrDefault(n => n > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 3, 4, 8, 16 };

            Assert.AreEqual(16, source.LastOrDefault(n => n > 3));
        }

        [Test]
        public void ListWithoutPredicateDoesNotIterate()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);

            Assert.AreEqual(3, source.LastOrDefault());
        }

        [Test]
        public void ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);

            Assert.Throws<NotSupportedException>(() => source.LastOrDefault(n => n > 3));
        }
    }
}
