using SocialNetworkProject.Data.Interfaces;
using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Repository
{
    public class PostRepository : IAllPosts
    {
        private AppDBContent appDBContent;
        public PostRepository(AppDBContent appDBContent)
        {
            this.appDBContent = appDBContent;
        }

        public IEnumerable<Post> GetPosts(long id)
        {
            return appDBContent.Post.Where(post => post.UserId == id).OrderByDescending(post => post.Date).ToList();
        }

        public IEnumerable<Post> GetUserPosts(string Nickname)
        {
            User CurrentUser = appDBContent.User.FirstOrDefault(u => u.Nickname == Nickname);
            List<Post> Posts = new List<Post>();
            if (CurrentUser != null)
            {
                List<Subscription> Subscriptions = appDBContent.Subscription.Where(s => s.OwnerId == CurrentUser.Id).ToList();
                DateTime CurrentDate = DateTime.Now;
                List<Post> FilteredPosts = new List<Post>();
                foreach (var subscription in Subscriptions)
                {
                    FilteredPosts = appDBContent.Post.Where(p => p.UserId == subscription.UserId).AsEnumerable().Where(p => (CurrentDate - p.Date).Days <= 3).OrderByDescending(p => p.Date).ToList();

                    Posts.AddRange(FilteredPosts);
                }
                List<long> excludedUserIds = appDBContent.Subscription.Where(u => u.OwnerId == CurrentUser.Id).Select(u => u.UserId).ToList();
                List<Post> posts = appDBContent.Post.ToList();
                FilteredPosts = appDBContent.Post.Where(p => !excludedUserIds.Contains(p.UserId)).AsEnumerable().Where(p => (CurrentDate - p.Date).Days <= 3).OrderByDescending(p => p.Date).ToList();
                Posts.AddRange(FilteredPosts);
            }
            return Posts;
        }
        public void CreatePost(Post post)
        {
            post.Date = DateTime.Now;
            appDBContent.Post.Add(post);
            appDBContent.SaveChanges();
        }
    }
}
