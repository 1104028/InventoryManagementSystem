using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class MilkProductionView
    {
        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        public DateTime ProductionDate { get; set; }

        public int AnimalTypeId { get; set; }

        public List<MilkProductionPerCow> Cows { get; set; }
    }
}