using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.DataLayer.Models;
namespace AgroExpress.Controllers
{
    [RBAC]
    public class ProductionController : Controller
    {
        // GET: Production
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Factory()
        {
            FactoryProduction productionInfo = new FactoryProduction();
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();

            ProductInf = ProductInf.Where(a => a.ProductName.ToLower() != "milk").ToList();

            int len = ProductInf.Count;
            List<ProductionInfo> NewProduction = new List<ProductionInfo>();
            for (int i = 0; i < len; i++)
            {
                NewProduction.Add(new ProductionInfo
                {
                    productlist = ProductInf.Select(x => new SelectListItem
                    {
                        Value = x.PKProductId.ToString(),
                        Text = x.ProductName
                    }),
                    Date = System.DateTime.Now,
                    Amount = 0

                });
            }
            productionInfo.productionlist = NewProduction;
            productionInfo.ConsumptionDate = System.DateTime.Now.Date;
            return View(productionInfo);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Factory(FactoryProduction productionInfo)
        {
            List<Production> ProductionList = new List<Production>();
            string name = HttpContext.User.Identity.Name;
            string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);

            foreach (var production in productionInfo.productionlist)
            {
                if (production.PKProductId != null && production.Amount != null)
                {
                    ProductionList.Add(new Production
                    {
                        ProductId = (int)production.PKProductId,
                        Amount = (double)production.Amount,
                        Date = production.Date,
                        OperatorName = operatorName
                    });
                }
            }
            if (productionInfo.ConsumptionDate != null && productionInfo.MilkConsumption != null)
            {
                AgroExpressDBAccess.AddFactoryMilkConsumption((DateTime)productionInfo.ConsumptionDate, (double)productionInfo.MilkConsumption);
            }
            string message = AgroExpressDBAccess.AddPoductionList(ProductionList);
            if (message == "yes")
            {
                return RedirectToAction(nameof(Factory));
            }
            else
            {
                var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
                int len = ProductInf.Count;
                List<ProductionInfo> NewProduction = new List<ProductionInfo>();
                for (int i = 0; i < len; i++)
                {
                    NewProduction.Add(new ProductionInfo
                    {
                        productlist = ProductInf.Select(x => new SelectListItem
                        {
                            Value = x.PKProductId.ToString(),
                            Text = x.ProductName
                        }),
                        Date = System.DateTime.Now,
                        Amount = 0

                    });
                }
                productionInfo.productionlist = NewProduction;
                ViewBag.message = message;
            }
            return View(productionInfo);
        }

