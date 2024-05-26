using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class ListTournamentViewModel
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long SponsorId { set; get; }
        public long Prize { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<ClassicTeamList> ClassicTeamLists { set; get; }
        public Team NewTeam { set; get; }
        public long DeleteTeamId { set; get; }
        public int Type { set; get; }
        public bool Complete { set; get; }
    }
}
