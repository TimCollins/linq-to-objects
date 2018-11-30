using System.Globalization;

namespace UnitTests.TestSupport
{
    public static class Int32Extensions
    {
        /// <summary>
        /// Convert an Int32 to a string using the invariant culture,
        /// without explicitly having to use CultureInfo.InvariantCulture everywhere.
        /// </summary>
        public static string ToInvariantString(this int value)
        {
            return value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
