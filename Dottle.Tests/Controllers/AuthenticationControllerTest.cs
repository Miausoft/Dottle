using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Dottle.Domain.Entities;
using Dottle.Persistence;
using Dottle.Persistence.Repository;
using Dottle.Services.Security.Password;
using Dottle.Web.Controllers;
using Dottle.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;

namespace Dottle.Tests.Controllers
{
    public class AuthenticationControllerTest
    {
        private readonly Mock<IRepository<User>> _repository;
        private readonly Mock<IPasswordHasher> _hasher;
        private readonly Mock<IMapper> _mapper;

        public AuthenticationControllerTest()
        {
            _repository = new Mock<IRepository<User>>();
            _hasher = new Mock<IPasswordHasher>();
            _mapper = new Mock<IMapper>();
        }

        [Fact]
        public void Login_ShouldReturnViewResult_WithInvalidLoginViewModel()
        {
            var controller = new AuthenticationController(_repository.Object, _hasher.Object, _mapper.Object);
            controller.ModelState.AddModelError("error", "error");

            var result = controller
                .Login(It.IsAny<LoginViewModel>())
                .GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Login_ShouldReturnViewResult_WithNonExistingUserLoginData()
        {
            _repository
                .Setup(x => x.SearchFor(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>() { }.AsQueryable());

            var controller = new AuthenticationController(_repository.Object, _hasher.Object, _mapper.Object);

            var result = controller
                .Login(It.IsAny<LoginViewModel>())
                .GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
        }

        [Fact]
        public void Login_ShouldReturnViewResult_WithInvalidUsersPassword()
        {
            List<User> returnUsers = new List<User>()
            {
                new User
                {
                    Email = "user@gmail.com",
                    Password = "password1"
                }
            };
            _repository
                .Setup(x => x.SearchFor(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(returnUsers.AsQueryable());
            _hasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(false);

            var controller = new AuthenticationController(_repository.Object, _hasher.Object, _mapper.Object);

            var result = controller
                .Login(new Web.Models.LoginViewModel { Email = "user@gmail.com", Password = "X!1411ASA!!-" })
                .GetAwaiter().GetResult() as ViewResult;

            Assert.NotNull(result);
        }

        /*[Fact]
        public void Login_ShouldReturnRedirectToActionResult_WithValidLoginData()
        {
            List<User> returnUsers = new List<User>()
            {
                new User
                {
                    Id = Guid.NewGuid(),
                    Email = "user@gmail.com",
                    Password = "password1",
                    FirstName = "FirstName",
                    LastName = "LastName"
                }
            };

            _hasher.Setup(x => x.Verify(It.IsAny<string>(), It.IsAny<string>())).Returns(true);
            _repository
                .Setup(x => x.SearchFor(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(returnUsers.AsQueryable());

            var authServiceMock = new Mock<IAuthenticationService>();
            authServiceMock
                .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
                .Returns(Task.FromResult((object)null));

            var serviceProviderMock = new Mock<IServiceProvider>();
            serviceProviderMock
                .Setup(_ => _.GetService(typeof(IAuthenticationService)))
                .Returns(authServiceMock.Object);

            var controller = new AuthenticationController(_repository.Object, _hasher.Object, _mapper.Object)
            {
                ControllerContext = new ControllerContext
                {
                    HttpContext = new DefaultHttpContext
                    {
                        RequestServices = serviceProviderMock.Object
                    }
                }
            };
           
            var result = controller.Login(new LoginViewModel { Email = "user@gmail.com", Password = "password1" })
                .GetAwaiter().GetResult() as RedirectToActionResult;

            Assert.NotNull(result);
        }*/
    }
}
