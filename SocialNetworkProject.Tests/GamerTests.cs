using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class GamerTests
    {
        [TestMethod]
        public void Gamer_ValidationAttributes_ValidateCorrectly()
        {
            // Arrange
            var gamer = new Gamer();

            // Act & Assert
            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.WinGamePercent), 50f));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.WinGamePercent), 150f));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.KPD), 5f));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.KPD), 15f));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.WinTournament), 10));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.WinTournament), -5));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.Instagram), "https://www.instagram.com/example"));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.Instagram), "invalid_instagram_url"));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.YouTube), "https://www.youtube.com/example"));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.YouTube), "invalid_youtube_url"));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.YouTubeSubs), "100000"));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.YouTubeSubs), "-500"));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.Twitch), "https://www.twitch.tv/example"));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.Twitch), "invalid_twitch_url"));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.TwitchSubs), "100000"));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.TwitchSubs), "-500"));

            Assert.IsTrue(ValidateProperty(gamer, nameof(gamer.Description), new string('a', 200)));
            Assert.IsFalse(ValidateProperty(gamer, nameof(gamer.Description), new string('a', 201)));
        }

        private bool ValidateProperty(object obj, string propertyName, object value)
        {
            var validationContext = new ValidationContext(obj) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateProperty(value, validationContext, validationResults);
        }
    }
}
