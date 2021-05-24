using System;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Dottle.Persistence.Repository;
using Dottle.Domain.Entities;
using Dottle.Web.Models;
using AutoMapper;
using System.Collections.Generic;

namespace Dottle.Web.Controllers
{
    public class PostController : Controller
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IMapper _mapper;

        public PostController(IRepository<Post> postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            /*List <TimeSheet> list = new List<TimeSheet>();
            int i = 1;
            foreach(var a in model.TimeSheets)
            {
                if(a.Selected)
                {
                    TimeSheet = new TimeSheet
                    {
                        DayOfWeek = i,
                        OpensAt = (int)TimeSpan.Parse(a.OpensAt).TotalSeconds,
                        ClosesAt = (int)TimeSpan.Parse(a.ClosesAt).TotalSeconds,
                        Post = 
                    }
                }
                i++;
            }

            Post post = new Post
            {
                Title = model.Title,
                Description = model.Description,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                User 
            };*/

            return Json(model);

            /*Post post = _mapper.Map<Post>(model);
            post.User.Id = new Guid(User.Identity.Name);

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));*/
        }
    }
}
