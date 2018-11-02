using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<TResult> Empty<TResult>()
        {
            yield return default(TResult);
        }
    }
}
