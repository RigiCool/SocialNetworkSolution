using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class OlympicTournamentViewModelTests
    {
        [TestMethod]
        public void OlympicTournamentViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var viewModel = new OlympicTournamentViewModel
            {
                Id = 1,
                Name = "Olympic Tournament",
                SponsorId = 1,
                Prize = 10000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14),
                OlympicTeamLists = new List<OlympicTeamList>(),
                PlayOffTeams = new List<Team>(),
                Matches = new List<Match>(),
                NewTeam = new Team(),
                DeleteTeamId = 10,
                Stage = 1,
                Status = true
            };

            // Assert
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Olympic Tournament", viewModel.Name);
            Assert.AreEqual(1, viewModel.SponsorId);
            Assert.AreEqual(10000, viewModel.Prize);
            Assert.AreEqual(DateTime.Today, viewModel.StartDate.Date);
            Assert.AreEqual(DateTime.Today.AddDays(14), viewModel.EndDate.Date);
            Assert.IsNotNull(viewModel.OlympicTeamLists);
            Assert.IsNotNull(viewModel.PlayOffTeams);
            Assert.IsNotNull(viewModel.Matches);
            Assert.IsNotNull(viewModel.NewTeam);
            Assert.AreEqual(10, viewModel.DeleteTeamId);
            Assert.AreEqual(1, viewModel.Stage);
            Assert.AreEqual(true, viewModel.Status);
        }
    }
}
