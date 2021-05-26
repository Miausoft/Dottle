using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Dottle.Web.Models;
using Microsoft.Extensions.Configuration;
using Dottle.Persistence.Repository;
using Dottle.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using System.Linq;

namespace Dottle.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IMapper _mapper;
        private readonly IConfiguration _config;

        public HomeController(IRepository<Post> postRepo, IMapper mapper, IConfiguration config)
        {
            _postRepo = postRepo;
            _mapper = mapper;
            _config = config;
        }

        public async Task<IActionResult> Index()
        {
            var layoutPreference = _config["UserConfig:SiteLayout"];
            ViewBag.PostLayout = layoutPreference;

            // TODO: MUST move mapping somewhere else

            var posts = await _postRepo.GetAllInclude(nameof(Post.Rates)).ToListAsync();
            var model = _mapper.Map<List<Post>, List<PostViewModel>>(posts);

            foreach (var x in posts)
            {
                var itemToChange = model.FirstOrDefault(m => m.Id.Equals(x.Id));
                if (itemToChange != null)
                {
                    itemToChange.AverageRate = x.Rates
                        .Select(r => r.Value)
                        .DefaultIfEmpty()
                        .Average();

                    itemToChange.UserRate = x.Rates
                        .FirstOrDefault(r =>
                        {
                            if (User.Identity.Name == null) return false;
                            return r.UserId.Equals(User.Identity.Name) && r.PostId.Equals(x.Id);
                        })?.Value;
                }
            }
            return View(model);
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
