using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Dottle.Helpers;
using Dottle.Models;

namespace Dottle.Controllers
{
    public class AuthController : Controller
    {

        public ServiceDbContext db;

        public AuthController(ServiceDbContext db)
        {
            this.db = db;
        }

        [HttpGet]
        public ViewResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> SignUp(UserRegisterModel user)
        {
            if (ModelState.IsValid)
            {
                UserModel newUser = new UserModel();
                newUser.Name = user.Name;
                newUser.Surname = user.Surname;
                newUser.PasswordHash = PasswordManager.HashPassword(user.Password);
                await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();
                return View("Success", newUser);
            }
            return View();
        }

        [HttpGet]
        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> SignIn(UserModel user)
        {
            if (ModelState.IsValid)
            {
                //var response = await db.Users.FindAsync(user.Email);
                //if (response != null) return View("LoggedIn");
                return View("SignIn");
            }
            else
            {
                return View();
            }
        }

    }
}
