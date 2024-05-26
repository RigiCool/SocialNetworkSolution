using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class SearchViewModelTests
    {
        [TestMethod]
        public void SearchViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var users = new List<User>
            {
                new User { Id = 1, Nickname = "User1" },
                new User { Id = 2, Nickname = "User2" }
            };

            var teams = new List<Team>
            {
                new Team { Id = 1, TeamName = "TeamA" },
                new Team { Id = 2, TeamName = "TeamB" }
            };

            var tournamentsOlympic = new List<TournamentOlympic>
            {
                new TournamentOlympic { Id = 1, Name = "TournamentOlympic1" },
                new TournamentOlympic { Id = 2, Name = "TournamentOlympic2" }
            };

            var tournamentsList = new List<TournamentList>
            {
                new TournamentList { Id = 1, Name = "TournamentList1" },
                new TournamentList { Id = 2, Name = "TournamentList2" }
            };

            var viewModel = new SearchViewModel
            {
                Users = users,
                Teams = teams,
                TournamentsOlympic = tournamentsOlympic,
                TournamentsList = tournamentsList
            };

            // Assert
            CollectionAssert.AreEqual(users, viewModel.Users.ToList());
            CollectionAssert.AreEqual(teams, viewModel.Teams.ToList());
            CollectionAssert.AreEqual(tournamentsOlympic, viewModel.TournamentsOlympic.ToList());
            CollectionAssert.AreEqual(tournamentsList, viewModel.TournamentsList.ToList());
        }
    }
}
