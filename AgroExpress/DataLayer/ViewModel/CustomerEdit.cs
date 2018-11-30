using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.DataLayer.ViewModel
{
    public class CustomerEdit
    {

        public int PKCustomerId { get; set; }

        public int LoginUserID { get; set; }

        [Display(Name = "Customer Full Name")]
        public string FullName { get; set; }

        [Display(Name = "Customer Address")]
        [MaxLength(250, ErrorMessage = "Address can be Maximum 250 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
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
        public int SubAreaId { get; set; }

        [Display(Name = "Rate")]
        public double Rate { get; set; }

        [Display(Name = "Service Charge")] 
        public double ServiceCharge { get; set; }

        [Display(Name = "Total Bill")]
        public double TotalBill { get; set; }

        [Display(Name = "Total paid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Total Paid Bill required")]
        public double TotalPaid { get; set; }

        public string UserName { get; set; }

        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }

        public bool SMS { get; set; }

    }
}