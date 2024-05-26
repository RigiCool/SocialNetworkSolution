using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.ViewModels;

namespace SocialNetworkProject.Tests.ViewModels
{
    [TestClass]
    public class NewTeamViewModelTests
    {
        [TestMethod]
        public void NewTeamViewModel_Properties_SetCorrectly()
        {
            // Arrange
            var viewModel = new NewTeamViewModel
            {
                Id = 1,
                TeamName = "Team",
                PlayerCount = 5,
                TeamCoachNickName = "TeamCoach"
            };

            // Assert
            Assert.AreEqual(1, viewModel.Id);
            Assert.AreEqual("Team", viewModel.TeamName);
            Assert.AreEqual(5, viewModel.PlayerCount);
            Assert.AreEqual("TeamCoach", viewModel.TeamCoachNickName);
        }
    }
}
