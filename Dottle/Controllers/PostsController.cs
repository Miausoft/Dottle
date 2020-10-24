using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dottle.Models;

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
            await db.Posts.AddAsync(post);
            await db.SaveChangesAsync();
            return Json("Saved!");
        }
        
    }
}
