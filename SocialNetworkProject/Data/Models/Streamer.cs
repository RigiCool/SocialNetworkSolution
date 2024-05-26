using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Streamer : IUserRole
    {
        public long Id { set; get; }
        public string Name { set; get; }
        [Url(ErrorMessage = "The Instagram field must be a valid URL.")]
        public string Instagram { set; get; }
        [Url(ErrorMessage = "The YouTube field must be a valid URL.")]
        public string YouTube { set; get; }
        [Range(0, 10000000, ErrorMessage = "The YouTube Subscribers field must be a positive number.")]
        public string YouTubeSubs { set; get; }
        [Range(0, 10000000, ErrorMessage = "The YouTube Average View Number field must be a positive number.")]
        public string AvgViewNumYT { set; get; }
        [Url(ErrorMessage = "The Twitch field must be a valid URL.")]
        public string Twitch { set; get; }
        [Range(0, 10000000, ErrorMessage = "The Twitch Subscribers field must be a positive number.")]
        public string TwitchSubs { set; get; }
        [Range(0, 10000000, ErrorMessage = "The Twitch Average View Number field must be a positive number.")]
        public string AvgViewNumTW { set; get; }
        public bool InLeaders { set; get; }
        public long UserId { set; get; }
        public virtual User User { get; set; }
        [StringLength(200, ErrorMessage = "The Description field must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}
