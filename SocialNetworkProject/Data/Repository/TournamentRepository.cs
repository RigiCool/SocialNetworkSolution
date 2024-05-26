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
    public class TournamentRepository : IAllTournaments
    {
        private AppDBContent appDBContent;
        public TournamentRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }
        public TournamentOlympic CreateTournamentOlympic(NewTournamentViewModel newTournament)
        {
            TournamentOlympic tournament = new TournamentOlympic
            {
                Name = newTournament.TournamentName,
                Prize = newTournament.Prize,
                StartDate = newTournament.StartDate,
                EndDate = newTournament.EndDate,
                SponsorId = appDBContent.User.FirstOrDefault(u => u.Nickname == newTournament.SponsorNickName).Id,
                Stage = 1,
                Status = false
            };
            appDBContent.TournamentOlympic.Add(tournament);
            appDBContent.SaveChanges();
            CreateMatches(tournament.Id);
            
            return tournament;
        }
        public void CreateMatches(long Id)
        {
            for (int i = 0; i < 7; i++)
            {
                Match match = new Match();
                match.TournamentId = Id;
                appDBContent.Match.Add(match);
                appDBContent.SaveChanges();
            }
        }
        public TournamentOlympic AddTeam(OlympicTournamentViewModel tournamentVM)
        {
            OlympicTeamList newTeam = new OlympicTeamList
            {
                Team = appDBContent.Team.FirstOrDefault(t => t.TeamName == tournamentVM.NewTeam.TeamName),
                Score = 0,
                TournamentId = tournamentVM.Id,
                TournamentName = tournamentVM.Name
            };
            appDBContent.OlympicTeamList.Add(newTeam);
            appDBContent.SaveChanges();
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            return tournament;
        }
        public List<OlympicTeamList> OlympicTeamList(long Id)
        {
            List<OlympicTeamList> OlympicTeamList = appDBContent.OlympicTeamList.Include(p => p.Team).Where(t => t.TournamentId == Id).OrderByDescending(score => score.Score).ToList();
            return OlympicTeamList;
        }
        public TournamentOlympic DeleteTeam(OlympicTournamentViewModel tournamentVM)
        {
            OlympicTeamList deleteTeam = appDBContent.OlympicTeamList.FirstOrDefault(t => t.id == tournamentVM.DeleteTeamId);
            appDBContent.OlympicTeamList.Remove(deleteTeam);
            appDBContent.SaveChanges();
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            return tournament;
        }
        public TournamentOlympic SaveTeam(OlympicTournamentViewModel tournamentVM)
        {
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            OlympicTeamList olympicTeam = null;
            foreach (var team in tournamentVM.OlympicTeamLists)
            {
                olympicTeam = appDBContent.OlympicTeamList.FirstOrDefault(ot => ot.id == team.id);
                olympicTeam.Score = team.Score;
            }

            appDBContent.SaveChanges();

            return tournament;
        }
        public TournamentOlympic CreatePlayOff(OlympicTournamentViewModel tournamentVM)
        {
            List<OlympicTeamList> OlympicTeamList = appDBContent.OlympicTeamList.Include(p => p.Team).Where(t => t.TournamentId == tournamentVM.Id).OrderByDescending(score => score.Score).ToList();
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            tournament.PlayOffTeams = new List<Team>();
            for (var i=0; i<8; i++)
            {
                tournament.PlayOffTeams.Add(OlympicTeamList[i].Team);
            }
            appDBContent.SaveChanges();
            return tournament;
        }
        public List<Team> GetPlayOffTeams(long Id)
        {
            var tournament = appDBContent.TournamentOlympic.Include(t => t.PlayOffTeams).FirstOrDefault(t => t.Id == Id);

            // If tournament is null or its PlayOffTeams property is null, return null
            if (tournament == null || tournament.PlayOffTeams == null)
            {
                return null;
            }

            // Return the list of playoff teams
            return tournament.PlayOffTeams.ToList();
        }

        public TournamentOlympic SaveBracket(OlympicTournamentViewModel tournamentVM)
        {
            TournamentOlympic tournament = GetTournamentById(tournamentVM.Id);
            List<Match> matchList = appDBContent.Match.Include(p => p.Team1).Include(p => p.Team2).Where(t => t.TournamentId == tournamentVM.Id).ToList();
            if (tournament.Stage == 1)
            {
                HashSet<string> processedTeams = new HashSet<string>();
                tournament.Status = true;

                for (int i = 0; i < 4; i++)
                {
                    string team1Name = tournamentVM.Matches[i].Team1?.TeamName ?? "Unknown";
                    string team2Name = tournamentVM.Matches[i].Team2?.TeamName ?? "Unknown";

                    if (processedTeams.Contains(team1Name) || processedTeams.Contains(team2Name))
                    {
                        tournament.Status = false;
                        break;
                    }
                    if(team1Name== team2Name)
                    {
                        tournament.Status = false;
                        break;
                    }
                    Team team1 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == team1Name);
                    Team team2 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == team2Name);

                    if (team1 != null && team2 != null)
                    {
                        matchList[i].TournamentId = tournamentVM.Id;
                        matchList[i].Team1 = team1;
                        matchList[i].Team2 = team2;
                        matchList[i].Team1Score = tournamentVM.Matches[i].Team1Score;
                        matchList[i].Team2Score = tournamentVM.Matches[i].Team2Score;

                        processedTeams.Add(team1Name);
                        processedTeams.Add(team2Name);
                    }
                    else
                    {
                        tournament.Status = false;
                        continue;
                    }
                }
            }

            else if (tournament.Stage == 2)
            {
                for (int i = 4; i < 6; i++)
                {
                    Team team1 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == tournamentVM.Matches[i].Team1.TeamName);
                    Team team2 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == tournamentVM.Matches[i].Team2.TeamName);
                    matchList[i].TournamentId = tournamentVM.Id;
                    matchList[i].Team1 = team1;
                    matchList[i].Team2 = team2;
                    matchList[i].Team1Score = tournamentVM.Matches[i].Team1Score;
                    matchList[i].Team2Score = tournamentVM.Matches[i].Team2Score;
                }
            }
            else if(tournament.Stage == 3)
            {
                Team team1 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == tournamentVM.Matches[6].Team1.TeamName);
                Team team2 = appDBContent.Team.FirstOrDefault(t1 => t1.TeamName == tournamentVM.Matches[6].Team2.TeamName);
                matchList[6].TournamentId = tournamentVM.Id;
                matchList[6].Team1 = team1;
                matchList[6].Team2 = team2;
                matchList[6].Team1Score = tournamentVM.Matches[6].Team1Score;
                matchList[6].Team2Score = tournamentVM.Matches[6].Team2Score;
            }
            appDBContent.SaveChanges();
            return tournament;
        }

        public TournamentOlympic ResetBracket(OlympicTournamentViewModel tournamentVM)
        {
            List<Match> matches = appDBContent.Match.Where(match => match.TournamentId == tournamentVM.Id).ToList();
            appDBContent.Match.RemoveRange(matches);
            appDBContent.SaveChanges();
            TournamentOlympic tournament = GetTournamentById(tournamentVM.Id);
            tournament.Stage = 1;
            CreateMatches(tournament.Id);
            return tournament;
        }

        public List<Match> GetMatches(long Id)
        {
            TournamentOlympic tournament = GetTournamentById(Id);
            List<Match> matchList = appDBContent.Match.Include(p => p.Team1).Include(p => p.Team2).Where(t => t.TournamentId == Id).ToList();
            return matchList;
        }

        public TournamentOlympic CompleteStage(OlympicTournamentViewModel tournamentVM)
        {
            TournamentOlympic tournament = GetTournamentById(tournamentVM.Id);
            List<Match> matchList = appDBContent.Match.Include(p => p.Team1).Include(p => p.Team2).Where(t => t.TournamentId == tournament.Id).ToList();
            if (tournament.Stage == 1)
            {
                for (int i = 0; i < 4; i++)
                {
                    if(matchList[i].Team1Score >= matchList[i].Team2Score)
                    {
                        matchList[i].Winner = matchList[i].Team1;
                    }
                    else
                    {
                        matchList[i].Winner = matchList[i].Team2;
                    }
                }
                matchList[4].Team1 = matchList[0].Winner;
                matchList[4].Team2 = matchList[1].Winner;
                matchList[5].Team1 = matchList[2].Winner;
                matchList[5].Team2 = matchList[3].Winner;
            }
            else if(tournament.Stage == 2)
            {
                for (int i = 4; i < 6; i++)
                {
                    if (matchList[i].Team1Score >= matchList[i].Team2Score)
                    {
                        matchList[i].Winner = matchList[i].Team1;
                    }
                    else
                    {
                        matchList[i].Winner = matchList[i].Team2;
                    }
                }
                matchList[6].Team1 = matchList[4].Winner;
                matchList[6].Team2 = matchList[5].Winner;
            }
            else
            {
                if (matchList[6].Team1Score >= matchList[6].Team2Score)
                {
                    matchList[6].Winner = matchList[6].Team1;
                }
                else
                {
                    matchList[6].Winner = matchList[6].Team2;
                }
            }
            tournament.Stage++;
            appDBContent.SaveChanges();
            return tournament;
        }

        public TournamentOlympic GetTournamentById(long Id)
        {
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Id == Id);
            return tournament;
        }

        public TournamentList CreateTournamentList(NewTournamentViewModel newTournament)
        {
            TournamentList tournament = new TournamentList
            {
                Name = newTournament.TournamentName,
                Prize = newTournament.Prize,
                StartDate = newTournament.StartDate,
                EndDate = newTournament.EndDate,
                SponsorId = appDBContent.User.FirstOrDefault(u => u.Nickname == newTournament.SponsorNickName).Id,
                Complete = false
            };
            appDBContent.TournamentList.Add(tournament);
            appDBContent.SaveChanges();
            CreateMatches(tournament.Id);

            return tournament;
        }

        public TournamentList AddTeam(ListTournamentViewModel tournamentVM)
        {
            ClassicTeamList newTeam = new ClassicTeamList
            {
                Team = appDBContent.Team.FirstOrDefault(t => t.TeamName == tournamentVM.NewTeam.TeamName),
                Score = 0,
                TournamentId = tournamentVM.Id,
                TournamentName = tournamentVM.Name
            };
            appDBContent.ClassicTeamList.Add(newTeam);
            appDBContent.SaveChanges();
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            return tournament;
        }

        public TournamentList DeleteTeam(ListTournamentViewModel tournamentVM)
        {
            ClassicTeamList deleteTeam = appDBContent.ClassicTeamList.FirstOrDefault(t => t.id == tournamentVM.DeleteTeamId);
            appDBContent.ClassicTeamList.Remove(deleteTeam);
            appDBContent.SaveChanges();
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            return tournament;
        }

        public TournamentList SaveTeam(ListTournamentViewModel tournamentVM)
        {
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            ClassicTeamList listTeam = null;
            foreach (var team in tournamentVM.ClassicTeamLists)
            {
                listTeam = appDBContent.ClassicTeamList.FirstOrDefault(ot => ot.id == team.id);
                listTeam.Score = team.Score;
            }
            tournament.Type = tournamentVM.Type;

            appDBContent.SaveChanges();

            return tournament;
        }
        public TournamentList GetTournamentById(TournamentList tournament)
        {
            tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Id == tournament.Id);
            return tournament;
        }

        public List<ClassicTeamList> ClassicTeamList(long Id)
        {
            List<ClassicTeamList> ClassicTeamList = appDBContent.ClassicTeamList.Include(p => p.Team).Where(t => t.TournamentId == Id).OrderByDescending(score => score.Score).ToList();
            return ClassicTeamList;
        }

        public TournamentList ListTournamentComplete(ListTournamentViewModel tournamentVM)
        {
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Id == tournamentVM.Id);
            tournament.Type = tournamentVM.Type;
            tournament.Complete = true;
            appDBContent.SaveChanges();
            return tournament;
        }
        
        public List<TournamentOlympic> GetTournamentsOlympics()
        {
            return appDBContent.TournamentOlympic.ToList();
        }

        public List<TournamentList> GetTournamentsLists()
        {
            return appDBContent.TournamentList.ToList();
        }
        public List<TournamentOlympic> GetTournamentsOlympics(string NickName)
        {
            User user = appDBContent.User.FirstOrDefault(u => u.Nickname == NickName);
            return appDBContent.TournamentOlympic.Where(t=> t.SponsorId==user.Id).ToList();
        }

        public List<TournamentList> GetTournamentsLists(string NickName)
        {
            User user = appDBContent.User.FirstOrDefault(u => u.Nickname == NickName);
            return appDBContent.TournamentList.Where(t => t.SponsorId == user.Id).ToList();
        }

        public TournamentOlympic GetTournamentOlympicByName(string Tournament)
        {
            TournamentOlympic tournament = appDBContent.TournamentOlympic.FirstOrDefault(tour => tour.Name == Tournament);
            return tournament;
        }

        public TournamentList GetTournamentListByName(string Tournament)
        {
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(tour => tour.Name == Tournament);
            return tournament;
        }

        public void DeleteTournamentOlympic(long TournamentId)
        {
            List<OlympicTeamList> list = appDBContent.OlympicTeamList.Where(l => l.TournamentId == TournamentId).ToList();
            appDBContent.OlympicTeamList.RemoveRange(list);

            List<Match> matchlist = appDBContent.Match.Where(m => m.TournamentId == TournamentId).ToList();
            appDBContent.Match.RemoveRange(matchlist);
            appDBContent.SaveChanges();

            TournamentOlympic tournament = appDBContent.TournamentOlympic.Include(t => t.PlayOffTeams).FirstOrDefault(t => t.Id == TournamentId);
            appDBContent.TournamentOlympic.Remove(tournament);
            appDBContent.SaveChanges();
        }

        public void DeleteTournamentList(long TournamentId)
        {
            List<ClassicTeamList> list = appDBContent.ClassicTeamList.Where(l => l.TournamentId == TournamentId).ToList();
            appDBContent.ClassicTeamList.RemoveRange(list);
            TournamentList tournament = appDBContent.TournamentList.FirstOrDefault(t => t.Id == TournamentId);
            appDBContent.TournamentList.Remove(tournament);
            appDBContent.SaveChanges();
        }

        public bool RepeatedTeamCheckTournamentOlympic(OlympicTournamentViewModel tournamentVM)
        {
            Team newTeam = appDBContent.Team.FirstOrDefault(t => t.TeamName == tournamentVM.NewTeam.TeamName);
            OlympicTeamList team = appDBContent.OlympicTeamList.FirstOrDefault(to => to.Team.Id == newTeam.Id && to.TournamentId == tournamentVM.Id);
            if(team == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool RepeatedTeamCheckTournamentList(ListTournamentViewModel tournamentVM)
        {
            Team newTeam = appDBContent.Team.FirstOrDefault(t => t.TeamName == tournamentVM.NewTeam.TeamName);
            ClassicTeamList team = appDBContent.ClassicTeamList.FirstOrDefault(to => to.Team.Id == newTeam.Id && to.TournamentId == tournamentVM.Id);
            if (team == null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
