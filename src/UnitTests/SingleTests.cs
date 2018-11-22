using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SingleTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Single());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Single(n => n > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.Single(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Single());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.Single(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWitMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.Single(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.Throws<InvalidOperationException>(() => source.Single(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };

            Assert.Throws<InvalidOperationException>(() => source.Single(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 3, 4 };

            Assert.AreEqual(4, source.Single(n => n > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 3, 4, 8, 16 };

            Assert.Throws<InvalidOperationException>(() => source.Single(x => x > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);

            Assert.Throws<InvalidOperationException>(() => query.Single());
        }

        [Test]
        public void EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(x => 10 / x);

            Assert.Throws<InvalidOperationException>(() => query.Single(x => true));
        }
    }
}
