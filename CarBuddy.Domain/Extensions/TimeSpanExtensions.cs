using System;

namespace CarBuddy.Domain.Extensions
{
    public static class TimeSpanExtensions
    {
        public static string ToHoursAndMinutesString(this TimeSpan time) => time.ToString("hh\\:mm");

        public static bool IsEqualTo(this TimeSpan time, TimeSpan other) => time.CompareTo(other) == 0;

        public static bool IsLessThan(this TimeSpan time, TimeSpan other) => time.CompareTo(other) < 0;
    }
}
