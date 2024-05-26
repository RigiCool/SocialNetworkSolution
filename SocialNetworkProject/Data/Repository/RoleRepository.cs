using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SocialNetworkProject.Data.Repository
{
    public class RoleRepository : IAllRoles
    {
        private AppDBContent appDBContent;
        public RoleRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public IEnumerable<Role> GetRoles(long id)
        {
            return appDBContent.Role.Where(role => role.Id == id).ToList();
        }
        public IEnumerable<Role> GetRoles(string _name)
        {
            return appDBContent.Role.Where(role => role.Name.Contains(_name)).ToList();
        }
        public IUserRole GetUserRole(User user)
        {
            if (user.Role.Name == "Gamer")
            {
                return appDBContent.Gamer.FirstOrDefault(gamer => gamer.UserId == user.Id);
            }
            else if (user.Role.Name == "Streamer")
            {
                return appDBContent.Streamer.FirstOrDefault(streamer => streamer.UserId == user.Id);
            }
            else if (user.Role.Name == "Commentator")
            {
                return appDBContent.Commentator.FirstOrDefault(commentator => commentator.UserId == user.Id);
            }
            else if (user.Role.Name == "Sponsor")
            {
                return appDBContent.Sponsor.FirstOrDefault(sponsor => sponsor.UserId == user.Id);
            }
            else if (user.Role.Name == "Player")
            {
                return appDBContent.Player.Include(player => player.Team).FirstOrDefault(player => player.UserId == user.Id);
            }
            else if (user.Role.Name == "TeamCoach")
            {
                return appDBContent.TeamCoach.FirstOrDefault(teamcoach => teamcoach.UserId == user.Id);
            }
            else
            {
                throw new InvalidOperationException($"Unsupported type: ");
            }
        }

        public void UpdateInfo(long id, Commentator commentator)
        {
            Commentator userCommentator = appDBContent.Commentator.FirstOrDefault(u => u.UserId == id);
            if (userCommentator != null)
            {
                userCommentator.MatchNum = commentator.MatchNum;
                userCommentator.Reputation = commentator.Reputation;
                userCommentator.Description = commentator.Description;
                userCommentator.Twitch = commentator.Twitch;
                userCommentator.YouTube = commentator.YouTube;
                userCommentator.Instagram = commentator.Instagram;
                userCommentator.Description = commentator.Description;
                appDBContent.SaveChanges();
            }
        }

        public void UpdateInfo(long id, Gamer gamer)
        {
            Gamer userGamer = appDBContent.Gamer.FirstOrDefault(u => u.UserId == id);
            if (userGamer != null)
            {
                userGamer.WinGamePercent = gamer.WinGamePercent;
                userGamer.KPD = gamer.KPD;
                userGamer.WinTournament = gamer.WinTournament;
                userGamer.Instagram = gamer.Instagram;
                userGamer.YouTube = gamer.YouTube;
                userGamer.YouTubeSubs = gamer.YouTubeSubs;
                userGamer.Twitch = gamer.Twitch;
                userGamer.TwitchSubs = gamer.TwitchSubs;
                userGamer.InLeaders = gamer.InLeaders;
                userGamer.Description = gamer.Description;
                appDBContent.SaveChanges();
            }
        }

        public void UpdateInfo(long id, Streamer streamer)
        {
            Streamer userStreamer = appDBContent.Streamer.FirstOrDefault(u => u.UserId == id);
            if (userStreamer != null)
            {
                userStreamer.Instagram = streamer.Instagram;
                userStreamer.YouTube = streamer.YouTube;
                userStreamer.Twitch = streamer.Twitch;
                userStreamer.YouTubeSubs = streamer.YouTubeSubs;
                userStreamer.TwitchSubs = streamer.TwitchSubs;
                userStreamer.AvgViewNumYT = streamer.AvgViewNumYT;
                userStreamer.AvgViewNumTW = streamer.AvgViewNumTW;
                userStreamer.InLeaders = streamer.InLeaders;
                userStreamer.Description = streamer.Description;
                appDBContent.SaveChanges();
            }
        }

        public void UpdateInfo(long id, Sponsor sponsor)
        {
            Sponsor userSponsor = appDBContent.Sponsor.FirstOrDefault(u => u.UserId == id);
            if (userSponsor != null)
            {
                userSponsor.Company = sponsor.Company;
                userSponsor.CompanySite = sponsor.CompanySite;
                userSponsor.CompanyNumber = sponsor.CompanyNumber;
                userSponsor.InLeaders = sponsor.InLeaders;
                userSponsor.Description = sponsor.Description;
                appDBContent.SaveChanges();
            }
        }

        public void UpdateInfo(long id, TeamCoach teamcoach)
        {
            TeamCoach userTeamCoach = appDBContent.TeamCoach.FirstOrDefault(u => u.UserId == id);
            if (userTeamCoach != null)
            {
                userTeamCoach.WinTournament = teamcoach.WinTournament;
                userTeamCoach.Instagram = teamcoach.Instagram;
                userTeamCoach.YouTube = teamcoach.YouTube;
                userTeamCoach.YouTubeSubs = teamcoach.YouTubeSubs;
                userTeamCoach.Twitch = teamcoach.Twitch;
                userTeamCoach.TwitchSubs = teamcoach.TwitchSubs;
                userTeamCoach.Description = teamcoach.Description;
                userTeamCoach.TeamName = teamcoach.TeamName;
                appDBContent.SaveChanges();
            }
        }
        public void UpdateInfo(long id, Player player)
        {
            Player userPlayer = appDBContent.Player.FirstOrDefault(u => u.UserId == id);
            if (userPlayer != null)
            {
                userPlayer.PrizeMoney = player.PrizeMoney;
                userPlayer.WinGamePercent = player.WinGamePercent;
                userPlayer.KPD = player.KPD;
                userPlayer.WinTournament = player.WinTournament;
                userPlayer.Instagram = player.Instagram;
                userPlayer.YouTube = player.YouTube;
                userPlayer.YouTubeSubs = player.YouTubeSubs;
                userPlayer.Twitch = player.Twitch;
                userPlayer.TwitchSubs = player.TwitchSubs;
                userPlayer.Description = player.Description;
                appDBContent.SaveChanges();
            }
        }
        public TeamCoach GetTeamCoach(long id)
        {
            TeamCoach teamCoach = appDBContent.TeamCoach.FirstOrDefault(t => t.UserId == id);
            return teamCoach;
        }

        public IEnumerable<Role> GetAllRoles => appDBContent.Role;
    }
}