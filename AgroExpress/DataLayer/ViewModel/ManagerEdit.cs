using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ManagerEdit
    {
        public int PKManagerId { get; set; }

        public int LoginUserID { get; set; }

        [Display(Name = "Full Name")]
        public string FullName { get; set; }


        [Display(Name = "Address")]
        [MaxLength(200, ErrorMessage = "Address can be Maximum 200 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Display(Name = "Email Address")]
        public string Email { get; set; }

        [Display(Name = "User Name")]
        public string UserName { get; set; }

        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int? SalePointId { get; set; }

        [Display(Name = "Selected Sale Points")]
        public string SelectedSalePoints { get; set; }
    }
}