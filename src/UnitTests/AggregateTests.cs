using System;
using LinqToObjects;
//using System.Linq;
using NUnit.Framework;
using UnitTests.TestSupport;

namespace UnitTests
{
    [TestFixture]
    public class AggregateTests
    {
        [Test]
        public void NullSourceUnseeded()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Aggregate((x, y) => x + y));
        }

        [Test]
        public void NullFuncUnseeded()
        {
            int[] source = { 1, 3 };

            Assert.Throws<ArgumentNullException>(() => source.Aggregate(null));
        }

        [Test]
        public void UnseededAggregation()
        {
            int[] source = { 1, 4, 5 };

            Assert.AreEqual(17, source.Aggregate((current, value) => current * 2 + value));
        }

        [Test]
        public void NullSourceSeeded()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Aggregate(3, (x, y) => x + y));
        }

        [Test]
        public void NullFuncSeeded()
        {
            int[] source = { 1, 3 };

            Assert.Throws<ArgumentNullException>(() => source.Aggregate(5, null));
        }

        [Test]
        public void SeededAggregation()
        {
            int[] source = { 1, 4, 5 };
            const int seed = 5;
            Func<int, int, int> func = (current, value) => current * 2 + value;

            // First iteration: 5 * 2 + 1 = 11
            // Second iteration: 11 * 2 + 4 = 26
            // Third iteration: 26 * 2 + 5 = 57
            Assert.AreEqual(57, source.Aggregate(seed, func));
        }

        [Test]
        public void DifferentSourceAndAccumulatorTypes()
        {
            const int largeValue = 2000000000;
            int[] source = { largeValue, largeValue, largeValue };
            var sum = source.Aggregate(0L, (acc, value) => acc + value);

            Assert.AreEqual(6000000000L, sum);
            Assert.IsTrue(sum > int.MaxValue);
        }

        [Test]
        public void EmptySequenceUnseeded()
        {
            int[] source = { };
            Assert.Throws<InvalidOperationException>(() => source.Aggregate((x, y) => x + y));
        }

        [Test]
        public void EmptySequenceSeeded()
        {
            int[] source = { };

            Assert.AreEqual(5, source.Aggregate(5, (x, y) => x + y));
        }

        [Test]
        public void EmptySequenceSeededWithResultSelector()
        {
            int[] source = { };

            Assert.AreEqual("5", source.Aggregate(5, (x, y) => x + y, x => x.ToInvariantString()));
        }

        [Test]
        public void FirstElementOfInputIsUsedAsSeedForUnseededOverload()
        {
            int[] source = { 5, 3, 2 };

            Assert.AreEqual(30, source.Aggregate((acc, value) => acc * value));
        }
    }
}
