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
    [Route("Post")]
    public class PostController : Controller
    {
        private readonly IRepository<Post> _postRepo;
        private readonly IMapper _mapper;

        public PostController(IRepository<Post> postRepo, IMapper mapper)
        {
            _postRepo = postRepo;
            _mapper = mapper;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Index(Guid postId)
        {
            return View(_mapper.Map<PostViewModel>(await _postRepo.GetByIdIncludes(p => p.Id.Equals(postId), nameof(Post.TimeSheets))));
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreatePostViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            //TODO: MUST move mapping somewhere else

            Post post = new Post
            {
                Title = model.Title,
                Description = model.Description,
                Email = model.Email,
                Phone = model.Phone,
                Address = model.Address,
                UserId = Guid.Parse(User.Identity.Name)
            };

            List<TimeSheet> timeSheets = new List<TimeSheet>();
            for (int i = 1; i <= 7; ++i)
            {
                if (model.TimeSheets[i].Selected)
                {
                    timeSheets.Add(new TimeSheet
                    {
                        DayOfWeek = i,
                        OpensAt = (int)TimeSpan.Parse(model.TimeSheets[i].OpensAt).TotalSeconds,
                        ClosesAt = (int)TimeSpan.Parse(model.TimeSheets[i].ClosesAt).TotalSeconds,
                        Post = post
                    });
                }
            }
            post.TimeSheets = timeSheets;

            await _postRepo.InsertAsync(post);
            await _postRepo.SaveAsync();

            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
        }

        [HttpPost("Delete/{postId}")]
        public async Task<IActionResult> Delete(Guid postId)
        {
            _postRepo.Delete(await _postRepo.GetByIdAsync(postId));
            await _postRepo.SaveAsync();

            //TODO: MUST return a view with the success message
            return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
        }
    }
}
