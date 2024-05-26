using SocialNetworkProject.Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.ViewModels
{
    public class NewTournamentViewModel
    {
        [Required(ErrorMessage = "The Tournament Name is required.")]
        [RegularExpression(@"^[A-Z].*", ErrorMessage = "The Tournament Name field must start with a capital letter.")]
        [StringLength(15, ErrorMessage = "Tournament Name must be between 3 and 20 characters", MinimumLength = 3)]
        public string TournamentName { set; get; }
        [Required(ErrorMessage = "The Tournament Type is required.")]
        public string TournamentType { set; get; }
        public string SponsorNickName { set; get; }
        [Range(10, ulong.MaxValue, ErrorMessage = "The Prize Money minimum is {1}.")]
        [RegularExpression(@"^\d+$", ErrorMessage = "The PrizeMoney field must be a whole number.")]
        public long Prize { set; get; }
        [Required(ErrorMessage = "The Start Date is required.")]
        public DateTime StartDate { set; get; }
        [Required(ErrorMessage = "The End Date is required.")]
        public DateTime EndDate { set; get; }
    }
}
