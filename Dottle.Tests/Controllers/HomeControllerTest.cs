using System;
using System.Collections.Generic;
using System.Text;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Dottle.Persistence.Repository;
using Dottle.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;

namespace Dottle.Tests.Controllers
{
    public class HomeControllerTest : BaseControllerTest
    {
        public HomeControllerTest() : base(
            new DbContextOptionsBuilder<DatabaseContext>()
            .UseInMemoryDatabase("DottleDB")
            .Options) 
        {
        }

        [Fact]
        public void Index_ShouldReturnViewResultWithPostList()
        {
            using var context = new DatabaseContext(ContextOptions);
            var repository = new Repository<Post>(context);
            var confMock = new Mock<IConfiguration>();
            var homeController = new HomeController(repository, confMock.Object);

            var result = homeController.Index().GetAwaiter().GetResult() as ViewResult;


        }
    }
}
