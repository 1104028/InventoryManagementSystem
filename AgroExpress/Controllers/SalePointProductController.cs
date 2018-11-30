using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer;
namespace AgroExpress.Controllers
{
    [RBAC]
    public class SalePointProductController : Controller
    {
        [NonAction]
        public ActionResult Index()
        {

            return View();
        }
        public ActionResult Add()
        {
            string name = HttpContext.User.Identity.Name;

            List<SalePoint> salePointList = new List<SalePoint>();
            salePointList = AgroExpressDBAccess.GetSalePointListForUSer(name);
            SalePointProductAdd salePointProductTransfer = new SalePointProductAdd();
            salePointProductTransfer.salepointlist = salePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            List<ProductTransferInfo> PTI = new List<ProductTransferInfo>();
            var MilkInfo = AgroExpressDBAccess.IsProductExist("Milk");
            if(MilkInfo!=null)
            {
                PTI.Add(new ProductTransferInfo { ProductId = MilkInfo.PKProductId, Amount = null });
            }
            else
            {
                Product MilkInfoAdd = new Product
                {
                    ProductName = "Milk",
                    SellingUnit = "Ltr",
                    Stock=0

                };
                AgroExpressDBAccess.AddProduct(MilkInfoAdd);
                MilkInfo = AgroExpressDBAccess.IsProductExist("Milk");
                PTI.Add(new ProductTransferInfo { ProductId = MilkInfo.PKProductId, Amount = null });
            }
            // Product dropdown list
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductInf = ProductInf.Where(a => a.ProductName.ToLower() != "milk").ToList();
            salePointProductTransfer.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });

            //
            int len = ProductInf.Count;
          
