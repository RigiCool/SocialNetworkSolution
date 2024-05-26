using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class SearchController : Controller
    {
        private readonly IAllUsers userRep;
        private readonly IAllTeams teamRep;
        private readonly IAllTournaments tournamentsRep;
        private readonly IAllRoles roleRep;
        private readonly IAllPosts postRep;
        private readonly IAllSubscriptions subRep;

        public SearchController(IAllUsers UserRep, IAllTeams TeamRep, IAllTournaments TournamentRep, IAllRoles RoleRep, IAllPosts PostRep, IAllSubscriptions SubRep)
        {
            this.userRep = UserRep;
            this.teamRep = TeamRep;
            this.tournamentsRep = TournamentRep;
            this.roleRep = RoleRep;
            this.postRep = PostRep;
            this.subRep = SubRep;
        }
        [HttpGet]
        public IActionResult Search()
        {
            SearchViewModel model = new SearchViewModel();
            model.Users = userRep.GetUsers();
            model.Teams = teamRep.AllTeams;
            model.TournamentsOlympic = tournamentsRep.GetTournamentsOlympics();
            model.TournamentsList = tournamentsRep.GetTournamentsLists();
            return View(model);
        }
        [Route("Search/Users/{User}")]
        public IActionResult Users(string User)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            if (currentUserNickName != User)
            {
                User user = userRep.GetUser(User);
                IUserRole userRole = roleRep.GetUserRole(user);
                IEnumerable<Post> UserPosts = postRep.GetPosts(user.Id);
                bool subscriptionCheck = subRep.CheckSubscription(currentUserNickName, user.Id);
                var ProfileObj = new ProfileViewModel<IUserRole>
                {
                    Id = user.Id,
                    Nickname = user.Nickname,
                    Name = user.Name,
                    Surname = user.Surname,
                    Age = user.Age,
                    Phone = user.Phone,
                    Email = user.Email,
                    ImageData = user.ImageData,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name,
                    Role = userRole,
                    Posts = UserPosts,
                    InSubscriptions = subscriptionCheck
                };
                return View(ProfileObj);
            }
            else
            {
                return RedirectToAction("Profile", "Profile");
            }
            
        }
        [Route("Search/Teams/{Team}")]
        public IActionResult Teams(string Team)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            User user = userRep.GetUser(currentUserNickName);
            Team team = teamRep.GetTeam(Team);
            if (user.Id != team.TeamCoach)
            {
                TeamViewModel teamModel = new TeamViewModel
                {
                    Id = team.Id,
                    TeamName = team.TeamName,
                    Desc = team.Desc,
                    TeamSite = team.TeamSite,
                    TeamLogo = team.TeamLogo,
                    PlayerCount = team.PlayerCount,
                    TeamCoach = roleRep.GetTeamCoach(team.TeamCoach),
                    TeamCoachImg = userRep.GetImage(team.TeamCoach)
                };
                teamModel.Players = teamRep.GetPlayers(team.Id);
                return View(teamModel);
            }
            else
            {
                return RedirectToAction("Team", "Team", new { id = team.Id });
            }
        }
        [Route("Search/TournamentsOlympic/{Tournament}")]
        public IActionResult TournamentsOlympic(string Tournament)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            User user = userRep.GetUser(currentUserNickName);
            TournamentOlympic tournament = tournamentsRep.GetTournamentOlympicByName(Tournament);
            if (user.Id != tournament.SponsorId)
            {
                OlympicTournamentViewModel tournamentVM = new OlympicTournamentViewModel
                {
                    Id = tournament.Id,
                    Name = tournament.Name,
                    SponsorId = tournament.SponsorId,
                    StartDate = tournament.StartDate,
                    EndDate = tournament.EndDate,
                    Prize = tournament.Prize,
                    PlayOffTeams = tournamentsRep.GetPlayOffTeams(tournament.Id),
                    Stage = tournament.Stage
                };
                tournamentVM.Matches = tournamentsRep.GetMatches(tournament.Id);
                tournamentVM.OlympicTeamLists = tournamentsRep.OlympicTeamList(tournamentVM.Id);
                return View(tournamentVM);
            }
            else
            {
                return RedirectToAction("TournamentOlympic", "Tournament", tournament);
            }
        }

        [Route("Search/TournamentsList/{Tournament}")]
        public IActionResult TournamentsList(string Tournament)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            User user = userRep.GetUser(currentUserNickName);
            TournamentList tournament = tournamentsRep.GetTournamentListByName(Tournament);
            if (user.Id != tournament.SponsorId)
            {
                ListTournamentViewModel tournamentVM = new ListTournamentViewModel
                {
                    Id = tournament.Id,
                    Name = tournament.Name,
                    SponsorId = tournament.SponsorId,
                    StartDate = tournament.StartDate,
                    EndDate = tournament.EndDate,
                    Prize = tournament.Prize,
                    Type = tournament.Type,
                    Complete = tournament.Complete
                };
                tournamentVM.ClassicTeamLists = tournamentsRep.ClassicTeamList(tournamentVM.Id);
                return View(tournamentVM);
            }
            else
            {
                return RedirectToAction("TournamentList", "Tournament", tournament);
            }
        }

        public IActionResult Subscribe(ProfileViewModel<IUserRole> ProfileObj)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            string user = subRep.Subscribe(currentUserNickName, ProfileObj.Nickname);
            return RedirectToAction("Users", "Search", new { User = user });
        }
        public IActionResult Unsubscribe(ProfileViewModel<IUserRole> ProfileObj)
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            string user = subRep.Unsubscribe(currentUserNickName, ProfileObj.Nickname);
            return RedirectToAction("Users", "Search", new { User = user });
        }
        public IActionResult Subscriptions()
        {
            string currentUserNickName = HttpContext.User.Identity.Name;
            List<User> users = subRep.Subscriptions(currentUserNickName);
            return View(users);
        }

    }
}
