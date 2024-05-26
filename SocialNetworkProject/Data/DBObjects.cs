using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using SocialNetworkProject.Data;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data
{
    public class DBObjects
    {
        private static Dictionary<string, Team> team;

        public static Dictionary<string, Team> Teams
        {
            get
            {
                if (team == null)
                {
                    var list = new Team[]
                    {
                        new Team { TeamName="Navy", Desc="Natus Vincere - Counter Strike team.",TeamCoach = 2, TeamLogo=System.IO.File.ReadAllBytes("C:/Users/Natan/Downloads/Navy.jpg")},
                        new Team { TeamName="Belan", Desc="Belum Anserinus - Counter Strike team.",TeamCoach = 1, TeamLogo=System.IO.File.ReadAllBytes("C:/Users/Natan/Downloads/Belan.jpg")}
                    };

                    team = new Dictionary<string, Team>();
                    foreach (Team element in list)
                        team.Add(element.TeamName, element);
                }
                return team;
            }
        }

        public static void Initial(AppDBContent content)
        {
            if (!content.Team.Any())
                content.Team.AddRange(Teams.Select(testc => testc.Value));
            if (!content.Role.Any())
            {
                content.AddRange(
                    new Role { Name = "Admin" },
                    new Role { Name = "Gamer" },
                    new Role { Name = "Streamer" },
                    new Role { Name = "Commentator" },
                    new Role { Name = "Sponsor" },
                    new Role { Name = "TeamCoach" },
                    new Role { Name = "Player" }
                );
            }
            content.SaveChanges();
            if (!content.User.Any())
            {
                content.AddRange(
                    new User
                    {
                        Name = "Natans",
                        Surname = "Tarikins",
                        Nickname = "Rigi",
                        Password = "Password1234!",
                        Phone = "23742584",
                        Email = "rigi@gmail.com",
                        RoleId = 3,
                        Role = content.Role.FirstOrDefault(p => p.Name == "Streamer"),
                        ImageData= System.IO.File.ReadAllBytes("C:/Users/Natan/Downloads/Rigi.jpg"),
                        Age = "21"
                    },
                    new User
                    {
                        Name = "Andrew",
                        Surname = "Gordin",
                        Nickname = "GoneFludd",
                        Password = "Password1234!",
                        Phone = "23755584",
                        Email = "gonefludd@gmail.com",
                        RoleId = 7,
                        Role = content.Role.FirstOrDefault(p => p.Name == "Player"),
                        Age = "18"
                    },
                    new User
                    {
                        Name = "Alexander",
                        Surname = "Ivancuk",
                        Nickname = "WildWolf",
                        Password = "Password1234!",
                        Phone = "23332522",
                        Email = "wildwolf@gmail.com",
                        RoleId = 5,
                        Role = content.Role.FirstOrDefault(p => p.Name == "Sponsor"),
                        Age = "20"
                    },
                    new User
                    {
                        Name = "Slawa",
                        Surname = "Marlow",
                        Nickname = "SlawaMarlow",
                        Password = "Password1234!",
                        Phone = "23755577",
                        Email = "slawamarlow@gmail.com",
                        RoleId = 6,
                        Role = content.Role.FirstOrDefault(p => p.Name == "TeamCoach"),
                        Age = "21"
                    },
                    new User
                    {
                        Name = "Tomas",
                        Surname = "Nestr",
                        Nickname = "MrKubik",
                        Password = "Password1234!",
                        Phone = "28855577",
                        Email = "kubik@gmail.com",
                        RoleId = 6,
                        Role = content.Role.FirstOrDefault(p => p.Name == "TeamCoach"),
                        Age = "23"
                    }

                );
            }
            content.SaveChanges();
            if (!content.Streamer.Any())
            {
                Streamer streamer = new Streamer
                {
                    UserId = content.User.FirstOrDefault(p => p.Role.Name == "Streamer").Id,
                    User = content.User.FirstOrDefault(p => p.Role.Name == "Streamer"),
                    YouTubeSubs = "100000",
                    TwitchSubs = "250000",
                    AvgViewNumYT = "14000",
                    AvgViewNumTW = "29000"
                };
                content.AddRange(streamer);
            }
            content.SaveChanges();
            if (!content.Player.Any())
            {
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        NickName = "GoneFludd",
                        WinGamePercent = 73,
                        KPD = 54,
                        WinTournament = 28,
                        UserId = content.User.FirstOrDefault(p => p.Role.Name == "Player").Id,
                        TeamId = 1
                    }
                );
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        TeamId = 1
                    }
                );
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        TeamId = 1
                    }
                );
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        TeamId = 2
                    }
                );
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        TeamId = 2
                    }
                );
                content.AddRange(
                    new Player
                    {
                        Name = "Player",
                        TeamId = 2
                    }
                );
            }
            if (!content.Sponsor.Any())
            {
                User user = content.User.FirstOrDefault(p => p.RoleId == 5);
                if (user != null)
                {
                    content.AddRange(
                    new Sponsor
                    {
                        UserId = user.Id
                    }
                );
                }
                
            }
            if (!content.TeamCoach.Any())
            {
                content.AddRange(
                    new TeamCoach
                    {
                        Name = "TeamCoach",
                        NickName = "SlawaMarlow",
                        UserId = content.User.FirstOrDefault(p => p.RoleId == 6 && p.Nickname == "SlawaMarlow").Id,
                        TeamName = "Navy"
                    }
                );
            }
            if (!content.TeamCoach.Any())
            {
                content.AddRange(
                    new TeamCoach
                    {
                        Name = "TeamCoach",
                        NickName = "MrKubik",
                        UserId = content.User.FirstOrDefault(p => p.RoleId == 6 && p.Nickname == "MrKubik").Id,
                        TeamName = "Belan"
                    }
                );
            }
            content.SaveChanges();
        }
    }
}

