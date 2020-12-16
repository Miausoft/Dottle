using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dottle.Models;

namespace Dottle.Helpers
{
    public static class MarkedDayHelper
    {
        public static DayParam GenerateDayParam(int i, string day, List<WorkingDay> marked)
        {
            var markedDays = GetProperties(marked, o => o.DayName);
            var index = markedDays.IndexOf(day);
            var isMarked = markedDays.Contains(day);
            DayParam dayParam = new DayParam
            {
                Name = day,
                Marked = isMarked,
                HourFrom = isMarked ? GetProperties(marked, o => o.HourFrom).ElementAt(index) : "0",
                HourTo = isMarked ? GetProperties(marked, o => o.HourTo).ElementAt(index) : "0",
                MinuteFrom = isMarked ? GetProperties(marked, o => o.MinuteFrom).ElementAt(index) : "0",
                MinuteTo = isMarked ? GetProperties(marked, o => o.MinuteTo).ElementAt(index) : "0"
            };
            return dayParam;
        }

        private static List<string> GetProperties(List<WorkingDay> days, Func<WorkingDay, IComparable> getProp)
        {
            return days.Select(o => (string)getProp(o)).ToList();
        }
    }
}
