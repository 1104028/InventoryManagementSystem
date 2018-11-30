using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("MEDICATION")]
    public class Medication
    {
        #region
        [Key]
        public int PKMedicationId { get; set; }

        [Display(Name = "Medicine Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Medicine Name required")]
        public string MedicineName { get; set; }

        [ForeignKey("animal")]
        [Display(Name = "Animal ID")]
        public int AnimalId { get; set; }
        public virtual AnimalInformation animal { get; set; }

        [Display(Name = "Medication Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Medication Date required")]
        [DataType(DataType.Date)]
        public DateTime MedicationDate { get; set; }

       

        [Display(Name = "Comments")]
        public string Comments { get; set; }



        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }


        #endregion
    }
}