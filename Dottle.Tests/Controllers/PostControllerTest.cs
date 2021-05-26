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
    public class PostControllerTest
    {
        private readonly Mock<IRepository<Post>> _repository;
        private readonly Mock<IMapper> _mapper;
        public PostControllerTest()
        {
            _repository = new Mock<IRepository<Post>>();
            _mapper = new Mock<IMapper>();
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
            var controller = new PostController(_repository.Object, _mapper.Object);

            var result = controller.Index() as ViewResult;

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
            var controller = new PostController(_repository.Object, _mapper.Object);
            var cpvm = new CreatePostViewModel
            {
                Title = "T1",
                Description = "D1",
                Email = "email@gmail.com",
                Phone = "+3706212211",
                Address = "A. A. 18-11",
                TimeSheets = new List<TimeSheetViewModel>() { }
            };

            var result = controller.Create(cpvm) as JsonResult;

            Assert.NotNull(result);
        }
    }
}
