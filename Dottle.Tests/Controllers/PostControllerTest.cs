using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Dottle.Persistence.Repository;
using Dottle.Web.Controllers;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using Dottle.Web.Models;

namespace Dottle.Tests.Controllers
{
    public class PostControllerTest : BaseControllerTest
    {
        public PostControllerTest() : base(
            new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("DottleDB")
            .Options)
        {
        }

        private PostController MockController(DatabaseContext context)
        {
            var repository = new Repository<Post>(context);
            var iMapper = new Mock<IMapper>();

            return new PostController(repository, iMapper.Object);
        }

        [Fact]
        public void Index_ShouldReturnEmptyViewResult()
        {
            using var context = new DatabaseContext(ContextOptions);
            var postController = MockController(context);

            var result = postController.Index() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnViewResult()
        {
            using var context = new DatabaseContext(ContextOptions);
            var postController = MockController(context);

            var result = postController.Create() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Create_ShouldReturnJson_WithCreatePostViewModel()
        {
            using var context = new DatabaseContext(ContextOptions);
            var postController = MockController(context);
            var cpvm = new CreatePostViewModel
            {
                Title = "T1",
                Description = "D1",
                Email = "email@gmail.com",
                Phone = "+3706212211",
                Address = "A. A. 18-11",
                TimeSheets = new List<TimeSheetViewModel>() { }
            };

            var result = postController.Create(cpvm) as JsonResult;

            Assert.NotNull(result);
        }
    }
}