        public ActionResult FactoryList()
        {
            FactoryProductionList Production = new FactoryProductionList();

            Production.EntryDateMax = System.DateTime.Now;
            Production.EntryDateMin = System.DateTime.Now.AddDays(-30);
            List<Production> ProductionInfo = AgroExpressDBAccess.ProductionInfoByDate(Production.EntryDateMin, Production.EntryDateMax);
            Production.SearchResult = ProductionInfo;
            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductInf = ProductInf.Where(a => a.ProductName.ToLower() != "milk").ToList();
            Production.ProductList = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            return View(Production);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FactoryList(FactoryProductionList request)
        {
            List<Production> ProductionInfo = AgroExpressDBAccess.ProductionInfoByDate(request.EntryDateMin, request.EntryDateMax);

            if (request.ProductID != null)
            {
                ProductionInfo = ProductionInfo.Where(a => a.ProductId == request.ProductID).ToList();
            }
            if (request.AmountMax != null)
            {
                ProductionInfo = ProductionInfo.Where(a => a.Amount <= request.AmountMax).ToList();
            }
            if (request.AmountMin != null)
            {
                ProductionInfo = ProductionInfo.Where(a => a.Amount >= request.AmountMin).ToList();
            }

            var ProductInf = AgroExpressDBAccess.GetAllEnabledProduct();
            ProductInf = ProductInf.Where(a => a.ProductName.ToLower() != "milk").ToList();
            request.ProductList = ProductInf.Select(x => new SelectListItem
            {
                Value = x.PKProductId.ToString(),
                Text = x.ProductName
            });
            request.SearchResult = ProductionInfo;
            return View(request);
        }

        public ActionResult ProductionDelete(int id)
        {
            AgroExpressDBAccess.ProductionDelete(id);
            return RedirectToAction(nameof(FactoryList));
        }

        [RBAC]
        public ActionResult AnimalTypeList()
        {
            var v = AnimalInformationDBAcces.GetEnabledAnimalType();
            return View(v);
        }

        public ActionResult Milk(int TypeID)
        {
            List<MilkProduction> prevmilkproductionlist = AnimalInformationDBAcces.GetallMilkProductionByDate(System.DateTime.Now.Date); ;

            prevmilkproductionlist = prevmilkproductionlist.Where(a => a.animal.AnimalTypeId == TypeID).ToList();

            List<AnimalInformation> animallist = new List<AnimalInformation>();

            animallist = AnimalInformationDBAcces.GetEnabledFemaleAnimalByTypeId(TypeID);

            MilkProductionView tem = new MilkProductionView();
            tem.ProductionDate = System.DateTime.Now.Date;
            List<MilkProductionPerCow> productionList = new List<MilkProductionPerCow>();
            foreach (var animal in animallist)
            {
                var previnfo = prevmilkproductionlist.Where(a => a.AnimalId == animal.PKAnimalId).SingleOrDefault();
                if (previnfo != null)
                {
                    productionList.Add(new MilkProductionPerCow { AnimalID = animal.PKAnimalId, AnimalCode = animal.AnimalCodeName, AfternoonAmount = previnfo.AfternoonAmount, MorningAmount = previnfo.MorningAmount });
                }
                else
                    productionList.Add(new MilkProductionPerCow { AnimalID = animal.PKAnimalId, AnimalCode = animal.AnimalCodeName, AfternoonAmount = 0, MorningAmount = 0 });
            }
            tem.Cows = productionList;
            tem.AnimalTypeId = TypeID;
            return View(tem);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Milk(MilkProductionView newentry, string Submit)
        {
            string name = HttpContext.User.Identity.Name;
            string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
            if (Submit == "Add")
            {
                if (ModelState.IsValid)
                {

                    List<MilkProductionPerCow> tem = newentry.Cows;
                    List<MilkProduction> entryList = new List<MilkProduction>();

                    var totalmilk = 0.0;
                    foreach (var entry in tem)
                    {
                        if (entry.MorningAmount != 0 || entry.AfternoonAmount != 0)
                        {
                            entryList.Add(new MilkProduction { AnimalId = entry.AnimalID, Date = newentry.ProductionDate, AfternoonAmount = entry.AfternoonAmount, MorningAmount = entry.MorningAmount, OperatorName = operatorName });
                            totalmilk += entry.AfternoonAmount + entry.MorningAmount;
                        }
                    }
                    if (totalmilk != 0)
                    {
                        if (AnimalInformationDBAcces.AddOrUpdateMilkProduction(entryList))
                        {
                            if (AgroExpressDBAccess.AddOrUpdateTotalMilkSummary(newentry.ProductionDate, totalmilk))
                            {
                                ViewBag.success = "Insert Successful";
                                return View(newentry);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.success = "Insert Failed, Total Value Can not be Zero";
                        return View(newentry);
                    }

                }
                return View(newentry);
            }
            else
            {
                ModelState.Clear();
                List<MilkProduction> prevmilkproductionlist = AnimalInformationDBAcces.GetallMilkProductionByDate(newentry.ProductionDate); ;

                prevmilkproductionlist = prevmilkproductionlist.Where(a => a.animal.AnimalTypeId == newentry.AnimalTypeId).ToList();

                List<AnimalInformation> animallist = new List<AnimalInformation>();

                animallist = AnimalInformationDBAcces.GetEnabledFemaleAnimalByTypeId(newentry.AnimalTypeId);

                MilkProductionView tem = new MilkProductionView();
                tem.ProductionDate = newentry.ProductionDate;
                List<MilkProductionPerCow> productionList = new List<MilkProductionPerCow>();
                foreach (var animal in animallist)
                {
                    var previnfo = prevmilkproductionlist.Where(a => a.AnimalId == animal.PKAnimalId).SingleOrDefault();
                    if (previnfo != null)
                    {
                        productionList.Add(new MilkProductionPerCow { AnimalID = animal.PKAnimalId, AnimalCode = animal.AnimalCodeName, AfternoonAmount = previnfo.AfternoonAmount, MorningAmount = previnfo.MorningAmount });
                    }
                    else
                        productionList.Add(new MilkProductionPerCow { AnimalID = animal.PKAnimalId, AnimalCode = animal.AnimalCodeName, AfternoonAmount = 0, MorningAmount = 0 });
                }
                tem.Cows = productionList;
                tem.AnimalTypeId = newentry.AnimalTypeId;
                return View(tem);
            }
        }

        [RBAC]
        public ActionResult AnimalTypeListEdit()
        {
            var v = AnimalInformationDBAcces.GetEnabledAnimalType();
            return View(v);
        }
        public ActionResult MilkEdit(int TypeID)
        {

            List<MilkProduction> prevmilkproductionlist = AnimalInformationDBAcces.GetallMilkProductionByDate(System.DateTime.Now.Date); ;

            prevmilkproductionlist = prevmilkproductionlist.Where(a => a.animal.AnimalTypeId == TypeID).ToList();


            MilkProductionView tem = new MilkProductionView();
            tem.ProductionDate = System.DateTime.Now.Date;
            List<MilkProductionPerCow> productionList = new List<MilkProductionPerCow>();

            if (prevmilkproductionlist.Count != 0)
            {
                foreach (var animal in prevmilkproductionlist)
                {
                    productionList.Add(new MilkProductionPerCow { AnimalID = animal.AnimalId, AnimalCode = animal.animal.AnimalCodeName, AfternoonAmount = animal.AfternoonAmount, MorningAmount = animal.MorningAmount });
                }
            }

            tem.Cows = productionList;
            tem.AnimalTypeId = TypeID;
            return View(tem);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MilkEdit(MilkProductionView newentry, string Submit)
        {
            string name = HttpContext.User.Identity.Name;
            string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
            if (Submit == "Add")
            {
                if (ModelState.IsValid)
                {
                    List<MilkProductionPerCow> tem = newentry.Cows;
                    List<MilkProduction> entryList = new List<MilkProduction>();

                    var totalmilk = 0.0;
                    foreach (var entry in tem)
                    {
                        if(entry.MorningAmount!=0 && entry.AfternoonAmount!=0)
                        {
                            entryList.Add(new MilkProduction { AnimalId = entry.AnimalID, Date = newentry.ProductionDate, AfternoonAmount = entry.AfternoonAmount, MorningAmount = entry.MorningAmount, OperatorName = operatorName });
                            totalmilk += entry.AfternoonAmount + entry.MorningAmount;
                        }
                    }
                    if (totalmilk != 0)
                    {
                        if (AnimalInformationDBAcces.AddOrUpdateMilkProduction(entryList))
                        {
                            if (AgroExpressDBAccess.AddOrUpdateTotalMilkSummary(newentry.ProductionDate, totalmilk))
                            {
                                ViewBag.success = "Update Successful";
                                return View(newentry);
                            }
                        }
                    }
                    else
                    {
                        ViewBag.success = "Update Failed, Total Value Can not be Zero";
                        return View(newentry);
                    }
                }
                return View(newentry);
            }
            else
            {
                ModelState.Clear();
                List<MilkProduction> prevmilkproductionlist = AnimalInformationDBAcces.GetallMilkProductionByDate(newentry.ProductionDate); ;

                prevmilkproductionlist = prevmilkproductionlist.Where(a => a.animal.AnimalTypeId == newentry.AnimalTypeId).ToList();

                MilkProductionView tem = new MilkProductionView();
                tem.ProductionDate = newentry.ProductionDate;
                List<MilkProductionPerCow> productionList = new List<MilkProductionPerCow>();

                if (prevmilkproductionlist.Count != 0)
                {
                    foreach (var animal in prevmilkproductionlist)
                    {
                        productionList.Add(new MilkProductionPerCow { AnimalID = animal.AnimalId, AnimalCode = animal.animal.AnimalCodeName, AfternoonAmount = animal.AfternoonAmount, MorningAmount = animal.MorningAmount });
                    }
                }
                tem.Cows = productionList;
                tem.AnimalTypeId = newentry.AnimalTypeId;
                return View(tem);
            }

        }

        public ActionResult MilkProductionList()
        {
            MilkproductionList milklist = new MilkproductionList();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                milklist.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }
            var animallist = AnimalInformationDBAcces.GetEnabledFemaleAnimal();
            if (animallist != null)
            {
                milklist.Manimallist = animallist.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            milklist.DateMax = System.DateTime.UtcNow.AddHours(6).Date;
            milklist.DateMin = milklist.DateMax.AddDays(-30);

            var milkproductionlist = AnimalInformationDBAcces.GetMilkProductionList(milklist.DateMin, milklist.DateMax);

            milklist.milkProductions = milkproductionlist;
            return View(milklist);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MilkProductionList(MilkproductionList milklist)
        {

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                milklist.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }
            var animallist = AnimalInformationDBAcces.GetEnabledFemaleAnimal();
            if (animallist != null)
            {
                milklist.Manimallist = animallist.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }



            var milkproductionlist = AnimalInformationDBAcces.GetMilkProductionList(milklist.DateMin, milklist.DateMax);

            if (milklist.VAnimalTypeId != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.animal.AnimalTypeId == milklist.VAnimalTypeId).ToList();
            }

            if (milklist.MAnimalId != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.AnimalId == milklist.MAnimalId).ToList();
            }

            if (milklist.MorningAmountMin != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.MorningAmount >= milklist.MorningAmountMin).ToList();
            }

            if (milklist.MorningAmountMax != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.MorningAmount <= milklist.MorningAmountMax).ToList();
            }

            if (milklist.AfternoonAmountMin != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.AfternoonAmount >= milklist.AfternoonAmountMin).ToList();
            }

            if (milklist.AfternoonAmountMax != null)
            {
                milkproductionlist = milkproductionlist.Where(a => a.AfternoonAmount <= milklist.AfternoonAmountMax).ToList();
            }


            milklist.milkProductions = milkproductionlist;

            return View(milklist);
        }
        public ActionResult MilksummaryAdd()
        {
            MilkSummaryAdd newentry = new MilkSummaryAdd();
            newentry.Date = System.DateTime.Now.Date;
            return View(newentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MilksummaryAdd(MilkSummaryAdd newentry)
        {
            if (ModelState.IsValid)
            {
                if (AgroExpressDBAccess.AddMilkSummary(newentry))
                {
                    return RedirectToAction("Index", "Administrator");
                }
            }

            return View(newentry);
        }

        public ActionResult MilksummaryEdit(DateTime date)
        {
            MilkSummeryEdit newentry = new MilkSummeryEdit();
            var milksummary = AgroExpressDBAccess.GetMilkSummaryByDate(date);
            if (milksummary != null)
            {
                newentry.Date = milksummary.Date;
                newentry.CulfMorning = milksummary.CulfMorning;
                newentry.CulfAfternoon = milksummary.CulfAfternoon;
                newentry.Stuff = milksummary.Stuff;
                newentry.Factory = milksummary.Factory;
                newentry.Wastage = milksummary.Wastage;
            }

            return View(newentry);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MilksummaryEdit(MilkSummeryEdit newentry)
        {
            if (ModelState.IsValid)
            {
                if (AgroExpressDBAccess.EditMilkSummary(newentry))
                {
                    return RedirectToAction("Index", "Administrator");
                }
            }

            return View(newentry);
        }

        public ActionResult MilkSummary()
        {

            List<MilkSummary> milksummarylist = AgroExpressDBAccess.GetAllMilkSummary();

            return View(milksummarylist);
        }

    }
}