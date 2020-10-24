using System.Collections.Generic;
using System.Diagnostics;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Support_Your_Locals.Models;

namespace Support_Your_Locals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            var posts = new List<PostModel>()
            {
                new PostModel { Title = "Fish", PhoneNumber = "863599081", Address = "Gedimino pr. 9, Vilnius", Description = "Catch the fish and it's yours", Email = "fisherman@gmail.com", TimeSheet = "time"},
                new PostModel { Title = "Padangu montuotojas", PhoneNumber = "863599081", Address = "Gedimino pr. 9, Vilnius", Description = "Catch the fish and it's yours", Email = "fisherman@gmail.com", TimeSheet = "time"}

            };

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
