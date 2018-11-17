using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class AnyTests
    {
        [Test]
        public void NullSourceWithoutPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Any());
        }

        [Test]
        public void NulSourceWithPredicate()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Any(n => n > 10));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.Any(null));
        }

        [Test]
        public void EmptySequenceWithoutPredicate()
        {
            Assert.IsFalse(new int[0].Any());
        }

        [Test]
        public void EmptySequenceWithPredicate()
        {
            Assert.IsFalse(new int[0].Any(n => n > 10));
        }

        [Test]
        public void NonEmptySequenceWithoutPredicate()
        {
            Assert.IsTrue(new int[1].Any());
        }

        [Test]
        public void NonEmptySequenceWithPredicateMatchingElement()
        {
            int[] source = { 1, 5, 20, 30 };

            Assert.IsTrue(source.Any(n => n > 10));
        }

        [Test]
        public void NonEmptySequenceWithPredicateNotMatchingElement()
        {
            int[] source = { 1, 5, 20, 30 };
            Assert.IsFalse(source.Any(n => n > 40));
        }

        [Test]
        public void SequenceIsNotEvaluatedAfterFirstMatch()
        {
            int[] source = { 10, 2, 0, 3 };
            var query = source.Select(n => 10 / n);

            Assert.IsTrue(query.Any(q => q > 2));
        }
    }
}
