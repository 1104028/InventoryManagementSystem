using AgroExpress.DataLayer;
using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Controllers
{
    public class SMSController : Controller
    {
        // GET: SMS

        [RBAC]
        public ActionResult Update()
        {
            SMSConfiguration sms = new SMSConfiguration();
            var smsdata = AgroExpressDBAccess.GetSMSPrivacyInformation();


            sms.APIKey = smsdata.APIKey;
            sms.Masking = smsdata.Masking;
            sms.ID = smsdata.ID;
            return View(sms);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(SMSConfiguration sms)
        {
            if (ModelState.IsValid)
            {
                if (sms.Masking == null)
                {
                    sms.Masking = "";
                }
                if (AgroExpressDBAccess.UpdateSMSConfiguration(sms))
                {
                    return RedirectToAction("Index", "Administrator");
                }
            }

            return View(sms);
        }

    }
}