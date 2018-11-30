using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class MilkSummeryEdit
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        
        [Display(Name = "Total Consume by Culf Morning")]
        public double CulfMorning { get; set; }

       
        [Display(Name = "Total Consume by Culf Afternoon")]
        public double CulfAfternoon { get; set; }

        [Display(Name = "Total Consume by Factory")]
        public double Factory { get; set; }

        [Display(Name = "Total Consume byStuff")]
        public double Stuff { get; set; }

       
        [Display(Name = "Total Wastage")]
        public double Wastage { get; set; }
    }
}