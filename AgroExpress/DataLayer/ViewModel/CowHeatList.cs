using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;
using AgroExpress.DataLayer.Models;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CowHeatList
    {      
        public int? animalTypeID { get; set; } 

        public int? animalID { get; set; }

        [Display(Name = "Heat Date Min")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? heatDateMin { get; set; }

        [Display(Name = "Heat Date Max")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? heatDateMax { get; set; }

        [Display(Name = "Next Heat Date Min")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? nextHeatDateMin { get; set; }

        [Display(Name = "Next Heat Date Max")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? nextHeatDateMax { get; set; }

        [Display(Name = "Injected Date Min")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? injectedDateMin { get; set; }

        [Display(Name = "Injected Date Max")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? injectedDateMax { get; set; }


        [Display(Name = "Expected Date Min")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? expectedDateMin { get; set; }

        [Display(Name = "Expected Date Max")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? expectedDateMax { get; set; }

        public List<CowHeatInfoView> heatList { get; set; }

        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public IEnumerable<SelectListItem> AnimalCodes { get; set; }

    }
}