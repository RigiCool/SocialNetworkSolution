using Microsoft.VisualStudio.TestTools.UnitTesting;
using SocialNetworkProject.Data.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SocialNetworkProject.Tests.Models
{
    [TestClass]
    public class CommentatorTests
    {
        [TestMethod]
        public void Commentator_ValidationAttributes_ValidateCorrectly()
        {
            // Arrange
            var commentator = new Commentator();

            // Act & Assert
            Assert.IsTrue(ValidateProperty(commentator, nameof(commentator.Instagram), "https://www.instagram.com/example"));
            Assert.IsFalse(ValidateProperty(commentator, nameof(commentator.Instagram), "invalid_instagram_url"));

            Assert.IsTrue(ValidateProperty(commentator, nameof(commentator.YouTube), "https://www.youtube.com/example"));
            Assert.IsFalse(ValidateProperty(commentator, nameof(commentator.YouTube), "invalid_youtube_url"));

            Assert.IsTrue(ValidateProperty(commentator, nameof(commentator.Twitch), "https://www.twitch.tv/example"));
            Assert.IsFalse(ValidateProperty(commentator, nameof(commentator.Twitch), "invalid_twitch_url"));

            Assert.IsTrue(ValidateProperty(commentator, nameof(commentator.MatchNum), 500));
            Assert.IsFalse(ValidateProperty(commentator, nameof(commentator.MatchNum), -5));

            Assert.IsTrue(ValidateProperty(commentator, nameof(commentator.Description), new string('a', 200)));
            Assert.IsFalse(ValidateProperty(commentator, nameof(commentator.Description), new string('a', 201)));
        }

        private bool ValidateProperty(object obj, string propertyName, object value)
        {
            var validationContext = new ValidationContext(obj) { MemberName = propertyName };
            var validationResults = new List<ValidationResult>();

            return Validator.TryValidateProperty(value, validationContext, validationResults);
        }
    }
}
