using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dottle.Web.Models;
using Microsoft.Extensions.Configuration;
using Dottle.Persistence.Repository;
using Dottle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Dottle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IConfiguration _config;

        public HomeController(IRepository<Post> postRepo, IConfiguration config)
        {
            _postRepo = postRepo;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var layoutPreference = _config["UserConfig:SiteLayout"];
            ViewBag.PostLayout = layoutPreference;
            var posts = await _postRepo.GetAll().ToListAsync();
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
