using LinqToObjects;
using NUnit.Framework;
using System;
//using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class AllTests
    {
        [Test]
        public void NullSource()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.All(n => n > 10));
        }

        [Test]
        public void NullPredicate()
        {
            int[] source = { 1, 3, 5 };

            Assert.Throws<ArgumentNullException>(() => source.All(null));
        }

        [Test]
        public void EmptySequenceReturnsTrue()
        {
            Assert.IsTrue(new int[0].All(n => n > 0));
        }

        [Test]
        public void PredicateMatchingNoElements()
        {
            int[] source = { 1, 5, 20, 30 };

            Assert.IsFalse(source.All(n => n < 1));
        }

        [Test]
        public void PredicateMatchingSomeElements()
        {
            int[] source = { 1, 5, 20, 30 };

            Assert.IsFalse(source.All(n => n > 5));
        }

        [Test]
        public void PredicateMatchingAllElements()
        {
            int[] source = { 1, 5, 20, 30 };

            Assert.IsTrue(source.All(n => n < 40));
        }

        //[Test]
        //public void SequenceIsNotEvaluatedAfterFirstNonMatch()
        //{
        //    int[] source = { 2, 10, 0, 3 };
        //    var query = source.Select(q => 10 / q);

        //    Assert.IsFalse(query.All(q => q > 2));
        //}
    }
}
