using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class OlympicTournamentViewModel
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long SponsorId { set; get; }
        public long Prize { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }
        public List<OlympicTeamList> OlympicTeamLists { set; get; }
        public List<Team> PlayOffTeams { set; get; }
        public List<Match> Matches { set; get; }
        public Team NewTeam { set; get; }
        public long DeleteTeamId { set; get; }
        public int Stage { set; get; }
        public bool Status { set; get; }

    }
}
