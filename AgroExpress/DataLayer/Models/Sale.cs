using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class Sale
    {
        #region
        [Key]
        public int PKSaleId { get; set; }

        [Display(Name = "Selling Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Date required")]
        [DataType(DataType.Date)]
        public DateTime DateTime { get; set; }

        [Display(Name = "Selling Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Amount required")]
        public double Amount { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Selling Rate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Rate required")]
        public double Rate { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Service Charge")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Service Charge required")]
        public double ServiceCharge { get; set; }


        
        [ForeignKey("customer")]
        [Column(Order =1)]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public virtual Customer customer { get; set; }

       
        [ForeignKey("product")]
        [Column(Order = 2)]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }

       


        [DefaultValue(false)]
        public bool SMSSent { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }

        #endregion
    }
}