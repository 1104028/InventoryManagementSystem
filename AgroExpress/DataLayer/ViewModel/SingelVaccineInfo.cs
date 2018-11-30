using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class SingelVaccineInfo
    {
        
        public int AnimalId { get; set; }

        public string CodeName { get; set; }

        [Display(Name = "Comments")]
        public string Comments { get; set; }

        [DefaultValue(false)]
        public bool isNotApplied { get; set; }

    }
}