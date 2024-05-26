using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class RegistrationLoginController:Controller
    {
        private readonly IAllUsers C_User;
        private readonly IAllRoles C_Role;
        public RegistrationLoginController(IAllUsers C_User, IAllRoles C_Role)
        {
            this.C_User = C_User;
            this.C_Role = C_Role;
        }
        IEnumerable<Role> C_Roles = null;
        [HttpGet]
        public IActionResult Registration()
        {
            C_Roles=C_Role.GetAllRoles;
            var UserObj = new UserRegistrationViewModel
            {
                User = null,
                Roles = C_Roles
            };
            return View(UserObj);
        }
        [HttpPost]
        public async Task<IActionResult> Registration(UserRegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = await C_User.Register(model.User);
                if (response.StatusCode == 200)
                {
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                }
                return RedirectToAction("Index", "Home");
            }
            model.Roles = C_Role.GetAllRoles;
            return View(model);
        }
        public IActionResult EmailVerification()
        {
            ViewBag.Message = "Done";
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(User user)
        {
            var response = await C_User.Login(user);
            if (response.StatusCode == 200)
            {
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(response.Data));
                return RedirectToAction("Index", "Home");
            }
            else
            {
                return View(user);
            }
        }
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Registration", "RegistrationLogin");
        }
    }
}
