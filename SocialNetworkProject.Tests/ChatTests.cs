using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class ChatTests
    {
        [TestMethod]
        public void Chat_Constructor_DefaultValues()
        {
            // Arrange & Act
            var chat = new Chat();
            chat.Messages = new List <Message>();

            // Assert
            Assert.AreEqual(0, chat.id);
            Assert.IsNull(chat.User1);
            Assert.IsNull(chat.User2);
            Assert.IsNotNull(chat.Messages);
            Assert.IsInstanceOfType(chat.Messages, typeof(List<Message>));
            Assert.AreEqual(0, chat.Messages.Count);
        }

        [TestMethod]
        public void Chat_SetProperties_ValuesSetCorrectly()
        {
            // Arrange
            var chat = new Chat();
            var user1 = new User { Id = 1, Nickname = "user1" };
            var user2 = new User { Id = 2, Nickname = "user2" };
            var messages = new List<Message> { new Message(), new Message() };

            // Act
            chat.id = 123;
            chat.User1 = user1;
            chat.User2 = user2;
            chat.Messages = messages;

            // Assert
            Assert.AreEqual(123, chat.id);
            Assert.AreSame(user1, chat.User1);
            Assert.AreSame(user2, chat.User2);
            Assert.AreSame(messages, chat.Messages);
        }
    }
}

