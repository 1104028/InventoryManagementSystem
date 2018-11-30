using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalVaccineEdit
    {
        public int PKVaccinationId { get; set; }

        [Display(Name = "Vaccine Name")]
        public string VaccineName { get; set; }

        [Display(Name = "Animal Code Name")]
        public string AnimalCodeName { get; set; }

        [Display(Name = "Next Vaccation Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime NextVaccationDate { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }
    }
}