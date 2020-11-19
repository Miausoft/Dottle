using System.Collections.Generic;

namespace Dottle.Models
{
    public class PostEditViewModel
    {
        public string PrettyTimeSheet;
        public PostModel Post;
        public List<string> Days;
        public List<string> MarkedDays;
        public List<string> MarkedHourTo;
        public List<string> MarkedHourFrom;
        public List<string> MarkedMinuteFrom;
        public List<string> MarkedMinuteTo;
    }
}
