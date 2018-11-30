using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalWeights
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Animal")]
        public int AnimalId { get; set; }
        public string AnimalName { get; set; }
        
        [Display(Name = "Weight")]
        
        public double Weight { get; set; }
    }
}