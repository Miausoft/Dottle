using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Persistence.Repository;
using Dottle.Web.Controllers;
using Moq;
using Xunit;
using Dottle.Web.Models;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Security.Principal;

namespace Dottle.Tests.Controllers
{
    public class PostControllerTest
    {
        private readonly Mock<IRepository<Post>> _repository;
        private readonly Mock<IMapper> _mapper;
        public PostControllerTest()
        {
            _repository = new Mock<IRepository<Post>>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Index_ShouldReturnEmptyViewResult()
        {
            _repository
                .Setup(x => x.GetByIdIncludes(It.IsAny<Expression<Func<Post, bool>>>(), It.IsAny<string>()))
                .Returns(Task.FromResult(new Post { }));
            _mapper
                .Setup(x => x.Map<PostViewModel>(It.IsAny<PostViewModel>()))
                .Returns(new PostViewModel { });
            
            var controller = new PostController(_repository.Object, _mapper.Object);

            var result = controller.Index(It.IsAny<Guid>()).GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnViewResult()
        {
            var controller = new PostController(_repository.Object, _mapper.Object);

            var result = controller.Create() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnJson_WithCreatePostViewModel()
        {
            var user = new ClaimsPrincipal(
                new ClaimsIdentity(new Claim[] {
                    new Claim(ClaimTypes.NameIdentifier, "Test"),
                    new Claim(ClaimTypes.Name, Guid.NewGuid().ToString())
                }, "TestAuthentication"));

            var controller = new PostController(_repository.Object, _mapper.Object);
            controller.ControllerContext = new ControllerContext();
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = user };

            var cpvm = new CreatePostViewModel
            {
                Title = "T1",
                Description = "D1",
                Email = "email@gmail.com",
                Phone = "+3706212211",
                Address = "A. A. 18-11",
                TimeSheets = new List<TimeSheetViewModel>() 
                {
                    new TimeSheetViewModel 
                    { 
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(), 
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    },
                    new TimeSheetViewModel
                    {
                        OpensAt = new TimeSpan(1,1,1,1,1).ToString(),
                        ClosesAt = new TimeSpan(2,1,1,1,1).ToString(),
                        Selected = true
                    }
                }
            };

            var result = controller.Create(cpvm).GetAwaiter().GetResult() as RedirectToActionResult;

            Assert.NotNull(result);
        }
    }
}
