using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FoodWebAppMvc.Models
{
    public class SignupLogin
    {
        [Key]
        public int userid { get; set; }
        [StringLength(50)]
        [RegularExpression(@"^[a-zA-Z. ]+$", ErrorMessage = "Name should only contain letters, spaces, and dots")]
        [Required(ErrorMessage = "Name Required")]
        public string Name { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage = "Email Required")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid email format")]
        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Password Required")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{6,}$", ErrorMessage = "Your password must be have at least,6 characters long,1 uppercase & 1 lowercase character,1 number")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [StringLength(50)]
        [Required(ErrorMessage ="Confirm Password Required!")]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*[a-z])(?=.*[0-9])(?=.*[^A-Za-z0-9]).{6,}$", ErrorMessage = "Your password must be have at least,6 characters long,1 uppercase & 1 lowercase character,1 number")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password not Matched")]
        public string ConfirmPassword { get; set; }

        public bool RememberMe { get; set; } 

    }
}