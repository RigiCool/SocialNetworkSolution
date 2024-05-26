using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class SubscriptionTests
    {
        [TestMethod]
        public void Subscription_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            long expectedOwnerId = 100;
            long expectedUserId = 200;

            // Act
            var subscription = new Subscription
            {
                Id = expectedId,
                OwnerId = expectedOwnerId,
                UserId = expectedUserId
            };

            // Assert
            Assert.AreEqual(expectedId, subscription.Id);
            Assert.AreEqual(expectedOwnerId, subscription.OwnerId);
            Assert.AreEqual(expectedUserId, subscription.UserId);
        }
    }
}
