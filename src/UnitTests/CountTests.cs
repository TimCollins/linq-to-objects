using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqToObjects
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
    }
}
