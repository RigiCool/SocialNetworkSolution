using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class SearchViewModel
    {
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Team> Teams { get; set; }
        public IEnumerable<TournamentOlympic> TournamentsOlympic { get; set; }
        public IEnumerable<TournamentList> TournamentsList { get; set; }
    }
}
