using Dottle.Models;
using Xunit;

namespace Dottle.Tests
{
    public class UserTests
    {

        [Fact]
        public void CanChangUserName()
        {
            //Arrange
            var u = new UserRegisterModel() { Name = "Test", Email = "another" };

            //Act
            u.Name = "New Name";

            //Assert
            Assert.Equal("New Name", u.Name);
        }

        [Fact]
        public void CanChangeUserEmail()
        {
            //Arrange
            var u = new UserRegisterModel() { Name = "Test", Email = "another" };

            //Act
            u.Email = "first";

            //Assert
            Assert.Equal("first", u.Email);
        }

    }
}
