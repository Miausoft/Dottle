using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Dottle.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Dottle.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ServiceDbContext db;
        private readonly IConfiguration _config;

        public HomeController(ILogger<HomeController> logger, ServiceDbContext db, IConfiguration config)
        {
            this.db = db;
            _logger = logger;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var layoutPreference = _config["UserConfig:SiteLayout"];
            ViewBag.PostLayout = layoutPreference;
            var posts = await db.Posts.ToListAsync();
            return View(posts);
        }

        [HttpPost]
        public RedirectResult UpdateSettings(ViewModels.UserSetting data)
        {
            _config["UserConfig:SiteLayout"] = data.SiteLayout;
            return new RedirectResult(url: "/", permanent: true);
        }
        public IActionResult Settings()
        {
            ViewModels.UserSetting uc = new ViewModels.UserSetting { SiteLayout = _config["UserConfig:SiteLayout"] };
            return View("Settings", uc);
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
