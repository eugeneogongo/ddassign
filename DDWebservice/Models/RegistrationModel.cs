using System.ComponentModel.DataAnnotations;


namespace DDWebservice.Models
{
    public class RegistrationUserModel
    {
       [Required (AllowEmptyStrings = false)]
       [EmailAddress]
        public  string Email { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FirstName { get; set; }
        [Required(AllowEmptyStrings = false)]
        public string FamilyName { get; set; }
        [Required(AllowEmptyStrings = false)]
        [MinLength(6),MaxLength(32)]
        public  string Password { get; set; }
    }
}