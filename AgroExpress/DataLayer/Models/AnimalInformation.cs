using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("ANIMALINFORMATION")]
    public class AnimalInformation
    {
        #region
        [Key]
        public int PKAnimalId { get; set; }

        [ForeignKey("animalType")]
        [Display(Name = "Animal Type ID")]
        public int AnimalTypeId { get; set; }
        public virtual AnimalType animalType { get; set; }

        [Display(Name = "Animal Code Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animal Code Name required")]
        public string AnimalCodeName { get; set; }

        [Display(Name = "Date of Entry")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Date required")]
        [DataType(DataType.Date)]
        public DateTime DateofEntry { get; set; }

        [Display(Name = "Date of Birth")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Date required")]
        [DataType(DataType.Date)]
        public DateTime DOB { get; set; }


        [Display(Name = "Date of Exit")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? DateofExit { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender required")]
        public string Gender { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Vaccination> animal { get; set; }
        public virtual ICollection<AnimalWeight> animalweight { get; set; }
        public virtual ICollection<CowHeat> heat { get; set; }
        public virtual ICollection<MilkProduction> milk { get; set; }

        #endregion
    }
}