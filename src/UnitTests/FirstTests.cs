using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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


    }
}
