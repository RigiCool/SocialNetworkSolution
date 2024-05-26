using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class PlayersListViewModelTests
    {
        [TestMethod]
        public void PlayersListViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var players = new List<Player>
            {
                new Player { Id = 1, Name = "Player 1" },
                new Player { Id = 2, Name = "Player 2" },
                new Player { Id = 3, Name = "Player 3" }
            };

            var viewModel = new PlayersListViewModel
            {
                AllPlayers = players,
                CurrentTeam = "Team A"
            };

            // Assert
            CollectionAssert.AreEqual(players, viewModel.AllPlayers.ToList());
            Assert.AreEqual("Team A", viewModel.CurrentTeam);
        }
    }
}
