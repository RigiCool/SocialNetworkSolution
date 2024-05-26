using Microsoft.EntityFrameworkCore;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Repository
{
    public class ChatRepository:IAllChat
    {
        private AppDBContent appDBContent;
        public ChatRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public Chat CreateChat(string user1Nickname, string user2Nickname)
        {
            User user1 = appDBContent.User.FirstOrDefault(u1 => u1.Nickname == user1Nickname);
            User user2 = appDBContent.User.FirstOrDefault(u2 => u2.Nickname == user2Nickname);
            Chat NewChat = new Chat
            {
                User1 = user1,
                User2 = user2,
            };
            appDBContent.Chat.Add(NewChat);
            appDBContent.SaveChanges();
            return NewChat;
        }

        public void CreateMessage(Message message, string owner)
        {
            if (message.Content != null)
            {
                User user = appDBContent.User.FirstOrDefault(u => u.Nickname == owner);
                message.SenderNickName = user.Nickname;
                message.SendTime = DateTime.Now;
                appDBContent.Message.Add(message);
                appDBContent.SaveChanges();
            }
        }

        public void DeleteChat(long id)
        {
            List<Message> DeleteMessages = appDBContent.Message.Where(m => m.ChatId == id).ToList();
            appDBContent.Message.RemoveRange(DeleteMessages);
            Chat DeleteChat = appDBContent.Chat.FirstOrDefault(c => c.id == id);
            appDBContent.Chat.Remove(DeleteChat);
            appDBContent.SaveChanges();
        }

        public Chat GetChat(string owner, string correspondent)
        {
            Chat chat = appDBContent.Chat.Include(c => c.User1).Include(c => c.User2).FirstOrDefault(c => (c.User1.Nickname == owner && c.User2.Nickname == correspondent) || (c.User1.Nickname == correspondent && c.User2.Nickname == owner));
            if (chat != null)
            {
                chat.Messages = GetMessages(chat.id);
            }
            return chat;
        }

        public List<Chat> GetChats(string currentUser)
        {
            return appDBContent.Chat.Where(c => c.User1.Nickname == currentUser || c.User2.Nickname == currentUser)
            .Select(c => new Chat
            {
                id = c.id,

                User1 = new User
                {
                    Id = c.User1.Id,
                    Nickname = c.User1.Nickname,
                    Name = c.User1.Name,
                    Surname = c.User1.Surname,
                    ImageData = c.User1.ImageData,
                    Role = c.User1.Role
                },

                User2 = new User
                {
                    Id = c.User2.Id,
                    Nickname = c.User2.Nickname,
                    Name = c.User2.Name,
                    Surname = c.User2.Surname,
                    ImageData = c.User2.ImageData,
                    Role = c.User2.Role
                }
            })
            .ToList();

        }

        public Message GetMessage(long id)
        {
            return appDBContent.Message.FirstOrDefault(m => m.Id == id);
        }


        public List<Message> GetMessages(long id)
        {
            return appDBContent.Message.Where(m => m.ChatId == id).OrderBy(m => m.SendTime).ToList();
        }
    }
}
