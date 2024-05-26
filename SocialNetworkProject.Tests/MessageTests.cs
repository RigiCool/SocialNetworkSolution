using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class MessageTests
    {
        [TestMethod]
        public void Message_Properties_SetAndGetCorrectly()
        {
            // Arrange
            var sendTime = DateTime.Now;

            // Act
            var message = new Message
            {
                Id = 1,
                ChatId = 100,
                Content = "Hello, world!",
                SenderNickName = "Sender1",
                SendTime = sendTime
            };

            // Assert
            Assert.AreEqual(1, message.Id);
            Assert.AreEqual(100, message.ChatId);
            Assert.AreEqual("Hello, world!", message.Content);
            Assert.AreEqual("Sender1", message.SenderNickName);
            Assert.AreEqual(sendTime, message.SendTime);
        }
    }
}
