using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Dottle.Persistence.Repository;
using Dottle.Web.Controllers;
using Dottle.Web.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using Xunit;
using System.Linq;

namespace Dottle.Tests.Controllers
{
    public class HomeControllerTest
    {
        private readonly Mock<IRepository<Post>> _repository;
        private readonly Mock<IConfiguration> _configuration;

        public HomeControllerTest()
        {
            _repository = new Mock<IRepository<Post>>();
            _configuration = new Mock<IConfiguration>();
        }

        private HomeController MockController(DatabaseContext context)
        {
            var repository = new Repository<Post>(context);
            var confMock = new Mock<IConfiguration>();
            return new HomeController(repository, confMock.Object);
        }

        /*[Fact]
        public void Index_ShouldReturnViewResultWithAllPosts()
        {
            var returns = new List<Post>
            {
            };

            var homeController = new HomeController(_repository.Object, _configuration.Object);
            _repository.Setup(x => x.GetAll()).Returns(returns.AsQueryable());

            var result = homeController.Index().GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<List<Post>>(result.Model);
        }*/

        [Fact]
        public void Settings_ShouldReturnViewResultNamedSettings()
        {
            var homeController = new HomeController(_repository.Object, _configuration.Object);

            var result = homeController.Settings() as ViewResult;

            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.IsType<UserSetting>(result.Model);
        }

        [Fact]
        public void UpdateSettings_ShouldReturnRedirectResult_WithValidUserSettingsData()
        {
            var homeController = new HomeController(_repository.Object, _configuration.Object);
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
