using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class TeamTests
    {
        [TestMethod]
        public void Team_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedTeamName = "Team A";
            string expectedDesc = "This is Team A";
            string expectedTeamSite = "https://example.com";
            byte[] expectedTeamLogo = new byte[] { 0x00, 0x01, 0x02 };
            List<Player> expectedPlayers = new List<Player>();
            long expectedTeamCoach = 100;
            int expectedPlayerCount = 5;

            // Act
            var team = new Team
            {
                Id = expectedId,
                TeamName = expectedTeamName,
                Desc = expectedDesc,
                TeamSite = expectedTeamSite,
                TeamLogo = expectedTeamLogo,
                Players = expectedPlayers,
                TeamCoach = expectedTeamCoach,
                PlayerCount = expectedPlayerCount
            };

            // Assert
            Assert.AreEqual(expectedId, team.Id);
            Assert.AreEqual(expectedTeamName, team.TeamName);
            Assert.AreEqual(expectedDesc, team.Desc);
            Assert.AreEqual(expectedTeamSite, team.TeamSite);
            Assert.AreEqual(expectedTeamLogo, team.TeamLogo);
            Assert.AreEqual(expectedPlayers, team.Players);
            Assert.AreEqual(expectedTeamCoach, team.TeamCoach);
            Assert.AreEqual(expectedPlayerCount, team.PlayerCount);
        }
    }
}
