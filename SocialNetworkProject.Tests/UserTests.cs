using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class UserTests
    {
        [TestMethod]
        public void User_Validations()
        {
            // Arrange
            var user = new User
            {
                Id = 1,
                Nickname = "user",
                Password = "password123",
                Name = "John",
                Surname = "Doe",
                Age = "30",
                Phone = "1234567890",
                Email = "test@example.com",
                ImageData = new byte[0],
                RoleId = 1
            };

            var context = new ValidationContext(user);
            var results = new System.Collections.Generic.List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(user, context, results, true);

            // Assert
            Assert.IsTrue(isValid);
        }
    }
}
