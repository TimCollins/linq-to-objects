using System;
using LinqToObjects;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class DistinctTests
    {
        [Test]
        public void NullSourceNoComparer()
        {
            string[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.Distinct());
        }

        [Test]
        public void NullSourceWithComparer()
        {
            string source = null;
            Assert.Throws<ArgumentNullException>(() => source.Distinct(StringComparer.Ordinal));
        }
    }
}
