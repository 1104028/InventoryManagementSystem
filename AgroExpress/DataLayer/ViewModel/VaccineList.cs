using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class VaccineList
    {
        //[Display(Name = "Vaccine Name")]
        //public IEnumerable<SelectListItem> VVaccineNameList { get; set; }
        //public string VVaccineName { get; set; }

        [Display(Name = "AnimalCategory")]
        public IEnumerable<SelectListItem> VAnimalTypes { get; set; }
        public int? VAnimalTypeId { get; set; }

        [Display(Name = "Animal ID")]
        public IEnumerable<SelectListItem> vacanimallist { get; set; }
        public int? VAnimalId { get; set; }

        [Display(Name = "Vaccination Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime VaccinationDateMin { get; set; }

        [Display(Name = "Next Vaccation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime VaccinationDateMax { get; set; }

        public List<Vaccination> vaccinelist { get; set; }
    }
}