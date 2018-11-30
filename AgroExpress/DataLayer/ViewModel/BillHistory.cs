using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class BillHistory
    {
        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int? SalePointId { get; set; }

        [Display(Name = "Area Name")]
        public IEnumerable<SelectListItem> arealist { get; set; }
        public int? AreaId { get; set; }

        [Display(Name = "Sub Area Name")]
        public IEnumerable<SelectListItem> subarealist { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public int? SubAreaId { get; set; }

        public IEnumerable<SelectListItem> selectedcustomerlist { get; set; }
        public int? CustomerID { get; set; }

        [Display(Name = "Billing Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Min Date required")]
        [DataType(DataType.Date)]
        public DateTime EntryDateMin { get; set; }

        [Display(Name = "Billing Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Max Date required")]
        [DataType(DataType.Date)]
        public DateTime EntryDateMax { get; set; }

        [Display(Name = "Paid Amount")]
        public double? PaidAmountVMin { get; set; }

        [Display(Name = "Paid Amount")]
        public double? PaidAmountVMax { get; set; }

        public List<BillingHistoryListView> allbills { get; set; }
    }
}