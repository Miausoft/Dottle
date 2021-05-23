using System;
using System.ComponentModel.DataAnnotations;

namespace Dottle.Domain.Entities
{
    public class TimeSheet
    {
        [Range(1, 7)]
        public int DayOfWeek { get; set; }

        [Range(0, 86400)]
        public int OpensAt { get; set; }

        [Range(0, 86400)]
        public int ClosesAt { get; set; }

        public Guid PostId { get; set; }
        public Post Post { get; set; }
    }
}
