using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class ListTournamentViewModelTests
    {
        [TestMethod]
        public void ListTournamentViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var classicTeamLists = new List<ClassicTeamList>();
            var newTeam = new Team();

            // Act
            var viewModel = new ListTournamentViewModel
            {
                Id = 1,
                Name = "Test Tournament",
                SponsorId = 1,
                Prize = 1000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                ClassicTeamLists = classicTeamLists,
                NewTeam = newTeam,
                DeleteTeamId = 2,
                Type = 1,
                Complete = false
            };

            // Assert
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Test Tournament", viewModel.Name);
            Assert.AreEqual(1, viewModel.SponsorId);
            Assert.AreEqual(1000, viewModel.Prize);
            Assert.AreEqual(classicTeamLists, viewModel.ClassicTeamLists);
            Assert.AreEqual(newTeam, viewModel.NewTeam);
            Assert.AreEqual(2, viewModel.DeleteTeamId);
            Assert.AreEqual(1, viewModel.Type);
            Assert.IsFalse(viewModel.Complete);
        }
    }
}
