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
    
    public class CustomerViewInfo
    {
        public string Mobile { get; set; }
        public string Adress { get; set; }
        public double Rate { get; set; }
        public double ServiceCharge { get; set; }

    }
   
    public class CustomerController : Controller
    {
        // GET: Customer
        [RBAC]
        public ActionResult Index(string ID)
        {
            ViewBag.ID = ID;
            return View();
        }
        [RBAC]
        public ActionResult EnabledCustomer()
        {
            CustomerListView culist = new CustomerListView();
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                culist.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                culist.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                culist.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            culist.customerlist = customerlist;


            if (customerlist != null)
            {
                culist.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            return View(culist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RBAC]
        public ActionResult EnabledCustomer(CustomerListView culist)
        {
            List<Customer> customerlist = AgroExpressDBAccess.GetallEnabledCustomer();
            List<Area> AreList = new List<Area>();


            if (culist.CustomerID != null)
            {
                customerlist = customerlist.Where(a => a.PKCustomerId == culist.CustomerID).ToList();

            }
            if (culist.Mobile != null)
            {
                customerlist = customerlist.Where(a => a.Mobile == culist.Mobile).ToList();
            }
            if (culist.SubAreaId != null)
            {
                customerlist = customerlist.Where(a => a.SubAreaId == culist.SubAreaId).ToList();

            }
            List<SubArea> subAreList = AgroExpressDBAccess.GetallEnabledSubArea();
            if (culist.SalePointId != null)
            {
                AreList = AgroExpressDBAccess.GetAreaBySalePointID((int)culist.SalePointId);

                if (AreList != null)
                {

                    subAreList = subAreList
                   .Where(x => AreList.Any(y => y.PKAreaId == x.AreaId)).ToList();
                }
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
            }
            if (culist.AreaId != null)
            {
                subAreList = subAreList
                  .Where(x => x.AreaId == culist.AreaId).ToList();
                customerlist = customerlist.Where(customer => subAreList.Any(subarea => subarea.PKSubAreaId == customer.SubAreaId)).ToList();
            }
            if (culist.RateMin != null)
            {
                customerlist = customerlist.Where(a => a.Rate >= culist.RateMin).ToList();
            }
            if (culist.RateMax != null)
            {
                customerlist = customerlist.Where(a => a.Rate <= culist.RateMax).ToList();
            }

            if (culist.DueMin != null)
            {
                customerlist = customerlist.Where(a => (a.TotalBill - a.TotalPaid) >= culist.DueMin).ToList();
            }
            if (culist.DueMax != null)
            {
                customerlist = customerlist.Where(a => (a.TotalBill - a.TotalPaid) <= culist.DueMax).ToList();
            }
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                culist.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                culist.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                culist.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

            culist.customerlist = customerlist;

            culist.customerlist = customerlist;
            //culist.StartValue = culist.StartValue;
            //culist.EndValue = culist.EndValue;
            culist.Mobile = culist.Mobile;
            //culist.Rate = culist.Rate;

            if (customerlist != null)
            {
                culist.selectedcustomerlist = customerlist.Select(x => new SelectListItem
                {
                    Value = x.PKCustomerId.ToString(),
                    Text = x.FullName
                });
            }

            return View(culist);
        }
        public ActionResult DisabledCustomer()
        {

            var culist = AgroExpressDBAccess.GetallDisabledCustomer();
            return View(culist);
        }

        [RBAC]
        public ActionResult Registration()
        {
            CustomerRegistration newCu = new CustomerRegistration();

            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                newCu.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                newCu.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                newCu.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }
            newCu.SMS = true;
            return View(newCu);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RBAC]
        public ActionResult Registration([Bind(Exclude = "TotalPaid,User_Id")] CustomerRegistration customer)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                customer.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                customer.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }
            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                customer.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }

           
            if (ModelState.IsValid)
            {
                var cu = AgroExpressDBAccess.IsMobileExists(customer.Mobile);
                if (cu != null)
                {
                    ModelState.AddModelError("Mobile", "Mobile number already Exists  ");
                    return View(customer);
                }
                if (AgroExpressDBAccess.AddCustomer(customer))
                {
                    ViewBag.success = "Customer added successfully";

                    ModelState.Clear();
                    CustomerRegistration tem = new CustomerRegistration();

                    if (salepointlist != null)
                    {
                        tem.salepointlist = salepointlist.Select(x => new SelectListItem
                        {
                            Value = x.PKSalePointID.ToString(),
                            Text = x.SalePointName
                        });
                    }

                    if (arealist != null)
                    {
                        tem.arealist = arealist.Select(x => new SelectListItem
                        {
                            Value = x.PKAreaId.ToString(),
                            Text = x.AreaName
                        });
                    }

                    if (sarealist != null)
                    {
                        tem.subarealist = sarealist.Select(x => new SelectListItem
                        {
                            Value = x.PKSubAreaId.ToString(),
                            Text = x.SubAreaName
                        });
                    }

                    return View(tem);
                }
            }

            return View(customer);
        }
        [RBAC]
        public ActionResult Edit(int id)
        {
            var customer = AgroExpressDBAccess.GetCustomerByID(id);
            var customeredit = new CustomerEdit();
            if (customer != null)
            {
                customeredit.PKCustomerId = customer.PKCustomerId;
                customeredit.Address = customer.Address;
                customeredit.SubAreaId = customer.SubAreaId;
                customeredit.Mobile = customer.Mobile;
                customeredit.Rate = customer.Rate;
                customeredit.ServiceCharge = customer.ServiceCharge;
                customeredit.TotalBill = customer.TotalBill;
                customeredit.TotalPaid = customer.TotalPaid;
                customeredit.FullName = customer.FullName;
                customeredit.LoginUserID = customer.LoginUserID;
                var user = AgroExpressDBAccess.GetUserByID(customer.LoginUserID);
                customeredit.UserName = user.UserName;
                customeredit.Password = user.Password;
                customeredit.SMS = customer.SendSMS;
            }


            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                customeredit.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            var areaid = AgroExpressDBAccess.GetAreaBySubAreaID(customer.SubAreaId);
            customeredit.AreaId = areaid;
            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                customeredit.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            var salepointid = AgroExpressDBAccess.GetSalePointByAreaID(areaid);
            customeredit.SalePointId = salepointid;

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                customeredit.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }
            return View(customeredit);
        }
        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(CustomerEdit customeredit)
        {
            List<SalePoint> salepointlist = AgroExpressDBAccess.GetallEnabledSalePoint();
            if (salepointlist != null)
            {
                customeredit.salepointlist = salepointlist.Select(x => new SelectListItem
                {
                    Value = x.PKSalePointID.ToString(),
                    Text = x.SalePointName
                });
            }

            List<Area> arealist = AgroExpressDBAccess.GetallEnabledArea();
            if (arealist != null)
            {
                customeredit.arealist = arealist.Select(x => new SelectListItem
                {
                    Value = x.PKAreaId.ToString(),
                    Text = x.AreaName
                });
            }

            List<SubArea> sarealist = AgroExpressDBAccess.GetallEnabledSubArea();
            if (sarealist != null)
            {
                customeredit.subarealist = sarealist.Select(x => new SelectListItem
                {
                    Value = x.PKSubAreaId.ToString(),
                    Text = x.SubAreaName
                });
            }
            if (ModelState.IsValid)
            {
                var userinfo = AgroExpressDBAccess.IsUserExist(customeredit.UserName);
                if(userinfo!=null)
                {
                    if (userinfo.PkUserID != customeredit.LoginUserID)
                    {
                        ModelState.AddModelError("UserName", "User Name Already Exists!!!");
                        return View(customeredit);
                    }
                }
                
                if (AgroExpressDBAccess.UpdateCustomer(customeredit))
                {
                    ViewBag.success = "Customer added successfully";

                    return RedirectToAction("EnabledCustomer");
                }
                
            }

            return View(customeredit);

        }
        [RBAC]
        public ActionResult Delete(int id)
        {
            var cId = AgroExpressDBAccess.GetCustomerByID(id);

            if (cId.IsDeleted == false)
            {
                AgroExpressDBAccess.DisableCustomer(cId.PKCustomerId);
                return RedirectToAction("EnabledCustomer");
            }
            else
            {
                return RedirectToAction("EnabledCustomer");
            }
        }
        [RBAC]
        public ActionResult Enable(int id)
        {
            AgroExpressDBAccess.EnableCustomer(id);
            return RedirectToAction("DisabledCustomer");

        }
        public JsonResult SalePointList(string SalePointName)
        {
            var selectList = new List<SelectListItem>();
            var areainfo = AgroExpressDBAccess.GetSalePointByName(SalePointName);
            var areaList = AgroExpressDBAccess.GetAreaBySalePointID(areainfo.PKSalePointID);
            if (areaList != null)
            {
                foreach (var area in areaList)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = area.PKAreaId.ToString(),
                        Text = area.AreaName,

                    });
                }
            }
            return Json(selectList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult AreaList(string AreaName)
        {
            var selectList = new List<SelectListItem>();
            var areainfo = AgroExpressDBAccess.GetAreaByName(AreaName);
            var subareaList = AgroExpressDBAccess.GetSubAreaByAreaID(areainfo.PKAreaId);
            if (subareaList != null)
            {
                foreach (var area in subareaList)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = area.PKSubAreaId.ToString(),
                        Text = area.SubAreaName,

                    });
                }
            }
            return Json(selectList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CustomerList(int SubAreaId)
        {
            var selectList = new List<SelectListItem>();
            var customerinfo = AgroExpressDBAccess.GetallEnabledCustomerBySubAreaId(SubAreaId);
           
            if (customerinfo != null)
            {
                foreach (var area in customerinfo)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = area.PKCustomerId.ToString(),
                        Text = area.FullName,

                    });
                }
            }
            return Json(selectList, JsonRequestBehavior.AllowGet);
        }
        public JsonResult CustomerInfo(int CustomerID)
        {

            var customerinfo = AgroExpressDBAccess.GetCustomerByID(CustomerID);

            var returnResult = new CustomerViewInfo
            {
                Mobile = customerinfo.Mobile,
                Adress = customerinfo.Address,
                Rate = customerinfo.Rate,
                ServiceCharge = customerinfo.ServiceCharge
            };

            return Json(returnResult, JsonRequestBehavior.AllowGet);
        }
        [RBAC]
        public ActionResult CustomerDueList()
        {
            var customerlist = AgroExpressDBAccess.GetallEnabledCustomer();

            List<CustomerDues> customerdueList = new List<CustomerDues>();
            foreach (var due in customerlist)
            {
                var subarea = AgroExpressDBAccess.GetSubAreaById(due.SubAreaId);
                double bill;
                if (due.TotalPaid >= due.TotalBill)
                    bill = 0;
                else
                {
                    bill = due.TotalBill - due.TotalPaid;

                    customerdueList.Add(new CustomerDues { CustomerName = due.FullName, Mobile = due.Mobile, SubAreaName = subarea.SubAreaName, TotalDue = bill });
                }

            }

            return View(customerdueList);
        }
        [RBAC]
        public ActionResult TransactionHistory(int? id)
        {
            
            if (id == null)
            {
                string name = HttpContext.User.Identity.Name;
                id = AgroExpressDBAccess.GetCustomerIdByUserId(name);
                if (id == 0)
                {
                    return RedirectToAction("ResourceNotFound", "Home");
                }
            }
            var customer = AgroExpressDBAccess.GetCustomerByID((int)id);
            if (customer != null)
            {

                List<CustomerTransactionDetails> li = new List<CustomerTransactionDetails>();
                CustomerTransactionDate tem = new CustomerTransactionDate();
                tem.ID = customer.PKCustomerId;
                tem.Name = customer.FullName;
                tem.transactiondetails = li;
                tem.EndDate = DateTime.Now.AddDays(1).Date;
                tem.StartDate = tem.EndDate.AddDays(-30);
                var cus = AgroExpressDBAccess.GetCustomerTransectionHistory(tem.StartDate, tem.EndDate, (int)id);

                List<CustomerTransactionDetails> transactions = new List<CustomerTransactionDetails>();

                tem.transactiondetails = cus;
                return View(tem);
            }

            return Redirect(Request.UrlReferrer.ToString());

        }
        [RBAC]
        [HttpPost]
        public ActionResult TransactionHistory(DateTime StartDate, DateTime EndDate, int ID)
        {
            var customer = AgroExpressDBAccess.GetCustomerByID(ID);
            if (customer != null)
            {

                List<CustomerTransactionDetails> li = new List<CustomerTransactionDetails>();
                CustomerTransactionDate tem = new CustomerTransactionDate();
                tem.ID = customer.PKCustomerId;
                tem.Name = customer.FullName;
                tem.transactiondetails = li;
                tem.EndDate = EndDate;
                tem.StartDate = StartDate;
                var cus = AgroExpressDBAccess.GetCustomerTransectionHistory(tem.StartDate, tem.EndDate.AddDays(1).Date, ID);

                List<CustomerTransactionDetails> transactions = new List<CustomerTransactionDetails>();

                tem.transactiondetails = cus;
                return View(tem);
            }
            return Redirect(Request.UrlReferrer.ToString());
        }
    }
}