using AgroExpress.DataLayer;
using AgroExpress.DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Controllers
{
    [RBAC]
    public class PartnerController : Controller
    {
        public ActionResult Registration()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(PartnerRegistration partner)
        {
            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsUserExist(partner.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(partner);
                }

                if (AgroExpressDBAccess.AddPartner(partner))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    PartnerRegistration registration = new PartnerRegistration();
                    return View(registration);
                }
            }
            return View(partner);
        }

        public ActionResult Edit(int id)
        {
            var partnerinfo = AgroExpressDBAccess.GetPartnerByID(id);
            PartnerEdit adminedit = new PartnerEdit();

            if (partnerinfo != null)
            {
                adminedit.PKPartnerId = partnerinfo.PKPartnerId;
                adminedit.FullName = partnerinfo.FullName;
                adminedit.Address = partnerinfo.Address;
                adminedit.Mobile = partnerinfo.Mobile;
                adminedit.Email = partnerinfo.Email;

                var user = AgroExpressDBAccess.GetUserByID(partnerinfo.LoginUserID);
                adminedit.UserName = user.UserName;
                adminedit.Password = user.Password;

                adminedit.LoginUserID = partnerinfo.LoginUserID;
            }
            return View(adminedit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PartnerEdit editentry)
        {
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
                if (AgroExpressDBAccess.UpdatePartner(editentry))
                {
                    ViewBag.success = "Partner has been updated successfully";

                    return RedirectToAction("EnabledList");
                }

            }
            return View();
        }

        public ActionResult EnabledList()
        {
            var adminlist = AgroExpressDBAccess.GetallEnabledPartner();

            return View(adminlist);
        }

        public ActionResult DisabledList()
        {
            var adminlist = AgroExpressDBAccess.GetallDisabledPartner();

            return View(adminlist);
        }

        public ActionResult Delete(int id)
        {
            var aId = AgroExpressDBAccess.GetPartnerByID(id);

            if (aId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisablePartner(aId.PKPartnerId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
            var aId = AgroExpressDBAccess.GetPartnerByID(id);

            if (aId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnablePartner(aId.PKPartnerId);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }
    }
}