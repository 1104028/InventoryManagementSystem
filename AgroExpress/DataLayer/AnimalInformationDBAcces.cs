using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;

namespace AgroExpress.DataLayer
{
    public class AnimalInformationDBAcces
    {
        //--------------------   Animal Type -------------------------------------
        #region

        public static bool IsAnimalTypeNameExists(string animalTypeName)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalTypes.SingleOrDefault(a => a.AnimalTypeName == animalTypeName);
                    if (v != null)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AddAnimalType(AnimalType animalType)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    databaseModel.AnimalTypes.Add(animalType);
                    databaseModel.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static List<AnimalInformation> GetAnimalByAnimalTypeId(int animaltypeid)
        {
            try
            {
                using (AnimalInformationContext dataModel = new AnimalInformationContext())
                {
                    var v = dataModel.AnimalInformations.Where(a => a.AnimalTypeId == animaltypeid).ToList();
                    return v;
                }
            }
            catch (Exception)
            { return null; }
        }
        public static List<AnimalType> GetEnabledAnimalType()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalTypes.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static List<AnimalType> GetDisabledAnimalType()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalTypes.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool EnableAnimalType(int typeID)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalTypes.Where(a => a.PKAnimalTypeId == typeID).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        datamodel.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool DisableAnimalType(int typeID)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalTypes.Where(a => a.PKAnimalTypeId == typeID).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        datamodel.SaveChanges();
                        return true;
                    }
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        #endregion

        //--------------------   Animal -------------------------------------
        #region

        public static bool AddAnimal(AnimalInformation animal)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    databaseModel.AnimalInformations.Add(animal);
                    databaseModel.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static bool IscodeNameExists(string codeName)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalInformations.Where(a => a.AnimalCodeName == codeName).FirstOrDefault();
                    if (v != null)
                    {
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static List<AnimalInformation> GetEnabledAnimal()
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.Include("animalType").Where(a => a.IsDeleted == false).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalInformation> GetNumberofEnabledAnimal(int numberofRows)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.Include("animalType").Where(a => a.IsDeleted == false).Take(numberofRows).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalInformation> GetAnimalListByTypeId(int animalTypeId)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.AnimalInformations.Where(a => a.IsDeleted == false && a.AnimalTypeId == animalTypeId).ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalInformation> GetDisabledAnimalList()
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.Where(a => a.IsDeleted == true).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static bool DisableAnimal(int animalID)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.AnimalInformations.SingleOrDefault(a => a.PKAnimalId == animalID);
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        databaseModel.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public static bool EnableAnimal(int animalID)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.AnimalInformations.SingleOrDefault(a => a.PKAnimalId == animalID);
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        databaseModel.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public static List<AnimalInformation> GetEnabledFemaleAnimal()
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.Include("animalType").Where(a => a.IsDeleted == false && a.Gender.ToLower() == "female").ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }
        public static List<AnimalInformation> GetEnabledFemaleAnimalByTypeId(int typeid)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.Include("animalType").Where(a => a.IsDeleted == false && a.Gender.ToLower() == "female" && a.AnimalTypeId == typeid).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static AnimalInformation GetAnimalInfoByID(int animalID)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalInformations.SingleOrDefault(a => a.PKAnimalId == animalID);
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static bool UpdateAnimal(AnimalUpdate editedAnimal)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.AnimalInformations.SingleOrDefault(a => a.PKAnimalId == editedAnimal.PKAnimalId);
                    if (v != null)
                    {
                        v.AnimalCodeName = editedAnimal.AnimalCodeName;
                        v.AnimalTypeId = editedAnimal.AnimalType;
                        v.Comments = editedAnimal.Comments;
                        v.DateofEntry = editedAnimal.DateofEntry;
                        v.DateofExit = editedAnimal.DateofExit;
                        v.Gender = editedAnimal.Gender;
                        v.IsDeleted = editedAnimal.IsDeleted;

                        databaseModel.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        public static List<AnimalInformation> GetAnimalListByAnimalTypeId(int animalTypeId)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.AnimalInformations.Where(a => a.IsDeleted == false && a.AnimalTypeId == animalTypeId).ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        #endregion

        //--------------------   Animal Weight -------------------------------------
        #region

        public static List<AnimalInformation> GetAvailableAnimalforWeightInput()
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var allAnimal = databaseModel.AnimalInformations.Include("animalType").Where(a => a.IsDeleted == false).ToList();
                    return allAnimal;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalWeight> GetWeightedAnimal()
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalWeight.Where(a => a.animal.IsDeleted == false).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalWeight> GetNumberofWeightedAnimal(int numberofRows)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalWeight.Where(a => a.animal.IsDeleted == false).Take(numberofRows).ToList();
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<AnimalWeightInfoView> AnimalWeightViewFormat(List<AnimalWeight> request)
        {
            List<AnimalWeightInfoView> returnList = new List<AnimalWeightInfoView>();
            try
            {
                using (AnimalInformationContext contex = new AnimalInformationContext())
                {
                    foreach (var tem in request)
                    {
                        var animalinfo = contex.AnimalInformations.Include("animalType").Where(a => a.PKAnimalId == tem.AnimalId).FirstOrDefault();
                        returnList.Add(new AnimalWeightInfoView
                        {
                            animalId = tem.AnimalId,
                            Date = tem.Date,
                            animalCodeName = animalinfo.AnimalCodeName,
                            gender = animalinfo.Gender,
                            animalType = animalinfo.animalType.AnimalTypeName,
                            Weight = tem.Weight,
                            animalTypeId = animalinfo.AnimalTypeId
                        });

                    }
                    return returnList;
                }


            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static string AddAnimalWeightList(List<AnimalWeight> requestList)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    if(requestList!=null)
                    {
                        foreach(var insert in requestList)
                        {
                            databaseModel.AnimalWeight.AddOrUpdate(insert);
                            databaseModel.SaveChanges();
                        }
                        return "OK";
                    }
                    return "not inserted"; 
                }
            }
            catch (Exception ec)
            {
                return ec.Message;
            }
        }

