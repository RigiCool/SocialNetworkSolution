using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class UserRegistrationViewModelTests
    {
        [TestMethod]
        public void UserRegistrationViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { Id = 1, Name = "Role1" },
                new Role { Id = 2, Name = "Role2" }
            };

            var user = new User
            {
                Id = 1,
                Nickname = "User1",
                Name = "John",
                Surname = "Doe",
                // Add more properties as needed
            };

            var viewModel = new UserRegistrationViewModel
            {
                Roles = roles,
                User = user
            };

            // Assert
            CollectionAssert.AreEqual(roles, viewModel.Roles.ToList());
            Assert.AreEqual(user, viewModel.User);
        }
    }
}
