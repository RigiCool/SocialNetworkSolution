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
    public class TeamRepository : IAllTeams
    {
        private AppDBContent appDBContent;

        public TeamRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Team> AllTeams => appDBContent.Team;

        public Team CreateTeam(NewTeamViewModel newTeam)
        {
            User user = appDBContent.User.FirstOrDefault(u => u.Nickname == newTeam.TeamCoachNickName);
            Team team = new Team
            {
                TeamName = newTeam.TeamName,
                PlayerCount = newTeam.PlayerCount,
                TeamCoach = user.Id
            };

            TeamCoach teamCoach = appDBContent.TeamCoach.FirstOrDefault(t => t.UserId == user.Id);
            teamCoach.TeamName = team.TeamName;

            appDBContent.Team.Add(team);
            appDBContent.SaveChanges();
            for (int i =0; i < team.PlayerCount; i++)
            {
                appDBContent.Player.Add(new Player { TeamId = team.Id});
                appDBContent.SaveChanges();
            }
            return team;
        }

        public BaseResponse<string> DeleteTeam(long TeamId)
        {
            try
            {
                Team team = GetTeam(TeamId);
                appDBContent.Player.Where(p => p.Name == null && p.TeamId == team.Id);
                appDBContent.Team.Remove(team);
                appDBContent.SaveChanges();
                return new BaseResponse<string>
                {
                    StatusCode = 200,
                    Success = true,
                    Message = "Team deleted.",
                    Data = "Error: None"
                };
            }
            catch (DbUpdateException ex)
            {
                return new BaseResponse<string>
                {
                    StatusCode = 547,
                    Success = false,
                    Message = "Cannot delete the team due to participations in tournaments.",
                    Data = "Error: Cannot delete the team due to existing references in the Match table."
                };
            }
        }

        public List<Player> GetPlayers(long id)
        {
            List<Player> Players = appDBContent.Player.Where(player => player.TeamId == id).Include(player => player.Team).ToList();
            return Players;
        }

        public Team GetTeam(long id)
        {
            Team team = appDBContent.Team.FirstOrDefault(t => t.Id == id);
            return team;
        }

        public Team GetTeam(string Team)
        {
            Team team = appDBContent.Team.FirstOrDefault(t => t.TeamName == Team);
            return team;
        }
        public Team GetTeamByUserNickName(string NickName)
        {
            User user = appDBContent.User.FirstOrDefault(u => u.Nickname == NickName);
            Team team = appDBContent.Team.FirstOrDefault(t => t.TeamCoach == user.Id);
            return team;
        }
        public Team SaveTeam(TeamViewModel teamModel)
        {
            Team team = appDBContent.Team.FirstOrDefault(t => t.Id == teamModel.Id);
            List<Player> Players = appDBContent.Player.Where(pl => pl.TeamId == teamModel.Id).ToList();
            if (teamModel.TeamLogo != null)
            {
                team.TeamLogo = teamModel.TeamLogo;
            }
            team.TeamSite = teamModel.TeamSite;
            team.Desc = teamModel.Desc;
            TeamCoach teamCoach = appDBContent.TeamCoach.FirstOrDefault(tc => tc.NickName == teamModel.TeamCoach.NickName);
            if (teamCoach != null)
            {
                team.TeamCoach = teamCoach.UserId;
            }
            Player editedPlayer = null;
            User sourceUser = null;
            for (int i=0; i < team.PlayerCount; i++)
            {
                if (Players[i].NickName != teamModel.Players[i].NickName)
                {
                    sourceUser = appDBContent.User.FirstOrDefault(u => u.Nickname == teamModel.Players[i].NickName);
                    if (sourceUser !=null)
                    {
                        editedPlayer = appDBContent.Player.FirstOrDefault(p => p.Id == teamModel.Players[i].Id);
                        editedPlayer.NickName = teamModel.Players[i].NickName;
                        editedPlayer.ImageData = sourceUser.ImageData;
                        editedPlayer.UserId = sourceUser.Id;
                    }
                }
            }
            appDBContent.SaveChanges();
            return team;
        }
    }
}
