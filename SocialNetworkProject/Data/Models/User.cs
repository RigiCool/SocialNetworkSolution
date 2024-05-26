using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetworkProject.Data.Models
{
    public class User
    {
        public long Id { set; get; }
        [Required(ErrorMessage = "Nickname is required")]
        [StringLength(15, ErrorMessage = "Nickname must be between 1 and 20 characters", MinimumLength = 3)]
        public string Nickname { set; get; }
        [Required(ErrorMessage = "Password is required")]
        [StringLength(100, ErrorMessage = "Password must be between 8 and 100 characters", MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must have at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string Password { set; get; }
        [Required(ErrorMessage = "Name is required")]
        [StringLength(20, ErrorMessage = "Name must be between 3 and 20 characters", MinimumLength = 3)]
        public string Name { set; get; }
        [Required(ErrorMessage = "Surname is required")]
        [StringLength(20, ErrorMessage = "Surname must be between 3 and 20 characters", MinimumLength = 3)]
        public string Surname { set; get; }
        [Required(ErrorMessage = "Age is required")]
        [Range(1, 120, ErrorMessage = "Age must be a valid number between 1 and 120")]
        public string Age { get; set; }
        [Required(ErrorMessage = "Phone is required")]
        [RegularExpression(@"^\+?[0-9]{1,3}-?[0-9]{3,14}$", ErrorMessage = "The phone field must be a valid phone number.")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address format")]
        public string Email { get; set; }
        public byte[] ImageData { get; set; }
        [Required(ErrorMessage = "RoleId is required")]
        public long RoleId { get; set; }
        public virtual Role Role { get; set; }
    }
}
