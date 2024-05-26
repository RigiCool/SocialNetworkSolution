using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class RoleTests
    {
        [TestMethod]
        public void Role_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "Admin";
            string expectedYouTubeLink = "https://www.youtube.com";
            string expectedTwitchLink = "https://www.twitch.tv";
            string expectedFacebook = "https://www.facebook.com";
            string expectedInstagram = "https://www.instagram.com";
            var users = new List<User>();

            // Act
            var role = new Role
            {
                Id = expectedId,
                Name = expectedName,
                YouTubeLink = expectedYouTubeLink,
                TwitchLink = expectedTwitchLink,
                Facebook = expectedFacebook,
                Instagram = expectedInstagram,
                Users = users
            };

            // Assert
            Assert.AreEqual(expectedId, role.Id);
            Assert.AreEqual(expectedName, role.Name);
            Assert.AreEqual(expectedYouTubeLink, role.YouTubeLink);
            Assert.AreEqual(expectedTwitchLink, role.TwitchLink);
            Assert.AreEqual(expectedFacebook, role.Facebook);
            Assert.AreEqual(expectedInstagram, role.Instagram);
            Assert.AreEqual(users, role.Users);
        }
    }
}
