using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SelectManyTests
    {
        [Test]
        public void ValidateNullSource()
        {
            int[] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.SelectMany(n => n.ToString()));
        }
    }
}
