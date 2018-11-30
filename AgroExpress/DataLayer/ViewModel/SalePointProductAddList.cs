using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.DataLayer.ViewModel
{
    public class SalePointProductAddList
    {

        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int? SalePointId { get; set; }



        public IEnumerable<SelectListItem> product { get; set; }
        public int? ProductId { get; set; }


        public double? StockAmountMin { get; set; }

        public double? StockAmountMAx { get; set; }

        [Display(Name = "Selling Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Min Date required")]
        [DataType(DataType.Date)]
        public DateTime EntryDateMin { get; set; }

        [Display(Name = "Selling Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Max Date required")]
        [DataType(DataType.Date)]
        public DateTime EntryDateMax { get; set; }

        public List<SalePointProductConsume> SearchResult { get; set; }
    }
}