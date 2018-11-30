using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class FactoryProductionList
    {
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public int? ProductID { get; set; }

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

        public double? AmountMin { get; set; }
        
        public double? AmountMax { get; set; }

        public List<Production> SearchResult { get; set; }
    }
}