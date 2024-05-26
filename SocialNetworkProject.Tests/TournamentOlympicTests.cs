using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class TournamentOlympicTests
    {
        [TestMethod]
        public void TournamentOlympic_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "Olympic Tournament";
            long expectedSponsorId = 100;
            long expectedPrize = 10000;
            DateTime expectedStartDate = new DateTime(2024, 6, 1);
            DateTime expectedEndDate = new DateTime(2024, 6, 10);
            List<Team> expectedPlayOffTeams = new List<Team> { new Team { Id = 1, TeamName = "Team A" }, new Team { Id = 2, TeamName = "Team B" } };
            int expectedStage = 1;
            bool expectedStatus = true;

            // Act
            var tournament = new TournamentOlympic
            {
                Id = expectedId,
                Name = expectedName,
                SponsorId = expectedSponsorId,
                Prize = expectedPrize,
                StartDate = expectedStartDate,
                EndDate = expectedEndDate,
                PlayOffTeams = expectedPlayOffTeams,
                Stage = expectedStage,
                Status = expectedStatus
            };

            // Assert
            Assert.AreEqual(expectedId, tournament.Id);
            Assert.AreEqual(expectedName, tournament.Name);
            Assert.AreEqual(expectedSponsorId, tournament.SponsorId);
            Assert.AreEqual(expectedPrize, tournament.Prize);
            Assert.AreEqual(expectedStartDate, tournament.StartDate);
            Assert.AreEqual(expectedEndDate, tournament.EndDate);
            CollectionAssert.AreEqual(expectedPlayOffTeams, tournament.PlayOffTeams);
            Assert.AreEqual(expectedStage, tournament.Stage);
            Assert.AreEqual(expectedStatus, tournament.Status);
        }
    }
}
