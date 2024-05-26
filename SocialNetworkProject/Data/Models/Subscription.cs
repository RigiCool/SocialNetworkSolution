using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Subscription
    {
        public long Id { set; get; }
        public long OwnerId { set; get; }
        public long UserId { set; get; }
    }
}
