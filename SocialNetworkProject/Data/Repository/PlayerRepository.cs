using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Repository
{
    public class PlayerRepository : IAllPlayers
    {
        private AppDBContent appDBContent;

        public PlayerRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Player> GetPlayers => appDBContent.Player.Include(p => p.Team);

        public Team DeletePlayer(TeamViewModel teamModel)
        {
            
            Player deletePlayer = appDBContent.Player.FirstOrDefault(p => p.Id == teamModel.DeletePlayerId);
            appDBContent.Player.Remove(deletePlayer);
            Team team = appDBContent.Team.FirstOrDefault(t => t.Id == teamModel.Id);
            appDBContent.Player.Add(new Player { TeamId = team.Id });
            appDBContent.SaveChanges();
            return team;
        }

        public Player GetPlayer(int PlayerId) => appDBContent.Player.FirstOrDefault(p => p.Id == PlayerId);
    }
}
