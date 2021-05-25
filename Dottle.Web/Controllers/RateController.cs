using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Persistence.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dottle.Web.Controllers
{
    [Route("Rate")]
    public class RateController : Controller
    {
        private readonly IRepository<Rate> _rateRepo;
        private readonly IRepository<Post> _postRepo;
        private readonly IMapper _mapper;

        public RateController(IRepository<Rate> rateRepo, IRepository<Post> postRepo, IMapper mapper)
        {
            _rateRepo = rateRepo;
            _postRepo = postRepo;
            _mapper = mapper;
        }

        [HttpGet("{postId}")]
        public async Task<IActionResult> Get(Guid postId)
        {
            Post post = await _postRepo.GetByIdIncludes(p => p.Id.Equals(postId), nameof(Post.Rates));

            if (post == null)
            {
                return NotFound();
            }

            if (HttpContext.Request.Headers["Content-Type"].Equals("application/json"))
            {
                //TODO: do not know how to parse json using js, so final rate is returned
                return Ok(post.Rates.Select(r => r.Value).DefaultIfEmpty().Average());
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
            }
        }

        [HttpPost("Create/{userId}/{postId}/{rate}")]
        public async Task<IActionResult> Create(Guid userId, Guid postId, int rate)
        {
            try
            {
                await _rateRepo.InsertAsync(new Rate { Value = rate, UserId = userId, PostId = postId });
                await _rateRepo.SaveAsync();
            }
            catch(Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            if (HttpContext.Request.Headers["Content-Type"].Equals("application/json"))
            {
                return Ok();
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace(nameof(Controller), ""));
            }
        }
    }
}
