using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("PRODUCT")]
    public class Product
    {
        #region
        [Key]
        public int PKProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Name required")]
        public string ProductName { get; set; }

        [Display(Name = "Product Selling Unit")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Product Selling Unit required")]
        public string SellingUnit { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Product In Stock")]
        public double Stock { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Production> production { get; set; }
        public virtual ICollection<SalePointProductStock> salepoinproduct { get; set; }
        #endregion
    }
}