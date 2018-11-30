using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ProductSaleAdd
    {

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime SaleDate { get; set; }

        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Sale point required")]
        public int SalePointId { get; set; }

        [Display(Name = "Area ID")]
        public IEnumerable<SelectListItem> Area { get; set; }
        public int? AreaID { get; set; }

        [Display(Name = "Sub Area ID")]
        public IEnumerable<SelectListItem> SubArea { get; set; }
        public int? SubAreaID { get; set; }

        [Display(Name = "Customer")]
        public IEnumerable<SelectListItem> Customer { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer required")]
        public int CustomerID { get; set; }

        public double? BillPaid { get; set; }

        public string CustomerInfo { get; set; } 

        public IEnumerable<SelectListItem> product { get; set; }

        public List<ProductSaleInfo> ProductSaleInfo { get; set; }
    }
}