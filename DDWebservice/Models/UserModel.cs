using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace DDWebservice.Models
{
    public class UserModel
    {
       [Required]
      public  string Email { get; set; }
      [Required]
      public string FirstName { get; set; }
      [Required]
        public string FamilyName { get; set; }
    }
}