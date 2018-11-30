using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CustomerRegistration
    {
        [Display(Name = "Customer Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Full Name required")]
        public string FullName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Address required")]
        [MaxLength(250, ErrorMessage = "Address can be Maximum 250 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int SalePointId { get; set; }

        [Display(Name = "Area Name")]
        public IEnumerable<SelectListItem> arealist { get; set; }
        public int AreaId { get; set; }

        [Display(Name = "Sub Area Name")]
        public IEnumerable<SelectListItem> subarealist { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public int SubAreaId { get; set; }

        [Display(Name = "Rate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Milk Rate Per Litre required")]
        public double Rate { get; set; }

        [Display(Name = "Service Charge")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Service Charge required")]
        public double ServiceCharge { get; set; }

        [Display(Name = "Previous Bill")]
        public double PreviousBill { get; set; }

        public bool SMS { get; set; }
    }
}