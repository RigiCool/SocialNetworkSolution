using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace SocialNetworkProject.Data
{
    public class AppDBContent : DbContext
    {
        public AppDBContent(DbContextOptions<AppDBContent> options) : base(options)
        {

        }
        public DbSet<Player> Player {get;set;}
        public DbSet<Team> Team { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<Admin> Admin { get; set; }
        public DbSet<Gamer> Gamer { get; set; }
        public DbSet<Streamer> Streamer { get; set; }
        public DbSet<Commentator> Commentator { get; set; }
        public DbSet<Sponsor> Sponsor { get; set; }
        public DbSet<TeamCoach> TeamCoach { get; set; }
        public DbSet<Post> Post { get; set; }
        public DbSet<TournamentOlympic> TournamentOlympic { get; set; }
        public DbSet<OlympicTeamList> OlympicTeamList { get; set; }
        public DbSet<Match> Match { get; set; }
        public DbSet<TournamentList> TournamentList { get; set; }
        public DbSet<ClassicTeamList> ClassicTeamList { get; set; }
        public DbSet<Subscription> Subscription { get; set; }
        public DbSet<Chat> Chat { get; set; }
        public DbSet<Message> Message { get; set; }

    }
}
