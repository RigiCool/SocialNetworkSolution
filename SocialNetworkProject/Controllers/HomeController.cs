using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkForGamers_Project.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAllPlayers playerRep;
        private readonly IAllPosts postRep;

        public HomeController(IAllPlayers PlayerRep, IAllPosts PostRep)
        {
            playerRep = PlayerRep;
            postRep = PostRep;
        }
        public IActionResult Index()
        {
            var HomePlayer = new HomeViewModel
            {
                Posts = postRep.GetUserPosts(User.Identity.Name)
            };
            return View(HomePlayer);
        }
        public IActionResult Start()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View();
            }
        }
    }
}
