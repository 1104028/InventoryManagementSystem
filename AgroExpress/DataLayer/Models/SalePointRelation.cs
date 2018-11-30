using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SalePointRelation
    {
        [Key, Column(Order = 0)]
        public int UserLoginId { get; set; }

        [Key, Column(Order = 1)]
        [ForeignKey("salepoint")]
        public int SalePointId { get; set; }
        public virtual SalePoint salepoint { get; set; }

    }
}