using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("BILLINGHISTORY")]
    public class BillingHistory
    {
        #region
        [Key]
        public int PKBillId { get; set; }

        [Display(Name = "Bill Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Billing Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Required]
        [ForeignKey("customer")]
       
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public virtual Customer customer { get; set; }

       

        [Display(Name = "Bill Paid")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Paid Amount required")]
        public double BillPaid { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
             
        #endregion
    }
}