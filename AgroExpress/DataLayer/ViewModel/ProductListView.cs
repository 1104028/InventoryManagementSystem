using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class ProductListView
    {
        public IEnumerable<SelectListItem> ProductList { get; set; }
        public int? SelectedProductID { get; set; }
        public double? MinimumStock { get; set; }
        public double? MaximumStock { get; set; }

        public List<Product> ProductSearchResult { get; set; }
    }
}