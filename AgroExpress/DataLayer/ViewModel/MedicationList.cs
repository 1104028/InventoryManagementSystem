using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class MedicationList
    {
        public int medicationId { get; set; } 

        [Display(Name = "Medicine Name")]
        public string MedicineName { get; set; }

        [Display(Name = "AnimalCodeName")]
        public string AnimalCodeName { get; set; }

        [Display(Name = "Medication Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime MedicationDate { get; set; }

        public string Dose { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [Display(Name = "Operator Name")]
        public string OperatorName { get; set; }

    }
}