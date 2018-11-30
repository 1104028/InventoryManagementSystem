using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using System.Data.Entity;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CowHeatAdd
    {
        [Display(Name = "AnimalCategory")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animla Category required")]
        public int animalTypeID { get; set; }
        
        [Display(Name = "Animal Code Name")]        
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animal Code Name required")]
        public int animalId { get; set; }

        [Display(Name = "Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Date required")]
        [DataType(DataType.Date)]
        public DateTime heatDate { get; set; }

        [Display(Name = "Heat Time")]
//        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Time required")]
        public string heatTime { get; set; }

        [Display(Name = "Injected Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
       // [Required(AllowEmptyStrings = false, ErrorMessage = "Injected Date required")]
        [DataType(DataType.Date)]
        public DateTime? injectedDate { get; set; }

        [Display(Name = "Injection Time")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Injection Time required")]
        public string injectedTime { get; set; }

        [Display(Name = "Heat Summary")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Heat Summary required")]
        public string heatSummary { get; set; }

        [Display(Name = "Expected Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Expected Delivery Date required")]
        [DataType(DataType.Date)]
        public DateTime? expectedDeliveryDate { get; set; }

        [Display(Name = "Delivery Status")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Delivery Status required")]
        public string deliveryStatus { get; set; }

        [Display(Name = "Actual Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "Actual Delivery Date required")]
        [DataType(DataType.Date)]
        public DateTime? actualDeliveryDate { get; set; }

        [Display(Name = "Next Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        [DataType(DataType.Date)]
        public DateTime nextHeatDate { get; set; }

        public string operatorName { get; set; }

        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public IEnumerable<SelectListItem> AnimalList { get; set; }
        public IEnumerable<SelectListItem> HeatTimeOptions { get; set; }
        public IEnumerable<SelectListItem> InjectedTimeOptions { get; set; }
        
    }
}