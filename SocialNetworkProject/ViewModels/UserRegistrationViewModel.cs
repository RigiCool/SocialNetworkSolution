using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class UserRegistrationViewModel
    {
        public IEnumerable<Role> Roles { get; set; }
        public User User { get; set; }
    }
}
