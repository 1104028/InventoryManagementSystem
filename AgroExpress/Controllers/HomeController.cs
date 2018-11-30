using AgroExpress.DataLayer;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace AgroExpress.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
        [RBAC]
        public ActionResult Registration()
        {
            return View();
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Registration(AdminRegistration admin)
        {
            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsUserExist(admin.UserName);
                if (user != null)
                {
                    ModelState.AddModelError("UserName", "User name already exist");
                    return View(admin);
                }

                if (AgroExpressDBAccess.AddAdmin(admin))
                {
                    ViewBag.success = "Registration Successsfull";
                    ModelState.Clear();
                    AdminRegistration registration = new AdminRegistration();
                    return View(registration);
                }
            }
            return View(admin);
        }

        [RBAC]
        public ActionResult Edit(int id)
        {
            var admininfo = AgroExpressDBAccess.GetAdminByID(id);
            AdminEdit adminedit = new AdminEdit();

            if(admininfo!=null)
            {
                adminedit.PKAdminId = admininfo.PKAdminId;
                adminedit.FullName = admininfo.FullName;
                adminedit.Address = admininfo.Address;
                adminedit.Mobile = admininfo.Mobile;
                adminedit.Email = admininfo.Email;

                var user = AgroExpressDBAccess.GetUserByID(admininfo.LoginUserID);
                adminedit.UserName = user.UserName;
                adminedit.Password = user.Password;

                adminedit.LoginUserID = admininfo.LoginUserID;
            }
            return View(adminedit);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AdminEdit editentry)
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
                if (AgroExpressDBAccess.UpdateAdmin(editentry))
                {
                    ViewBag.success = "Admin has been updated successfully";

                    return RedirectToAction("EnabledList");
                }

            }
            return View();
        }
        [RBAC]
        public ActionResult ResourceNotFound()
        {
            return View();
        }
        [RBAC]
        public ActionResult EnabledList()
        {
            var adminlist = AgroExpressDBAccess.GetallEnabledAdmin();

            return View(adminlist);
        }
        [RBAC]
        public ActionResult DisabledList()
        {
            var adminlist = AgroExpressDBAccess.GetallDisabledAdmin();

            return View(adminlist);
        }
        [RBAC]
        public ActionResult Delete(int id)
        {
            var aId = AgroExpressDBAccess.GetAdminByID(id);

            if (aId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableAdmin(aId.PKAdminId);
                return RedirectToAction("EnabledList");
            }
            else
            {
                return RedirectToAction("EnabledList");
            }
        }
        [RBAC]
        public ActionResult Enable(int id)
        {
            var aId = AgroExpressDBAccess.GetAdminByID(id);

            if (aId.IsDeleted == true)
            {
                AgroExpressDBAccess.EnableAdmin(aId.PKAdminId);
                return RedirectToAction("DisabledList");
            }
            else
            {
                return RedirectToAction("DisabledList");
            }
        }
        [RBAC]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Home");
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        // [Authorize]
        // [HttpPost]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginView login, string ReturnUrl = "")
        {

            if (ModelState.IsValid)
            {
                var user = AgroExpressDBAccess.IsEnableUserExist(login.UserName);
                if (!user)
                {
                    
                    ModelState.AddModelError("UserName", "User name not found!!");
                    return View();
                }
                //var pass = Cryptography.Hash(login.Password);
                var pass = login.Password;
                var u = AgroExpressDBAccess.IsUserPassValid(login.UserName, pass);
                if (u == null)
                {
                    ModelState.AddModelError("Password", "User name and password not matched!!!");
                    return View();
                }
                int timeout = login.RememberMe ? 525600 : 60; // 525600 min = 1 year
                var ticket = new FormsAuthenticationTicket(login.UserName, login.RememberMe, timeout);
                string encrypted = FormsAuthentication.Encrypt(ticket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, encrypted);
                cookie.Expires = DateTime.Now.AddMinutes(timeout);
                cookie.HttpOnly = true;
                Response.Cookies.Add(cookie);
             
                return RedirectToAction("Index", "Administrator");

            }
            return View();
        }
        [RBAC]
        public ActionResult ChangePassword()
        {
            ChangePassword updatep = new ChangePassword();
           
            string name = HttpContext.User.Identity.Name;
            var user= AgroExpressDBAccess.IsUserExist(name);
            updatep.LoginUserID = user.PkUserID;
            updatep.UserName = user.UserName;
            return View(updatep);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePassword updatep)
        {
            if (ModelState.IsValid)
            {
                if (updatep.Password != updatep.ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Password Not match");
                    return View(updatep);
                }

                var prevpass = AgroExpressDBAccess.IsUserExist(updatep.UserName);
                if (prevpass != null)
                {
                    if (prevpass.Password != updatep.PrevPassword)
                    {
                        ModelState.AddModelError("PrevPassword", "InCorrect Previous Password");
                        return View(updatep);
                    }

                    if (prevpass.UserName == updatep.UserName)
                    {
                        UpdatePassword updatepass = new UpdatePassword();
                        updatepass.UserName = updatep.UserName;
                        updatepass.Password = updatep.Password;
                        updatepass.UserType = prevpass.UserType;
                        AgroExpressDBAccess.UpdatePassword(updatepass);

                        return RedirectToAction("Index", "Administrator");

                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "User does not Exists!!!");
                    return View(updatep);
                }


            }
            return View(updatep);
        }

        [RBAC]
        public ActionResult CustomerChangePassword()
        {
            ChangePassword updatep = new ChangePassword();
            string name = HttpContext.User.Identity.Name;
            var user = AgroExpressDBAccess.IsUserExist(name);
            updatep.LoginUserID = user.PkUserID;
            updatep.UserName = user.UserName;
            return View(updatep);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CustomerChangePassword(ChangePassword updatep)
        {
            if (ModelState.IsValid)
            {
                if (updatep.Password != updatep.ConfirmPassword)
                {
                    ModelState.AddModelError("Password", "Password Not match");
                    return View(updatep);
                }

                var prevpass = AgroExpressDBAccess.IsUserExist(updatep.UserName);
                if (prevpass != null)
                {
                    if (prevpass.Password != updatep.PrevPassword)
                    {
                        ModelState.AddModelError("PrevPassword", "InCorrect Previous Password");
                        return View(updatep);
                    }

                    if (prevpass.UserName == updatep.UserName)
                    {
                        UpdatePassword updatepass = new UpdatePassword();
                        updatepass.UserName = updatep.UserName;
                        updatepass.Password = updatep.Password;
                        updatepass.UserType = prevpass.UserType;
                        AgroExpressDBAccess.UpdatePassword(updatepass);

                        return RedirectToAction("Index");

                    }
                }
                else
                {
                    ModelState.AddModelError("UserName", "User does not Exists!!!");
                    return View(updatep);
                }

            }
            return View(updatep);
        }
    }
}