using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class NewTeamViewModel
    {
        public int Id { set; get; }
        [Required(ErrorMessage = "The Team Name is required.")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Team Name field must start with a capital letter.")]
        [StringLength(15, ErrorMessage = "Team Name must be between 3 and 20 characters", MinimumLength = 3)]
        public string TeamName { set; get; }
        [RegularExpression(@"^(?!0+$).*$", ErrorMessage = "The Player count is required.")]
        public int PlayerCount { set; get; }
        public string TeamCoachNickName { set; get; }
    }
}
