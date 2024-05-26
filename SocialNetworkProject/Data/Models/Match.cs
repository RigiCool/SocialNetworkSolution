using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Match
    {
        public long id { set; get; }
        public Team Team1 { set; get; }
        public Team Team2 { set; get; }
        public int Team1Score { set; get; }
        public int Team2Score { set; get; }
        public Team Winner { set; get; }
        public long TournamentId { set; get; }
    }
}
