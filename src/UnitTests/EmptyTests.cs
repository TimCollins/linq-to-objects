using LinqToObjects;
using NUnit.Framework;

namespace UnitTests
{
    [TestFixture]
    public class EmptyTests
    {
        [Test]
        public void ShouldReturnEmptySequence()
        {
            var output = Enumerable.Empty<int>();
            var enumerator = output.GetEnumerator();
            Assert.IsFalse(enumerator.MoveNext());
        }
    }
}
