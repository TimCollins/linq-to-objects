using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestFixture]
    public class SelectManyTests
    {
        [Test]
        public void ValidateNullSource()
        {
            int[] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.SelectMany(n => n.ToString()));
        }

        [Test]
        public void ValidateNullSelector()
        {
            int[] numbers = { 3, 5, 10 };
            Func<int, IEnumerable<int>> projection = null;

            Assert.Throws<ArgumentNullException>(() => numbers.SelectMany(projection));
        }

        [Test]
        public void SimpleFlatten()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // Calling .ToCharArray() is unnecessary as string implements IEnumerable<char>
            var query = numbers.SelectMany(n => n.ToString().ToCharArray());

            query.AssertSequenceEqual('3', '5', '2', '0', '1', '5');
        }

        [Test]
        public void SimpleFlattenWithIndex()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // Add each element's value to its index and project that to the new sequence
            var query = numbers.SelectMany((n, index) => (n + index).ToString().ToCharArray());

            query.AssertSequenceEqual('3', '6', '2', '2', '1', '8');
        }

        [Test]
        public void FlattenWithProjection()
        {
            int[] numbers = { 3, 5, 20, 15 };
            // Flatten each number to its constituent characters but then project each 
            // char to a string of the original element which is responsible for creating that
            // char, as well as the char itself.
            // 20 => 20: 2 and 20: 0.
            var query = numbers.SelectMany(n => n.ToString().ToCharArray(), (n, c) => n + ": " + c);

            query.AssertSequenceEqual("3: 3", "5: 5", "20: 2", "20: 0", "15: 1", "15: 5");
        }

        [Test]
        public void SimpleFlattenWihProjectionAndIndex()
        {
            int[] numbers = { 3, 5, 20, 15 };
            var query = numbers.SelectMany((n, index) => (n + index).ToString().ToCharArray(),
                (n, c) => n + ": " + c);

            query.AssertSequenceEqual("3: 3", "5: 6", "20: 2", "20: 2", "15: 1", "15: 8");
        }

        [Test]
        public void ValidateNulSourceWithIndex()
        {
            int[] numbers = null;

            Assert.Throws<ArgumentNullException>(() => numbers.SelectMany((n, index) => (n + index).ToString().ToCharArray()));
        }
    }
}
