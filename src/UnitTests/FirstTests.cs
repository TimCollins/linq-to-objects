using LinqToObjects;
using NUnit.Framework;
using System;
//using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class FirstTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.First());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.First(n => n > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.First(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.First());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.First());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };

            Assert.AreEqual(5, source.First());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };

            Assert.Throws<InvalidOperationException>(() => source.First(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWitMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.First(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.Throws<InvalidOperationException>(() => source.First(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };

            Assert.Throws<InvalidOperationException>(() => source.First(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 3, 4 };

            Assert.AreEqual(4, source.First(n => n > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 3, 4, 8, 16 };

            Assert.AreEqual(4, source.First(n => n > 3));
        }

        [Test]
        public void EarlyOutAfterFirstElementWithoutPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(n => 10 / n);

            Assert.AreEqual(0, query.First());
        }

        [Test]
        public void EarlyOutAfterFirstElementWithPredicate()
        {
            int[] source = { 15, 1, 0, 3 };
            var query = source.Select(n => 10 / n);

            Assert.AreEqual(10, query.First(n => n > 5));
        }
    }
}
