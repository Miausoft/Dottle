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
            var search = db.Users.FindAsync(user.Name);
            if (search.Result != null)
            {
                ModelState.AddModelError("Name", "Such user already exists!");
            }

            if (!ModelState.IsValid)
            {
                return View(user);
            }

            UserModel newUser = new UserModel();
            newUser.Name = user.Name;
            newUser.Surname = user.Surname;
            string salt = PasswordManager.CreateSalt();
            newUser.PasswordHash = PasswordManager.HashPassword(user.Password, salt);
            newUser.PasswordSalt = salt;
            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
            return View("SuccessSignUp", newUser);
        }

        [HttpGet]
        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> SignIn(UserRegisterModel user)
        {
            var storedUser = await db.Users.FindAsync(user.Name);
            if (storedUser != null)
            {
                if (PasswordManager.VerifyHashedPassword(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
                {
                    return View("SuccessSignIn", storedUser);
                }
                ModelState.AddModelError("Password", "Invalid password!");
            }
            else
            {
                ModelState.AddModelError("Name", "No such user!");
            }
            return !ModelState.IsValid ? View(user) : View("SuccessSignIn", storedUser);
        }

    }
}
