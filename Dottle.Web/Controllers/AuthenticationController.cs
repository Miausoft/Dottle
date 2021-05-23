using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Dottle.Persistence.Repository;
using Dottle.Domain.Entities;
using Dottle.Web.Models.User;
using AutoMapper;
using System.Linq;
using Dottle.Services.Security.Password;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Dottle.Web.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IPasswordHasher _hasher;
        private readonly IMapper _mapper;

        public AuthenticationController(IRepository<User> userRepo, IPasswordHasher hasher, IMapper mapper)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = _userRepo.SearchFor(u => u.Email.Equals(model.Email)).FirstOrDefault();
            if(user == null)
            {
                // TODO: pranesimas apie neteisinga slaptazodi ar el. pasta
                return View();
            }

            if(!_hasher.Verify(model.Password, user.Password))
            {
                // TODO: pranesimas apie neteisinga slaptazodi ar el. pasta
                return View();
            }

            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Id.ToString()),
                new Claim(ClaimTypes.GivenName, user.FirstName)
            };
            var identity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
        }
    }
}
