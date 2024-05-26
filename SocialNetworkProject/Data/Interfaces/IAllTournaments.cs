using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllTournaments
    {
        public TournamentOlympic CreateTournamentOlympic(NewTournamentViewModel newTournament);
        public TournamentOlympic AddTeam(OlympicTournamentViewModel tournament);
        public TournamentOlympic DeleteTeam(OlympicTournamentViewModel tournamentVM);
        public TournamentOlympic SaveTeam(OlympicTournamentViewModel tournamentVM);
        public TournamentOlympic CreatePlayOff(OlympicTournamentViewModel tournamentVM);
        public TournamentOlympic SaveBracket(OlympicTournamentViewModel tournamentVM);
        public TournamentOlympic ResetBracket(OlympicTournamentViewModel tournamentVM);
        public TournamentOlympic CompleteStage(OlympicTournamentViewModel tournamentVM);
        public List<Team> GetPlayOffTeams(long Id);
        public List<Match> GetMatches(long Id);
        public TournamentOlympic GetTournamentById(long Id);
        //public void DeleteTournament(NewTournamentViewModel newTournament);
        public List<OlympicTeamList> OlympicTeamList(long Id);
        public TournamentList CreateTournamentList(NewTournamentViewModel newTournament);
        public TournamentList AddTeam(ListTournamentViewModel tournament);
        public TournamentList DeleteTeam(ListTournamentViewModel tournamentVM);
        public TournamentList SaveTeam(ListTournamentViewModel tournamentVM);
        public TournamentList GetTournamentById(TournamentList tournament);
        public TournamentList ListTournamentComplete(ListTournamentViewModel tournament);
        public List<ClassicTeamList> ClassicTeamList(long Id);
        public List<TournamentOlympic> GetTournamentsOlympics();
        public List<TournamentList> GetTournamentsLists();
        public TournamentOlympic GetTournamentOlympicByName(string Tournament);
        public TournamentList GetTournamentListByName(string Tournament);
        public List<TournamentList> GetTournamentsLists(string NickName);
        public List<TournamentOlympic> GetTournamentsOlympics(string NickName);
        public void DeleteTournamentOlympic(long TournamentId);
        public void DeleteTournamentList(long TournamentId);
        public bool RepeatedTeamCheckTournamentOlympic(OlympicTournamentViewModel tournamentVM);
        public bool RepeatedTeamCheckTournamentList(ListTournamentViewModel tournamentVM);
    }
}
