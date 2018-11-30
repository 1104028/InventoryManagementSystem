using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class Production
    {
        #region
        [Key]
        public int PKProductionId { get; set; }

        [Display(Name = "Production Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Production Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Amount required")]
        public double Amount { get; set; }

        [Required]
        [Column(Order =1)]
        [ForeignKey("product")]
        [Display(Name = "Product Name")]
        public int ProductId { get; set; }
        public virtual Product product { get; set; }

       

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
        #endregion
    }
}