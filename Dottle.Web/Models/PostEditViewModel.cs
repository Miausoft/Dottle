using System.Collections.Generic;

namespace Dottle.Models
{
    public class PostEditViewModel
    {
        public string PrettyTimeSheet;
        public PostModel Post;
        public List<string> Days;
        public List<WorkingDay> MarkedTimes;
    }
}
