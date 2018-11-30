using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CustomerDues
    {
        [Display(Name = "Customer Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Name required")]
        public string CustomerName { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number required")]
        public string Mobile { get; set; }

        [Display(Name = "Sub Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public string SubAreaName { get; set; }

        [Display(Name = "Total Due")]
        public double TotalDue { get; set; }
    }
}