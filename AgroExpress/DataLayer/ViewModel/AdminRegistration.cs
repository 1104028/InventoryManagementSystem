using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AdminRegistration
    {
        [Display(Name = "Admin Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Admin Full Name required")]
        public string FullName { get; set; }


        [Display(Name = "Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address required")]
        [MaxLength(200, ErrorMessage = "Address can be Maximum 200 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Display(Name = "Email Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email Address required")]
        public string Email { get; set; }

        [Display(Name = "Admin User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Admin User Name required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

    }
}