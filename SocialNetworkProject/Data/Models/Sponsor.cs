using SocialNetworkProject.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class Sponsor : IUserRole
    {
        public long Id { set; get; }
        public string Name { set; get; }
        public string Surname { set; get; }
        [StringLength(50, ErrorMessage = "The Company field must be at most {1} characters long.")]
        [RegularExpression(@"^[A-Z].*$", ErrorMessage = "The Company field must start with a capital letter.")]
        public string Company { set; get; }
        [Url(ErrorMessage = "The CompanySite field must be a valid URL.")]
        public string CompanySite { set; get; }
        [RegularExpression(@"^\+?[0-9]{1,3}-?[0-9]{3,14}$", ErrorMessage = "The CompanyNumber field must be a valid phone number.")]
        public string CompanyNumber { set; get; }
        public bool InLeaders { set; get; }
        public long UserId { set; get; }
        public virtual User User { get; set; }
        [StringLength(200, ErrorMessage = "The Description field must be at most {1} characters long.")]
        public string Description { get; set; }
    }
}
