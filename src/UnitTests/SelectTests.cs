using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using LinqToObjects;
//using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SelectTests
    {
        [Test]
        public void NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.Select(s => s + 1));
        }

        [Test]
        public void NullProjectionThrowsArgumentNullException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, int> projection = null;

            Assert.Throws<ArgumentNullException>(() => source.Select(projection));
        }

        // 2 x Index tests

        [Test]
        public void SimpleProjectionToDifferentType()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(x => x.ToString());

            result.AssertSequenceEqual("1", "5", "2");
        }

        [Test]
        public void SideEffectsInProjection()
        {
            int[] source = new int[3]; // Actual values won't be relevant
            int count = 0;
            var query = source.Select(x => count++);
            query.AssertSequenceEqual(0, 1, 2);
            query.AssertSequenceEqual(3, 4, 5);
            count = 10;
            query.AssertSequenceEqual(10, 11, 12);
        }

        [Test]
        public void SimpleProjection()
        {
            int[] source = { 1, 5, 2 };
            var result = source.Select(s => s * 2);

            result.AssertSequenceEqual(2, 10, 4);
        }

        [Test]
        public void SimpleProjectionWithQueryExpression()
        {
            int[] source = { 1, 5, 2 };
            var result = from s in source
                         select s * 2;

            result.AssertSequenceEqual(2, 10, 4);
        }

        [Test]
        public void EmptySource()
        {
            int[] source = new int[0];
            var result = source.Select(s => s * 2);

            result.AssertSequenceEqual();
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred(src => src.Select(x => x * 2));
        }

        // 3 x WithIndex tests


    }
}
