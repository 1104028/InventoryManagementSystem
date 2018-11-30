using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.DataLayer;

namespace AgroExpress.Controllers
{
    [RBAC]
    public class AnimalTypeController : Controller
    {
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(AnimalType animalType)
        {
            if (ModelState.IsValid)
            {
                if (AnimalInformationDBAcces.IsAnimalTypeNameExists(animalType.AnimalTypeName))
                {
                    ModelState.AddModelError("AnimalTypeName", "Animal Type name already exist!!!");
                    return View();
                }
                else
                {
                    AnimalType newAnimalType = new AnimalType();

                    newAnimalType.AnimalTypeName = animalType.AnimalTypeName;

                    newAnimalType.IsDeleted = false;

                    if (AnimalInformationDBAcces.AddAnimalType(newAnimalType))
                    {
                        ViewBag.success = "Animal Type added successfully";
                        AnimalType tem = new AnimalType();
                        ModelState.Clear();
                        return View(tem);
                    }
                    else
                    {
                        ViewBag.success = "Unable to add please try again later!!!";
                        return View();
                    }

                }
            }
            return View();
        }

        public ActionResult List()
        {
            var v = AnimalInformationDBAcces.GetEnabledAnimalType();
            return View(v);
        }

        public ActionResult DisabledAnimalTypeList()
        {
            var v = AnimalInformationDBAcces.GetDisabledAnimalType();
            return View(v);
        }

        public ActionResult Delete(int Id)
        {
            var v = AnimalInformationDBAcces.DisableAnimalType(Id);
            return RedirectToAction("List");
        }

        public ActionResult Enable(int Id)
        {
            var v = AnimalInformationDBAcces.EnableAnimalType(Id);
            return RedirectToAction("DisabledAnimalTypeList");
        }
    }
}