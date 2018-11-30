using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class CustomerTransactionDetails
    {


        [Display(Name = "Buying Date")]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[DataType(DataType.Date)]
        public DateTime BuyingDate { get; set; }

        public string ProductName { get; set; }

        [Display(Name = "Paid")]
        public double TotalPaid { get; set; }


    }
}