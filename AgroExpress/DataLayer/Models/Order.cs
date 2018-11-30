using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class Order
    {
        [Key]
        public int PKOrderId { get; set; }

        [Required]
        [ForeignKey("customer")]
        [Column(Order = 1)]
        [Display(Name = "Customer Name")]
        public int CustomerId { get; set; }
        public virtual Customer customer { get; set; }


        [Required]
        [ForeignKey("product")]
        [Column(Order = 2)]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }


        [Display(Name = "Order Placing Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order Date required")]
        [DataType(DataType.Date)]
        public DateTime OrderPlacingDateTime { get; set; }


        [Display(Name = "Order Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Order Date required")]
        [DataType(DataType.Date)]
        public DateTime OrderDateTime { get; set; }



        [Display(Name = "Selling Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Selling Amount required")]
        public double Amount { get; set; }

        [DefaultValue(false)]
        public bool IsDelivered { get; set; }
 
    }
}