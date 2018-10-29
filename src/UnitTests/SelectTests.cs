using NUnit.Framework;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SelectTests
    {
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
    }
}
