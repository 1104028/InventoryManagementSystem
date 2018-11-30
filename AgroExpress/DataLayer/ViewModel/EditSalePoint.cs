using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class EditSalePoint
    {
        public int PKSalePointID { get; set; }

        [Display(Name = "Sale Point Name")]
        public string SalePointName { get; set; }


        [Display(Name = "Sale Point Address")]
        [MaxLength(200, ErrorMessage = "Address can be Maximum 200 characters")]
        public string SalePointAddress { get; set; }
    }
}