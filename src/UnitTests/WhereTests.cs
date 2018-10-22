using System;
using System.Collections.Generic;
//using System.Linq;
using LinqToObjects;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class WhereTests
    {
        [Test]
        public void SimpleFiltering()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };

            var result = source.Where(x => x < 4);

            result.AssertSequenceEqual(1, 3, 2, 1);
        }

        [Test]
        public void NullSourceThrowsNullArgumentException()
        {
            IEnumerable<int> source = null;

            Assert.Throws<ArgumentNullException>(() => source.Where(x => x > 5));
        }

        [Test]
        public void NullPredicateThrowsNullArgumentException()
        {
            int[] source = { 1, 3, 7, 9, 10 };
            Func<int, bool> predicate = null;

            Assert.Throws<ArgumentNullException>(() => source.Where(predicate));
        }

        [Test]
        public void ExecutionIsDeferred()
        {
            ThrowingEnumerable.AssertDeferred<int>(src => src.Where(x => x > 0));
        }

        [Test]
        public void SimpleFilteringWithQueryExpression()
        {
            int[] source = { 1, 3, 4, 2, 8, 1 };
            var result = from i in source
                         where i < 4
                         select i;

            result.AssertSequenceEqual(1, 3, 2, 1);
        }
    }
}
