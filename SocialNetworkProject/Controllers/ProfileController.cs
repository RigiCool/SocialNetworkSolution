using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using SocialNetworkProject.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class ProfileController : Controller
    {
        private readonly IAllUsers C_User;
        private readonly IAllRoles C_Role;
        private readonly IAllPosts C_Post;
        public ProfileController(IAllUsers C_User, IAllRoles C_Role, IAllPosts C_Post)
        {
            this.C_User = C_User;
            this.C_Role = C_Role;
            this.C_Post = C_Post;
        }
        [HttpGet]
        public IActionResult Profile()
        {
            string[]? errorsArray = TempData["Errors"] as string[];
            List<string>? errors = errorsArray != null ? errorsArray.ToList() : null;
            User user =C_User.GetUser(User.Identity.Name);
            IUserRole userRole = C_Role.GetUserRole(user);
            IEnumerable<Post> UserPosts = C_Post.GetPosts(user.Id);
            var ProfileObj = new ProfileViewModel<IUserRole>
            {
                Id = user.Id,
                Nickname = user.Nickname,
                Name = user.Name,
                Surname = user.Surname,
                Age = user.Age,
                Phone = user.Phone,
                Email = user.Email,
                ImageData = user.ImageData,
                RoleId = user.RoleId,
                RoleName = user.Role.Name,
                Role = userRole,
                Posts = UserPosts,
                Errors = errors
            };
            return View(ProfileObj);
        }
        [HttpPost]
        public IActionResult ProfileForGamer(ProfileViewModel<Gamer> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as Gamer);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }

        }

        [HttpPost]
        public IActionResult ProfileForStreamer(ProfileViewModel<Streamer> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as Streamer);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }
        }
        [HttpPost]
        public IActionResult ProfileForCommentator(ProfileViewModel<Commentator> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as Commentator);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }
        }

        [HttpPost]
        public IActionResult ProfileForSponsor(ProfileViewModel<Sponsor> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as Sponsor);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }
        }
        [HttpPost]
        public IActionResult ProfileForTeamCoach(ProfileViewModel<TeamCoach> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as TeamCoach);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }
        }

        [HttpPost]
        public IActionResult ProfileForPlayer(ProfileViewModel<Player> ProfileObj)
        {
            if (ModelState.IsValid)
            {
                C_Role.UpdateInfo(ProfileObj.Id, ProfileObj.Role as Player);
                return RedirectToAction("Profile", "Profile");
            }
            else
            {
                List<string> errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage).ToList();
                TempData["Errors"] = errors;
                return RedirectToAction("Profile", "Profile");
            }
        }
        [HttpGet]
        public IActionResult News()
        {
            return PartialView("News");
        }
        [HttpPost]
        public IActionResult SaveImage(IFormFile imageData)
        {
            if (imageData != null && imageData.Length > 0)
            {
                if (!imageData.ContentType.StartsWith("image/"))
                {
                    string[] errors = new string[1];
                    errors[0] = "The uploaded file is not a valid";
                    TempData["Errors"] = errors;
                    return RedirectToAction("Profile", "Profile");
                }

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    imageData.CopyTo(memoryStream);
                    byte[] ImageData = memoryStream.ToArray();
                    C_User.SaveImage(ImageData, User.Identity.Name);
                    return RedirectToAction("Profile", "Profile");
                }
            }
            else
            {
                return RedirectToAction("Profile", "Profile");
            }
        }
        [HttpPost]
        public IActionResult DeleteUser()
        {
            C_User.DeleteUser(User.Identity.Name);
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Registration", "RegistrationLogin");
        }
    }
}
