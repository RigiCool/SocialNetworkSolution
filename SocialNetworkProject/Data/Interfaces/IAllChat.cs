using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllChat
    {
        public Chat CreateChat(string user1Nickname, string user2Nickname);
        public void CreateMessage(Message message, string owner);
        public void DeleteChat(long id);
        public List<Message> GetMessages(long id);
        public Message GetMessage(long id);
        public Chat GetChat(string owner, string correspondent);
        public List<Chat> GetChats(string currentUser);
    }
}
