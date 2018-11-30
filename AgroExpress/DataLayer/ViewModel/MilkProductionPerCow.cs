using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class MilkProductionPerCow
    {
        public int AnimalID { get; set; }
        public string AnimalCode { get; set; }
        
        [Display(Name = "Morning Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Morning Amount required")]
        public double MorningAmount { get; set; }

        [Display(Name = "Afternoon Amount")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Afternoon Amount required")]
        public double AfternoonAmount { get; set; }
    }
}