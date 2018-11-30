using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class Vaccination
    {
        #region
        
        [Key]
        public int PKVaccinationId { get; set; }

        [Display(Name = "Vaccine Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vaccine Name required")]
        public string VaccineName { get; set; }

        
        [ForeignKey("animal")]
        [Display(Name = "Animal ID")]
        public int AnimalId { get; set; }
        public virtual AnimalInformation animal { get; set; }
        
        [Display(Name = "Vaccination Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vaccination Date required")]
        [DataType(DataType.Date)]
        public DateTime VaccinationDate { get; set; }

        [Display(Name = "Next Vaccation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime NextVaccationDate { get; set; }

        [DefaultValue(true)]
        public bool IsApplied { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Operator Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Operator Name required")]
        public string OperatorName { get; set; }
        #endregion
    }
}