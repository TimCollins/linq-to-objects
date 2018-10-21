using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections;

namespace UnitTests
{
    /// <summary>
    /// Class to help for deferred execution tests: it throws an exception
    /// if GetEnumerator is called.
    /// </summary>
    internal class ThrowingEnumerable : IEnumerable<int>
    {
        public IEnumerator<int> GetEnumerator()
        {
            throw new InvalidOperationException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        internal static void AssertDeferred<T>(Func<IEnumerable<int>, IEnumerable<T>> deferredFunction)
        {
            ThrowingEnumerable source = new ThrowingEnumerable();
            var result = deferredFunction(source);

            using (var iterator = result.GetEnumerator())
            {
                Assert.Throws<InvalidOperationException>(() => iterator.MoveNext());
            }
        }
    }
}
