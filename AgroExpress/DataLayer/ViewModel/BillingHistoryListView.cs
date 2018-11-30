using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class BillingHistoryListView
    {
        [Display(Name = "Bill Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Billing Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Customer Name")]
        public string CustomerName { get; set; }
     
        [Display(Name = "Bill Paid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Paid Amount required")]
        public double BillPaid { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
    }
}