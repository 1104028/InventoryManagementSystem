using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SMSConfiguration
    {
        
        [Key]
        public int ID { get; set; }

        [Display(Name = "API Key")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Password required")]
        public string APIKey { get; set; }

        [Display(Name = "Masking")]
        public string Masking { get; set; }
    }
}