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
    public class SalePointController : Controller
    {
        // GET: SalePoint
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(SalePoint newentry)
        {
            if(ModelState.IsValid)
            {
                var salepoint = AgroExpressDBAccess.IsSalePointExist(newentry.SalePointName);

                if (salepoint != null)
                {
                    ModelState.AddModelError("SalePointName", "Sale Point already exist");
                    return View(newentry);
                }

                var salepointt = new SalePoint();
                salepointt.SalePointName = newentry.SalePointName;
                salepointt.SalePointAddress = newentry.SalePointAddress;
                salepointt.IsDeleted = false;

                if (AgroExpressDBAccess.AddSalePoint(salepointt))
                {
                    ViewBag.success = "Sale Point Add Successsfull";
                    ModelState.Clear();
                    SalePoint registration = new SalePoint();
                    return View(registration);
                }
            }
            return View(newentry);
        }

        public ActionResult EnabledList()
        {
            var salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            return View(salepointlist);
        }

        public ActionResult DisabledList()
        {
            var salepointlist = AgroExpressDBAccess.GetallDisabledSalePoint();
            return View(salepointlist);
        }

        public ActionResult Delete(int id)
        {
            var cId = AgroExpressDBAccess.GetSalePointById(id);

            if (cId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableSalePoint(cId.PKSalePointID);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
            var cId = AgroExpressDBAccess.GetSalePointById(id);

            if (cId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnableSalePoint(cId.PKSalePointID);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }

        public ActionResult Edit(int id)
        {
            var salepoint = AgroExpressDBAccess.GetSalePointById(id);
            EditSalePoint editentry = new EditSalePoint();
            if (salepoint != null)
            {
                
                editentry.PKSalePointID = salepoint.PKSalePointID;
                editentry.SalePointName = salepoint.SalePointName;
                editentry.SalePointAddress = salepoint.SalePointAddress;
            }
            return View(editentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSalePoint editentry)
        {

            if (ModelState.IsValid)
            {
                var name = AgroExpressDBAccess.IsSalePointExist(editentry.SalePointName);
                if (name!=null)
                {
                    ModelState.AddModelError("SalePointName", "Sale Point Name already exist!!!");
                    return View(editentry);
                }
                else
                {
                    if (AgroExpressDBAccess.UpdateSalepoint(editentry))
                    {
                        return RedirectToAction("EnabledList");
                    }
                }
            }

            return View(editentry);

        }
    }
}