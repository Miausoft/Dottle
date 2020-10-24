using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                await db.Users.AddAsync(user);
                await db.SaveChangesAsync();
                var allUsers = await db.Users.ToListAsync();
                //return View("Thanks", allUsers);
            }
            return View();
        }

        [HttpGet]
        public ViewResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<ViewResult> SignIn(UserLoginModel user)
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
