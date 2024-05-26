using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class ProfileViewModelTests
    {
        [TestMethod]
        public void ProfileViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var imageData = new byte[] { 0x00, 0x01, 0x02 };
            var role = new Admin(); // Assuming UserRole is a class implementing IUserRole
            var posts = new List<Post>(); // Assuming Post is a class
            var errors = new List<string> { "Error 1", "Error 2" };

            // Act
            var viewModel = new ProfileViewModel<IUserRole>
            {
                Id = 1,
                Nickname = "user",
                Name = "John",
                Surname = "Doe",
                Age = "30",
                Phone = "1234567890",
                Email = "test@example.com",
                ImageData = imageData,
                RoleId = 1,
                RoleName = "UserRole",
                Role = role,
                Posts = posts,
                InSubscriptions = true,
                Description = "Test description",
                Errors = errors
            };

            // Assert
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("user", viewModel.Nickname);
            Assert.AreEqual("John", viewModel.Name);
            Assert.AreEqual("Doe", viewModel.Surname);
            Assert.AreEqual("30", viewModel.Age);
            Assert.AreEqual("1234567890", viewModel.Phone);
            Assert.AreEqual("test@example.com", viewModel.Email);
            Assert.AreEqual(imageData, viewModel.ImageData);
            Assert.AreEqual(1, viewModel.RoleId);
            Assert.AreEqual("UserRole", viewModel.RoleName);
            Assert.AreEqual(role, viewModel.Role);
            Assert.AreEqual(posts, viewModel.Posts);
            Assert.IsTrue(viewModel.InSubscriptions);
            Assert.AreEqual("Test description", viewModel.Description);
            CollectionAssert.AreEqual(errors, viewModel.Errors);
        }
    }
}
