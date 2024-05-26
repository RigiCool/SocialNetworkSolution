using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class ChatViewModel
    {
        public Chat Chat { get; set; }
        public Message NewMessage { get; set; }
        public Message DeleteMessage { get; set; }
    }
}
