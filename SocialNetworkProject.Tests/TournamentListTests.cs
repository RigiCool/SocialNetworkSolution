using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class TournamentListTests
    {
        [TestMethod]
        public void TournamentList_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "Tournament A";
            long expectedSponsorId = 100;
            long expectedPrize = 5000;
            DateTime expectedStartDate = new DateTime(2024, 5, 1);
            DateTime expectedEndDate = new DateTime(2024, 5, 10);
            int expectedType = 1;
            bool expectedComplete = false;

            // Act
            var tournament = new TournamentList
            {
                Id = expectedId,
                Name = expectedName,
                SponsorId = expectedSponsorId,
                Prize = expectedPrize,
                StartDate = expectedStartDate,
                EndDate = expectedEndDate,
                Type = expectedType,
                Complete = expectedComplete
            };

            // Assert
            Assert.AreEqual(expectedId, tournament.Id);
            Assert.AreEqual(expectedName, tournament.Name);
            Assert.AreEqual(expectedSponsorId, tournament.SponsorId);
            Assert.AreEqual(expectedPrize, tournament.Prize);
            Assert.AreEqual(expectedStartDate, tournament.StartDate);
            Assert.AreEqual(expectedEndDate, tournament.EndDate);
            Assert.AreEqual(expectedType, tournament.Type);
            Assert.AreEqual(expectedComplete, tournament.Complete);
        }
    }
}
