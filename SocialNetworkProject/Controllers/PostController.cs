using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Controllers
{
    public class PostController : Controller
    {
        private readonly IAllUsers C_User;
        private readonly IAllPosts C_Post;
        public PostController(IAllPosts C_Post, IAllUsers C_User)
        {
            this.C_Post = C_Post;
            this.C_User = C_User;
        }
        [HttpGet]
        public IActionResult Post()
        {
            return View("Post"); // Return the modal view
        }
        [HttpPost]
        public IActionResult Post(Post model, IFormFile imageData)
        {
            if (ModelState.IsValid)
            {
                if (imageData != null && imageData.Length > 0)
                {
                    if (!imageData.ContentType.StartsWith("image/"))
                    {
                        TempData["Errors"] = "The uploaded file is not a valid";
                        return View(model);
                    }

                    using (var memoryStream = new MemoryStream())
                    {
                        imageData.CopyTo(memoryStream);
                        model.ImageData = memoryStream.ToArray();
                    }
                    User user = C_User.GetUser(User.Identity.Name);
                    model.UserId = user.Id;
                    // Save the model to the database or perform other necessary actions
                    C_Post.CreatePost(model);
                    return RedirectToAction("Profile", "Profile");
                }
                else
                {
                    TempData["Errors"] = "The file was not uploaded";
                    return View(model);
                }
            }
            else
            {
                return View(model);
            }
            

        }
    }
}