            for (int i = 0; i < len; i++)
            {
                PTI.Add(new ProductTransferInfo { ProductId=null,Amount=null });
            }
            salePointProductTransfer.TransferedProductInfo = PTI;
            salePointProductTransfer.Date = System.DateTime.Now.Date;
            return View(salePointProductTransfer);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SalePointProductAdd Request)
        {
            
            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                List<ProductTransferInfo> PTI = Request.TransferedProductInfo;
                int len = PTI.Count;
                List<SalePointProductConsume> newProduct = new List<SalePointProductConsume>();
                for(int i=0;i<len;i++)
                {
                    if(PTI[i].ProductId!=null&&(PTI[i].Amount!=null))
                    {
                        newProduct.Add(new SalePointProductConsume
                        {
                            SalePointId=Request.SalePointId,
                            Date=Request.Date,
                            ProductId= (int)PTI[i].ProductId,
                            Amount= (int)PTI[i].Amount,
                            OperatorName= operatorName
                        });
                    }
                }
                ViewBag.message = AgroExpressDBAccess.AddSalePointProduct(newProduct);
                if(ViewBag.message=="yes")
                {
                    return RedirectToAction(nameof(Index));
                }

            }
            foreach (ModelState modelState in ViewData.ModelState.Values)
            {
                foreach (ModelError error in modelState.Errors)
                {
                    string errorme = error.ToString();
                }
            }
            List<SalePoint> salePointList = new List<SalePoint>();
            salePointList = AgroExpressDBAccess.GetSalePointListForUSer(HttpContext.User.Identity.Name);
            SalePointProductAdd salePointProductTransfer = new SalePointProductAdd();
            salePointProductTransfer.salepointlist = salePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            List<ProductTransferInfo> PTI1 = new List<ProductTransferInfo>();
            var MilkInfo = AgroExpressDBAccess.IsProductExist("Milk");
            if (MilkInfo != null)
            {
                PTI1.Add(new ProductTransferInfo { ProductId = MilkInfo.PKProductId, Amount = null });
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
                PTI1.Add(new ProductTransferInfo { ProductId = MilkInfo.PKProductId, Amount = 0 });
            }
            // Product dropdown list
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductInf = ProductInf.Where(a => a.ProductName.ToLower() != "milk").ToList();
            salePointProductTransfer.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });

            //
            int len1 = ProductInf.Count;

            for (int i = 0; i < len1; i++)
            {
                PTI1.Add(new ProductTransferInfo { ProductId = null, Amount = null });
            }
            salePointProductTransfer.TransferedProductInfo = PTI1;
            return View(salePointProductTransfer);
        }

        public ActionResult Stock()
        {
            SalePointProductStockList request = new SalePointProductStockList();
            List<SalePoint> SalePointList = new List<SalePoint>();
            SalePointList = AgroExpressDBAccess.GetallEnabledSalePoint();

            request.salepointlist = SalePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
          
            request.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            List<SalePointProductStock> productStock = new List<SalePointProductStock>();
            productStock = AgroExpressDBAccess.GetSalePointProductStock();
            request.SearchResult= productStock.OrderBy(o => o.SalePointId).ToList();
            return View(request);
        }
        [HttpPost]
        public ActionResult Stock(SalePointProductStockList request)
        {
           
            List<SalePoint> SalePointList = new List<SalePoint>();
            SalePointList = AgroExpressDBAccess.GetallEnabledSalePoint();

            request.salepointlist = SalePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            request.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            List<SalePointProductStock> productStock = new List<SalePointProductStock>();
            productStock = AgroExpressDBAccess.GetSalePointProductStock();
           

            if(request.SalePointId!=null)
            {
                productStock = productStock.Where(a => a.SalePointId == request.SalePointId).ToList();
            }
            if(request.ProductId!=null)
            {
                productStock = productStock.Where(a => a.ProductId == request.ProductId).ToList();
            }
            if(request.StockAmountMin!=null)
            {
                productStock = productStock.Where(a => a.ProductStock >= request.StockAmountMin).ToList();
            }
            if (request.StockAmountMAx != null)
            {
                productStock = productStock.Where(a => a.ProductStock <= request.StockAmountMAx).ToList();
            }
            request.SearchResult = productStock.OrderBy(o => o.SalePointId).ToList();
            return View(request);

            
        }

        public ActionResult TransferList()
        {
            SalePointProductAddList request = new SalePointProductAddList();
            List<SalePoint> SalePointList = new List<SalePoint>();
            SalePointList = AgroExpressDBAccess.GetallEnabledSalePoint();

            request.salepointlist = SalePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            request.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            request.EntryDateMax = System.DateTime.Now;
            request.EntryDateMin = request.EntryDateMax.AddMonths(-1);
            request.SearchResult = AgroExpressDBAccess.GetSellPointProductAdd(request.EntryDateMin, request.EntryDateMax).OrderBy(a => a.Date).ToList();

            return View(request);
        }
        [HttpPost]
        public ActionResult TransferList(SalePointProductAddList request)
        {
            
            List<SalePoint> SalePointList = new List<SalePoint>();
            SalePointList = AgroExpressDBAccess.GetallEnabledSalePoint();

            request.salepointlist = SalePointList.Select(x => new SelectListItem
            {
                Value = x.PKSalePointID.ToString(),
                Text = x.SalePointName
            });
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            request.product = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            if(ModelState.IsValid)
            {
                var result = AgroExpressDBAccess.GetSellPointProductAdd(request.EntryDateMin, request.EntryDateMax).OrderBy(a => a.Date).ToList();
                if (request.ProductId!=null)
                {
                    result = result.Where(a => a.ProductId == request.ProductId).ToList();
                }
                if(request.SalePointId!=null)
                {
                    result = result.Where(a => a.SalePointId == request.SalePointId).ToList();
                }
                if (request.StockAmountMin != null)
                {
                    result = result.Where(a => a.Amount >= request.StockAmountMin).ToList();
                }
                if (request.StockAmountMAx != null)
                {
                    result = result.Where(a => a.Amount <= request.StockAmountMAx).ToList();
                }
                request.SearchResult = result.OrderBy(a=>a.salepoint.SalePointName).ToList();

            }
          
            return View(request);
        }

        public ActionResult Delete(int id)
        {
            AgroExpressDBAccess.sellPointProductTranferDellete(id);
            return RedirectToAction(nameof(TransferList));
        }

    }
}