using NUnit.Framework;
using System;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class RangeTests
    {
        [Test]
        public void NegativeCountShouldThrowException()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(10, -1));
        }

        [Test]
        public void CountTooLargeShouldBeHandled()
        {
            // Overflow the int boundary by starting at max and incrementing by 2
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(int.MaxValue, 2));
            // Overflow the int boundary by starting at 2 and incrementing by max
            Assert.Throws<ArgumentOutOfRangeException>(() => Enumerable.Range(2, int.MaxValue));

            Assert.Throws<ArgumentOutOfRangeException>(() => 
                Enumerable.Range(int.MaxValue / 2, (int.MaxValue / 2) + 3));
        }

        [Test]
        public void ValidRange()
        {
            var input = Enumerable.Range(5, 3);

            input.AssertSequenceEqual(5, 6, 7);
        }

    }
}
