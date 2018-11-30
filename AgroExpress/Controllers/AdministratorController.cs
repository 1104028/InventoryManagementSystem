using AgroExpress.DataLayer;
using AgroExpress.DataLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Controllers
{
    public class AdministratorController : Controller
    {
        // GET: Administrator
        [RBAC]
        public ActionResult Index(int? orderid)
        {
            Administrator admin = new Administrator();



            DateTime date = System.DateTime.Now.Date;
            DateTime date1 = date.AddDays(+7);

            //--------------------------Medication ------------------------------
            //#region
            //var availablemedicinedose = AnimalInformationDBAcces.GetAvailableMedicinedose();
            //if (availablemedicinedose!=null)
            //{
            //    availablemedicinedose = availablemedicinedose.Where(a => a.MedicationDate >= date && a.MedicationDate <= date1).ToList();

            //}
            //List<MedicationList> lists = new List<MedicationList>();

            //foreach (var medicineli in availablemedicinedose)
            //{
            //    var animalcodename = AnimalInformationDBAcces.GetAnimalInfoByID(medicineli.medicine.AnimalId);
            //    lists.Add(new MedicationList
            //    {
            //        medicationId = medicineli.medicationId,
            //        MedicineName = medicineli.medicine.MedicineName,
            //        AnimalCodeName = animalcodename.AnimalCodeName,
            //        MedicationDate = medicineli.MedicationDate,
            //        Dose = medicineli.Dose,
            //        Comments = medicineli.medicine.Comments,
            //        OperatorName = medicineli.medicine.OperatorName

            //    });

            //}

            //admin.medicinelist = lists;
            //#endregion

            //--------------------------Vaccination ------------------------------
            #region
            //var availablevaccine = AnimalInformationDBAcces.GetAvailableVaccines();

            //if (availablevaccine != null)
            //{
            //    availablevaccine = availablevaccine.Where(a => a.VaccinationDate >= date && a.VaccinationDate <= date1).ToList();

            //}
            //admin.vaccinelist = availablevaccine;
            #endregion

            //--------------------------Cow heat ------------------------------
            //admin.cowheatlist =  AnimalInformationDBAcces.GetHeatListforDateRange(date,date1);

            //--------------------------order List ------------------------------
            if (orderid != null)
            {
                AgroExpressDBAccess.UpdateOrder((int)orderid);
            }
            admin.orderlist = AgroExpressDBAccess.GetAllNotDeliveredOrder();

            //--------------------------Product and sale ------------------------------
            #region
            var milks = AgroExpressDBAccess.GetMilkSummaryByDate(System.DateTime.Now.Date.AddDays(-7), System.DateTime.Now.Date.AddDays(1));


            List<ProductandSaleHistory> history = new List<ProductandSaleHistory>();

            foreach (var item in milks)
            {
                if (milks != null)
                {
                    var sale = AgroExpressDBAccess.GetSaleHistoryByDate(item.Date, item.Date.AddDays(1));
                    
                    double totalsale = 0.0;
                    if (sale != null)
                    {
                        foreach (var milksale in sale)
                        {
                            totalsale += milksale.Amount;
                        }
                    }

                    history.Add(new ProductandSaleHistory
                    {
                        Date = item.Date,
                        TotalProduction = item.TotalProduction,
                        CulfMorning = item.CulfMorning,
                        CulfAfternoon = item.CulfAfternoon,
                        Factory = item.Factory,
                        Stuff = item.Stuff,
                        Wastage = item.Wastage,
                        TotalSale = totalsale
                    });
                }
            }


        admin.productandsale = history;

            #endregion

            #region
            string name = HttpContext.User.Identity.Name;
        int id = AgroExpressDBAccess.GetCustomerIdByUserId(name);
            if (id != 0)
            {
                var customer = AgroExpressDBAccess.GetCustomerByID((int)id);
        admin.CustomerName = customer.FullName;
                if (customer != null)
                {
                    var cus = AgroExpressDBAccess.GetCustomerTransectionHistory(DateTime.Now.AddDays(-30).Date, DateTime.Now.AddDays(1).Date, (int)id);

        admin.TransactionHistory = cus;


                }
}


#endregion

//----------------Notification----------


var notification = AgroExpressDBAccess.GetNotificationByDate(System.DateTime.Now);
            if (notification != null)
            {
                admin.Notification = notification;
            }
            return View(admin);
        }

        [RBAC]
public ActionResult NotificationDelete(int id)
{

    AgroExpressDBAccess.DeleteNotificationByID(id);
    return RedirectToAction("Index");
}

    }
}