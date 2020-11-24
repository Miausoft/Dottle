using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dottle.Models;

namespace Dottle.Helpers
{
    public class MarkedDayHelper
    {
        public static DayParam generateDayParam(int i, string day, List<WorkingDay> marked)
        {
            DayParam dayParam = new DayParam();
            dayParam.Name = day;
            dayParam.Marked = getProperties(marked, o => o.DayName).Contains(day);
            dayParam.HourFrom = getProperties(marked, o => o.DayName).Contains(day) ? getProperties(marked, o => o.HourFrom).ElementAt(getProperties(marked, o => o.DayName).IndexOf(day)) : "0";
            dayParam.HourTo = getProperties(marked, o => o.DayName).Contains(day) ? getProperties(marked, o => o.HourTo).ElementAt(getProperties(marked, o => o.DayName).IndexOf(day)) : "0";
            dayParam.MinuteFrom = getProperties(marked, o => o.DayName).Contains(day) ? getProperties(marked, o => o.MinuteFrom).ElementAt(getProperties(marked, o => o.DayName).IndexOf(day)) : "0";
            dayParam.MinuteTo = getProperties(marked, o => o.DayName).Contains(day) ? getProperties(marked, o => o.MinuteTo).ElementAt(getProperties(marked, o => o.DayName).IndexOf(day)) : "0";
            return dayParam;
        }

        private static List<string> getProperties(List<WorkingDay> days, Func<WorkingDay, IComparable> getProp)
        {
            return days.Select(o => (string)getProp(o)).ToList();
        }
    }
}
