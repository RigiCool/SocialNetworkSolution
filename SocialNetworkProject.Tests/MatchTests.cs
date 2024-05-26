using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class MatchTests
    {
        [TestMethod]
        public void Match_Properties_SetAndGetCorrectly()
        {
            // Arrange
            var team1 = new Team { Id = 1, TeamName = "Team 1" };
            var team2 = new Team { Id = 2, TeamName = "Team 2" };
            var winner = new Team { Id = 1, TeamName = "Team 1" };

            // Act
            var match = new Match
            {
                id = 1,
                Team1 = team1,
                Team2 = team2,
                Team1Score = 2,
                Team2Score = 1,
                Winner = winner,
                TournamentId = 100
            };

            // Assert
            Assert.AreEqual(1, match.id);
            Assert.AreEqual(team1, match.Team1);
            Assert.AreEqual(team2, match.Team2);
            Assert.AreEqual(2, match.Team1Score);
            Assert.AreEqual(1, match.Team2Score);
            Assert.AreEqual(winner, match.Winner);
            Assert.AreEqual(100, match.TournamentId);
        }
    }
}
