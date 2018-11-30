using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;

namespace AgroExpress.Controllers
{
   
    public class ProductController : Controller
    {
        // GET: Produact
        [RBAC]
        public ActionResult Index()
        {
            var ProductList = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductListView ProductListV = new ProductListView();
            ProductListV.ProductList = ProductList.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            if (ProductList != null)
                ProductListV.ProductSearchResult = ProductList;
            else
            {
                ProductListV.ProductSearchResult = new List<Product>();
            }
            return View(ProductListV);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Exclude = "ProductSearchResult")]ProductListView SerchCriteria)
        {

            var ProductList = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductListView ProductListV = new ProductListView();
            ProductListV.ProductList = ProductList.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName,
                Selected = x.PKProductId == SerchCriteria.SelectedProductID ? true : false
            });
            var SearchResult = AgroExpressDBAccess.SearchProduct(SerchCriteria.SelectedProductID, SerchCriteria.MinimumStock, SerchCriteria.MaximumStock);
            if (SearchResult != null)
            {
                ProductListV.ProductSearchResult = SearchResult;
            }
            else
            {
                ProductListV.ProductSearchResult = new List<Product>();
            }
            ProductListV.MaximumStock = SerchCriteria.MaximumStock;
            ProductListV.MinimumStock = SerchCriteria.MinimumStock;
            ModelState.Clear();
            return View(ProductListV);
        }
        [RBAC]
        public ActionResult Add()
        {
            return View();
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                var productobj = AgroExpressDBAccess.IsProductExist(product.ProductName);
                if (productobj != null)
                {
                    ModelState.AddModelError("ProductName", "Product name already exist");
                    return View(product);
                }

                if (AgroExpressDBAccess.AddProduct(product))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    Product producttem = new Product();
                    return View(producttem);
                }
            }
            return View(product);
        }
        [RBAC]
        public ActionResult Edit(int ID)
        {
            var Product = AgroExpressDBAccess.GetProductByID(ID);
            if (Product != null)
            {
                return View(Product);
            }

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RBAC]
        public ActionResult Edit(Product product)
        {
            if (AgroExpressDBAccess.UpdateProduct(product))
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Message = "Unable to update";
                return View(product);
            }

        }
        [RBAC]
        public ActionResult Delete(int ID)
        {
            AgroExpressDBAccess.DeleteProductByID(ID);

            return RedirectToAction("Index");
        }
        [RBAC]
        public ActionResult DisabledProduts()
        {
            var ProductList = AgroExpressDBAccess.GetAllDisabledProduct();
            return View(ProductList);
        }
        [RBAC]
        public ActionResult Enable(int ID)
        {
            AgroExpressDBAccess.EnableProductByID(ID);

            return RedirectToAction("DisabledProduts");
        }
        public JsonResult Stock(int ProductID)
        {

            double Stock = AgroExpressDBAccess.GetProductStockAmount(ProductID);
            return Json(Stock, JsonRequestBehavior.AllowGet);
        }
        [RBAC]
        public ActionResult Sale()
        {
            ProductSaleAdd ProductSaleView = ProductSaleInitial();
            
            return View(ProductSaleView);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Sale(ProductSaleAdd request)
        {
            string name = HttpContext.User.Identity.Name;
            string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);

            bool smsStatus = false;
            if (ModelState.IsValid)
            {
                List<Sale> SaleRequest = new List<Sale>();

                double TotalBill = 0;
                double TottalPai = 0;
                foreach (var sale in request.ProductSaleInfo)
                {
                    if (sale.ProductId != null && sale.ProductId != 0 && sale.Amount != null && sale.Amount != 0 && sale.Rate != null && sale.Servicecharge != null)
                    {
                        SaleRequest.Add(new Sale
                        {
                            ProductId = (int)sale.ProductId,
                            Amount = (double)sale.Amount,
                            Rate = (double)sale.Rate,
                            ServiceCharge = (double)sale.Servicecharge,
                            CustomerId = request.CustomerID,
                            OperatorName = operatorName,
                            DateTime = request.SaleDate,
                            SMSSent = smsStatus
                        });
                        TotalBill += (double)sale.Amount * ((double)sale.Rate + (double)sale.Servicecharge);
                    }

                }
                if (request.BillPaid != null)
                    TottalPai = (double)request.BillPaid;
                AgroExpressDBAccess.AddProductSaleList(SaleRequest, request.CustomerID, request.SalePointId, TotalBill, TottalPai, operatorName);

            }
            ModelState.Clear();
            ProductSaleAdd ProductSaleView = ProductSaleInitial();
            return View(ProductSaleView);
        }

        private ProductSaleAdd ProductSaleInitial()
        {
            string name = HttpContext.User.Identity.Name;

            ProductSaleAdd ProductSale = new ProductSaleAdd();
            ProductSale.SaleDate = System.DateTime.UtcNow.AddHours(6);

            List<SalePoint> salePointList = new List<SalePoint>();
            salePointList = AgroExpressDBAccess.GetSalePointListForUSer(name);
            if (salePointList != null)
            {
                ProductSale.salepointlist = salePointList.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                arealist = arealist.Where(a => salePointList.Any(b => b.PKSalePointID == a.SalePointId)).ToList();
                ProductSale.Area = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                sarealist = sarealist.Where(a => arealist.Any(b => b.PKAreaId == a.AreaId)).ToList();
                ProductSale.SubArea = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            List<Product> productList = AgroExpressDBAccess.GetAllEnabledProduct();
            List<ProductSaleInfo> PSI = new List<ProductSaleInfo>();
            var MilkInfo = AgroExpressDBAccess.IsProductExist("Milk");
            if (MilkInfo != null)
            {
                PSI.Add(new ProductSaleInfo { ProductId = MilkInfo.PKProductId, Amount = null, Rate = null, Servicecharge = null });
            }
            else
            {
                Product MilkInfoAdd = new Product
                {
                    ProductName = "Milk",
                    SellingUnit = "Ltr",
                    Stock = 0

                };
                AgroExpressDBAccess.AddProduct(MilkInfoAdd);
                MilkInfo = AgroExpressDBAccess.IsProductExist("Milk");
                PSI.Add(new ProductSaleInfo { ProductId = MilkInfo.PKProductId, Amount = null, Rate = null, Servicecharge = null });
            }
            // Product dropdown list

            productList = productList.Where(a => a.ProductName.ToLower() != "milk").ToList();
            ProductSale.product = productList.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });

            //
            int len = productList.Count;

            for (int i = 0; i < len; i++)
            {
                PSI.Add(new ProductSaleInfo { ProductId = null, Amount = null, Rate = null, Servicecharge = 0 });
            }
            ProductSale.ProductSaleInfo = PSI;

            List<Customer> customer = AgroExpressDBAccess.GetallEnabledCustomer();
            customer = customer.Where(a => sarealist.Any(b => b.PKSubAreaId == a.SubAreaId)).ToList();
            ProductSale.Customer = customer.Select(x => new SelectListItem
            {
                Value = x.PKCustomerId.ToString(),
                Text = x.FullName
            });
            return ProductSale;
        }
        [RBAC]
        public ActionResult SingleSale()
        {
            SingleSaleAdd single = new SingleSaleAdd();

            single.DateTime = System.DateTime.UtcNow.AddHours(6);
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                single.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                single.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                single.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            List<Customer> culist = AgroExpressDBAccess.GetallEnabledCustomer();
            if (culist != null)
            {
                single.customerlist = culist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            if (prlist != null)
            {
                single.productlist = prlist.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }

           

            return View(single);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SingleSale(SingleSaleAdd single)
        {

            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                single.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                single.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                single.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            List<Customer> culist = AgroExpressDBAccess.GetallEnabledCustomer();
            if (culist != null)
            {
                single.customerlist = culist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            List<Product> prlist = AgroExpressDBAccess.GetAllEnabledProduct();
            if (prlist != null)
            {
                single.productlist = prlist.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }

            if (ModelState.IsValid)
            {
               
            }
          

            return View(single);
        }

        public JsonResult SalePointStock(int ProductID, int SalePointID)
        {

            double Stock = AgroExpressDBAccess.SalePointProductStock(ProductID, SalePointID);
            return Json(Stock, JsonRequestBehavior.AllowGet);
        }
        [RBAC]
        public ActionResult SaleHistory()
        {
            SaleListView listview = new SaleListView();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                listview.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                listview.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                listview.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            if (customerlist != null)
            {
                listview.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            var productList = AgroExpressDBAccess.GetAllEnabledProduct();

            if (productList != null)
            {
                listview.selectedproductlist = productList.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }

            listview.EntryDateMax = System.DateTime.Now.Date.AddDays(1);
            listview.EntryDateMin = listview.EntryDateMax.AddDays(-30);

            var saleifo = AgroExpressDBAccess.GetSaleInfoByDate(listview.EntryDateMin, listview.EntryDateMax);
            List<SaleList> sale = new List<SaleList>();
            if (saleifo != null)
            {
                foreach (var li in saleifo)
                {
                    sale.Add(new SaleList
                    {
                        DateTime = li.DateTime,
                        CustomerName = li.customer.FullName,
                        ProductName = li.product.ProductName,
                        Amount = li.Amount,
                        Total = (li.Amount * li.Rate) + (li.Amount * li.ServiceCharge),
                        SMSSent = li.SMSSent,
                        OperatorName = li.OperatorName
                    });
                }
            }

            listview.salelist = sale;
            return View(listview);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RBAC]
        public ActionResult SaleHistory(SaleListView listview)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                listview.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                listview.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                listview.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            if (customerlist != null)
            {
                listview.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }
            var productList = AgroExpressDBAccess.GetAllEnabledProduct();

            if (productList != null)
            {
                listview.selectedproductlist = productList.Select(x => new SelectListItem
                {
                    Value = x.PKProductId.ToString(),
                    Text = x.ProductName
                });
            }
            var saleifo = AgroExpressDBAccess.GetSaleInfoByDate(listview.EntryDateMin, listview.EntryDateMax.AddDays(1));
            if (listview.AmountVMax != null)
            {
                saleifo = saleifo.Where(a => a.Amount <= listview.AmountVMax).ToList();
            }
            if (listview.AmountVMin != null)
            {
                saleifo = saleifo.Where(a => a.Amount >= listview.AmountVMin).ToList();
            }

            if (listview.CustomerID != null)
            {
                saleifo = saleifo.Where(a => a.CustomerId == listview.CustomerID).ToList();
            }
            if (listview.SelectedProductID != null)
            {
                saleifo = saleifo.Where(a => a.ProductId == listview.SelectedProductID).ToList();
            }
            List<Area> AreList = new List<Area>();
            List<SubArea> subAreList = AgroExpressDBAccess.GetallEnabledSubArea();
            if (listview.SalePointId != null)
            {
                AreList = AgroExpressDBAccess.GetAreaBySalePointID((int)listview.SalePointId);

                if (AreList != null)
                {

                    subAreList = subAreList
                   .Where(x => AreList.Any(y => y.PKAreaId == x.AreaId)).ToList();
                }
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
                saleifo = saleifo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }
            if (listview.AreaId != null)
            {
                subAreList = subAreList.Where(su => su.AreaId == listview.AreaId).ToList();
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
                saleifo = saleifo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }
            if (listview.SubAreaId != null)
            {
                customerlist = customerlist.Where(customer => customer.SubAreaId == listview.SubAreaId).ToList();
                saleifo = saleifo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }

            List<SaleList> sale = new List<SaleList>();
            if (saleifo != null)
            {
                foreach (var li in saleifo)
                {
                    sale.Add(new SaleList
                    {
                        DateTime = li.DateTime,
                        CustomerName = li.customer.FullName,
                        ProductName = li.product.ProductName,
                        Amount = li.Amount,
                        SMSSent = li.SMSSent,
                        OperatorName = li.OperatorName
                    });
                }
            }

            listview.salelist = sale;
            return View(listview);
        }

        [RBAC]
        public ActionResult BillHistory()
        {
            BillHistory listview = new BillHistory();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                listview.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                listview.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                listview.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            if (customerlist != null)
            {
                listview.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            listview.EntryDateMax = System.DateTime.Now.Date.AddDays(1);
            listview.EntryDateMin = listview.EntryDateMax.AddDays(-30);

            var billinfo = AgroExpressDBAccess.GetBillingInfoByDate(listview.EntryDateMin, listview.EntryDateMax);
            List<BillingHistoryListView> bill = new List<BillingHistoryListView>();
            if (billinfo != null)
            {
                foreach (var li in billinfo)
                {
                    bill.Add(new BillingHistoryListView
                    {
                        DateTime = li.DateTime,
                        CustomerName = li.customer.FullName,
                        BillPaid = li.BillPaid,
                        OperatorName = li.OperatorName
                    });
                }
            }

            listview.allbills = bill;
            return View(listview);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RBAC]
        public ActionResult BillHistory(BillHistory listview)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                listview.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                listview.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                listview.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            if (customerlist != null)
            {
                listview.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            var billinfo = AgroExpressDBAccess.GetBillingInfoByDate(listview.EntryDateMin, listview.EntryDateMax.AddDays(1));
            if (listview.PaidAmountVMax != null)
            {
                billinfo = billinfo.Where(a => a.BillPaid <= listview.PaidAmountVMax).ToList();
            }
            if (listview.PaidAmountVMin != null)
            {
                billinfo = billinfo.Where(a => a.BillPaid >= listview.PaidAmountVMin).ToList();
            }

            if (listview.CustomerID != null)
            {
                billinfo = billinfo.Where(a => a.CustomerId == listview.CustomerID).ToList();
            }

            List<Area> AreList = new List<Area>();
            List<SubArea> subAreList = AgroExpressDBAccess.GetallEnabledSubArea();
            if (listview.SalePointId != null)
            {
                AreList = AgroExpressDBAccess.GetAreaBySalePointID((int)listview.SalePointId);

                if (AreList != null)
                {

                    subAreList = subAreList
                   .Where(x => AreList.Any(y => y.PKAreaId == x.AreaId)).ToList();
                }
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
                billinfo = billinfo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }
            if (listview.AreaId != null)
            {
                subAreList = subAreList.Where(su => su.AreaId == listview.AreaId).ToList();
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
                billinfo = billinfo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }
            if (listview.SubAreaId != null)
            {
                customerlist = customerlist.Where(customer => customer.SubAreaId == listview.SubAreaId).ToList();
                billinfo = billinfo.Where(salei => customerlist.Any(customer => customer.PKCustomerId == salei.CustomerId)).ToList();
            }
            List<BillingHistoryListView> bill = new List<BillingHistoryListView>();
            if (billinfo != null)
            {
                foreach (var li in billinfo)
                {
                    bill.Add(new BillingHistoryListView
                    {
                        DateTime = li.DateTime,
                        CustomerName = li.customer.FullName,
                        BillPaid = li.BillPaid,
                        OperatorName = li.OperatorName
                    });
                }
            }

            listview.allbills = bill;
            return View(listview);
        }


    }
}