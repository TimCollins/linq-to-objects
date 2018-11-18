using NUnit.Framework;
using System;
using System.Linq;
using UnitTests.TestSupport;

namespace UnitTests
{
    [TestFixture]
    public class LastTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Last());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Last(n => n > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.Last(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Last());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.Last());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };

            Assert.AreEqual(10, source.Last());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Last(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWitMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.Last(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.Throws<InvalidOperationException>(() => source.Last(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };

            Assert.Throws<InvalidOperationException>(() => source.Last(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 3, 4 };

            Assert.AreEqual(4, source.Last(n => n > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 3, 4, 8, 16 };

            Assert.AreEqual(16, source.Last(n => n > 3));
        }

        [Test]
        public void ListWithoutPredicateDoesNotIterate()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);

            Assert.AreEqual(3, source.Last());
        }

        [Test]
        public void ListWithPredicateStillIterates()
        {
            var source = new NonEnumerableList<int>(1, 5, 10, 3);

            Assert.Throws<NotSupportedException>(() => source.Last(n => n > 3));
        }
    }
}
