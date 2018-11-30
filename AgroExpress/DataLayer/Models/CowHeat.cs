using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("COWHEAT")]
    public class CowHeat
    {
        #region                
        [Key,Column(Order = 1)]
        [ForeignKey("animal")]
        [Display(Name = "Animal ID")]
        public int AnimalId { get; set; }
        public virtual AnimalInformation animal { get; set; }

        
        [Key,Column(Order = 2)]
        [Display(Name = "Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Date required")]
        [DataType(DataType.Date)]
        public DateTime HeatDate { get; set; }

        [Display(Name = "Heat Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Time required")]
        public string HeatTime { get; set; }

        [Display(Name = "Next Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Next Heat Date required")]
        [DataType(DataType.Date)]
        public DateTime NextHeatDate { get; set; }

        [Display(Name = "Injected Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? InjectedDate { get; set; }

        [Display(Name = "Injection Time")]
        public string InjectedTime { get; set; }

        [Display(Name = "Heat Summary")]
        public string HeatSummary { get; set; }

        [Display(Name = "Expected Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ExpectedDeliveryDate { get; set; }

        [Display(Name = "Delivery Status")]
        public string DeliveryStatus { get; set; }

        [Display(Name = "Actual Delivery/Miscarriage  Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? ActualDeliveryDate { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }

        #endregion
    }
}