using LinqToObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
//using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class ConcatTests
    {
        [Test]
        public void WillThrowExceptionForNullFirstArgument()
        {
            List<string> first = null;
            var second = new List<string> { "something", "else" };
            Assert.Throws<ArgumentNullException>(() => { first.Concat(second); });
        }

        [Test]
        public void WillThrowExceptionForNullSecondArgument()
        {
            var first = new List<string>();
            Assert.Throws<ArgumentNullException>(() => { first.Concat(null); });
        }

        [Test]
        public void SimpleConcatenation()
        {
            var first = new string[] { "a", "b" };
            var second = new string[] { "c", "d" };

            var output = first.Concat(second);

            output.AssertSequenceEqual("a", "b", "c", "d");
        }

        [Test]
        public void FirstSequenceIsNotAccessedBeforeUse()
        {
            var first = new ThrowingEnumerable();
            var second = new int[] { 5 };

            var query = first.Concat(second);

            using (var iterator = query.GetEnumerator())
            {
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }

        [Test]
        public void SecondSequenceIsNotAccessedBeforeUse()
        {
            var first = new int[] { 5 };
            var second = new ThrowingEnumerable();

            var query = first.Concat(second);

            using (var iterator = query.GetEnumerator())
            {
                // MoveNext will succeed as there is an element in first.
                Assert.IsTrue(iterator.MoveNext());
                Assert.AreEqual(5, iterator.Current);

                // This will fail as the second sequence doesn't support MoveNext().
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }
    }
}
