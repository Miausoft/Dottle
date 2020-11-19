using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dottle.Models;
using Microsoft.EntityFrameworkCore.Internal;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore.Storage;

namespace Dottle.Controllers
{
    public class PostsController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ServiceDbContext db;

        public PostsController(ServiceDbContext db)
        {
            this.db = db;
        }
        
        public IActionResult New()
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

            return View(days);
        }

        public async Task<IActionResult> Show(int id)
        {
            var post = await db.Posts.FindAsync(id);
            PostShowViewModel postShow = new PostShowViewModel();
            postShow.Post = post;
            postShow.PrettyTimeSheet = PrettyTimeSheet(post.TimeSheet);
            return View(postShow);
        }
        public async Task<IActionResult> Edit(int id)
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
            var post = await db.Posts.FindAsync(id);
            List<string> markedDays = new List<string>();
            List<string> markedHourFrom = new List<string>();
            List<string> markedHourTo = new List<string>();
            List<string> markedMinuteFrom = new List<string>();
            List<string> markedMinuteTo = new List<string>();
            var timeSheet = JsonConvert.DeserializeObject<List<WorkingDay>>(post.TimeSheet);
            foreach (WorkingDay day in timeSheet)
            {
                markedDays.Add(day.DayName);
                markedHourFrom.Add(NumberToTime(day.HourFrom));
                markedHourTo.Add(NumberToTime(day.HourTo));
                markedMinuteFrom.Add(NumberToTime(day.MinuteFrom));
                markedMinuteTo.Add(NumberToTime(day.MinuteTo));
            }
            PostEditViewModel postEdit = new PostEditViewModel();
            postEdit.Post = post;
            postEdit.Days = days;
            postEdit.PrettyTimeSheet = PrettyTimeSheet(post.TimeSheet);
            postEdit.MarkedDays = markedDays;
            postEdit.MarkedHourFrom = markedHourFrom;
            postEdit.MarkedHourTo = markedHourTo;
            postEdit.MarkedMinuteFrom = markedMinuteFrom;
            postEdit.MarkedMinuteTo = markedMinuteTo;
            return View(postEdit);
        }

        [HttpPost]
        // TODO: change param to PostModel for auto-deserialization
        public async Task<JsonResult> Create(string jsonPost)
        {
            PostModel post = JsonConvert.DeserializeObject<PostModel>(jsonPost);
            List<string> errors = ValidatePost(post);
            
            if (errors.Count != 0)
            {
                // TODO: create Error validation class and return actual JSON
                return Json(string.Join("\n", errors));
            }

            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return Json("Your post has been successfully created!");
        }
        [HttpPut]
        public async Task<JsonResult> Update(string jsonPost, int id)
        {
            PostModel updatedPost = JsonConvert.DeserializeObject<PostModel>(jsonPost);
            List<string> errors = ValidatePost(updatedPost);

            if (errors.Count != 0)
            {
                return Json(string.Join("\n", errors));
            }
            var post = await db.Posts.FindAsync(id);
            post.Title = updatedPost.Title;
            post.PhoneNumber = updatedPost.PhoneNumber;
            post.Email = updatedPost.Email;
            post.Address = updatedPost.Address;
            post.Description = updatedPost.Description;
            post.TimeSheet = updatedPost.TimeSheet;
            await db.SaveChangesAsync();
            return Json("Post has been successfully saved!");
        }
        private List<string> ValidatePost(PostModel post)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(post.Title))
            {
                errors.Add("Error: Title can't be empty!");
            }
            
            if (!IsValidPhone(post.PhoneNumber))
            {
               errors.Add("Error: Invalid phone number!");
            }
            
            if (!IsValidEmail(post.Email))
            {
                errors.Add("Error: Invalid email address");
            }
            
            if (string.IsNullOrEmpty(post.Address))
            {
                errors.Add("Error: Address can't be empty!");
            }

            return errors;
        }
        public bool IsValidPhone(string Phone)
        {
            var r = new Regex(@"[0-9().+\s]+");
            return r.IsMatch(Phone) && !string.IsNullOrEmpty(Phone);
        } 

        public bool IsValidEmail(string Email)
        {
            var r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return r.IsMatch(Email) && !string.IsNullOrEmpty(Email);
        }

        private string PrettyTimeSheet(string ts)
        {
            StringBuilder sb = new StringBuilder("");
            var timeSheet = JsonConvert.DeserializeObject<List<WorkingDay>>(ts);
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
            if (string.IsNullOrEmpty(time)) return "00";
            if (System.Int32.Parse(time) < 10) time = "0" + time;
            return time;
        }

    }
}
