using System;

namespace CarBuddy.Domain.Extensions
{
    internal static class StringExtensions
    {
        internal static bool EqualsIgnoreCase(this string source, string target)
            => string.Compare(source, target, StringComparison.InvariantCultureIgnoreCase) == 0;
    }
}
