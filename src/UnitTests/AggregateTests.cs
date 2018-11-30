using System;
using System.Linq;
using NUnit.Framework;

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


    }
}
