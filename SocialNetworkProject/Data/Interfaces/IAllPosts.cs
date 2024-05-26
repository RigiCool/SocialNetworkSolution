using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Interfaces
{
    public interface IAllPosts
    {
        public IEnumerable<Post> GetPosts(long id);
        public IEnumerable<Post> GetUserPosts(string Nickname);
        public void CreatePost(Post post);
    }
}
