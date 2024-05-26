using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class PostTests
    {
        [TestMethod]
        public void Post_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedTitle = "Title";
            string expectedText = "Content";
            DateTime expectedDate = DateTime.Now;
            byte[] expectedImageData = new byte[] { 0x01, 0x02, 0x03 };
            long expectedUserId = 10;

            // Act
            var post = new Post
            {
                Id = expectedId,
                Title = expectedTitle,
                Text = expectedText,
                Date = expectedDate,
                ImageData = expectedImageData,
                UserId = expectedUserId
            };

            // Assert
            Assert.AreEqual(expectedId, post.Id);
            Assert.AreEqual(expectedTitle, post.Title);
            Assert.AreEqual(expectedText, post.Text);
            Assert.AreEqual(expectedDate, post.Date);
            Assert.AreEqual(expectedImageData, post.ImageData);
            Assert.AreEqual(expectedUserId, post.UserId);
        }
    }
}
