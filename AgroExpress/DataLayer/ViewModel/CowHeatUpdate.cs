using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CowHeatUpdate
    {

        [Display(Name = "Animal ID")]
        public int animalId { get; set; }

        [Display(Name = "Animal Code Name")]
        public string animalCodeName { get; set; }

        [Display(Name = "Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Date required")]
        [DataType(DataType.Date)]
        public DateTime heatDate { get; set; }

        [Display(Name = "Heat Time")]
        public string heatTime { get; set; }

        [Display(Name = "Injected Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? injectedDate { get; set; }

        [Display(Name = "Injection Time")]
        public string injectedTime { get; set; }

        [Display(Name = "Heat Summary")]
        public string heatSummary { get; set; }

        [Display(Name = "Expected Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? expectedDeliveryDate { get; set; }

        [Display(Name = "Delivery Status")]
        public string deliveryStatus { get; set; }

        [Display(Name = "Actual Delivery Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? actualDeliveryDate { get; set; }

        [Display(Name = "Next Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime nextHeatDate { get; set; }

        public string operatorName { get; set; }

        public IEnumerable<SelectListItem> HeatTimeOptions { get; set; }
        public IEnumerable<SelectListItem> InjectedTimeOptions { get; set; }
    }
}