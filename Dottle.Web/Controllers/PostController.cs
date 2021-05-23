using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Dottle.Web.Models;
using System.Text.RegularExpressions;
using System.Linq;
using Dottle.Web.Helpers;
using Dottle.ViewModels;
using Dottle.Persistence.Repository;
using Dottle.Domain.Entities;
using Dottle.Web.Models.Post;
using AutoMapper;

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
        public IActionResult Create()
        {
            return View(DayHelper.Days);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            Post post = _mapper.Map<Post>(model);
            post.User.Id = new Guid(User.Identity.Name);
        }
    }
}
