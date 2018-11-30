using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CowHeatInfoView
    {
        [Display(Name = "Animal ID")]
        public int animalId { get; set; }

        public string animalCodeName { get; set; }

        public int animalTypeID { get; set; }

        public string animalTypeName { get; set; }

        [Display(Name = "Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        [DataType(DataType.Date)]
        public DateTime heatDate { get; set; }

        [Display(Name = "Heat Time")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Heat Time required")]
        public string heatTime { get; set; }

        [Display(Name = "Next Heat Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]        
        [DataType(DataType.Date)]
        public DateTime nextHeatDate { get; set; }

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

        [Display(Name = "Actual Delivery/Miscarriage  Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime? actualDeliveryDate { get; set; }


    }
}