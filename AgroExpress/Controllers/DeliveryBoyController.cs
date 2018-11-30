using AgroExpress.DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.Controllers
{
    [RBAC]
    public class DeliveryBoyController : Controller
    {
        // GET: DeliveryBoy
        
        public ActionResult Registration()
        {
            DeliveryBoyRegistration newdeliveryboy = new DeliveryBoyRegistration();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                newdeliveryboy.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }
            return View(newdeliveryboy);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(DeliveryBoyRegistration delivery)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                delivery.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }


            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsUserExist(delivery.UserName);
                if(user!=null)
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(delivery);
                }

                if (AgroExpressDBAccess.AddDeliveryBoy(delivery))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    DeliveryBoyRegistration registration = new DeliveryBoyRegistration();


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
            return View(delivery);
        }

        public ActionResult Edit(int id)
        {
            var deliveryboyinfo = AgroExpressDBAccess.GetDeliveryBoyByID(id);
            DeliveryBoyEdit deliveryboyedit = new DeliveryBoyEdit();

            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                deliveryboyedit.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            if (deliveryboyinfo != null)
            {
                deliveryboyedit.PKDeliveryBoyId = deliveryboyinfo.PKDeliveryBoyId;
                deliveryboyedit.FullName = deliveryboyinfo.FullName;
                deliveryboyedit.Address = deliveryboyinfo.Address;
                deliveryboyedit.Mobile = deliveryboyinfo.Mobile;
                deliveryboyedit.Email = deliveryboyinfo.Email;

                var user = AgroExpressDBAccess.GetUserByID(deliveryboyinfo.LoginUserID);
                deliveryboyedit.UserName = user.UserName;
                deliveryboyedit.Password = user.Password;

                var deliveryboysalepoints = AgroExpressDBAccess.GetSalePointListForUSerId(deliveryboyinfo.LoginUserID);
                if(deliveryboysalepoints!=null)
                {
                    for(var i=0;i<deliveryboysalepoints.Count;i++)
                    {
                        if(i== deliveryboysalepoints.Count-1)
                            deliveryboyedit.SelectedSalePoints += deliveryboysalepoints[i].SalePointName;
                        else
                            deliveryboyedit.SelectedSalePoints += deliveryboysalepoints[i].SalePointName + ",";
                    }
                }
                deliveryboyedit.LoginUserID = deliveryboyinfo.LoginUserID;
            }
            return View(deliveryboyedit);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(DeliveryBoyEdit editentry)
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
                if (AgroExpressDBAccess.UpdateDeliveryBoy(editentry))
                {
                    ViewBag.success = "Delivery Boy has been updated successfully";

                    return RedirectToAction("EnabledList");
                }

            }
            return View(editentry);
        }

        public ActionResult EnabledList()
        {
            var deliveryboylist = AgroExpressDBAccess.GetallEnabledDeliveryBoy();

            return View(deliveryboylist);
        }

        public ActionResult DisabledList()
        {
            var deliveryboylist = AgroExpressDBAccess.GetallDisabledDeliveryBoy();

            return View(deliveryboylist);
        }

        public ActionResult Delete(int id)
        {
            var aId = AgroExpressDBAccess.GetDeliveryBoyByID(id);

            if (aId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableDeliveryBoy(aId.PKDeliveryBoyId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }

        public ActionResult Enable(int id)
        {
            var aId = AgroExpressDBAccess.GetDeliveryBoyByID(id);

            if (aId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnableDeliveryBoy(aId.PKDeliveryBoyId);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }
    }
}