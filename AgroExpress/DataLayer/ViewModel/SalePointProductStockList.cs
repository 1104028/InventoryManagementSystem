using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.DataLayer.ViewModel
{
    public class SalePointProductStockList
    {

        [Display(Name = "Sale Point")]
        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int? SalePointId { get; set; }

       

        public IEnumerable<SelectListItem> product { get; set; }
        public int? ProductId { get; set; }


        public double? StockAmountMin { get; set; }

        public double? StockAmountMAx { get; set; }

        public List<SalePointProductStock> SearchResult { get; set; }
    }
}