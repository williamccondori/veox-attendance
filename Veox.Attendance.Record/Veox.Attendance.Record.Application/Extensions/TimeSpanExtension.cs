using System;

namespace Veox.Attendance.Record.Application.Extensions
{
    public static class TimeSpanExtension
    {
        public static string ToLocalString(this TimeSpan timeSpan)
        {
            var hours = timeSpan.Days * 24;
            hours += Math.Abs(timeSpan.Hours);

            var minutes = Math.Abs(timeSpan.Minutes);

            var date = $"{hours:00}:{minutes:00}";

            if (timeSpan < TimeSpan.Zero)
            {
                date = $"- {date}";
            }

            return date;
        }
    }
}