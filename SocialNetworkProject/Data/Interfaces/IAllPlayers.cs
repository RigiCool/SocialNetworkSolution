using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllPlayers
    {
        IEnumerable<Player> GetPlayers { get; }
        Player GetPlayer(int PlayerId);
        public Team DeletePlayer(TeamViewModel teamModel);
    }
}
