using System;

namespace CarBuddy.Domain.Extensions
{
    public static class DateTimeExtensions
    {
        public static bool EqualTo(this DateTime source, DateTime target) => source.CompareTo(target) == 0;

        public static bool IsGreaterThan(this DateTime source, DateTime target) => source.CompareTo(target) > 0;

        public static bool IsLessThan(this DateTime source, DateTime target) => source.CompareTo(target) < 0;

        public static bool IsLessThan(this DateTime source, DateTime target, int days) => source.AddDays(days).CompareTo(target) < 0;

        public static string ToDateStringInvariantCulture(this DateTime dateTime) =>
            dateTime.ToString("dd/MM/yyyy");
    }
}
