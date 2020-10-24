using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Support_Your_Locals.Controllers;
using Support_Your_Locals.Models;
using Xunit;

namespace Support_Your_Locals.Tests
{
    public class HomeControllerTests
    {

        [Fact]
        public void IndexActionModelIsComplete()
        {
            //Arrange
            var controller = new HomeController(null);
            UserRegisterModel[] users = new UserRegisterModel[]
            {
                new UserRegisterModel{Name="Evaldas", Email="kitas"}, new UserRegisterModel {Name="Kazys", Email = "Vienas"}
            };

            //Act
            var model = (controller.Index() as ViewResult)?.ViewData.Model as IEnumerable<UserRegisterModel>;

            //Assert
            Assert.Equal(users, model, Comparer.Get<UserRegisterModel>((u1, u2) => u1.Name == u2.Name && u1.Email == u2.Email));
        }
    }
}
