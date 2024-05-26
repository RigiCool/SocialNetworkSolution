using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Message
    {
        public long Id { set; get; }
        public long ChatId { set; get; }
        public string Content { set; get; }
        public string SenderNickName { set; get; }
        public DateTime SendTime { set; get; }
    }
}
