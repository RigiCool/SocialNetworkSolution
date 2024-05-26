using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Commentator : IUserRole
    {
        public long Id { set; get; }
        public string Name { set; get; }
        [Url(ErrorMessage = "The Instagram field must be a valid URL.")]
        public string Instagram { set; get; }
        [Url(ErrorMessage = "The YouTube field must be a valid URL.")]
        public string YouTube { set; get; }
        [Url(ErrorMessage = "The Twitch field must be a valid URL.")]
        public string Twitch { set; get; }
        [Range(0, 777, ErrorMessage = "The MatchNum field must be a positive number.")]
        public int MatchNum { set; get; }
        [Range(0, 10, ErrorMessage = "The Reputation field must be between {1} and {2}.")]
        public string Reputation { set; get; }
        public bool InLeaders { set; get; }
        public long UserId { set; get; }
        public virtual User User { get; set; }
        [StringLength(200, ErrorMessage = "The Description field must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}
