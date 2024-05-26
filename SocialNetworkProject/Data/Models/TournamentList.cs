using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class TournamentList
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long SponsorId { set; get; }
        public long Prize { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public int Type { set; get; }
        public bool Complete { set; get; }
    }
}
