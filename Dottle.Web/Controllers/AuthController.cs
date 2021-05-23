/*using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using Dottle.Helpers;
using Dottle.Models;
using Dottle.ViewModels;
using Microsoft.AspNetCore.Http;

namespace Dottle.Controllers
{
    public class AuthController : Controller
    {

        public ServiceDbContext db;
        private bool AllowedToAuthenticate = true;
        private Authenticator authenticator;
        
        public AuthController(ServiceDbContext db)
        {
            this.db = db;
            this.authenticator = new Authenticator(5);
            this.authenticator.ThresholdReached += s_AttemptsReached;
            this.AllowedToAuthenticate = true;
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
            int? attempts = HttpContext.Session.GetInt32("s_attempts");
            if (attempts.HasValue)
                authenticator.total = attempts.Value;
            else
                authenticator.total = 0;
            var storedUser = await db.Users.FindAsync(user.Name);
            if (storedUser != null)
            {
                if (authenticator.Authenticate(user.Password, storedUser.PasswordHash, storedUser.PasswordSalt))
                {
                    if (AllowedToAuthenticate)
                    {
                        HttpContext.Session.SetString("User", storedUser.Name);
                        Auditor a = new Auditor();
                        a.OnAudit += new Auditor.SignInHandler(delegate(object a, AuditArgs b)
                        {
                            Console.WriteLine(b.Message);
                        });
                        a.Message = storedUser.Name;
                        return View("SuccessSignIn", storedUser);
                    }

                }
                ModelState.AddModelError("Password", "Invalid password!");
            }
            else
            {
                ModelState.AddModelError("Name", "No such user!");
            }
            if (!AllowedToAuthenticate) return View("Disabled");
            HttpContext.Session.SetInt32("s_attempts", authenticator.total);
            return !ModelState.IsValid ? View(user) : View("SuccessSignIn", storedUser);
        }

        [HttpGet]
        public ViewResult SignOut()
        {
            HttpContext.Session.Clear();
            return View("SignOut");
        }

        [HttpGet]
        public async Task<ViewResult> Edit()
        {
            string userName = HttpContext.Session.GetString("User");
            if (string.IsNullOrEmpty((userName)))
            {
                return View("SessionFailure");
            }
            var currUser = await db.Users.FindAsync(userName);
            var uEditModel = new UserEditModel() {Username = currUser.Name};
            return View("Edit", uEditModel);
        }

        [HttpPost]
        public async Task<ViewResult> Update(UserEditModel newUser)
        {
            if (!newUser.Password.Equals(newUser.PasswordConfirm)) return View("UpdateFailure");
            var storedUser = await db.Users.FindAsync(newUser.Username);
            string newSalt = PasswordManager.CreateSalt();
            storedUser.PasswordSalt = newSalt;
            storedUser.PasswordHash = PasswordManager.HashPassword(newUser.Password, newSalt);
            await db.SaveChangesAsync();
            return View("UpdateSuccess");
        }

        private void s_AttemptsReached(object sender, EventArgs e)
        {
            Console.WriteLine("User has reached maximum sign in attempts");
            this.AllowedToAuthenticate = false;
        }

       *//* public async Task<IHttpActionResult> changePassword(UserRegisterModel usermodel)
        {
            var storedUser = await db.Users.FindAsync(usermodel.Name);
            if (storedUser == null)
            {
                return (IHttpActionResult)NotFound();
            }
            string salt = PasswordManager.CreateSalt();
            storedUser.PasswordHash = PasswordManager.HashPassword(usermodel.Password, salt);
            storedUser.PasswordSalt = salt;
            await db.SaveChangesAsync();
            //var result = await db.Users.UpdateAsync(storedUser);
            /*if (!result.Succeeded)
            {
                //throw exception......
            }
            return (IHttpActionResult)Ok();
        }

        public async Task<IHttpActionResult> changeUsername(UserRegisterModel usermodel)
        {
            var storedUser = await db.Users.FindAsync(usermodel.Name);
            if (storedUser == null)
            {
                return (IHttpActionResult)NotFound();
            }
            storedUser.Name = usermodel.Name;
            await db.SaveChangesAsync();
            return (IHttpActionResult)Ok();
        }
    *//*

    }
}
*/
