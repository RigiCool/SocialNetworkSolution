using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllRoles
    {
        IEnumerable<Role> GetAllRoles { get; }
        IEnumerable<Role> GetRoles(long id);
        IEnumerable<Role> GetRoles(string _name);
        public IUserRole GetUserRole(User user);
        void UpdateInfo(long id, Commentator commentator);
        void UpdateInfo(long id, Gamer gamer);
        void UpdateInfo(long id, Streamer streamer);
        void UpdateInfo(long id, Sponsor sponsor);
        void UpdateInfo(long id, TeamCoach teamcoach);
        void UpdateInfo(long id, Player player);
        public TeamCoach GetTeamCoach(long id);
    }
}
