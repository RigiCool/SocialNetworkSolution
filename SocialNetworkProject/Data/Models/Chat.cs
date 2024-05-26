using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Chat
    {
        public long id { set; get; }
        public User User1 { set; get; }
        public User User2 { set; get; }
        public List<Message> Messages { set; get; }
    }
}
