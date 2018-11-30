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
    public class FirmManagerController : Controller
    {
        public ActionResult Registration()
        {
            FirmManagerRegistration newfirmmanager = new FirmManagerRegistration();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                newfirmmanager.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            return View(newfirmmanager);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(FirmManagerRegistration firmmanager)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                firmmanager.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsUserExist(firmmanager.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(firmmanager);
                }

                if (AgroExpressDBAccess.AddFirmManager(firmmanager))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    FirmManagerRegistration registration = new FirmManagerRegistration();

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
            return View(firmmanager);
        }
        public ActionResult Edit(int id)
        {
            var firmmanagerinfo = AgroExpressDBAccess.GetFirmManagerByID(id);
            FirmManagerEdit manageredit = new FirmManagerEdit();

            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                manageredit.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (firmmanagerinfo != null)
            {
                manageredit.PKFirmManagerId = firmmanagerinfo.PKFirmManagerId;
                manageredit.FullName = firmmanagerinfo.FullName;
                manageredit.Address = firmmanagerinfo.Address;
                manageredit.Mobile = firmmanagerinfo.Mobile;
                manageredit.Email = firmmanagerinfo.Email;

                var user = AgroExpressDBAccess.GetUserByID(firmmanagerinfo.LoginUserID);
                manageredit.UserName = user.UserName;
                manageredit.Password = user.Password;

                var salepoints = AgroExpressDBAccess.GetSalePointListForUSerId(firmmanagerinfo.LoginUserID);
                if (salepoints != null)
                {
                    for (var i = 0; i < salepoints.Count; i++)
                    {
                        if (i == salepoints.Count - 1)
                            manageredit.SelectedSalePoints += salepoints[i].SalePointName;
                        else
                            manageredit.SelectedSalePoints += salepoints[i].SalePointName + ",";
                    }
                }
                manageredit.LoginUserID = firmmanagerinfo.LoginUserID;
            }
            return View(manageredit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(FirmManagerEdit editentry)
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
                if (AgroExpressDBAccess.UpdateFirmManager(editentry))
                {
                    ViewBag.success = "Firm Manager has been updated successfully";

                    return RedirectToAction("EnabledList");
                }

            }
            return View(editentry);
        }

        public ActionResult EnabledList()
        {
            var firmmanagerlist = AgroExpressDBAccess.GetallEnabledFirmManager();

            return View(firmmanagerlist);
        }

        public ActionResult DisabledList()
        {
            var firmmanagerlist = AgroExpressDBAccess.GetallDisabledFirmManager();

            return View(firmmanagerlist);
        }

        public ActionResult Delete(int id)
        {
            var fmId = AgroExpressDBAccess.GetFirmManagerByID(id);

            if (fmId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableFirmManager(fmId.PKFirmManagerId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
           
                AgroExpressDBAccess.EnableFirmManager(id);
                return RedirectToAction("DisabledList");
            
        }
    }
}