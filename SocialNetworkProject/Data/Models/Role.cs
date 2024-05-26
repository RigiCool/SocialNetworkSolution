using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Role
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { set; get; }
        public string YouTubeLink { set; get; }
        public string TwitchLink { set; get; }
        public string Facebook { set; get; }
        public string Instagram { set; get; }
    }
}
