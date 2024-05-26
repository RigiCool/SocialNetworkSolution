using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class SponsorTests
    {
        [TestMethod]
        public void Sponsor_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "John";
            string expectedSurname = "Doe";
            string expectedCompany = "ABC Inc.";
            string expectedCompanySite = "https://www.abc.com";
            string expectedCompanyNumber = "+1234567890";
            bool expectedInLeaders = true;
            long expectedUserId = 123;
            string expectedDescription = "Description";

            // Act
            var sponsor = new Sponsor
            {
                Id = expectedId,
                Name = expectedName,
                Surname = expectedSurname,
                Company = expectedCompany,
                CompanySite = expectedCompanySite,
                CompanyNumber = expectedCompanyNumber,
                InLeaders = expectedInLeaders,
                UserId = expectedUserId,
                Description = expectedDescription
            };

            // Assert
            Assert.AreEqual(expectedId, sponsor.Id);
            Assert.AreEqual(expectedName, sponsor.Name);
            Assert.AreEqual(expectedSurname, sponsor.Surname);
            Assert.AreEqual(expectedCompany, sponsor.Company);
            Assert.AreEqual(expectedCompanySite, sponsor.CompanySite);
            Assert.AreEqual(expectedCompanyNumber, sponsor.CompanyNumber);
            Assert.AreEqual(expectedInLeaders, sponsor.InLeaders);
            Assert.AreEqual(expectedUserId, sponsor.UserId);
            Assert.AreEqual(expectedDescription, sponsor.Description);
        }
    }
}
