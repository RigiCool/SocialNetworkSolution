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
    public class TournamentController : Controller
    {
        private readonly IAllUsers userRep;
        private readonly IAllTeams teamRep;
        private readonly IAllTournaments tournamentsRep;
        private readonly IAllRoles roleRep;
        private readonly IAllPosts postRep;

        public TournamentController(IAllUsers UserRep, IAllTeams TeamRep, IAllTournaments TournamentRep, IAllRoles RoleRep, IAllPosts PostRep)
        {
            this.userRep = UserRep;
            this.teamRep = TeamRep;
            this.tournamentsRep = TournamentRep;
            this.roleRep = RoleRep;
            this.postRep = PostRep;
        }
        List<Team> teamContainer = new List<Team>();
        [HttpGet]
        public IActionResult CreateTournament()
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            NewTournamentViewModel newTournament = new NewTournamentViewModel();
            return View(newTournament);
        }
        [HttpPost]
        public IActionResult CreateTournament(NewTournamentViewModel newTournament)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (ModelState.IsValid)
            {
                if (newTournament.TournamentType == "Tournament Olympic")
                {
                    newTournament.SponsorNickName = User.Identity.Name;
                    TournamentOlympic tournament = tournamentsRep.CreateTournamentOlympic(newTournament);
                    return RedirectToAction("TournamentOlympic", "Tournament", tournament);
                }
                else if (newTournament.TournamentType == "Tournament List")
                {
                    newTournament.SponsorNickName = User.Identity.Name;
                    TournamentList tournament = tournamentsRep.CreateTournamentList(newTournament);
                    return RedirectToAction("TournamentList", "Tournament", tournament);
                }
                else
                {
                    return View(newTournament);
                }
            }
            else
            {
                return View(newTournament);
            }
            
        }
        [HttpGet]
        public IActionResult TournamentOlympic(TournamentOlympic tournament)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            tournament = tournamentsRep.GetTournamentById(tournament.Id);
            OlympicTournamentViewModel tournamentVM = new OlympicTournamentViewModel
            {
                Id = tournament.Id,
                Name = tournament.Name,
                SponsorId = tournament.SponsorId,
                StartDate = tournament.StartDate,
                EndDate = tournament.EndDate,
                Prize = tournament.Prize,
                PlayOffTeams = tournamentsRep.GetPlayOffTeams(tournament.Id),
                Stage = tournament.Stage,
                Status = tournament.Status
            };
            tournamentVM.Matches = tournamentsRep.GetMatches(tournament.Id);
            tournamentVM.OlympicTeamLists = tournamentsRep.OlympicTeamList(tournamentVM.Id);
            return View(tournamentVM);
        }
        [HttpPost]
        public IActionResult AddTeamOlympic(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (teamRep.GetTeam(tournamentVM.NewTeam.TeamName)!=null)
            {
                if (tournamentsRep.RepeatedTeamCheckTournamentOlympic(tournamentVM))
                {
                    TournamentOlympic tournament = tournamentsRep.AddTeam(tournamentVM);
                    return RedirectToAction("TournamentOlympic", "Tournament", tournament);
                }
                else
                {
                    TempData["Errors"] = "The Team has already been added";
                    return RedirectToAction("TournamentOlympic", "Tournament", new TournamentOlympic { Id = tournamentVM.Id });
                }
            }
            else
            {
                TempData["Errors"] = "The Team was not found";
                return RedirectToAction("TournamentOlympic", "Tournament", new TournamentOlympic { Id = tournamentVM.Id });
            }
        }
        [HttpPost]
        public IActionResult DeleteTeamOlympic(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.DeleteTeam(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }
        [HttpPost]
        public IActionResult SaveTeamOlympic(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.SaveTeam(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }
        public IActionResult ListStageComplete(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.CreatePlayOff(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }
        public IActionResult SaveOlympicBracket(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.SaveBracket(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }
        public IActionResult ResetOlympicBracket(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.ResetBracket(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }
        public IActionResult CompleteStage(OlympicTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentOlympic tournament = tournamentsRep.CompleteStage(tournamentVM);
            return RedirectToAction("TournamentOlympic", "Tournament", tournament);
        }

        [HttpGet]
        public IActionResult TournamentList(TournamentList tournament)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            tournament = tournamentsRep.GetTournamentById(tournament);
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
        [HttpPost]
        public IActionResult AddTeamClassic(ListTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            if (teamRep.GetTeam(tournamentVM.NewTeam.TeamName) != null)
            {
                if (tournamentsRep.RepeatedTeamCheckTournamentList(tournamentVM))
                {
                    TournamentList tournament = tournamentsRep.AddTeam(tournamentVM);
                    return RedirectToAction("TournamentList", "Tournament", tournament);
                }
                else
                {
                    TempData["Errors"] = "The Team has already been added";
                    return RedirectToAction("TournamentList", "Tournament", new TournamentOlympic { Id = tournamentVM.Id });
                }
            }
            else
            {
                TempData["Errors"] = "The Team was not found";
                return RedirectToAction("TournamentList", "Tournament", new TournamentOlympic { Id = tournamentVM.Id });
            }
        }
        [HttpPost]
        public IActionResult DeleteTeamClassic(ListTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentList tournament = tournamentsRep.DeleteTeam(tournamentVM);
            return RedirectToAction("TournamentList", "Tournament", tournament);
        }
        [HttpPost]
        public IActionResult SaveTeamClassic(ListTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentList tournament = tournamentsRep.SaveTeam(tournamentVM);
            return RedirectToAction("TournamentList", "Tournament", tournament);
        }
        public IActionResult ListTournamentComplete(ListTournamentViewModel tournamentVM)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            TournamentList tournament = tournamentsRep.ListTournamentComplete(tournamentVM);
            return RedirectToAction("TournamentList", "Tournament", tournament);
        }
        public IActionResult MyTournaments()
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            string currentUserNickName = HttpContext.User.Identity.Name;
            List<TournamentList> tournamentLists = tournamentsRep.GetTournamentsLists(currentUserNickName);
            List<TournamentOlympic> tournamentOlympics = tournamentsRep.GetTournamentsOlympics(currentUserNickName);
            SearchViewModel model = new SearchViewModel();
            model.TournamentsList = tournamentLists;
            model.TournamentsOlympic = tournamentOlympics;
            return View(model);
        }
        [Route("Tournament/DeleteTournamentOlympic/{TournamentId}")]
        public IActionResult DeleteTournamentOlympic(long TournamentId)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            tournamentsRep.DeleteTournamentOlympic(TournamentId);
            return RedirectToAction("MyTournaments", "Tournament");
        }
        [Route("Tournament/DeleteTournamentList/{TournamentId}")]
        public IActionResult DeleteTournamentList(long TournamentId)
        {
            if (!User.IsInRole("Sponsor"))
            {
                return RedirectToAction("Index", "Home");
            }
            tournamentsRep.DeleteTournamentList(TournamentId);
            return RedirectToAction("MyTournaments", "Tournament");
        }
    }
}
