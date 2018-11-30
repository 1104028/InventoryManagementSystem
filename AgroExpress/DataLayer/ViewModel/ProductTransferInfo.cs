using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ProductTransferInfo
    {
        [Display(Name = "Amount")]
        public double? Amount { get; set; }

        public int? ProductId { get; set; }
    }
}