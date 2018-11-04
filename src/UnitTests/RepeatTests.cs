using LinqToObjects;
using NUnit.Framework;
using System;
//using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class RepeatTests
    {
        [Test]
        public void SimpleRepeatReturnsExpectdResult()
        {
            var res = Enumerable.Repeat("fred", 2);
            res.AssertSequenceEqual("fred", "fred");
        }

        [Test]
        public void SimpleNumericRepeatReturnsExpectdResult()
        {
            var res = Enumerable.Repeat(17, 2);
            res.AssertSequenceEqual(17, 17);
        }

        [Test]
        public void ZeroCountShouldReturnEmptySequence()
        {
            var res = Enumerable.Repeat("fred", 0);
            res.AssertSequenceEqual();
        }

        [Test]
        public void NullStringShouldBeRepeated()
        {
            var res = Enumerable.Repeat<string>(null, 2);
            res.AssertSequenceEqual(null, null);
        }

        [Test]
        public void NegativeCountShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Repeat("fred", -1));
        }
    }
}
