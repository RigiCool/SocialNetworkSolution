using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class PlayerTests
    {
        [TestMethod]
        public void Player_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "John Doe";
            string expectedNickName = "johnny";
            byte[] expectedImageData = new byte[] { 0x01, 0x02, 0x03 };
            ulong expectedPrizeMoney = 100000;
            long expectedUserId = 10;
            float expectedWinGamePercent = 75.5f;
            float expectedKPD = 2.5f;
            int expectedWinTournament = 5;
            string expectedInstagram = "https://www.instagram.com/example/";
            string expectedYouTube = "https://www.youtube.com/example/";
            string expectedYouTubeSubs = "1000000";
            string expectedTwitch = "https://www.twitch.tv/example/";
            string expectedTwitchSubs = "500000";
            string expectedDescription = "Lorem ipsum dolor sit amet, consectetur adipiscing elit.";
            long expectedTeamId = 20;
            var expectedTeam = new Team();

            // Act
            var player = new Player
            {
                Id = expectedId,
                Name = expectedName,
                NickName = expectedNickName,
                ImageData = expectedImageData,
                PrizeMoney = expectedPrizeMoney,
                UserId = expectedUserId,
                WinGamePercent = expectedWinGamePercent,
                KPD = expectedKPD,
                WinTournament = expectedWinTournament,
                Instagram = expectedInstagram,
                YouTube = expectedYouTube,
                YouTubeSubs = expectedYouTubeSubs,
                Twitch = expectedTwitch,
                TwitchSubs = expectedTwitchSubs,
                Description = expectedDescription,
                TeamId = expectedTeamId,
                Team = expectedTeam
            };

            // Assert
            Assert.AreEqual(expectedId, player.Id);
            Assert.AreEqual(expectedName, player.Name);
            Assert.AreEqual(expectedNickName, player.NickName);
            Assert.AreEqual(expectedImageData, player.ImageData);
            Assert.AreEqual(expectedPrizeMoney, player.PrizeMoney);
            Assert.AreEqual(expectedUserId, player.UserId);
            Assert.AreEqual(expectedWinGamePercent, player.WinGamePercent);
            Assert.AreEqual(expectedKPD, player.KPD);
            Assert.AreEqual(expectedWinTournament, player.WinTournament);
            Assert.AreEqual(expectedInstagram, player.Instagram);
            Assert.AreEqual(expectedYouTube, player.YouTube);
            Assert.AreEqual(expectedYouTubeSubs, player.YouTubeSubs);
            Assert.AreEqual(expectedTwitch, player.Twitch);
            Assert.AreEqual(expectedTwitchSubs, player.TwitchSubs);
            Assert.AreEqual(expectedDescription, player.Description);
            Assert.AreEqual(expectedTeamId, player.TeamId);
            Assert.AreEqual(expectedTeam, player.Team);
        }
    }
}
