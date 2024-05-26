using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class TeamController : Controller
    {
        private readonly IAllUsers userRep;
        private readonly IAllTeams teamRep;
        private readonly IAllTournaments tournamentsRep;
        private readonly IAllRoles roleRep;
        private readonly IAllPlayers playerRep;

        public TeamController(IAllUsers UserRep, IAllTeams TeamRep, IAllTournaments TournamentRep, IAllRoles RoleRep, IAllPlayers PlayerRep)
        {
            this.userRep = UserRep;
            this.teamRep = TeamRep;
            this.tournamentsRep = TournamentRep;
            this.roleRep = RoleRep;
            this.playerRep = PlayerRep;
        }
        [HttpGet]
        public IActionResult CreateTeam()
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            string currentUserNickName = HttpContext.User.Identity.Name;
            Team team = teamRep.GetTeamByUserNickName(currentUserNickName);
            if (team != null)
            {
                return RedirectToAction("Team", "Team", new { id = team.Id });
            }
            NewTeamViewModel newTeam = new NewTeamViewModel();
            return View(newTeam);
        }
        [HttpPost]
        public IActionResult CreateTeam(NewTeamViewModel newTeam)
        {

            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                newTeam.TeamCoachNickName = User.Identity.Name;
                Team team = teamRep.CreateTeam(newTeam);
                return RedirectToAction("Team", "Team", team);
            }
            else
            {
                return View(newTeam);
            }
               
        }
        [HttpGet]
        public IActionResult Team(Team team)
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            team = teamRep.GetTeam(team.Id);
            TeamViewModel teamModel = new TeamViewModel
            {
                Id = team.Id,
                TeamName = team.TeamName,
                Desc = team.Desc,
                TeamSite = team.TeamSite,
                TeamLogo = team.TeamLogo,
                PlayerCount = team.PlayerCount,
                TeamCoach = roleRep.GetTeamCoach(team.TeamCoach),
                TeamCoachImg = userRep.GetImage(team.TeamCoach),
            };
            teamModel.Players = teamRep.GetPlayers(team.Id);
            return View(teamModel);
        }
        [HttpPost]
        public IActionResult Team(TeamViewModel teamModel, IFormFile teamLogo)
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                if (teamLogo != null && teamLogo.Length != 0)
                {
                    if (!teamLogo.ContentType.StartsWith("image/"))
                    {
                        string[] errors = { "The uploaded file is not a valid" };
                        TempData["Errors"] = errors;
                        return RedirectToAction("Team", "Team", new { id = teamModel.Id });
                    }
                    using (var memoryStream = new MemoryStream())
                    {
                        teamLogo.CopyTo(memoryStream);
                        teamModel.TeamLogo = memoryStream.ToArray();
                    }
                }
                Team team = teamRep.SaveTeam(teamModel);
                return RedirectToAction("Team", "Team", new { id = team.Id });
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Team", "Team", new { id = teamModel.Id });
            }
        }
        public IActionResult DeletePlayer(TeamViewModel teamModel)
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            Team team = playerRep.DeletePlayer(teamModel);
            return RedirectToAction("Team", "Team", new { id = team.Id });
        }
        public IActionResult MyTeam()
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            string currentUserNickName = HttpContext.User.Identity.Name;
            Team team = teamRep.GetTeamByUserNickName(currentUserNickName);
            if (team==null)
            {
                return RedirectToAction("CreateTeam", "Team");
            }
            else
            {
                return RedirectToAction("Team", "Team", new { id = team.Id });
            }
        }
        [HttpPost]
        public IActionResult DeleteTeam(TeamViewModel teamModel)
        {
            if (!User.IsInRole("TeamCoach"))
            {
                return RedirectToAction("Index", "Home");
            }
            BaseResponse<string> response = teamRep.DeleteTeam(teamModel.Id);
            if (!response.Success)
            {
                TempData["Errors"] = response.Message;
            }
            return RedirectToAction("Index", "Home");
        }
    }
}
