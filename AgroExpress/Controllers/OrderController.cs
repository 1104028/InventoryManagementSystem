using AgroExpress.DataLayer;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Controllers
{
    [RBAC]
    public class OrderController : Controller
    {
        public ActionResult Add()
        {
            AddOrderListView newentry = new AddOrderListView();

            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            List<AddOrder> orders = new List<AddOrder>();
            int len = ProductInf.Count;

            for (int i = 0; i < len; i++)
            {
                orders.Add(new AddOrder
                {
                    productlist = ProductInf.Select(x => new SelectListItem
                    {
                        Value = x.PKProductId.ToString(),
                        Text = x.ProductName
                    }),
                    OrderPlacingDateTime = DateTime.Now.Date,
                    Amount = 0

                });
            }

            newentry.orderlist = orders;
            return View(newentry);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddOrderListView newentry)
        {
            List<Order> neworder = new List<Order>();

            for (int i = 0; i < newentry.orderlist.Count; i++)
            {
                if (newentry.orderlist[i].PKProductId != 0 && newentry.orderlist[i].Amount != 0 && newentry.orderlist[i].OrderPlacingDateTime != null)
                {
                    neworder.Add(new Order
                    {
                        CustomerId = 1,
                        ProductId = newentry.orderlist[i].PKProductId,
                        OrderPlacingDateTime = newentry.orderlist[i].OrderPlacingDateTime,
                        OrderDateTime = System.DateTime.Now.Date,
                        Amount = newentry.orderlist[i].Amount
                    });
                }
            }

            if (AgroExpressDBAccess.AddOrder(neworder))
            {
                ViewBag.success("Order Successful");
            }
            else
            {
                ViewBag.success("Sorry, Your order is not placed, Please try again");
            }



            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            List<AddOrder> orders = new List<AddOrder>();
            int len = ProductInf.Count;

            for (int i = 0; i < len; i++)
            {
                orders.Add(new AddOrder
                {
                    productlist = ProductInf.Select(x => new SelectListItem
                    {
                        Value = x.PKProductId.ToString(),
                        Text = x.ProductName
                    }),
                    OrderPlacingDateTime = System.DateTime.Now.Date,
                    Amount = 0

                });
            }

            newentry.orderlist = orders;
            return View(newentry);
        }

        public ActionResult SingleOrderAdd()
        {
            AddOrder newentry = new AddOrder();
            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            if (prlist != null)
            {
                newentry.productlist = prlist.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }
            newentry.OrderPlacingDateTime = System.DateTime.Now.Date;
            return View(newentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingleOrderAdd(AddOrder newentry)
        {
            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            if (prlist != null)
            {
                newentry.productlist = prlist.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }

            if (ModelState.IsValid)
            {
                if (AgroExpressDBAccess.AddSingleOrder(newentry))
                {
                    ModelState.Clear();
                    ViewBag.success = "Order has been placed Successfully";
                }
                else
                {
                    ViewBag.success = "Sorry, Your order is not placed, Please try again";
                }
            }

            newentry.OrderPlacingDateTime = newentry.OrderPlacingDateTime;
            return View(newentry);
        }

        public ActionResult OrderList()
        {
            return View();
        }
    }
}