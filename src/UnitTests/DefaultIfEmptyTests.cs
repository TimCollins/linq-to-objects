using System;
using System.Linq;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class DefaultIfEmptyTests
    {
        [Test]
        public void NullSourceNoDefaultValue()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty());
        }

        [Test]
        public void NullSourceWithDefaultValue()
        {
            int[] source = null;

            Assert.Throws<ArgumentNullException>(() => source.DefaultIfEmpty(5));
        }

        [Test]
        public void EmptySequenceNoDefaultValue()
        {
            Enumerable.Empty<int>().DefaultIfEmpty(5).AssertSequenceEqual(5);
        }

        [Test]
        public void EmptySequenceWithDefaultValue()
        {
            int[] source = { 3, 1, 4 };

            source.DefaultIfEmpty().AssertSequenceEqual(source);
        }

        [Test]
        public void NonEmptySequenceWithDefaultValue()
        {
            int[] source = { 3, 1, 4 };

            source.DefaultIfEmpty(5).AssertSequenceEqual(source);
        }
    }
}
