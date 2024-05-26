using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class ProfileViewModel<T> where T : IUserRole
    {
        public long Id { set; get; }
        public string Nickname { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        public string Age { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public byte[] ImageData { get; set; }
        public long RoleId { get; set; }
        public string RoleName { get; set; }
        public T Role { get; set; }
        public IEnumerable<Post> Posts { get; set; }
        public bool InSubscriptions { get; set; }
        public string Description { get; set; }
        public List<string> Errors { get; set; }
    }
}
