using AgroExpress.DataLayer;
using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            return View();
        }

        //public ActionResult Sale()
        //{
        //    ProductsSaleView newentry = new ProductsSaleView();

        //    newentry.SaleDate = System.DateTime.Now;
        //    newentry.TotalAmount = 0;
        //    newentry.PaidAmount = 0;
        //    var arealistlist = AgroExpressDBAccess.GetallEnabledArea();
        //    if (arealistlist != null)
        //    {
        //        newentry.Arealist = arealistlist.Select(x => new SelectListItem
        //        {
        //            Value = x.PKAreaId.ToString(),
        //            Text = x.AreaName
        //        });
        //    }

        //    var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();
        //    if (customerlist != null)
        //    {
        //        newentry.CustomerList = customerlist.Select(x => new SelectListItem
        //        {
        //            Value = x.PKCustomerId.ToString(),
        //            Text = x.FullName
        //        });
        //    }

        //    List<Sale> allproducts = new List<Sale>();

        //    var productlist = AgroExpressDBAccess.GetallProduct();

        //    for (var i = 1; i <= 10; i++)
        //    {
        //        allproducts.Add(new ProductsSale
        //        {
        //            Rate = 0,
        //            Amount = 0,
        //            Total = 0,
        //            ProductList = productlist.Select(x => new SelectListItem
        //            {
        //                Value = x.Product_Id.ToString(),
        //                Text = x.Product_Name
        //            })
        //        });
        //    }
        //    newentry.productsale = allproducts;

        //    return View(newentry);

        //}

    }
}