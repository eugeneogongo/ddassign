using System.ComponentModel.DataAnnotations;

namespace DDWebservice.Models
{
    public class UserLoginModel
    {
        [Required(AllowEmptyStrings = false)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(6), MaxLength(32)]
        public string Password { get; set; }
    }
}