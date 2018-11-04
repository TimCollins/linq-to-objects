//using LinqToObjects;
using NUnit.Framework;
using System.Linq;

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

        /// <summary>
        /// Empty() caches an empty sequence for the same sequence called with the same 
        /// type argument.
        /// </summary>
        [Test]
        public void EmptyShouldBeASingletonPerType()
        {
            Assert.AreSame(Enumerable.Empty<int>(), Enumerable.Empty<int>());
            Assert.AreSame(Enumerable.Empty<long>(), Enumerable.Empty<long>());
            Assert.AreSame(Enumerable.Empty<string>(), Enumerable.Empty<string>());
            Assert.AreSame(Enumerable.Empty<object>(), Enumerable.Empty<object>());

            Assert.AreNotSame(Enumerable.Empty<long>(), Enumerable.Empty<int>());
            Assert.AreNotSame(Enumerable.Empty<string>(), Enumerable.Empty<object>());
        }
    }
}
