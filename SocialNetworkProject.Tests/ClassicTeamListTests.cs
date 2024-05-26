using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class ClassicTeamListTests
    {
        [TestMethod]
        public void ClassicTeamList_SetProperties_ValuesSetCorrectly()
        {
            // Arrange
            var classicTeamList = new ClassicTeamList();
            var team = new Team { Id = 1, TeamName = "Team A" };

            // Act
            classicTeamList.id = 123;
            classicTeamList.Team = team;
            classicTeamList.Score = 100;
            classicTeamList.TournamentId = 456;
            classicTeamList.TournamentName = "Tournament X";

            // Assert
            Assert.AreEqual(123, classicTeamList.id);
            Assert.AreSame(team, classicTeamList.Team);
            Assert.AreEqual(100, classicTeamList.Score);
            Assert.AreEqual(456, classicTeamList.TournamentId);
            Assert.AreEqual("Tournament X", classicTeamList.TournamentName);
        }
    }
}
