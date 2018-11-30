using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer;
using AgroExpress.DataLayer.ViewModel;

namespace AgroExpress.Controllers
{
    [RBAC]
    public class AreaController : Controller
    {
        // GET: Area
        public ActionResult EnabledArea()
        {
            var arealist = AgroExpressDBAccess.GetallEnabledArea();

            return View(arealist);
        }

        public ActionResult DisabledArea()
        {
            var arealist = AgroExpressDBAccess.GetallDisabledArea();

            return View(arealist);
        }

        public ActionResult Add()
        {
            AddArea addarea = new AddArea();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                addarea.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            return View(addarea);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddArea narea)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                narea.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (ModelState.IsValid)
            {
                if (AgroExpressDBAccess.isAreaExist(narea.AreaName))
                {
                    ModelState.AddModelError("AreaName", "Area name already exist!!!");
                    return View(narea);
                }
                else
                {
                    Area newentry = new Area();

                    newentry.AreaName = narea.AreaName;
                    newentry.SalePointId = narea.SalePointId;
                    newentry.IsDeleted = false;
                    if (AgroExpressDBAccess.AddArea(newentry))
                    {
                        ViewBag.success = "Area added successfully";
                        ModelState.Clear();
                        AddArea tem = new AddArea();
                        if (salepointlist != null)
                        {
                            tem.salepointlist = salepointlist.Select(x => new SelectListItem
                            {
                                Value = x.PKSalePointID.ToString(),
                                Text = x.SalePointName
                            });
                        }

                        return View(tem);
                    }

                }
            }
            return View(narea);

        }
        public ActionResult Delete(int id)
        {
            var area = AgroExpressDBAccess.isAreaExistById(id);
            if (area)
            {
                AgroExpressDBAccess.DisableArea(id);
            }
            return RedirectToAction("EnabledArea");

        }

        public ActionResult Enable(int id)
        {
            var area = AgroExpressDBAccess.isAreaExistById(id);
            if (area)
            {
                AgroExpressDBAccess.EnableArea(id);
            }
            return RedirectToAction("DisabledArea");

        }
        public ActionResult Edit(int id)
        {
            var area = AgroExpressDBAccess.GetAreaInformationById(id);
            EditArea editentry = new EditArea();
            if (area != null)
            {
                List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
                if (salepointlist != null)
                {
                    editentry.salepointlist = salepointlist.Select(x => new SelectListItem
                    {
                        Value = x.PKSalePointID.ToString(),
                        Text = x.SalePointName
                    });
                }

                editentry.PKAreaId = area.PKAreaId;
                editentry.AreaName = area.AreaName;
                editentry.SalePointId = area.SalePointId;
            }
            return View(editentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditArea narea)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                narea.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (ModelState.IsValid)
            {
                //if (AgroExpressDBAccess.isAreaExist(narea.AreaName))
                //{
                //    ModelState.AddModelError("AreaName", "Area name already exist!!!");
                //    return View(narea);
                //}
                //else
                //{
                    if (AgroExpressDBAccess.UpdateArea(narea))
                    {
                        return RedirectToAction("EnabledArea");
                    }                   
                //}
            }

            return View(narea);

        }
    }
}