using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class AdminTests
    {
        [TestMethod]
        public void Admin_Constructor_DefaultValues()
        {
            // Arrange & Act
            var admin = new Admin();

            // Assert
            Assert.AreEqual(0, admin.Id);
            Assert.IsNull(admin.Name);
            Assert.AreEqual(0, admin.UserId);
        }

        [TestMethod]
        public void Admin_SetProperties_ValuesSetCorrectly()
        {
            // Arrange
            var admin = new Admin();

            // Act
            admin.Id = 1;
            admin.Name = "John";
            admin.UserId = 123;

            // Assert
            Assert.AreEqual(1, admin.Id);
            Assert.AreEqual("John", admin.Name);
            Assert.AreEqual(123, admin.UserId);
        }
    }
}

