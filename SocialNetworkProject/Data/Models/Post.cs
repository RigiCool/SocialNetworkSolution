using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Post
    {
        public long Id { set; get; }
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Title field must start with a capital letter.")]
        public string Title { set; get; }
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Content field must start with a capital letter.")]
        public string Text { set; get; }
        public DateTime Date { set; get; }
        public byte[] ImageData { get; set; }
        public long UserId { get; set; }
    }
}
