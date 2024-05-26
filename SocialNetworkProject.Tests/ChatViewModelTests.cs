using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class ChatViewModelTests
    {
        [TestMethod]
        public void ChatViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var chat = new Chat();
            var newMessage = new Message();
            var deleteMessage = new Message();

            // Act
            var viewModel = new ChatViewModel
            {
                Chat = chat,
                NewMessage = newMessage,
                DeleteMessage = deleteMessage
            };

            // Assert
            Assert.AreEqual(chat, viewModel.Chat);
            Assert.AreEqual(newMessage, viewModel.NewMessage);
            Assert.AreEqual(deleteMessage, viewModel.DeleteMessage);
        }
    }
}
