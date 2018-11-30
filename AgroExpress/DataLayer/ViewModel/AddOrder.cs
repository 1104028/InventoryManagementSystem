using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AddOrder
    {
        [Display(Name = "Product Name")]
        public IEnumerable<SelectListItem> productlist { get; set; }
        public int PKProductId { get; set; }

        [Display(Name = "Order Placing Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order Date required")]
        [DataType(DataType.Date)]
        public DateTime OrderPlacingDateTime { get; set; }

        [Display(Name = "Selling Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Amount required")]
        public double Amount { get; set; }
    }
}