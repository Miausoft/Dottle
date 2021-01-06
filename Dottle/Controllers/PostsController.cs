using System;
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
using System.Linq;
using Dottle.Helpers;
using Microsoft.AspNetCore.Http;

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
            return View(DayHelper.Days);
        }

        public async Task<IActionResult> Show(int id)
        {
            var post = await db.Posts.FindAsync(id);
            PostShowViewModel postShow = new PostShowViewModel();
            postShow.Post = post;
            postShow.PrettyTimeSheet = PrettyTimeSheet(post.TimeSheet);
            return View(postShow);
        }
        
        public async Task<RedirectResult> Delete(int id)
        {
            var post = await db.Posts.FindAsync(id);
            db.Remove(post);
            await db.SaveChangesAsync();
            return new RedirectResult(url: "/", permanent: true);
        }
        public delegate WorkingDay ConstructMarkedDay(WorkingDay day);
        public async Task<IActionResult> Edit(int id)
        {
            var post = await db.Posts.FindAsync(id);
            var timeSheet = JsonConvert.DeserializeObject<List<WorkingDay>>(post.TimeSheet);
            ConstructMarkedDay constructor = day => new WorkingDay
            {
                DayName = day.DayName,
                HourFrom = NumberToTime(day.HourFrom),
                HourTo = NumberToTime(day.HourTo),
                MinuteFrom = NumberToTime(day.MinuteFrom),
                MinuteTo = NumberToTime(day.MinuteTo)
            };
            List<WorkingDay> markedTimes = timeSheet.Select(day => constructor(day)).ToList();
            PostEditViewModel postEdit = new PostEditViewModel
            {
                Post = post,
                Days = DayHelper.Days,
                PrettyTimeSheet = PrettyTimeSheet(post.TimeSheet),
                MarkedTimes = markedTimes
            };
            return View(postEdit);
        }

        [HttpPost]
        public async Task<JsonResult> Create(string jsonPost)
        {
            PostModel post = JsonConvert.DeserializeObject<PostModel>(jsonPost);
            post.UserId = HttpContext.Session.GetString("User");
            List<string> errors = ValidatePost(post);
            
            if (errors.Count != 0)
            {
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

        Func<string, bool> IsValidEmail = delegate (string Email) 
        {
            var r = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            return r.IsMatch(Email) && !string.IsNullOrEmpty(Email); 
        };

        private string PrettyTimeSheet(string ts)
        {
            StringBuilder sb = new StringBuilder("");
            var timeSheet = JsonConvert.DeserializeObject<List<WorkingDay>>(ts);
            foreach (var day in timeSheet.Where(day => !string.IsNullOrEmpty(day.DayName)))
            {
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
            try
            {
                int timeInt = Int32.Parse(time);
                if (Int32.Parse(time) < 10) time = "0" + time;
            }
            catch (Exception e) 
            {
                Console.WriteLine(e.StackTrace);
                Console.WriteLine("Error occured parsing time - defaulting to zero");
                time = "00";
            }
            return time;
        }
        [HttpPost]
        public async Task<JsonResult> RatePost(string rating, int id)
        {
            var post = await db.Posts.FindAsync(id);
            var avg = (post.Total + Int32.Parse(rating)) / (post.Quantity + 1);
            post.Total = post.Total + Int32.Parse(rating);
            post.Quantity++;
            post.Rating = avg;
            await db.SaveChangesAsync();
            return Json(avg);
        }

    }
}
