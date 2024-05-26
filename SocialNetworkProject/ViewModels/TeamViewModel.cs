using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class TeamViewModel
    {
        public long Id { set; get; }
        public string TeamName { set; get; }
        [StringLength(75, ErrorMessage = "The Description field must be at most {1} characters long.")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Description field must start with a capital letter.")]
        public string Desc { set; get; }
        [Url(ErrorMessage = "The Team Site field must be a valid URL.")]
        public string TeamSite { set; get; }
        public byte[] TeamLogo { get; set; }
        public List<Player> Players { set; get; }
        public int PlayerCount { set; get; }
        public TeamCoach TeamCoach { set; get; }
        public byte[] TeamCoachImg { get; set; }
        public long DeletePlayerId { get; set; }
    }
}
