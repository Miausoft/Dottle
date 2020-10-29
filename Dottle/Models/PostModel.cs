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
                sb.Append("<div>");
                sb.Append(day.DayName + " - ");
                sb.Append("from: " + NumberToTime(day.HourFrom) + ":" + NumberToTime(day.MinuteFrom) + " ");
                sb.Append("to: " + NumberToTime(day.HourTo) + ":" + NumberToTime(day.MinuteTo) + "\n");
                sb.Append("</div>");
            }

            return sb.ToString();
        }
        private string NumberToTime (string time)
        {
            if (System.Int32.Parse(time) < 10) time = "0" + time;
            return time;
        }
    }
}
