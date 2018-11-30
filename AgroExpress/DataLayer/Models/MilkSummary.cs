using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class MilkSummary
    {
        #region
        [Key]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Production")]
        public double TotalProduction { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Consume by Culf Morning")]
        public double CulfMorning { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Consume by Culf Afternoon")]
        public double CulfAfternoon { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Consume byStuff")]
        public double Stuff { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Consume by Factory")]
        public double Factory { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Total Wastage")]
        public double Wastage { get; set; }

        #endregion
    }
}