        public static AnimalWeight GetWeightInfoByIDDate(int id, DateTime date)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    return databaseModel.AnimalWeight.SingleOrDefault(a => a.AnimalId == id && a.Date == date.Date);
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static AnimalWeightInfoView AnimalWeightViewFormatSingle(AnimalWeight request)
        {
            AnimalWeightInfoView returnList = new AnimalWeightInfoView();
            try
            {
                using (AnimalInformationContext contex = new AnimalInformationContext())
                {
                    var animalinfo = contex.AnimalInformations.Include("animalType").Where(a => a.PKAnimalId == request.AnimalId).FirstOrDefault();

                    returnList.animalId = animalinfo.PKAnimalId;
                    returnList.Date = request.Date;
                    returnList.animalCodeName = animalinfo.AnimalCodeName;
                    returnList.gender = animalinfo.Gender;
                    returnList.animalType = animalinfo.animalType.AnimalTypeName;
                    returnList.Weight = request.Weight;
                    returnList.animalTypeId = animalinfo.AnimalTypeId;

                    return returnList;
                }


            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static string UpdateAnimalWeight(AnimalWeightInfoView editedWeight)
        {
            try
            {
                using (AnimalInformationContext dataModel = new AnimalInformationContext())
                {
                    var v = dataModel.AnimalWeight.SingleOrDefault(a => a.AnimalId == editedWeight.animalId && a.Date == editedWeight.Date);
                    if (v != null)
                    {
                        v.Weight = editedWeight.Weight;
                        dataModel.SaveChanges();
                        return "OK";
                    }
                    return "No Such Record is Found.";
                }


            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static List<AnimalWeight> GetAnimalWeightsByDate(DateTime date)
        {
            try
            {
                using (AnimalInformationContext dataModel = new AnimalInformationContext())
                {
                    var v = dataModel.AnimalWeight.Include("animal").Where(a => a.Date == date).ToList();


                    return v;

                }


            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        //-------------------- Cow Heat -------------------------------------
        #region

        public static List<AnimalInformation> GetAvailableAnimalforHeat()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.AnimalInformations.Where(a => a.IsDeleted == false).ToList();
                    return v;

                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool IsHeatInputPossible(int id, DateTime date)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.CowHeat.SingleOrDefault(a => a.AnimalId == id && a.HeatDate == date);
                    if (v == null)
                        return true;
                    else return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AddHeat(CowHeat newHeatInfo)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    datamodel.CowHeat.Add(newHeatInfo);
                    datamodel.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<CowHeat> GetHeatListforDateRange(DateTime minDate, DateTime maxDate)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.CowHeat.Where(a => a.HeatDate >= minDate.Date && a.HeatDate <= maxDate.Date).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static CowHeat GetHeatInfoByIDDate(int id, DateTime date)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.CowHeat.SingleOrDefault(a => a.AnimalId == id && a.HeatDate == date.Date);
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<CowHeatInfoView> CowHeatListViewFormat(List<CowHeat> request)
        {
            List<CowHeatInfoView> returnList = new List<CowHeatInfoView>();
            try
            {
                using (AnimalInformationContext contex = new AnimalInformationContext())
                {
                    foreach (var tem in request)
                    {
                        var animalinfo = contex.AnimalInformations.Include("animalType").Where(a => a.PKAnimalId == tem.AnimalId).FirstOrDefault();
                        returnList.Add(new CowHeatInfoView
                        {
                            animalId = tem.AnimalId,
                            animalCodeName = animalinfo.AnimalCodeName,
                            animalTypeID = animalinfo.AnimalTypeId,
                            animalTypeName = animalinfo.animalType.AnimalTypeName,
                            actualDeliveryDate = tem.ActualDeliveryDate,
                            deliveryStatus = tem.DeliveryStatus,
                            expectedDeliveryDate = tem.ExpectedDeliveryDate,
                            heatDate = tem.HeatDate,
                            heatSummary = tem.HeatSummary,
                            heatTime = tem.HeatSummary,
                            injectedDate = tem.InjectedDate,
                            injectedTime = tem.InjectedTime,
                            nextHeatDate = tem.NextHeatDate
                        });

                    }
                    return returnList;
                }


            }
            catch (Exception e)
            {
                return null;
            }

        }

        public static bool CowHeatUpdate(CowHeat updatedHeatInfo)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.CowHeat.SingleOrDefault(a => a.AnimalId == updatedHeatInfo.AnimalId && a.HeatDate == updatedHeatInfo.HeatDate.Date);
                    if (v != null)
                    {
                        v.ActualDeliveryDate = updatedHeatInfo.ActualDeliveryDate;
                        v.AnimalId = updatedHeatInfo.AnimalId;
                        v.DeliveryStatus = updatedHeatInfo.DeliveryStatus;
                        v.ExpectedDeliveryDate = updatedHeatInfo.ExpectedDeliveryDate;
                        v.HeatDate = updatedHeatInfo.HeatDate;
                        v.HeatSummary = updatedHeatInfo.HeatSummary;
                        v.HeatTime = updatedHeatInfo.HeatTime;
                        v.InjectedDate = updatedHeatInfo.InjectedDate;
                        v.InjectedTime = updatedHeatInfo.InjectedTime;
                        v.NextHeatDate = updatedHeatInfo.NextHeatDate;
                        v.OperatorName = updatedHeatInfo.OperatorName;

                        databaseModel.SaveChanges();
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception ec)
            {
                return false;
            }
        }

        #endregion


        //--------------------   Animal Medication  -------------------------------------
        #region
        public static bool AddAnimalMedication(AnimalMdicationAdd newentry, string operatorname)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var animal = AnimalInformationDBAcces.GetAnimalInfoByID((int)newentry.AnimalId);
                    if (animal != null)
                    {
                        Medication add = new Medication();
                        add.AnimalId = (int)newentry.AnimalId;
                        add.MedicationDate = newentry.MedicationDate;
                        add.MedicineName = newentry.MedicineName;
                        add.Comments = newentry.Comments;


                        add.OperatorName = operatorname;

                        datamodel.Medication.Add(add);
                        datamodel.SaveChanges();



                        var medicineid = datamodel.Medication.SingleOrDefault(a => a.MedicationDate == newentry.MedicationDate && a.MedicineName == newentry.MedicineName && a.AnimalId == newentry.AnimalId);

                        bool isinserted = false;

                        if (medicineid.PKMedicationId != 0)
                        {
                            var animalInfo = datamodel.AnimalInformations.Where(a => a.PKAnimalId == newentry.AnimalId).FirstOrDefault();
                            var medicationdates = newentry.SelectedCourseDates;
                            Char delimiter = ',';
                            string[] split = medicationdates.Split(delimiter);

                            string dates;
                            List<Notification> NotificationInfo = new List<Notification>();


                            for (var i = 0; i < split.Length; i++)
                            {
                                MedicineCourse addnew = new MedicineCourse();
                                addnew.medicationId = medicineid.PKMedicationId;
                                addnew.Dose = newentry.Dose;
                                addnew.status = true;

                                dates = split[i];

                                var sourcedater = DateTime.Parse(dates);
                                var date = sourcedater.ToString("M/d/yyyy");
                                DateTime outputdater;
                                DateTime.TryParseExact(date.ToString(), "M/d/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out outputdater);

                                addnew.MedicationDate = outputdater;
                                if (AnimalInformationDBAcces.AddMedicationDose(addnew))
                                {
                                    if (newentry.GetNotiFication)
                                    {

                                        var typeinf = datamodel.AnimalTypes.Where(a => a.PKAnimalTypeId == newentry.AnimalTypeId).FirstOrDefault();
                                        for (int index = -1; index < 1; index++)
                                        {
                                            NotificationInfo.Add(new Notification
                                            {
                                                Date = addnew.MedicationDate.Date.AddDays(index),
                                                NotificationMessage = "Animal code name: " + animalInfo.AnimalCodeName + ", Medicine Name: " + newentry.MedicineName + ", Date: " + addnew.MedicationDate.ToString("dddd, dd MMMM yyyy"),
                                                GroupID = "Medicine" + newentry.MedicineName + newentry.MedicationDate.ToString("MM/dd/yyyy")
                                            });
                                        }

                                    }
                                    isinserted = true;
                                }

                            }
                            if (newentry.GetNotiFication)
                                AgroExpressDBAccess.AddNotificationList(NotificationInfo);
                        }
                        if (isinserted == true)
                            return true;

                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static Medication IsMedicineExistsByNameandDate(string medicinename, DateTime date, int animalid)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.Medication.SingleOrDefault(a => a.MedicationDate == date && a.MedicineName == medicinename && a.AnimalId == animalid);
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool AddMedicationDose(MedicineCourse newentry)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    datamodel.MedicineCourses.Add(newentry);
                    datamodel.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Medication> GetAvailableMedicine()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.Medication.Include("animal").ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<MedicineCourse> GetAvailableMedicinedose(DateTime max, DateTime min)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.MedicineCourses.Include("medicine").Where(a => a.MedicationDate >= min && a.MedicationDate <= max).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static List<MedicineCourse> GetAvailableMedicinedose()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.MedicineCourses.Include("medicine").ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static MedicineCourse GetMedicationDoseByIdanddate(int id, DateTime date)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.MedicineCourses.Where(a => a.medicationId == id && a.MedicationDate == date).SingleOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool DeleteMedicationCourse(int id, DateTime date)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.MedicineCourses.Where(a => a.medicationId == id && a.MedicationDate == date).SingleOrDefault();
                    if (v != null)
                    {
                        datamodel.MedicineCourses.Remove(v);
                        datamodel.SaveChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        //--------------------   Animal Vaccine  -------------------------------------
        #region
        public static bool AddAnimalVaccine(AnimalVaccineAdd newentry, string operatorname)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var animal = AnimalInformationDBAcces.GetAnimalInfoByID(newentry.AnimalId);
                    if (animal != null)
                    {
                        Vaccination add = new Vaccination();
                        add.AnimalId = newentry.AnimalId;
                        add.VaccinationDate = newentry.VaccinationDate;
                        add.VaccineName = newentry.VaccineName;
                        add.NextVaccationDate = newentry.NextVaccationDate;
                        add.Comments = newentry.Comments;
                        add.IsApplied = true;
                        add.OperatorName = operatorname;
                        datamodel.Vaccinations.Add(add);
                        datamodel.SaveChanges();
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Vaccination> GetAvailableVaccines()
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.Vaccinations.Include("animal").ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Vaccination GetVccineById(int id)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.Vaccinations.Include("animal").Where(a => a.PKVaccinationId == id).SingleOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static bool UpdateVaccination(AnimalVaccineEdit editentry, string operatorname)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var vaccine = datamodel.Vaccinations.SingleOrDefault(a => a.PKVaccinationId == editentry.PKVaccinationId);
                    if (vaccine != null)
                    {

                        vaccine.NextVaccationDate = editentry.NextVaccationDate;
                        vaccine.Comments = editentry.Comments;
                        vaccine.OperatorName = operatorname;
                        datamodel.SaveChanges();
                        return true;
                    }
                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static bool AddVaccination(AnimalVaccinationInfoAdd newentry, string operatorname)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {

                    foreach (var item in newentry.VaccinationInfo)
                    {
                        Vaccination add = new Vaccination();
                        add.AnimalId = item.AnimalId;
                        add.VaccinationDate = newentry.VaccinationDate;
                        add.VaccineName = newentry.VaccineName;
                        add.NextVaccationDate = newentry.NextVaccationDate;
                        add.Comments = item.Comments;
                        add.IsApplied = item.isNotApplied ? false : true;
                        add.OperatorName = operatorname;
                        datamodel.Vaccinations.Add(add);
                        datamodel.SaveChanges();
                    }
                    if (newentry.GetNotiFication)
                    {
                        List<Notification> NotificationInfo = new List<Notification>();
                        var typeinf = datamodel.AnimalTypes.Where(a => a.PKAnimalTypeId == newentry.AnimalTypeId).FirstOrDefault();
                        for (int i = -3; i < 1; i++)
                        {
                            NotificationInfo.Add(new Notification
                            {
                                Date = newentry.NextVaccationDate.Date.AddDays(i),
                                NotificationMessage = "Animal type: " + typeinf.AnimalTypeName + ", next Vaccination Date of: " + newentry.VaccineName + " is " + newentry.NextVaccationDate.ToString("dddd, dd MMMM yyyy"),
                                GroupID = "Vaccine" + newentry.VaccineName + newentry.VaccinationDate.ToString("MM/dd/yyyy")
                            });
                        }
                        AgroExpressDBAccess.AddNotificationList(NotificationInfo);
                    }
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public static List<Vaccination> GetAvailableVaccines(DateTime max, DateTime min)
        {
            try
            {
                using (AnimalInformationContext datamodel = new AnimalInformationContext())
                {
                    var v = datamodel.Vaccinations.Include("animal").Where(a => a.VaccinationDate >= min && a.VaccinationDate <= max).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        #endregion

        //-------------------- Milk Production -------------------------------------
        #region
        public static bool AddOrUpdateMilkProduction(List<MilkProduction> ProductionList)
        {
            try
            {
                using (AnimalInformationContext dataModel = new AnimalInformationContext())
                {
                    foreach (var milk in ProductionList)
                    {
                        var v = dataModel.MilkProduction.SingleOrDefault(a => a.Date == milk.Date && a.AnimalId == milk.AnimalId);
                        if (v != null)
                        {
                            v.MorningAmount = milk.MorningAmount;
                            v.AfternoonAmount = milk.AfternoonAmount;
                            v.OperatorName = milk.OperatorName;

                            dataModel.SaveChanges();
                        }
                        else
                        {
                            MilkProduction tmp = new MilkProduction();
                            tmp.Date = milk.Date;
                            tmp.AnimalId = milk.AnimalId;
                            tmp.AfternoonAmount = milk.AfternoonAmount;
                            tmp.MorningAmount = milk.MorningAmount;
                            tmp.OperatorName = milk.OperatorName;

                            dataModel.MilkProduction.Add(tmp);
                            dataModel.SaveChanges();
                        }
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<MilkProduction> GetallMilkProductionByDate(DateTime date)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.MilkProduction.Include("animal").Where(a => a.Date == date).ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        public static List<MilkProduction> GetMilkProductionList(DateTime min, DateTime max)
        {
            try
            {
                using (AnimalInformationContext databaseModel = new AnimalInformationContext())
                {
                    var v = databaseModel.MilkProduction.Include("animal").Where(a => a.Date >= min && a.Date <= max).ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }
        #endregion
    }
}