using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ProductionInfo
    {
        [Display(Name = "Product Name")]
        public IEnumerable<SelectListItem> productlist { get; set; }
        public int? PKProductId { get; set; }

        [Display(Name = "Production Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Production Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount required")]
        public double? Amount { get; set; }
    }
}