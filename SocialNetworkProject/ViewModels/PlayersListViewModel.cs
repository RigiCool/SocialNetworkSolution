using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class PlayersListViewModel
    {
        public IEnumerable<Player> AllPlayers { get; set; }
        public string CurrentTeam { get; set; }
    }
}
