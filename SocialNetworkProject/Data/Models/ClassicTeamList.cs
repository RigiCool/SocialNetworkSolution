using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class ClassicTeamList
    {
        public long id { get; set; }
        public Team Team { get; set; }
        public int Score { get; set; }
        public long TournamentId { get; set; }
        public string TournamentName { get; set; }
    }
}
