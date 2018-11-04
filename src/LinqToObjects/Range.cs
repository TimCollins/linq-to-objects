using System;
using System.Collections.Generic;

namespace LinqToObjects
{
    public static partial class Enumerable
    {
        public static IEnumerable<int> Range(int start, int count)
        {
            if (count < 0)
            {
                throw new ArgumentOutOfRangeException("count cannot be negative");
            }

            // Convert everything to long to avoid overflows. There are other ways of checking
            // for overflow, but this way make the code correct in the most obvious way.
            if ((long)start + (long)count - 1L > int.MaxValue)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            // Can't just do this as the execution of the code can either be immediate or deferred.
            // If it's deferred then the validation doesn't fire and if it's immediate then
            // yield return won't work.
            // Each block can be one or the other but not both.
            //yield return 1;
            return RangeImpl(start, count);
        }

        
        private static IEnumerable<int> RangeImpl(int start, int count)
        {
            for (var i = 0; i < count; i++)
            {
                yield return start + i;
            }
        }
    }
}
