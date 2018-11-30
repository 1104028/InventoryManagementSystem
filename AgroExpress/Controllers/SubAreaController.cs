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
    public class SubAreaController : Controller
    {
        // GET: SubArea
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Add()
        {
            AddSubArea addsubarea = new AddSubArea();
            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                addsubarea.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            return View(addsubarea);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AddSubArea nsubarea)
        {
            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                nsubarea.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }


            if (ModelState.IsValid)
            {
                if (AgroExpressDBAccess.isSubAreaExist(nsubarea.SubAreaName))
                {
                    ModelState.AddModelError("SubAreaName", "Sub Area name already exist!!!");
                    return View(nsubarea);
                }
                else
                {
                    SubArea newentry = new SubArea();

                    newentry.SubAreaName = nsubarea.SubAreaName;
                    newentry.AreaId = nsubarea.AreaId;
                    newentry.IsDeleted = false;
                    if (AgroExpressDBAccess.AddSubArea(newentry))
                    {
                        ViewBag.success = "Sub Area added successfully";
                        ModelState.Clear();
                        AddSubArea tem = new AddSubArea();
                        if (arealist != null)
                        {
                            tem.arealist = arealist.Select(x => new SelectListItem
                            {
                                Value = x.PKAreaId.ToString(),
                                Text = x.AreaName
                            });
                        }

                        return View(tem);
                    }

                }
            }
            return View(nsubarea);

        }

        public ActionResult EnabledList()
        {
            var subarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            return View(subarealist);
        }

        public ActionResult DisabledList()
        {
            var subarealist = AgroExpressDBAccess.GetallDisabledSubArea();
            return View(subarealist);
        }

        public ActionResult Delete(int id)
        {
            var sId = AgroExpressDBAccess.GetSubAreaById(id);

            if (sId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableSubArea(sId.PKSubAreaId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
            var sId = AgroExpressDBAccess.GetSubAreaById(id);

            if (sId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnableSubArea(sId.PKSubAreaId);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }

        public ActionResult Edit(int id)
        {
            var sarea = AgroExpressDBAccess.GetSubAreaById(id);
            EditSubArea editentry = new EditSubArea();
            if (sarea != null)
            {
                List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
                if (arealist != null)
                {
                    editentry.arealist = arealist.Select(x => new SelectListItem
                    {
                        Value = x.PKAreaId.ToString(),
                        Text = x.AreaName
                    });
                }

                editentry.PKSubAreaId = sarea.PKSubAreaId;
                editentry.SubAreaName = sarea.SubAreaName;
                editentry.AreaId = sarea.AreaId;
            }
            return View(editentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EditSubArea narea)
        {
            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                narea.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            if (ModelState.IsValid)
            {
                //if (AgroExpressDBAccess.isSubAreaExist(narea.SubAreaName))
                //{
                //    ModelState.AddModelError("SubAreaName", "Sub Area Name name already exist!!!");
                //    return View(narea);
                //}
                //else
                //{
                    if (AgroExpressDBAccess.UpdateSubArea(narea))
                    {
                        return RedirectToAction("EnabledList");
                    }
                //}
            }

            return View(narea);

        }
    }
}