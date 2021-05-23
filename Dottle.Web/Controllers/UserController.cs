using System.Threading.Tasks;
using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Persistence.Repository;
using Dottle.Services.Security.Password;
using Dottle.Web.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace Dottle.Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<User> _userRepo;
        private readonly IPasswordHasher _hasher;
        private readonly IMapper _mapper;

        public UserController(IRepository<User> userRepo, IPasswordHasher hasher, IMapper mapper)
        {
            _userRepo = userRepo;
            _hasher = hasher;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            User user = _mapper.Map<User>(model);
            user.Password = _hasher.Hash(user.Password);

            await _userRepo.InsertAsync(user);
            await _userRepo.SaveAsync();

            return View("Index");
        }
    }
}
