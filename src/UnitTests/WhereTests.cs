using NUnit.Framework;
using System.Linq;

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

            Assert.AreEqual(result, new int[] { 1, 3, 2, 1 });
        }
    }
}
