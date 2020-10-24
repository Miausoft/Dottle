using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dottle.Models;
using Microsoft.EntityFrameworkCore.Internal;

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
            List<string> days = new List<string>();
            days.Add("Monday");
            days.Add("Tuesday");
            days.Add("Wednesday");
            days.Add("Thursday");
            days.Add("Friday");
            days.Add("Saturday");
            days.Add("Sunday");

            return View(days);
        }

        [HttpPost]
        public async Task<JsonResult> Create(string jsonPost)
        {
            PostModel post = JsonConvert.DeserializeObject<PostModel>(jsonPost);
            List<string> errors = ValidatePost(post);
            if (errors.Count == 0)
            {
                await db.Posts.AddAsync(post);
                await db.SaveChangesAsync();
                return Json("Your post has been successfully created!");
            }

            return Json(string.Join("\n", errors));
        }

        private List<string> ValidatePost(PostModel post)
        {
            List<string> errors = new List<string>();
            if (string.IsNullOrEmpty(post.Title))
            {
                errors.Add("Error: Title can't be empty!");
            }

            if (string.IsNullOrEmpty(post.PhoneNumber))
            {
                errors.Add("Error: Phone number can't be empty!");
            }
            
            if (string.IsNullOrEmpty(post.Email))
            {
                errors.Add("Error: Email can't be empty!");
            }
            
            if (string.IsNullOrEmpty(post.Address))
            {
                errors.Add("Error: Address can't be empty!");
            }

            return errors;
        }
        
    }
}
