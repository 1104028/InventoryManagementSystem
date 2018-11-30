using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SalePointProductConsume
    {
        #region

        [Key]
        public int PKSalePointProductConsumeId { get; set; }

        [Display(Name = "Production Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Production Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount required")]
        public double Amount { get; set; }

        [Required]
        [Column(Order = 1)]
        [ForeignKey("product")]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }

        [Column(Order = 2)]
        [ForeignKey("salepoint")]
        public int SalePointId { get; set; }
        public virtual SalePoint salepoint { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
        [DefaultValue(0)]
        [Display(Name = "Total In Stock")]
        public double ProductStock { get; set; }
        #endregion
    }
}