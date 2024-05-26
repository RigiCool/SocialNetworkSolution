using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Admin : IUserRole
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public long UserId { set; get; }
    }
}
