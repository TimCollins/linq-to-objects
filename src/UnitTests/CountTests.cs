using LinqToObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
//using System.Linq;
using UnitTests.TestSupport;

namespace UnitTests
{
    [TestFixture]
    public class CountTests
    {
        [Test]
        public void ShouldReturnCorrectCountForArray()
        {
            string[] input = { "apple", "banana", "mango", "orange", "passionfruit", "grape" };
            var count = input.Count();

            Assert.AreEqual(6, count);
        }

        [Test]
        public void ShouldReturnCorrectCountForList()
        {
            var input = new List<int> { 1, 2, 5 };
            var count = input.Count();

            Assert.AreEqual(3, count);
        }

        [Test]
        public void ShouldReturnCorrectCountForListWithPredicate()
        {
            var input = new List<int> { 1, 2, 5, 8, 12 };
            var count = input.Count(c => c % 2 == 0);

            Assert.AreEqual(3, count);
        }

        [Test]
        public void ShouldThrowExceptionForNullInput()
        {
            int[] input = null;

            Assert.Throws<ArgumentNullException>(() => input.Count());
        }

        [Test]
        public void ShouldThrowExceptionForNullInputWithPredicate()
        {
            int[] input = null;

            Assert.Throws<ArgumentNullException>(() => input.Count(i => i % 2 == 0));
        }

        [Test]
        public void GenericOnlyCollectionCount()
        {
            Assert.AreEqual(5, new GenericOnlyCollection<int>(Enumerable.Range(2, 5)).Count());
        }

        [Test]
        public void SemiGenericCollectionCount()
        {
            Assert.AreEqual(5, new SemiGenericCollection(Enumerable.Range(2, 5)).Count());
        }

        [Test]
        public void RegularGenericCollectionCount()
        {
            Assert.AreEqual(5, new List<int>(Enumerable.Range(2, 5)).Count());
        }
    }
}
