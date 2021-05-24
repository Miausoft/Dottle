using System;
using System.Collections.Generic;
using System.Text;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Dottle.Persistence.Repository;
using Dottle.Web.Controllers;
using Dottle.Web.ViewModels;
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

        private HomeController MockHomeController(DatabaseContext context)
        {
            var repository = new Repository<Post>(context);
            var confMock = new Mock<IConfiguration>();
            return new HomeController(repository, confMock.Object);
        }

        

        [Fact]
        public void Index_ShouldReturnViewResultWithPostList()
        {
            using var context = new DatabaseContext(ContextOptions);
            var homeController = MockHomeController(context);

            var result = homeController.Index().GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Post>>(result.Model);
        }

        [Fact]
        public void Settings_ShouldReturnViewResultNamedSettings()
        {
            using var context = new DatabaseContext(ContextOptions);
            var homeController = MockHomeController(context);

            var result = homeController.Settings() as ViewResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<UserSetting>(result.Model);
        }

        [Fact]
        public void UpdateSettings_ShouldReturnRedirectResult_WithValidUserSettingsData()
        {
            using var context = new DatabaseContext(ContextOptions);
            var homeController = MockHomeController(context);
            UserSetting userSetting = new UserSetting
            {
                SiteLayout = string.Empty,
            };

            var result = homeController.UpdateSettings(userSetting) as RedirectResult;

            Assert.NotNull(result);
            Assert.Equal("/", result.Url);
        }
    }
}
