using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Dottle.Models
{
    public class PostModel
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Required]
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter title")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Add description")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Enter phone number")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Enter email address")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Enter business address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Select working hours")]
        public string TimeSheet { get; set; }

        public string PrettyWorkingHours()
        {
            StringBuilder sb = new StringBuilder("");
            var timeSheet = JsonConvert.DeserializeObject<List<WorkingDay>>(this.TimeSheet);
            foreach (WorkingDay day in timeSheet)
            {
                if (string.IsNullOrEmpty(day.DayName)) continue;
                string hourFrom = day.HourFrom;
                string hourTo = day.HourTo;
                string minuteFrom = day.MinuteFrom;
                string minuteTo = day.MinuteTo;
                sb.Append("<div>");
                sb.Append(day.DayName + " - ");
                if (System.Int32.Parse(day.HourFrom) < 10) hourFrom = NumberToTime(day.HourFrom);
                if (System.Int32.Parse(day.HourTo) < 10) hourTo = NumberToTime(day.HourTo);
                if (System.Int32.Parse(day.MinuteFrom) < 10) minuteFrom = NumberToTime(day.MinuteFrom);
                if (System.Int32.Parse(day.MinuteTo) < 10) minuteTo = NumberToTime(day.MinuteTo);
                sb.Append("from: " + hourFrom + ":" + minuteFrom + " ");
                sb.Append("to: " + hourTo + ":" + minuteTo + "\n");
                sb.Append("</div>");
            }

            return sb.ToString();
        }
        private string NumberToTime (string time)
        {
            time = "0" + time;
            return time;
        }
    }
}
