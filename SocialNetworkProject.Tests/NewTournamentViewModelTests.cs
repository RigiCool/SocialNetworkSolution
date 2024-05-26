using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.ViewModels;
using System;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class NewTournamentViewModelTests
    {
        [TestMethod]
        public void NewTournamentViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var viewModel = new NewTournamentViewModel
            {
                TournamentName = "Tournament",
                TournamentType = "Type1",
                SponsorNickName = "Sponsor1",
                Prize = 1000,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            };

            // Assert
            Assert.AreEqual("Tournament", viewModel.TournamentName);
            Assert.AreEqual("Type1", viewModel.TournamentType);
            Assert.AreEqual("Sponsor1", viewModel.SponsorNickName);
            Assert.AreEqual(1000, viewModel.Prize);
            Assert.AreEqual(DateTime.Today, viewModel.StartDate.Date);
            Assert.AreEqual(DateTime.Today.AddDays(7), viewModel.EndDate.Date);
        }
    }
}
