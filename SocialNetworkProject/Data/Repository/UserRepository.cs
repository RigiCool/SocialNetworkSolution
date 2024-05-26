using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Repository
{
    public class UserRepository : IAllUsers
    {
        private AppDBContent appDBContent;
        private IAllRoles C_Role;
        private IAllTeams C_Team;
        private IAllTournaments C_Tournament;
        public UserRepository(AppDBContent appDBContent, IAllRoles C_Role, IAllTeams C_Team, IAllTournaments C_Tournament)
        {
            this.appDBContent = appDBContent;
            this.C_Role = C_Role;
            this.C_Team = C_Team;
            this.C_Tournament = C_Tournament;
        }
        public IEnumerable<User> GetUsers()
        {
            return appDBContent.User.Include(user => user.Role).Select(user => new User { Id = user.Id, Nickname = user.Nickname, Name = user.Name, Surname = user.Surname, ImageData = user.ImageData, Role = user.Role }).ToList();
        }
        public async Task<BaseResponse<ClaimsIdentity>> Register(User IncomingUser)
        {
            if (DoesUserExist(IncomingUser.Nickname))
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 404,
                    Success = true,
                    Message = "User with this name exist"
                };
            }
            else
            {
                if (DoesEmailExist(IncomingUser.Email))
                {
                    return new BaseResponse<ClaimsIdentity>
                    {
                        StatusCode = 404,
                        Success = true,
                        Message = "User with this email exist"
                    };
                }
                else
                {
                    switch (IncomingUser.RoleId)
                    {
                        case 1:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Admin");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Admin.Add(new Admin { Name = "Admin", UserId = IncomingUser.Id });
                            break;
                        case 2:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Gamer");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Gamer.Add(new Gamer { Name = "Gamer", UserId = IncomingUser.Id });
                            break;
                        case 3:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Streamer");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Streamer.Add(new Streamer { Name = "Streamer", UserId = IncomingUser.Id });
                            break;
                        case 4:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Commentator");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Commentator.Add(new Commentator { Name = "Commentator", UserId = IncomingUser.Id });
                            break;
                        case 5:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Sponsor");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Sponsor.Add(new Sponsor { Name = "Sponsor", UserId = IncomingUser.Id });
                            break;
                        case 6:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "TeamCoach");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.TeamCoach.Add(new TeamCoach { Name = "TeamCoach", NickName = IncomingUser.Nickname, UserId = IncomingUser.Id });
                            break;
                        case 7:
                            IncomingUser.Role = appDBContent.Role.FirstOrDefault(p => p.Name == "Player");
                            appDBContent.User.Add(IncomingUser);
                            appDBContent.SaveChanges();
                            appDBContent.Player.Add(new Player { Name = "Player", NickName = IncomingUser.Nickname, UserId = IncomingUser.Id });
                            break;
                    }
                    appDBContent.SaveChanges();
                    var response = Authenticate(IncomingUser);
                    return new BaseResponse<ClaimsIdentity>
                    {
                        StatusCode = 200,
                        Success = true,
                        Data = response,
                        Message = "OK"
                    };
                }
            }
        }
        public async Task<BaseResponse<ClaimsIdentity>> Login(User IncomingUser)
        {
            var ValidUser = appDBContent.User.Include(user => user.Role).FirstOrDefault(user => user.Nickname == IncomingUser.Nickname);

            if (ValidUser==null || IncomingUser.Password==null)
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 500,
                    Success = true,
                    Message = "Error. Null reference exception."
                };
            }

            if (IncomingUser.Password == ValidUser.Password)
            {
                var response = Authenticate(ValidUser);
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 200,
                    Success = true,
                    Data = response,
                    Message = "OK"
                };
            }
            else
            {
                return new BaseResponse<ClaimsIdentity>
                {
                    StatusCode = 404,
                    Success = true,
                    Message = "User was not found"
                };
            }
        }
        public bool DoesUserExist(string validStr)
        {
            // Check if a user with the given username exists in the database
            return appDBContent.User.Any(user => user.Nickname == validStr);
        }
        public bool DoesEmailExist(string validStr)
        {
            // Check if a user with the given username exists in the database
            return appDBContent.User.Any(user => user.Email == validStr);
        }
        private ClaimsIdentity Authenticate(User user)
        {
            var claims = new List<Claim>{
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Nickname),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name.ToString())
            };
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            return id;
        }
        public User GetUser(string nickname)
        {
            User user = appDBContent.User.Include(user => user.Role).FirstOrDefault(user => user.Nickname == nickname);
            return user;
        }

        public byte[] GetImage(long id)
        {
            return appDBContent.User.FirstOrDefault(user => user.Id == id).ImageData;
        }

        public void SaveImage(byte[] ImageDate, string Nickname)
        {
            User user = GetUser(Nickname);
            user.ImageData = ImageDate;
            appDBContent.SaveChanges();
        }

        public void DeleteUser(string NickName)
        {
            User user = GetUser(NickName);
            IUserRole userRole = C_Role.GetUserRole(user);
            if (userRole is Player player)
            {
                appDBContent.Player.Remove(player);
            }
            else if (userRole is Gamer gamer)
            {
                appDBContent.Gamer.Remove(gamer);
            }
            else if (userRole is Commentator commentator)
            {
                appDBContent.Commentator.Remove(commentator);
            }
            else if (userRole is Sponsor sponsor)
            {
                List<long> TournamentsOlympicId = appDBContent.TournamentOlympic.Where(to => to.SponsorId == user.Id).Select(to => to.Id).ToList();
                foreach (long id in TournamentsOlympicId)
                {
                    C_Tournament.DeleteTournamentOlympic(id);
                }

                List<long> TournamentsListId = appDBContent.TournamentList.Where(to => to.SponsorId == user.Id).Select(to => to.Id).ToList();
                foreach (long id in TournamentsListId)
                {
                    C_Tournament.DeleteTournamentOlympic(id);
                }
                appDBContent.Sponsor.Remove(sponsor);
            }
            else if (userRole is Streamer streamer)
            {
                appDBContent.Streamer.Remove(streamer);
            }
            else if (userRole is TeamCoach teamcoach)
            {
                Team team = appDBContent.Team.FirstOrDefault(t => t.TeamCoach == user.Id);
                if(team != null)
                {
                    C_Team.DeleteTeam(team.Id);
                    appDBContent.TeamCoach.Remove(teamcoach);
                }
            }

            List<Post> posts = appDBContent.Post.Where(p => p.UserId == user.Id).ToList();
            appDBContent.Post.RemoveRange(posts);

            appDBContent.User.Remove(user);
            appDBContent.SaveChanges();
        }
    }
}
