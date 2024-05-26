using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class TeamViewModelTests
    {
        [TestMethod]
        public void TeamViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Id = 1, Name = "Player1" },
                new Player { Id = 2, Name = "Player2" }
            };

            var teamCoach = new TeamCoach
            {
                Id = 1,
                Name = "Coach1",
                // Add more properties as needed
            };

            var viewModel = new TeamViewModel
            {
                Id = 1,
                TeamName = "Team1",
                Desc = "Description",
                TeamSite = "http://example.com",
                TeamLogo = new byte[0],
                Players = players,
                PlayerCount = players.Count,
                TeamCoach = teamCoach,
                TeamCoachImg = new byte[0],
                DeletePlayerId = 2
            };

            // Assert
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Team1", viewModel.TeamName);
            Assert.AreEqual("Description", viewModel.Desc);
            Assert.AreEqual("http://example.com", viewModel.TeamSite);
            CollectionAssert.AreEqual(players, viewModel.Players);
            Assert.AreEqual(players.Count, viewModel.PlayerCount);
            Assert.AreEqual(teamCoach, viewModel.TeamCoach);
            Assert.AreEqual(2, viewModel.DeletePlayerId);
        }
    }
}
