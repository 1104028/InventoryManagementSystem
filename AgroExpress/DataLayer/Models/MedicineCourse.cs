using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroExpress.DataLayer.Models
{
    public class MedicineCourse
    {
        [Key]
        [Column(Order = 1)]
        [ForeignKey("medicine")]
        public int medicationId { get; set; }
        public virtual Medication medicine { get; set; }

        [Key]
        [Column(Order = 2)]
        [Display(Name = "Medication Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Medication Date required")]
        [DataType(DataType.Date)]
        public DateTime MedicationDate { get; set; }

        public string Dose { get; set; }

        [DefaultValue(true)]
        public bool status { get; set; }
    
    }
}