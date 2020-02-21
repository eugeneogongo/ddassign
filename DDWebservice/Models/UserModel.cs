using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace DDWebservice.Models
{
    public class UserModel
    {
       [Required(ErrorMessage = "Email Address is Required")]
      public  string Email { get; set; }
      public string FirstName { get; set; }
        public string FamilyName { get; set; }
    }
}