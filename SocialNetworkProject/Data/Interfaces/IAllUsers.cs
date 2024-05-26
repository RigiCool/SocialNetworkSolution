using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllUsers
    {
        public IEnumerable<User> GetUsers();
        public Task<BaseResponse<ClaimsIdentity>> Register(User IncomingUser);
        public Task<BaseResponse<ClaimsIdentity>> Login(User IncomingUser);
        public User GetUser(string nickname);
        public void SaveImage(byte[] ImageDate, string Nickname);
        public byte[] GetImage(long id);
        public void DeleteUser(string NickName);
    }
}