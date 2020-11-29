using System;
using System.Collections.Generic;

namespace Dottle.Helpers
{
    public static class DayHelper
    {
        private static readonly Lazy<List<string>> LazyDays = new Lazy<List<string>>(() =>
        {
            List<string> days = new List<string>
            {
                "Monday",
                "Tuesday",
                "Wednesday",
                "Thursday",
                "Friday",
                "Saturday",
                "Sunday"
            };
            return days;
        });

        public static List<string> Days
        {
            get
            {
                return LazyDays.Value;
            }
        }
    }
}
