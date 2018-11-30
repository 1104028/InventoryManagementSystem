using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalMdicationAdd
    {
        [Display(Name = "Medicine Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Medicine Name required")]
        public string MedicineName { get; set; }

        [Display(Name = "AnimalCategory")]
        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public int? AnimalTypeId { get; set; }

        [Display(Name = "Animal Name")]
        public IEnumerable<SelectListItem> animallist { get; set; }
        public int AnimalId { get; set; }
       
        [Display(Name = "Medication Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime MedicationDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Medication Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime MedicationCourseDate { get; set; }

        public string SelectedCourseDates { get; set; }

        public string Dose { get; set; }

        [DefaultValue(true)]
        public bool GetNotiFication { get; set; }
    }
}