using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllTeams
    {
        IEnumerable<Team> AllTeams { get; }
        public Team GetTeam(long Id);
        public Team GetTeam(string Team);
        public Team GetTeamByUserNickName(string NickName);
        public Team CreateTeam(NewTeamViewModel newTeam);
        public List<Player> GetPlayers(long id);
        public Team SaveTeam(TeamViewModel teamModel);
        public BaseResponse<string> DeleteTeam(long TeamId);
    }
}
