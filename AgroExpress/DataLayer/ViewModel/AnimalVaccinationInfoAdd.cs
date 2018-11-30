using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalVaccinationInfoAdd
    {
        [Display(Name = "Vaccine Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Vaccine Name required")]
        public string VaccineName { get; set; }

        //[Display(Name = "AnimalCategory")]
        //public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        //public int AnimalTypeId { get; set; }

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
        public bool GetNotiFication { get; set; }

        public int AnimalTypeId { get; set; }
        public List<SingelVaccineInfo> VaccinationInfo { get; set; }

    }
}