using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CustomerListView
    {
        #region
        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int? SalePointId { get; set; }

        [Display(Name = "Area Name")]
        public IEnumerable<SelectListItem> arealist { get; set; }
        public int? AreaId { get; set; }

        [Display(Name = "Sub Area Name")]
        public IEnumerable<SelectListItem> subarealist { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public int? SubAreaId { get; set; }

        [Display(Name = "Due Range Start Value")]
        public double? DueMin { get; set; }

        [Display(Name = "Due Range End Value")]
        public double? DueMax { get; set; }

        [Display(Name = "Mobile Number")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Display(Name = "Rate")]
        public double? RateMin { get; set; }

        [Display(Name = "Rate")]
        public double? RateMax { get; set; }

        public IEnumerable<SelectListItem> selectedcustomerlist { get; set; }
        public int? CustomerID { get; set; }

        public List<Customer> customerlist { get; set; }
        #endregion
    }
}