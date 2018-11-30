using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("ANIMALWEIGHT")]
    public class AnimalWeight
    {
        #region
        [Key]
        [Column(Order =1)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Required]
        [Key]
        [Column(Order = 2)]
        [ForeignKey("animal")]
        [Display(Name = "Animal")]
        public int AnimalId { get; set; }
        public virtual AnimalInformation animal { get; set; }

        [Display(Name = "Weight")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Weight required")]
        public double Weight { get; set; }

        #endregion
    }
}