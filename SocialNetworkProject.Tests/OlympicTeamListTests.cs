using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class OlympicTeamListTests
    {
        [TestMethod]
        public void OlympicTeamList_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            var expectedTeam = new Team();
            int expectedScore = 100;
            long expectedTournamentId = 10;
            string expectedTournamentName = "Olympics";

            // Act
            var olympicTeamList = new OlympicTeamList
            {
                id = expectedId,
                Team = expectedTeam,
                Score = expectedScore,
                TournamentId = expectedTournamentId,
                TournamentName = expectedTournamentName
            };

            // Assert
            Assert.AreEqual(expectedId, olympicTeamList.id);
            Assert.AreEqual(expectedTeam, olympicTeamList.Team);
            Assert.AreEqual(expectedScore, olympicTeamList.Score);
            Assert.AreEqual(expectedTournamentId, olympicTeamList.TournamentId);
            Assert.AreEqual(expectedTournamentName, olympicTeamList.TournamentName);
        }
    }
}
