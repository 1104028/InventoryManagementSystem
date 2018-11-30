using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class SaleList
    {
        [Display(Name = "Selling Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Selling Amount")]
        public double Amount { get; set; }

        [Display(Name = "Total")]
        public double Total { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
       
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        public bool SMSSent { get; set; }

        [Display(Name = "Operator Name")]
        public string OperatorName { get; set; }
    }
}