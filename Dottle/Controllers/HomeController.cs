using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dottle.Models;
using Microsoft.EntityFrameworkCore;

namespace Dottle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ServiceDbContext db;

        public HomeController(ILogger<HomeController> logger, ServiceDbContext db)
        {
            this.db = db;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var posts = await db.Posts.ToListAsync();
            return View(posts);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
