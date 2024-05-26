using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Team
    {
        public long Id { set; get; }
        public string TeamName { set; get; }
        public string Desc { set; get; }
        public string TeamSite { set; get; }
        public byte[] TeamLogo { set; get; }
        public List<Player> Players { set; get; }
        public long TeamCoach { set; get; }
        public int PlayerCount { set; get; }
    }
}
