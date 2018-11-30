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
    public class ManagerController : Controller
    {
       
        public ActionResult Registration()
        {
            ManagerRegistration newmanager = new ManagerRegistration();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                newmanager.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            return View(newmanager);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(ManagerRegistration manager)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                manager.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsUserExist(manager.UserName);
                if(user!=null)
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(manager);
                }

                if (AgroExpressDBAccess.AddManager(manager))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    ManagerRegistration registration = new ManagerRegistration();

                    if (salepointlist != null)
                    {
                        registration.salepointlist = salepointlist.Select(x => new SelectListItem
                        {
                            Value = x.PKSalePointID.ToString(),
                            Text = x.SalePointName
                        });
                    }
                    return View(registration);
                }

            }
            return View(manager);
        }
        public ActionResult Edit(int id)
        {
            var managerinfo = AgroExpressDBAccess.GetManagerByID(id);
            ManagerEdit manageredit = new ManagerEdit();

            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                manageredit.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (managerinfo != null)
            {
                manageredit.PKManagerId = managerinfo.PKManagerId;
                manageredit.FullName = managerinfo.FullName;
                manageredit.Address = managerinfo.Address;
                manageredit.Mobile = managerinfo.Mobile;
                manageredit.Email = managerinfo.Email;

                var user = AgroExpressDBAccess.GetUserByID(managerinfo.LoginUserID);
                manageredit.UserName = user.UserName;
                manageredit.Password = user.Password;

                var deliveryboysalepoints = AgroExpressDBAccess.GetSalePointListForUSerId(managerinfo.LoginUserID);
                if (deliveryboysalepoints != null)
                {
                    for (var i = 0; i < deliveryboysalepoints.Count; i++)
                    {
                        if (i == deliveryboysalepoints.Count - 1)
                            manageredit.SelectedSalePoints += deliveryboysalepoints[i].SalePointName;
                        else
                            manageredit.SelectedSalePoints += deliveryboysalepoints[i].SalePointName + ",";
                    }
                }
                manageredit.LoginUserID = managerinfo.LoginUserID;
            }
            return View(manageredit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ManagerEdit editentry)
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

            if (ModelState.IsValid)
            {
                var userinfo = AgroExpressDBAccess.IsUserExist(editentry.UserName);
                if (userinfo != null)
                {
                    if (userinfo.PkUserID != editentry.LoginUserID)
                    {
                        ModelState.AddModelError("UserName", "User Name Already Exists!!!");
                        return View(editentry);
                    }
                }
                if (AgroExpressDBAccess.UpdateManager(editentry))
                {
                    ViewBag.success = "Manager has been updated successfully";

                    return RedirectToAction("EnabledList");
                }

            }
            return View(editentry);
        }

        public ActionResult EnabledList()
        {
            var managerlist = AgroExpressDBAccess.GetallEnabledManager();

            return View(managerlist);
        }

        public ActionResult DisabledList()
        {
            var managerlist = AgroExpressDBAccess.GetallDisabledManager();

            return View(managerlist);
        }

        public ActionResult Delete(int id)
        {
            var mId = AgroExpressDBAccess.GetManagerByID(id);

            if (mId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableManager(mId.PKManagerId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
            var mId = AgroExpressDBAccess.GetManagerByID(id);

            if (mId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnableManager(mId.PKManagerId);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }


    }
}