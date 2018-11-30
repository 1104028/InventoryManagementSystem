using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer;
using System.Data.Entity;

namespace AgroExpress.Controllers

{

    public class AnimalController : Controller
    {
        //------------------Animal ------------------------------

        #region

        [RBAC]
        public ActionResult Add()
        {
            AnimalAdd animal = new AnimalAdd();
            animal.GenderOption = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a",Selected=true},
               new SelectListItem() {Text="Female", Value="female"},
               new SelectListItem() { Text="Male", Value="male"}

            };
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                animal.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }
            animal.DateofEntry = System.DateTime.Now.Date;
            animal.DOB = System.DateTime.Now.Date;
            return View(animal);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AnimalAdd newAnimal)
        {
            newAnimal.GenderOption = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a",Selected= newAnimal.Gender=="n/a"?true:false},
               new SelectListItem() {Text="Female", Value="female",Selected= newAnimal.Gender=="female"?true:false},
               new SelectListItem() { Text="Male", Value="male",Selected= newAnimal.Gender=="male"?true:false}

            };
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newAnimal.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName,
                    Selected = x.PKAnimalTypeId == newAnimal.AnimalType ? true : false
                });
            }

            if (ModelState.IsValid)
            {
                AnimalInformation newAnimalInfo = new AnimalInformation();

                if (AnimalInformationDBAcces.IscodeNameExists(newAnimal.AnimalCodeName))
                {
                    ModelState.AddModelError("AnimalCodeName", "Code Name already Exists");
                    return View(newAnimal);
                }

                newAnimalInfo.AnimalCodeName = newAnimal.AnimalCodeName;
                newAnimalInfo.AnimalTypeId = newAnimal.AnimalType;
                newAnimalInfo.Comments = newAnimal.Comments;
                newAnimalInfo.DateofEntry = newAnimal.DateofEntry;
                newAnimalInfo.DateofExit = newAnimal.DateofExit;
                newAnimalInfo.Gender = newAnimal.Gender;
                newAnimalInfo.DOB = newAnimal.DOB;

                if (AnimalInformationDBAcces.AddAnimal(newAnimalInfo))
                {
                    ViewBag.success = "Animal has been added successfully";
                    ModelState.Clear();
                    return View(newAnimal);
                }

            }
            return View(newAnimal);
        }

        [RBAC]
        public ActionResult List()
        {

            AnimalList animal = new AnimalList();
            animal.GenderOptions = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a"},
               new SelectListItem() {Text="Female", Value="female"},
               new SelectListItem() { Text="Male", Value="male"}

            };
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                animal.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var animallist = AnimalInformationDBAcces.GetNumberofEnabledAnimal(50);
            if (animallist != null)
            {
                animal.searchResult = animallist;
                animal.AnimalCodes = animallist.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            return View(animal);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult List(AnimalList AnimalSearch)
        {
            var animallist = AnimalInformationDBAcces.GetEnabledAnimal();
            AnimalList animal = new AnimalList();
            animal.GenderOptions = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a",Selected=animal.Gender=="n/a"?true:false},
               new SelectListItem() {Text="Female", Value="female", Selected=animal.Gender=="female"?true:false},
               new SelectListItem() { Text="Male", Value="male", Selected=animal.Gender=="male"?true:false}
            };

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                animal.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName,
                    Selected = x.PKAnimalTypeId == animal.AnimalTypeID ? true : false
                });
            }

            if (animallist != null)
            {
                animal.AnimalCodes = animallist.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName,
                    Selected = x.PKAnimalId == AnimalSearch.AnimalID ? true : false
                });

                if (AnimalSearch.AnimalTypeID != null)
                {
                    animallist = animallist.Where(a => a.AnimalTypeId == AnimalSearch.AnimalTypeID).ToList();
                }
                if (AnimalSearch.AnimalID != null)
                {
                    if (AnimalSearch.AnimalID != 0)
                        animallist = animallist.Where(a => a.PKAnimalId == AnimalSearch.AnimalID).ToList();
                }
                if (AnimalSearch.Gender != null)
                {
                    animallist = animallist.Where(a => a.Gender == AnimalSearch.Gender).ToList();
                }
                if (AnimalSearch.EntryDateMin != null)
                {
                    animallist = animallist.Where(a => a.DateofEntry >= AnimalSearch.EntryDateMin).ToList();
                }
                if (AnimalSearch.EntryDateMax != null)
                {
                    animallist = animallist.Where(a => a.DateofEntry <= AnimalSearch.EntryDateMax).ToList();
                }
                if (AnimalSearch.ExitDateMin != null)
                {
                    animallist = animallist.Where(a => a.DateofExit >= AnimalSearch.ExitDateMin).ToList();
                }
                if (AnimalSearch.ExitDateMax != null)
                {
                    animallist = animallist.Where(a => a.DateofExit <= AnimalSearch.ExitDateMax).ToList();
                }

                animal.searchResult = animallist;

            }

            return View(animal);
        }


        public JsonResult GetAnimalListForType(int animalTypeID)
        {
            var custmoAnimalList = new List<SelectListItem>();
            var animalList = AnimalInformationDBAcces.GetAnimalListByTypeId(animalTypeID);
            if (animalList != null)
            {
                foreach (var animal in animalList)
                {
                    custmoAnimalList.Add(new SelectListItem
                    {
                        Value = animal.PKAnimalId.ToString(),
                        Text = animal.AnimalCodeName,

                    });
                }
            }
            return Json(custmoAnimalList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetFemaleAnimalListForType(int animalTypeID)
        {
            var custmoAnimalList = new List<SelectListItem>();
            var animalList = AnimalInformationDBAcces.GetAnimalListByTypeId(animalTypeID);
            animalList = animalList.Where(a => a.Gender.ToUpper() == "FEMALE").ToList();
            if (animalList != null)
            {
                foreach (var animal in animalList)
                {
                    custmoAnimalList.Add(new SelectListItem
                    {
                        Value = animal.PKAnimalId.ToString(),
                        Text = animal.AnimalCodeName,

                    });
                }
            }
            return Json(custmoAnimalList, JsonRequestBehavior.AllowGet);
        }
        [RBAC]
        public ActionResult DisabledAnimalList()
        {
            var v = AnimalInformationDBAcces.GetDisabledAnimalList();
            return View(v);
        }

        [RBAC]
        public ActionResult Disable(int id)
        {
            var v = AnimalInformationDBAcces.DisableAnimal(id);
            return RedirectToAction("List");
        }

        [RBAC]
        public ActionResult Enable(int id)
        {
            var v = AnimalInformationDBAcces.EnableAnimal(id);
            return RedirectToAction("DisabledAnimalList");
        }

        [RBAC]
        public ActionResult Edit(int id)
        {
            AnimalUpdate animalEdit = new AnimalUpdate();

            var animalinfo = AnimalInformationDBAcces.GetAnimalInfoByID(id);
            if (animalinfo != null)
            {
                animalEdit.PKAnimalId = id;
                animalEdit.AnimalCodeName = animalinfo.AnimalCodeName;
                animalEdit.Comments = animalinfo.Comments;
                animalEdit.DateofEntry = animalinfo.DateofEntry;
                animalEdit.DateofExit = animalinfo.DateofExit;
                animalEdit.Gender = animalinfo.Gender;
                animalEdit.AnimalType = animalinfo.AnimalTypeId;
                animalEdit.IsDeleted = animalinfo.IsDeleted;

            }

            animalEdit.GenderOption = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a" ,Selected= animalinfo.Gender=="n/a"?true:false
               },
               new SelectListItem() {Text="Female", Value="female", Selected=animalinfo.Gender=="female"?true:false
               },
               new SelectListItem() { Text="Male", Value="male", Selected=animalinfo.Gender=="male"?true:false
               }

            };


            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                animalEdit.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName,
                    Selected = animalinfo.AnimalTypeId == animalEdit.AnimalType ? true : false
                });
            }

            return View(animalEdit);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(AnimalUpdate editedAnimal)
        {
            editedAnimal.GenderOption = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a" ,Selected= editedAnimal.Gender=="n/a"?true:false},
               new SelectListItem() {Text="Female", Value="female", Selected=editedAnimal.Gender=="female"?true:false },
               new SelectListItem() { Text="Male", Value="male", Selected=editedAnimal.Gender=="male"?true:false}
            };
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                editedAnimal.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName,
                    Selected = x.PKAnimalTypeId == editedAnimal.AnimalType ? true : false
                });
            }

            if (ModelState.IsValid)
            {

                if (AnimalInformationDBAcces.UpdateAnimal(editedAnimal))
                {
                    ViewBag.success = "Animal Information Updated Successfully";
                    ModelState.Clear();
                    return RedirectToAction("List");
                }

            }

            return View(editedAnimal);
        }

        #endregion

        //------------------Weight ------------------------------

        #region
        [RBAC]
        public ActionResult WeightAdd()
        {
            AnimalWeightAdd newWeight = new AnimalWeightAdd();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newWeight.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            newWeight.SearchDate = System.DateTime.Now.Date;
            var previousanimalweights = AnimalInformationDBAcces.GetAnimalWeightsByDate(newWeight.SearchDate);

            List<AnimalWeights> showAnimalList = new List<AnimalWeights>();

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforWeightInput();
            if (availableAnimal != null)
            {
                foreach (var animal in availableAnimal)
                {
                    var animalweight = previousanimalweights.Where(a => a.AnimalId == animal.PKAnimalId).SingleOrDefault();
                    if(animalweight!=null)
                    {
                        showAnimalList.Add(new AnimalWeights
                        {
                            Date = newWeight.SearchDate,
                            AnimalId = animal.PKAnimalId,
                            AnimalName = animal.AnimalCodeName,
                            Weight = animalweight.Weight
                        });
                    }
                    else
                    {
                        showAnimalList.Add(new AnimalWeights
                        {
                            Date = newWeight.SearchDate,
                            AnimalId = animal.PKAnimalId,
                            AnimalName = animal.AnimalCodeName,
                            Weight = 0
                        });
                    }
                }
                newWeight.animalweightlist = showAnimalList;
            }
            return View(newWeight);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WeightAdd(AnimalWeightAdd searchWeight, string Submit)
        {
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                searchWeight.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            if (Submit == "Add")
            {
                if (ModelState.IsValid)
                {
                    List<AnimalWeight> addWeightList = new List<AnimalWeight>();
                    foreach (var v in searchWeight.animalweightlist)
                    {
                        if (v.Weight != 0)
                        {
                            addWeightList.Add(new AnimalWeight
                            {
                                AnimalId = v.AnimalId,
                                Date = v.Date,
                                Weight = (double)v.Weight
                            });

                        }
                    }
                    string status = AnimalInformationDBAcces.AddAnimalWeightList(addWeightList);
                    if (status == "OK")
                    {
                        ViewBag.Message = "Insert Successful";
                    }
                    else
                    {
                        ViewBag.Message = "Operation Failed, Please try again";
                    }
                }


                var previousanimalweights = AnimalInformationDBAcces.GetAnimalWeightsByDate(searchWeight.SearchDate);

                var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforWeightInput();

                if (searchWeight.AnimalTypeId != null)
                {
                    previousanimalweights = previousanimalweights.Where(a => a.animal.AnimalTypeId == searchWeight.AnimalTypeId).ToList();
                    availableAnimal = availableAnimal.Where(a => a.AnimalTypeId == searchWeight.AnimalTypeId).ToList();
                }

                List<AnimalWeights> showAnimalList = new List<AnimalWeights>();

                if (availableAnimal != null)
                {
                    foreach (var animal in availableAnimal)
                    {
                        var animalweight = previousanimalweights.Where(a => a.AnimalId == animal.PKAnimalId).SingleOrDefault();
                        if (animalweight != null)
                        {
                            showAnimalList.Add(new AnimalWeights
                            {
                                Date = searchWeight.SearchDate,
                                AnimalId = animal.PKAnimalId,
                                AnimalName = animal.AnimalCodeName,
                                Weight = animalweight.Weight
                            });
                        }
                        else
                        {
                            showAnimalList.Add(new AnimalWeights
                            {
                                Date = searchWeight.SearchDate,
                                AnimalId = animal.PKAnimalId,
                                AnimalName = animal.AnimalCodeName,
                                Weight = 0
                            });
                        }
                    }
                    searchWeight.animalweightlist = showAnimalList;
                }

                return View(searchWeight);
            }
            else
            {
                ModelState.Clear();
                var previousanimalweights = AnimalInformationDBAcces.GetAnimalWeightsByDate(searchWeight.SearchDate);

                var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforWeightInput();

                if (searchWeight.AnimalTypeId != null)
                {
                    previousanimalweights = previousanimalweights.Where(a => a.animal.AnimalTypeId == searchWeight.AnimalTypeId).ToList();
                    availableAnimal = availableAnimal.Where(a => a.AnimalTypeId == searchWeight.AnimalTypeId).ToList();
                }

                List<AnimalWeights> showAnimalList = new List<AnimalWeights>();

                if (availableAnimal != null)
                {
                    foreach (var animal in availableAnimal)
                    {
                        var animalweight = previousanimalweights.Where(a => a.AnimalId == animal.PKAnimalId).SingleOrDefault();
                        if (animalweight != null)
                        {
                            showAnimalList.Add(new AnimalWeights
                            {
                                Date = searchWeight.SearchDate,
                                AnimalId = animal.PKAnimalId,
                                AnimalName = animal.AnimalCodeName,
                                Weight = animalweight.Weight
                            });
                        }
                        else
                        {
                            showAnimalList.Add(new AnimalWeights
                            {
                                Date = searchWeight.SearchDate,
                                AnimalId = animal.PKAnimalId,
                                AnimalName = animal.AnimalCodeName,
                                Weight = 0
                            });
                        }
                    }
                    searchWeight.animalweightlist = showAnimalList;
                }
                return View(searchWeight);
            }
        }

        [RBAC]
        public ActionResult WeightList()
        {
            AnimalWeightList animalWeight = new AnimalWeightList();
            animalWeight.GenderOptions = new List<SelectListItem>()
            {
               new SelectListItem() { Text="N/A", Value="n/a"},
               new SelectListItem() {Text="Female", Value="female"},
               new SelectListItem() { Text="Male", Value="male"}

            };

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                animalWeight.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            List<AnimalWeight> weightedAnimal = AnimalInformationDBAcces.GetNumberofWeightedAnimal(50);

            if (weightedAnimal != null)
            {
                animalWeight.searchResult = AnimalInformationDBAcces.AnimalWeightViewFormat(weightedAnimal);

                animalWeight.AnimalCodes = animalWeight.searchResult.Select(x => new SelectListItem
                {
                    Value = x.animalId.ToString(),
                    Text = x.animalCodeName
                });
            }

            return View(animalWeight);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WeightList(AnimalWeightList weightSearchList)
        {
            if (ModelState.IsValid)
            {
                AnimalWeightList animalWeightList = new AnimalWeightList();

                animalWeightList.GenderOptions = new List<SelectListItem>()
                {
               new SelectListItem() { Text="N/A", Value="n/a",Selected=weightSearchList.Gender=="n/a"?true:false},
               new SelectListItem() {Text="Female", Value="female", Selected=weightSearchList.Gender=="female"?true:false},
               new SelectListItem() { Text="Male", Value="male", Selected=weightSearchList.Gender=="male"?true:false}
                };

                var types = AnimalInformationDBAcces.GetEnabledAnimalType();
                if (types != null)
                {
                    animalWeightList.AnimalTypes = types.Select(x => new SelectListItem
                    {
                        Value = x.PKAnimalTypeId.ToString(),
                        Text = x.AnimalTypeName,
                        Selected = x.PKAnimalTypeId == weightSearchList.AnimalTypeID ? true : false
                    });
                }

                List<AnimalWeight> weightedAnimal = new List<AnimalWeight>();
                weightedAnimal = AnimalInformationDBAcces.GetWeightedAnimal();

                if (weightedAnimal != null)
                {
                    animalWeightList.searchResult = AnimalInformationDBAcces.AnimalWeightViewFormat(weightedAnimal);
                    animalWeightList.AnimalCodes = animalWeightList.searchResult.Select(x => new SelectListItem
                    {
                        Value = x.animalId.ToString(),
                        Text = x.animalCodeName,
                        Selected = x.animalId == weightSearchList.AnimalID ? true : false
                    });
                }

                List<AnimalWeightInfoView> searchResultWeight = new List<AnimalWeightInfoView>();
                searchResultWeight = AnimalInformationDBAcces.AnimalWeightViewFormat(weightedAnimal);

                if (weightSearchList != null)
                {

                    if (weightSearchList.AnimalTypeID != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.animalTypeId == weightSearchList.AnimalTypeID).ToList();
                    }
                    if (weightSearchList.AnimalID != null)
                    {
                        if (weightSearchList.AnimalID != 0)
                            searchResultWeight = searchResultWeight.Where(a => a.animalId == weightSearchList.AnimalID).ToList();
                    }
                    if (weightSearchList.Gender != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.gender == weightSearchList.Gender).ToList();
                    }
                    if (weightSearchList.DateMin != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.Date >= weightSearchList.DateMin).ToList();
                    }
                    if (weightSearchList.DateMax != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.Date <= weightSearchList.DateMax).ToList();
                    }
                    if (weightSearchList.weightMin != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.Weight >= weightSearchList.weightMin).ToList();
                    }
                    if (weightSearchList.weightMax != null)
                    {
                        searchResultWeight = searchResultWeight.Where(a => a.Weight <= weightSearchList.weightMax).ToList();
                    }
                }

                animalWeightList.searchResult = searchResultWeight;

                return View(animalWeightList);
            }

            return View("WeightList");
        }

        [RBAC]
        public ActionResult WeightEdit(int id, DateTime date)
        {
            var weightInfo = AnimalInformationDBAcces.GetWeightInfoByIDDate(id, date);

            AnimalWeightInfoView currentWeight = new AnimalWeightInfoView();
            currentWeight = AnimalInformationDBAcces.AnimalWeightViewFormatSingle(weightInfo);

            return View(currentWeight);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult WeightEdit(AnimalWeightInfoView editedWeight)
        {
            if (ModelState.IsValid)
            {
                string status = AnimalInformationDBAcces.UpdateAnimalWeight(editedWeight);
                if (status == "OK")
                {
                    ViewBag.success = "Animal Weight Updated Successfully";
                    //ModelState.Clear();
                    return RedirectToAction("WeightList");
                }

                else
                {
                    ViewBag.Message = status;
                    return RedirectToAction("WeightList");
                }

            }

            return View(editedWeight);
        }



        #endregion

        //------------------Cow HEAT ------------------------------

        #region
        [RBAC]
        public ActionResult CowHeatAdd()
        {
            CowHeatAdd newHeat = new CowHeatAdd();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newHeat.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                newHeat.AnimalList = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            newHeat.HeatTimeOptions = new List<SelectListItem>()
{
new SelectListItem() { Text="Morning", Value="morning",Selected=true},
new SelectListItem() {Text="Noon", Value="noon"},
new SelectListItem() { Text="Afternoon", Value="afternoon"},
new SelectListItem() { Text="Night", Value="night"}
};

            newHeat.InjectedTimeOptions = new List<SelectListItem>()
{
new SelectListItem() { Text="Morning", Value="morning",Selected=true},
new SelectListItem() {Text="Noon", Value="noon"},
new SelectListItem() { Text="Afternoon", Value="afternoon"},
new SelectListItem() { Text="Night", Value="night"}
};

            newHeat.heatDate = System.DateTime.Now.AddDays(3);
            newHeat.nextHeatDate = newHeat.heatDate.AddDays(18);
            return View(newHeat);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CowHeatAdd(CowHeatAdd posetedHeat)
        {
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                posetedHeat.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName,
                    Selected = x.PKAnimalTypeId == posetedHeat.animalTypeID ? true : false
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                posetedHeat.AnimalList = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName,
                    Selected = x.PKAnimalId == posetedHeat.animalId ? true : false
                });
            }

            posetedHeat.HeatTimeOptions = new List<SelectListItem>(){
                                                     new SelectListItem() { Text="Morning", Value="morning",Selected=posetedHeat.heatTime=="morning"?true:false},
                                                     new SelectListItem() {Text="Noon", Value="noon",Selected=posetedHeat.heatTime=="noon"?true:false},
                                                     new SelectListItem() { Text="Afternoon", Value="afternoon",Selected=posetedHeat.heatTime=="afternoon"?true:false},
                                                     new SelectListItem() { Text="Night", Value="night",Selected=posetedHeat.heatTime=="night"?true:false}
            };

            posetedHeat.InjectedTimeOptions = new List<SelectListItem>(){
                                                    new SelectListItem() { Text="Morning", Value="morning",Selected=posetedHeat.injectedTime=="morning"?true:false},
                                                    new SelectListItem() {Text="Noon", Value="noon",Selected=posetedHeat.injectedTime=="noon"?true:false},
                                                    new SelectListItem() { Text="Afternoon", Value="afternoon",Selected=posetedHeat.injectedTime=="afternoon"?true:false},
                                                    new SelectListItem() { Text="Night", Value="night",Selected=posetedHeat.injectedTime=="night"?true:false}
            };

            if (ModelState.IsValid)
            {
                CowHeat newHeatInfo = new CowHeat();

                if (AnimalInformationDBAcces.IsHeatInputPossible(posetedHeat.animalId, posetedHeat.heatDate))
                {
                    newHeatInfo.AnimalId = posetedHeat.animalId;
                    newHeatInfo.ActualDeliveryDate = posetedHeat.actualDeliveryDate;
                    newHeatInfo.DeliveryStatus = posetedHeat.deliveryStatus;
                    newHeatInfo.ExpectedDeliveryDate = posetedHeat.expectedDeliveryDate;
                    newHeatInfo.HeatDate = posetedHeat.heatDate;
                    newHeatInfo.HeatSummary = posetedHeat.heatSummary;
                    newHeatInfo.HeatTime = posetedHeat.heatTime;
                    newHeatInfo.InjectedDate = posetedHeat.injectedDate;
                    newHeatInfo.InjectedTime = posetedHeat.injectedTime;

                    newHeatInfo.NextHeatDate = posetedHeat.heatDate.AddDays(18);

                    string name = HttpContext.User.Identity.Name;
                    string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                    newHeatInfo.OperatorName = operatorName;

                    if (AnimalInformationDBAcces.AddHeat(newHeatInfo))
                    {
                        ViewBag.success = "Animal Heat has been added successfully";
                        ModelState.Clear();
                        List<Notification> NotificationInfo = new List<Notification>();
                        var animal = AnimalInformationDBAcces.GetAnimalInfoByID(newHeatInfo.AnimalId);
                        for (int i = -1; i < 6; i++)
                        {
                            NotificationInfo.Add(new Notification
                            {
                                Date = newHeatInfo.HeatDate.Date.AddDays(i),
                                NotificationMessage = "Heat date of animal : " + animal.AnimalCodeName + " is from " + newHeatInfo.HeatDate.ToString("dddd, dd MMMM yyyy") + " to " + newHeatInfo.HeatDate.Date.AddDays(7).ToString("dddd, dd MMMM yyyy"),
                                GroupID = "Heat" + newHeatInfo.AnimalId + newHeatInfo.HeatDate.ToString("MM/dd/yyyy")
                            });
                        }
                        bool sta = AgroExpressDBAccess.AddNotificationList(NotificationInfo);
                        ModelState.Clear();
                        return View(posetedHeat);
                    }
                    else
                    {
                        ModelState.Clear();
                        return View(posetedHeat);
                    }
                }
                else
                {
                    ModelState.AddModelError("animalId", "There is alrady a record on this date for this Animal.");
                    return View(posetedHeat);
                }
            }
            return View(posetedHeat);
        }

        [RBAC]
        public ActionResult CowHeatList()
        {
            CowHeatList viewListObject = new CowHeatList();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                viewListObject.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var animallist = AnimalInformationDBAcces.GetEnabledAnimal();
            if (animallist != null)
            {
                viewListObject.AnimalCodes = animallist.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            List<CowHeat> initialHeatSearchList = AnimalInformationDBAcces.GetHeatListforDateRange(System.DateTime.Now.Date.AddDays(-30), System.DateTime.Now.Date.AddDays(30));
            viewListObject.heatList = AnimalInformationDBAcces.CowHeatListViewFormat(initialHeatSearchList);

            return View(viewListObject);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CowHeatList(CowHeatList viewListObject)
        {
            if (ModelState.IsValid)
            {
                var types = AnimalInformationDBAcces.GetEnabledAnimalType();
                if (types != null)
                {
                    viewListObject.AnimalTypes = types.Select(x => new SelectListItem
                    {
                        Value = x.PKAnimalTypeId.ToString(),
                        Text = x.AnimalTypeName
                    });
                }

                var animallist = AnimalInformationDBAcces.GetEnabledAnimal();
                if (animallist != null)
                {
                    viewListObject.AnimalCodes = animallist.Select(x => new SelectListItem
                    {
                        Value = x.PKAnimalId.ToString(),
                        Text = x.AnimalCodeName
                    });
                }

                List<CowHeat> initialHeatSearchList = AnimalInformationDBAcces.GetHeatListforDateRange(System.DateTime.Now.Date.AddDays(-3650), System.DateTime.Now.Date.AddDays(3650));

                List<CowHeatInfoView> searchedHeatList = AnimalInformationDBAcces.CowHeatListViewFormat(initialHeatSearchList);

                if (viewListObject.animalID != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.animalId == viewListObject.animalID).ToList();
                }

                if (viewListObject.heatDateMin != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.heatDate >= viewListObject.heatDateMin).ToList();
                }

                if (viewListObject.heatDateMax != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.heatDate <= viewListObject.heatDateMax).ToList();
                }

                if (viewListObject.nextHeatDateMin != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.nextHeatDate >= viewListObject.nextHeatDateMin).ToList();
                }

                if (viewListObject.nextHeatDateMax != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.nextHeatDate <= viewListObject.nextHeatDateMax).ToList();
                }

                if (viewListObject.injectedDateMin != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.injectedDate >= viewListObject.injectedDateMin).ToList();
                }

                if (viewListObject.injectedDateMax != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.injectedDate <= viewListObject.injectedDateMax).ToList();
                }

                if (viewListObject.expectedDateMin != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.expectedDeliveryDate >= viewListObject.expectedDateMin).ToList();

                }

                if (viewListObject.expectedDateMax != null)
                {
                    searchedHeatList = searchedHeatList.Where(a => a.expectedDeliveryDate <= viewListObject.expectedDateMax).ToList();
                }

                viewListObject.heatList = searchedHeatList;

                return View(viewListObject);
            }

            return View(viewListObject);
        }

        [RBAC]
        public ActionResult CowHeatUpdate(int id, DateTime date)
        {
            CowHeatUpdate updateHeatInfo = new CowHeatUpdate();
            var heatInfo = AnimalInformationDBAcces.GetHeatInfoByIDDate(id, date);

            if (heatInfo != null)
            {
                updateHeatInfo.actualDeliveryDate = heatInfo.ActualDeliveryDate;
                updateHeatInfo.animalId = heatInfo.AnimalId;
                updateHeatInfo.animalCodeName = AnimalInformationDBAcces.GetAnimalInfoByID(heatInfo.AnimalId).AnimalCodeName;
                updateHeatInfo.deliveryStatus = heatInfo.DeliveryStatus;
                updateHeatInfo.expectedDeliveryDate = heatInfo.ExpectedDeliveryDate;
                updateHeatInfo.heatDate = heatInfo.HeatDate;
                updateHeatInfo.heatSummary = heatInfo.HeatSummary;
                updateHeatInfo.heatTime = heatInfo.HeatTime;
                updateHeatInfo.injectedDate = heatInfo.InjectedDate;
                updateHeatInfo.injectedTime = heatInfo.InjectedTime;
                updateHeatInfo.nextHeatDate = heatInfo.NextHeatDate;

                updateHeatInfo.HeatTimeOptions = new List<SelectListItem>(){
                            new SelectListItem() { Text="Morning", Value="morning",Selected=heatInfo.HeatTime=="morning"?true:false},
                            new SelectListItem() {Text="Noon", Value="noon",Selected=heatInfo.HeatTime=="noon"?true:false},
                            new SelectListItem() { Text="Afternoon", Value="afternoon",Selected=heatInfo.HeatTime=="afternoon"?true:false},
                            new SelectListItem() { Text="Night", Value="night",Selected=heatInfo.HeatTime=="night"?true:false}
                };

                updateHeatInfo.InjectedTimeOptions = new List<SelectListItem>(){
                            new SelectListItem() { Text="Morning", Value="morning",Selected=heatInfo.InjectedTime=="morning"?true:false},
                            new SelectListItem() {Text="Noon", Value="noon",Selected=heatInfo.InjectedTime=="noon"?true:false},
                            new SelectListItem() { Text="Afternoon", Value="afternoon",Selected=heatInfo.InjectedTime=="afternoon"?true:false},
                            new SelectListItem() { Text="Night", Value="night",Selected=heatInfo.InjectedTime=="night"?true:false}
                };

            }

            return View(updateHeatInfo);
        }


        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CowHeatUpdate(CowHeatUpdate updatedHeatInfo)
        {

            if (ModelState.IsValid)
            {
                CowHeat editedCowHeat = new CowHeat();

                editedCowHeat.ActualDeliveryDate = updatedHeatInfo.actualDeliveryDate;
                editedCowHeat.AnimalId = updatedHeatInfo.animalId;
                editedCowHeat.DeliveryStatus = updatedHeatInfo.deliveryStatus;
                editedCowHeat.ExpectedDeliveryDate = updatedHeatInfo.expectedDeliveryDate;
                editedCowHeat.HeatDate = updatedHeatInfo.heatDate;
                editedCowHeat.HeatSummary = updatedHeatInfo.heatSummary;
                editedCowHeat.HeatTime = updatedHeatInfo.heatTime;
                editedCowHeat.InjectedDate = updatedHeatInfo.injectedDate;
                editedCowHeat.InjectedTime = updatedHeatInfo.injectedTime;
                editedCowHeat.NextHeatDate = updatedHeatInfo.nextHeatDate;

                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                editedCowHeat.OperatorName = name;

                if (AnimalInformationDBAcces.CowHeatUpdate(editedCowHeat))
                {
                    ViewBag.success = "Animal Weight Updated Successfully";
                    return RedirectToAction("CowHeatList");
                }

                else
                {
                    ViewBag.success = "Animal Weight was not Updated.";
                    return RedirectToAction("CowHeatList");
                }

            }

            return View(updatedHeatInfo);
        }

        #endregion

        //------------------Animal Medicine ------------------------------
        #region
        [RBAC]
        public ActionResult MedicineAdd()
        {
            AnimalMdicationAdd newentry = new AnimalMdicationAdd();
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newentry.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                newentry.animallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }
            newentry.MedicationDate = System.DateTime.Now.Date;
            newentry.MedicationCourseDate = System.DateTime.Now.Date;
            newentry.GetNotiFication = true;
            return View(newentry);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MedicineAdd(AnimalMdicationAdd newentry)
        {
            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                if (AnimalInformationDBAcces.AddAnimalMedication(newentry, operatorName))
                {
                    ModelState.Clear();
                    ViewBag.success = "Animal Medication has been added successfully";
                }
                else
                    ViewBag.success = "Sorry, Operation Falied, Please try again";
            }
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newentry.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                newentry.animallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }
            newentry.MedicationDate = System.DateTime.Now.Date;
            newentry.MedicationCourseDate = System.DateTime.Now.Date;

            newentry.Comments = null;
            newentry.Dose = null;
            newentry.MedicineName = null;
            newentry.SelectedCourseDates = null;

            return View(newentry);
        }

        [RBAC]
        public ActionResult MedicationList()
        {
            MedicationListView entrylist = new MedicationListView();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                entrylist.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                entrylist.vacanimallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            entrylist.VaccinationDateMax = System.DateTime.Now.Date;
            entrylist.VaccinationDateMin = System.DateTime.Now.Date.AddDays(-30);


            var availablemedicinedose = AnimalInformationDBAcces.GetAvailableMedicinedose(entrylist.VaccinationDateMax, entrylist.VaccinationDateMin);

            List<MedicationList> lists = new List<MedicationList>();

            foreach (var medicineli in availablemedicinedose)
            {
                var animalcodename = AnimalInformationDBAcces.GetAnimalInfoByID(medicineli.medicine.AnimalId);
                lists.Add(new MedicationList
                {
                    medicationId = medicineli.medicationId,
                    MedicineName = medicineli.medicine.MedicineName,
                    AnimalCodeName = animalcodename.AnimalCodeName,
                    MedicationDate = medicineli.MedicationDate,
                    Dose = medicineli.Dose,
                    Comments = medicineli.medicine.Comments,
                    OperatorName = medicineli.medicine.OperatorName

                });

            }

            entrylist.medicinelist = lists;
            return View(entrylist);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MedicationList(MedicationListView entrylist)
        {

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                entrylist.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                entrylist.vacanimallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }



            var availablemedicinedose = AnimalInformationDBAcces.GetAvailableMedicinedose(entrylist.VaccinationDateMax, entrylist.VaccinationDateMin);

            if (entrylist.VAnimalId != null)
            {
                availablemedicinedose = availablemedicinedose.Where(a => a.medicine.AnimalId == entrylist.VAnimalId).ToList();
            }
            if (entrylist.MedicineNames != null)
            {
                availablemedicinedose = availablemedicinedose.Where(a => a.medicine.MedicineName == entrylist.MedicineNames).ToList();
            }
            List<Medication> animalmedicationList = AnimalInformationDBAcces.GetAvailableMedicine();
            if (entrylist.VAnimalTypeId != null)
            {
                var animallist = AnimalInformationDBAcces.GetAnimalByAnimalTypeId((int)entrylist.VAnimalTypeId);

                if (animallist != null)
                {
                    animalmedicationList = animalmedicationList
                    .Where(x => animallist.Any(y => y.PKAnimalId == x.AnimalId)).ToList();
                }
                availablemedicinedose = availablemedicinedose.Where(x => animalmedicationList.Any(animal => animal.PKMedicationId == x.medicationId)).ToList();

            }
            List<MedicationList> lists = new List<MedicationList>();

            foreach (var medicineli in availablemedicinedose)
            {
                var animalcodename = AnimalInformationDBAcces.GetAnimalInfoByID(medicineli.medicine.AnimalId);
                lists.Add(new MedicationList
                {
                    medicationId = medicineli.medicationId,
                    MedicineName = medicineli.medicine.MedicineName,
                    AnimalCodeName = animalcodename.AnimalCodeName,
                    MedicationDate = medicineli.MedicationDate,
                    Dose = medicineli.Dose,
                    Comments = medicineli.medicine.Comments,
                    OperatorName = medicineli.medicine.OperatorName

                });

            }
            entrylist.medicinelist = lists;
            return View(entrylist);
        }

        [RBAC]
        public ActionResult MedicationDelete(int id, DateTime date)
        {
            var medicationcourse = AnimalInformationDBAcces.GetMedicationDoseByIdanddate(id, date);
            if (medicationcourse != null)
            {
                if (AnimalInformationDBAcces.DeleteMedicationCourse(medicationcourse.medicationId, medicationcourse.MedicationDate))
                {

                }
            }
            return RedirectToAction("MedicationList");
        }
        #endregion

        //------------------Animal Vaccine ------------------------------
        #region

        public ActionResult AnimalTypeList()
        {
            var v = AnimalInformationDBAcces.GetEnabledAnimalType();
            return View(v);
        }

        [RBAC]
        public ActionResult VaccineAdd()
        {
            AnimalVaccineAdd newentry = new AnimalVaccineAdd();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newentry.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                newentry.animallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            newentry.VaccinationDate = System.DateTime.Now.Date;
            newentry.NextVaccationDate = System.DateTime.Now.Date;

            return View(newentry);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vaccine(AnimalVaccineAdd newentry)
        {
            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                newentry.AnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                newentry.animallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                if (AnimalInformationDBAcces.AddAnimalVaccine(newentry, operatorName))
                {
                    ViewBag.success = "Animal Vaccine Inserted Successfully";
                }
                ModelState.Clear();
            }
            return View(newentry);
        }

        public ActionResult Vaccination(int TypeID)
        {

            AnimalVaccinationInfoAdd animalVaccineInfo = new AnimalVaccinationInfoAdd();
            animalVaccineInfo.VaccinationDate = System.DateTime.Now;
            animalVaccineInfo.NextVaccationDate = animalVaccineInfo.VaccinationDate.AddDays(1);
            var AnimalList = AnimalInformationDBAcces.GetAnimalListByTypeId(TypeID);

            var VaccinInfo = new List<SingelVaccineInfo>();
            animalVaccineInfo.VaccinationInfo = VaccinInfo;

            if (AnimalList != null)
            {
                foreach (var animal in AnimalList)
                {
                    VaccinInfo.Add(new SingelVaccineInfo
                    {
                        AnimalId = animal.PKAnimalId,
                        CodeName = animal.AnimalCodeName
                    });
                }
                animalVaccineInfo.VaccinationInfo = VaccinInfo;
            }
            animalVaccineInfo.GetNotiFication = true;
            animalVaccineInfo.AnimalTypeId = TypeID;

            return View(animalVaccineInfo);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Vaccination(AnimalVaccinationInfoAdd animalVaccineInfo)
        {
            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);


                if (AnimalInformationDBAcces.AddVaccination(animalVaccineInfo, operatorName))
                {
                    ViewBag.success = "Animal Vaccine Inserted Successfully";
                }
                ModelState.Clear();
                return RedirectToAction("AnimalTypeList");
            }
            else
            {
                var AnimalList = AnimalInformationDBAcces.GetAnimalListByTypeId(animalVaccineInfo.AnimalTypeId);

                var VaccinInfo = new List<SingelVaccineInfo>();
                animalVaccineInfo.VaccinationInfo = VaccinInfo;

                if (AnimalList != null)
                {
                    foreach (var animal in AnimalList)
                    {
                        VaccinInfo.Add(new SingelVaccineInfo
                        {
                            AnimalId = animal.PKAnimalId,
                            CodeName = animal.AnimalCodeName
                        });
                    }
                    animalVaccineInfo.VaccinationInfo = VaccinInfo;
                }
                animalVaccineInfo.GetNotiFication = true;
                return View(animalVaccineInfo);
            }

        }
        [RBAC]
        public ActionResult VaccineList()
        {
            VaccineList vaccinelistview = new VaccineList();

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                vaccinelistview.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                vaccinelistview.vacanimallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }

            vaccinelistview.VaccinationDateMax = System.DateTime.Now.Date;
            vaccinelistview.VaccinationDateMin = System.DateTime.Now.Date.AddDays(-30);



            var availablevaccine = AnimalInformationDBAcces.GetAvailableVaccines(vaccinelistview.VaccinationDateMax, vaccinelistview.VaccinationDateMin);
            vaccinelistview.vaccinelist = availablevaccine;

            return View(vaccinelistview);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VaccineList(VaccineList vaccinelistview)
        {

            var types = AnimalInformationDBAcces.GetEnabledAnimalType();
            if (types != null)
            {
                vaccinelistview.VAnimalTypes = types.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalTypeId.ToString(),
                    Text = x.AnimalTypeName
                });
            }

            var availableAnimal = AnimalInformationDBAcces.GetAvailableAnimalforHeat();

            if (availableAnimal != null)
            {
                vaccinelistview.vacanimallist = availableAnimal.Select(x => new SelectListItem
                {
                    Value = x.PKAnimalId.ToString(),
                    Text = x.AnimalCodeName
                });
            }


            var availablevaccine = AnimalInformationDBAcces.GetAvailableVaccines(vaccinelistview.VaccinationDateMax, vaccinelistview.VaccinationDateMin);
            if (vaccinelistview.VAnimalTypeId != null)
            {
                availablevaccine = availablevaccine.Where(a => a.animal.AnimalTypeId == vaccinelistview.VAnimalTypeId).ToList();
            }
            if (vaccinelistview.VAnimalId != null)
            {
                availablevaccine = availablevaccine.Where(a => a.AnimalId == vaccinelistview.VAnimalId).ToList();
            }

            if (vaccinelistview.VaccinationDateMax != null)
            {
                availablevaccine = availablevaccine.Where(a => a.VaccinationDate >= vaccinelistview.VaccinationDateMin && a.VaccinationDate <= vaccinelistview.VaccinationDateMax).ToList();
            }
            vaccinelistview.vaccinelist = availablevaccine;

            return View(vaccinelistview);
        }

        [RBAC]
        public ActionResult VaccineEdit(int id)
        {
            AnimalVaccineEdit editentry = new AnimalVaccineEdit();
            var Vaccine = AnimalInformationDBAcces.GetVccineById(id);
            if (Vaccine != null)
            {
                editentry.PKVaccinationId = Vaccine.PKVaccinationId;
                editentry.NextVaccationDate = Vaccine.NextVaccationDate;
                editentry.AnimalCodeName = Vaccine.animal.AnimalCodeName;
                editentry.Comments = Vaccine.Comments;

            }
            return View(editentry);
        }

        [RBAC]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult VaccineEdit(AnimalVaccineEdit editentry)
        {


            if (ModelState.IsValid)
            {
                string name = HttpContext.User.Identity.Name;
                string operatorName = AgroExpressDBAccess.GetFullNamebyUserID(name);
                if (AnimalInformationDBAcces.UpdateVaccination(editentry, operatorName))
                {
                    return RedirectToAction("VaccineList");
                }
            }
            var Vaccine = AnimalInformationDBAcces.GetVccineById(editentry.PKVaccinationId);
            if (Vaccine != null)
            {
                editentry.PKVaccinationId = Vaccine.PKVaccinationId;
                editentry.NextVaccationDate = Vaccine.NextVaccationDate;
                editentry.AnimalCodeName = Vaccine.animal.AnimalCodeName;
                editentry.Comments = Vaccine.Comments;

            }
            return View(editentry);
        }
        #endregion



        public JsonResult AnimalList(int animaltypeid)
        {
            var selectList = new List<SelectListItem>();
            var animals = AnimalInformationDBAcces.GetAnimalListByAnimalTypeId(animaltypeid);

            if (animals != null)
            {
                foreach (var animal in animals)
                {
                    selectList.Add(new SelectListItem
                    {
                        Value = animal.PKAnimalId.ToString(),
                        Text = animal.AnimalCodeName

                    });
                }
            }
            return Json(selectList, JsonRequestBehavior.AllowGet);
        }
    }
}