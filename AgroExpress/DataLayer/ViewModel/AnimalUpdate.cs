using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalUpdate
    {
        public int PKAnimalId { get; set; }

        public bool IsDeleted { get; set; }

        [Display(Name = "Animal Code Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animal Code Name required")]
        public string AnimalCodeName { get; set; }

        [Display(Name = "Date of Entry")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Entry Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateofEntry { get; set; }

        [Display(Name = "Date of Exit")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateofExit { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Gender")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Gender required")]
        public string Gender { get; set; }

        [Display(Name = "AnimalCategory")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animla Category required")]
        public int AnimalType { get; set; }

        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public IEnumerable<SelectListItem> GenderOption { get; set; }
    }
}