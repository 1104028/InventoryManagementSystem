using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class SingleSaleAdd
    {
        [Display(Name = "Selling Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Date required")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Selling Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Amount required")]
        public double Amount { get; set; }

        [Display(Name = "Selling Rate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Rate required")]
        public double Rate { get; set; }

        [Display(Name = "Service Charge")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Service Charge required")]
        public double ServiceCharge { get; set; }

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

        [Display(Name = "Customer Name")]
        public IEnumerable<SelectListItem> customerlist { get; set; }
        public int CustomerId { get; set; }

        [Display(Name = "Customer Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Address required")]
        [MaxLength(250, ErrorMessage = "Address can be Maximum 250 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Display(Name = "Product Name")]
        public IEnumerable<SelectListItem> productlist { get; set; }
        public int ProductId { get; set; }
       
    }
}