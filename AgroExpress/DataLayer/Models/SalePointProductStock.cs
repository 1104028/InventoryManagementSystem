using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SalePointProductStock
    {
        #region
        [Key, Column(Order = 0)]
        [ForeignKey("salepoint")]
        public int SalePointId { get; set; }
        public virtual SalePoint salepoint { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("product")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total In Stock")]
        public double ProductStock { get; set; }
        #endregion
    }
}