using System;
using LinqToObjects;
//using System.Linq;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class SingleOrDefaultTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault());
        }

        [Test]
        public void NullSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault(n => n > 2));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.SingleOrDefault(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            int[] source = { };

            Assert.AreEqual(0, source.SingleOrDefault());
        }

        [Test]
        public void SingleElementSequenceWithoutPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.SingleOrDefault());
        }

        [Test]
        public void MultipleElementSequenceWithoutPredicate()
        {
            int[] source = { 5, 10 };

            Assert.Throws<InvalidOperationException>(() => source.SingleOrDefault());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            int[] source = { };

            Assert.AreEqual(0, source.SingleOrDefault(n => n < 2));
        }

        [Test]
        public void SingleElementSequenceWithMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(5, source.SingleOrDefault(n => n > 2));
        }

        [Test]
        public void SingleElementSequenceWithNonMatchingPredicate()
        {
            int[] source = { 5 };

            Assert.AreEqual(0, source.SingleOrDefault(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithNoPredicateMatches()
        {
            int[] source = { 1, 2, 2, 1 };

            Assert.AreEqual(0, source.SingleOrDefault(n => n > 10));
        }

        [Test]
        public void MultipleElementSequenceWithSinglePredicateMatch()
        {
            int[] source = { 1, 2, 3, 4 };

            Assert.AreEqual(4, source.SingleOrDefault(n => n > 3));
        }

        [Test]
        public void MultipleElementSequenceWithMultiplePredicateMatches()
        {
            int[] source = { 1, 2, 3, 4, 8, 16 };

            Assert.Throws<InvalidOperationException>(() => source.SingleOrDefault(n => n > 3));
        }

        [Test]
        public void EarlyOutWithoutPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(n => 10 / n);

            Assert.Throws<InvalidOperationException>(() => query.SingleOrDefault());
        }

        [Test]
        public void EarlyOutWithPredicate()
        {
            int[] source = { 1, 2, 0 };
            var query = source.Select(n => 10 / n);

            Assert.Throws<InvalidOperationException>(() => query.SingleOrDefault(n => true));
            //Assert.Throws<DivideByZeroException>(() => query.SingleOrDefault(n => true));
        }
    }
}
