using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Player : IUserRole
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string NickName { set; get; }
        public byte[] ImageData { get; set; }
        [Range(0, ulong.MaxValue, ErrorMessage = "The PrizeMoney field must be a positive number.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "The PrizeMoney field must be a whole number.")]
        public ulong PrizeMoney { set; get; }
        public long UserId { set; get; }
        [Range(0, 100, ErrorMessage = "The WinGamePercent field must be between {1} and {2}.")]
        public float WinGamePercent { set; get; }
        [Range(0, 10, ErrorMessage = "The KPD field must be between {1} and {2}.")]
        public float KPD { set; get; }
        [Range(0, 777, ErrorMessage = "The number of Tournaments won field must be a positive number.")]
        public int WinTournament { set; get; }
        [Url(ErrorMessage = "The Instagram field must be a valid URL.")]
        public string Instagram { set; get; }
        [Url(ErrorMessage = "The YouTube field must be a valid URL.")]
        public string YouTube { set; get; }
        [Range(0, 10000000, ErrorMessage = "The YouTube Subscribers field must be a positive number.")]
        public string YouTubeSubs { set; get; }
        [Url(ErrorMessage = "The Twitch field must be a valid URL.")]
        public string Twitch { set; get; }
        [Range(0, 10000000, ErrorMessage = "The Twitch Subscribers field must be a positive number.")]
        public string TwitchSubs { set; get; }
        [StringLength(200, ErrorMessage = "The Description field must be at most {1} characters long.")]
        public string Description { get; set; }
        public long TeamId { set; get; }
        public virtual Team Team { set; get; }
    }
}
