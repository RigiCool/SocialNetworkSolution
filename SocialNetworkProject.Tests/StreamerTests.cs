using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class StreamerTests
    {
        [TestMethod]
        public void Streamer_Properties_SetAndGetCorrectly()
        {
            // Arrange
            long expectedId = 1;
            string expectedName = "Streamer1";
            string expectedInstagram = "https://www.instagram.com/streamer1";
            string expectedYouTube = "https://www.youtube.com/c/streamer1";
            string expectedYouTubeSubs = "1000000";
            string expectedAvgViewNumYT = "500000";
            string expectedTwitch = "https://www.twitch.tv/streamer1";
            string expectedTwitchSubs = "500000";
            string expectedAvgViewNumTW = "200000";
            bool expectedInLeaders = true;
            long expectedUserId = 123;
            string expectedDescription = "Description";

            // Act
            var streamer = new Streamer
            {
                Id = expectedId,
                Name = expectedName,
                Instagram = expectedInstagram,
                YouTube = expectedYouTube,
                YouTubeSubs = expectedYouTubeSubs,
                AvgViewNumYT = expectedAvgViewNumYT,
                Twitch = expectedTwitch,
                TwitchSubs = expectedTwitchSubs,
                AvgViewNumTW = expectedAvgViewNumTW,
                InLeaders = expectedInLeaders,
                UserId = expectedUserId,
                Description = expectedDescription
            };

            // Assert
            Assert.AreEqual(expectedId, streamer.Id);
            Assert.AreEqual(expectedName, streamer.Name);
            Assert.AreEqual(expectedInstagram, streamer.Instagram);
            Assert.AreEqual(expectedYouTube, streamer.YouTube);
            Assert.AreEqual(expectedYouTubeSubs, streamer.YouTubeSubs);
            Assert.AreEqual(expectedAvgViewNumYT, streamer.AvgViewNumYT);
            Assert.AreEqual(expectedTwitch, streamer.Twitch);
            Assert.AreEqual(expectedTwitchSubs, streamer.TwitchSubs);
            Assert.AreEqual(expectedAvgViewNumTW, streamer.AvgViewNumTW);
            Assert.AreEqual(expectedInLeaders, streamer.InLeaders);
            Assert.AreEqual(expectedUserId, streamer.UserId);
            Assert.AreEqual(expectedDescription, streamer.Description);
        }
    }
}
