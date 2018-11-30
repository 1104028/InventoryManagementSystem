using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class MilkproductionList
    {
        [Display(Name = "AnimalCategory")]
        public IEnumerable<SelectListItem> VAnimalTypes { get; set; }
        public int? VAnimalTypeId { get; set; }

        [Display(Name = "Animal ID")]
        public IEnumerable<SelectListItem> Manimallist { get; set; }
        public int? MAnimalId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateMin { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime DateMax { get; set; }

        [Display(Name = "Morning Amount")]
        public double? MorningAmountMin { get; set; }

        [Display(Name = "Morning Amount")]
        public double? MorningAmountMax { get; set; }

        [Display(Name = "Afternoon Amount")]
        public double? AfternoonAmountMin { get; set; }

        [Display(Name = "Afternoon Amount")]
        public double? AfternoonAmountMax { get; set; }

        public List<MilkProduction> milkProductions { get; set; }
    }
}