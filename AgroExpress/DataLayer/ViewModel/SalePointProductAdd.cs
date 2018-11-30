using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class SalePointProductAdd
    {
        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        [Required(AllowEmptyStrings  = false, ErrorMessage = "Sale point is required")]
        public int SalePointId { get; set; }

        [Display(Name = "Date")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date required")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        public IEnumerable<SelectListItem> product { get; set; }

        public List<ProductTransferInfo> TransferedProductInfo { get; set; }

    }
}