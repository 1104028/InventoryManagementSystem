using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ProductSaleInfo
    {
        [Display(Name = "Amount")]
        public double? Amount { get; set; }

        public int? ProductId { get; set; }

        public double? Rate { get; set; }
        
        public double? Servicecharge { get; set; }
    }
}