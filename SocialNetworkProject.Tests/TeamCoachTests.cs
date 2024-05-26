using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class TeamCoachTests
    {
        [TestMethod]
        public void TeamCoach_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "John Doe";
            string expectedNickName = "JohnD";
            int expectedWinTournament = 5;
            string expectedInstagram = "https://www.instagram.com/johndoe";
            string expectedYouTube = "https://www.youtube.com/user/johndoe";
            string expectedYouTubeSubs = "1000000";
            string expectedTwitch = "https://www.twitch.tv/johndoe";
            string expectedTwitchSubs = "500000";
            long expectedUserId = 100;
            string expectedDescription = "Experienced team coach";
            string expectedTeamName = "Team A";

            // Act
            var teamCoach = new TeamCoach
            {
                Id = expectedId,
                Name = expectedName,
                NickName = expectedNickName,
                WinTournament = expectedWinTournament,
                Instagram = expectedInstagram,
                YouTube = expectedYouTube,
                YouTubeSubs = expectedYouTubeSubs,
                Twitch = expectedTwitch,
                TwitchSubs = expectedTwitchSubs,
                UserId = expectedUserId,
                Description = expectedDescription,
                TeamName = expectedTeamName
            };

            // Assert
            Assert.AreEqual(expectedId, teamCoach.Id);
            Assert.AreEqual(expectedName, teamCoach.Name);
            Assert.AreEqual(expectedNickName, teamCoach.NickName);
            Assert.AreEqual(expectedWinTournament, teamCoach.WinTournament);
            Assert.AreEqual(expectedInstagram, teamCoach.Instagram);
            Assert.AreEqual(expectedYouTube, teamCoach.YouTube);
            Assert.AreEqual(expectedYouTubeSubs, teamCoach.YouTubeSubs);
            Assert.AreEqual(expectedTwitch, teamCoach.Twitch);
            Assert.AreEqual(expectedTwitchSubs, teamCoach.TwitchSubs);
            Assert.AreEqual(expectedUserId, teamCoach.UserId);
            Assert.AreEqual(expectedDescription, teamCoach.Description);
            Assert.AreEqual(expectedTeamName, teamCoach.TeamName);
        }
    }
}
