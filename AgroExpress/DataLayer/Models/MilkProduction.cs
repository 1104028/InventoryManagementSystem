using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("MILKPRODUCTION")]
    public class MilkProduction
    {
        #region
        [Key]
        public int PKMilkProductionID { get; set; }


        [ForeignKey("animal")]
        [Display(Name = "Animal ID")]
        public int AnimalId { get; set; }
        public virtual AnimalInformation animal { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Morning Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Morning Amount required")]
        public double MorningAmount { get; set; }

        [Display(Name = "Afternoon Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Afternoon Amount required")]
        public double AfternoonAmount { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
        #endregion
    }
}