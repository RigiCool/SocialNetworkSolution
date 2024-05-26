using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class ChatController : Controller
    {
        private readonly IAllUsers C_User;
        private readonly IAllChat C_Chat;
        public ChatController(IAllChat C_Chat, IAllUsers C_User)
        {
            this.C_Chat = C_Chat;
            this.C_User = C_User;
        }
        
        [Route("Chat/Chat/{User}")]
        public IActionResult Chat(string User)
        {
            string correspondent = User;
            string currentUserNickName = HttpContext.User.Identity.Name;

            Chat chat = C_Chat.GetChat(currentUserNickName, correspondent);

            if(chat == null)
            {
                chat = C_Chat.CreateChat(currentUserNickName, correspondent);
            }

            ChatViewModel model = new ChatViewModel
            {
                Chat = chat
            };

            return View(model);
        }
        
        public IActionResult CreateMessage(ChatViewModel model)
        {
            model.NewMessage.ChatId = model.Chat.id;
            string currentUserNickName = User.Identity.Name;
            C_Chat.CreateMessage(model.NewMessage, currentUserNickName);
            return RedirectToAction("Chat", "Chat", new { User = (model.Chat.User1.Nickname == currentUserNickName) ? model.Chat.User2.Nickname : model.Chat.User1.Nickname });
        }

        public IActionResult DeleteChat(ChatViewModel model)
        {
            C_Chat.DeleteChat(model.Chat.id);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult Chats(ChatViewModel model)
        {
            string currentUserNickName = User.Identity.Name;
            List<Chat> chats = C_Chat.GetChats(currentUserNickName);
            return View(chats);
        }
    }
}
