using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalWeightInfoView
    {
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public int animalId { get; set; }

     
        [Display(Name = "Animal")]
        public string animalCodeName { get; set; }

        [Display(Name = "Animal Type Name")]
        public string animalType { get; set; }

        public int animalTypeId { get; set; }

        [Display(Name = "Gender")]
        public string gender { get; set; }

        [Display(Name = "Weight")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Weight required")]
        public double Weight { get; set; }
    }
}