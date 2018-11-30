using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Data.Entity.Migrations;
using AgroExpress.DataLayer.Models;
using AgroExpress.DataLayer.ViewModel;
using AgroExpress.Help;
namespace AgroExpress.DataLayer
{
    public class AgroExpressDBAccess
    {
        //-----------DATABASE--------------
        #region

        /// <summary>
        /// Check Database Connection
        /// </summary>
        /// <returns></returns>
        public static bool CheckDatabaseConnection()
        {
            try
            {
                using (AgroExpressContext databaseContext = new AgroExpressContext())
                {
                    if (databaseContext.Database.Exists())
                    {
                        databaseContext.Database.Connection.Open();
                        if (databaseContext.Database.Connection.State == ConnectionState.Open)
                        {
                            databaseContext.Database.Connection.Close();
                            return true;
                        }
                        else return false;
                    }
                    else return false;
                }
            }
            catch (SqlException ex)
            {
                return false;
            }
        }
        #endregion

        //--------------------   User Login -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="LoginInfo"></param>
        /// <returns></returns>
        public static bool AddUserLoginInfo(UserLogin LoginInfo)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    databaseModel.UserLoginInfo.Add(LoginInfo);
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
        /// <param name="UserID"></param>
        /// <returns></returns>

        public static UserLogin IsUserExist(string UserID)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.UserLoginInfo.Where(a => a.UserName == UserID).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static bool IsEnableUserExist(string UserName)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.UserLoginInfo.Where(a => a.UserName == UserName).FirstOrDefault();
                    if (v == null)
                    {
                        return false;
                    }
                    else
                    {
                        int userID = v.PkUserID;
                        if (v.UserType == 1)
                        {
                            var adv = dataModel.Admins.Where(a => a.LoginUserID == userID).FirstOrDefault();
                            if (adv != null)
                            {
                                return !adv.IsDeleted;
                            }
                            else return false;
                        }
                        else if (v.UserType == 2)
                        {
                            var manv = dataModel.Managers.Single(a => a.LoginUserID == userID);
                            if (manv != null)
                            {
                                return !manv.IsDeleted;
                            }
                            else return false;
                        }
                        else if (v.UserType == 3)
                        {
                            var manv = dataModel.DeliveryBoy.Single(a => a.LoginUserID == userID);
                            if (manv != null)
                            {
                                return !manv.IsDeleted;
                            }
                            else return false;
                        }
                        else if (v.UserType == 4)
                        {
                            var manv = dataModel.Customers.Single(a => a.LoginUserID == userID);
                            if (manv != null)
                            {
                                return !manv.IsDeleted;
                            }
                            else return false;
                        }
                        else if (v.UserType == 5)
                        {
                            var manv = dataModel.FirmManger.Single(a => a.LoginUserID == userID);
                            if (manv != null)
                            {
                                return !manv.IsDeleted;
                            }
                            else return false;
                        }
                        else if (v.UserType == 6)
                        {
                            var manv = dataModel.Partners.Single(a => a.LoginUserID == userID);
                            if (manv != null)
                            {
                                return !manv.IsDeleted;
                            }
                            else return false;
                        }
                        return false;
                    }
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
        /// <param name="updatep"></param>
        /// <returns></returns>
        public static bool UpdatePassword(UpdatePassword updatep)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.UserLoginInfo.Where(a => a.UserName == updatep.UserName && a.UserType == updatep.UserType).FirstOrDefault();
                    if (temp != null)
                    {
                        temp.UserName = updatep.UserName;
                        temp.Password = updatep.Password;
                        temp.UserType = updatep.UserType;

                        databaseModel.SaveChanges();
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetFullNamebyID(int userID)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.UserLoginInfo.Where(a => a.PkUserID == userID).FirstOrDefault();
                    int userType = v.UserType;
                    if (v.UserType == 1)
                    {
                        var adv = dataModel.Admins.Where(a => a.LoginUserID == userID).FirstOrDefault();
                        if (adv != null)
                        {
                            return adv.FullName;
                        }
                        else return null;
                    }
                    else if (v.UserType == 2)
                    {
                        var manv = dataModel.Managers.Single(a => a.LoginUserID == userID);
                        if (manv != null)
                        {
                            return manv.FullName;
                        }
                        else return null;
                    }
                    else if (v.UserType == 3)
                    {
                        var manv = dataModel.DeliveryBoy.Single(a => a.LoginUserID == userID);
                        if (manv != null)
                        {
                            return manv.FullName;
                        }
                        else return null;
                    }
                    else if (v.UserType == 4)
                    {
                        var manv = dataModel.Customers.Single(a => a.LoginUserID == userID);
                        if (manv != null)
                        {
                            return manv.FullName;
                        }
                        else return null;
                    }
                    else if (v.UserType == 5)
                    {
                        var manv = dataModel.FirmManger.Single(a => a.LoginUserID == userID);
                        if (manv != null)
                        {
                            return manv.FullName;
                        }
                        else return null;
                    }
                    else if (v.UserType == 6)
                    {
                        var manv = dataModel.Partners.Single(a => a.LoginUserID == userID);
                        if (manv != null)
                        {
                            return manv.FullName;
                        }
                        else return null;
                    }
                    else return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static string GetFullNamebyUserID(string userID)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.UserLoginInfo.Where(a => a.UserName == userID).FirstOrDefault();
                    if (v != null)
                    {
                        int userType = v.UserType;
                        if (v.UserType == 1)
                        {
                            var adv = dataModel.Admins.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else if (v.UserType == 2)
                        {
                            var adv = dataModel.Managers.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else if (v.UserType == 3)
                        {
                            var adv = dataModel.DeliveryBoy.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else if (v.UserType == 4)
                        {
                            var adv = dataModel.Customers.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else if (v.UserType == 5)
                        {
                            var adv = dataModel.FirmManger.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else if (v.UserType == 6)
                        {
                            var adv = dataModel.Partners.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.FullName;
                            }
                            else return null;
                        }
                        else return null;

                    }

                    else return null;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <param name="pass"></param>
        /// <returns></returns>
        public static UserLogin IsUserPassValid(string UserID, string pass)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.UserLoginInfo.Where(a => a.UserName == UserID && a.Password == pass).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public static UserLogin GetUserByID(int UserID)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.UserLoginInfo.Where(a => a.PkUserID == UserID).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static int GetCustomerIdByUserId(string userID)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.UserLoginInfo.Where(a => a.UserName == userID).FirstOrDefault();
                    if (v != null)
                    {
                        int userType = v.UserType;
                        if (v.UserType == 4)
                        {
                            var adv = dataModel.Customers.Where(a => a.LoginUserID == v.PkUserID).FirstOrDefault();
                            if (adv != null)
                            {
                                return adv.PKCustomerId;
                            }
                            else return 0;
                        }

                        else return 0;

                    }

                    else return 0;
                }
            }
            catch (Exception)
            {
                return 0;
            }
        }


        #endregion
        //--------------------   Admin -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="admin"></param>
        /// <returns></returns>
        public static bool AddAdmin(AdminRegistration admin)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var NewUser = new UserLogin();
                    NewUser.UserName = admin.UserName;
                    NewUser.Password = admin.Password;
                    NewUser.UserType = 1;
                    AgroExpressDBAccess.AddUserLoginInfo(NewUser);

                    var Auser = AgroExpressDBAccess.IsUserExist(admin.UserName);

                    if (Auser != null)
                    {
                        var admint = new Admin();


                        admint.Mobile = admin.Mobile;
                        admint.Email = admin.Email;

                        admint.FullName = admin.FullName;
                        admint.Address = admin.Address;
                        admint.Mobile = admin.Mobile;
                        admint.Email = admin.Email;
                        admint.LoginUserID = Auser.PkUserID;
                        admint.IsDeleted = false;

                        databaseModel.Admins.Add(admint);
                        databaseModel.SaveChanges();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Admin> GetallAdmin()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Admins.ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Admin> GetallEnabledAdmin()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Admins.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Admin> GetallDisabledAdmin()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Admins.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableAdmin(int adminid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Admins.Where(a => a.PKAdminId == adminid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableAdmin(int adminid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Admins.Where(a => a.PKAdminId == adminid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Admin GetAdminByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Admins.Where(a => a.PKAdminId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdateAdmin(AdminEdit editadmin)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.Admins.Where(a => a.PKAdminId == editadmin.PKAdminId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editadmin.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editadmin.UserName;
                            updatep.Password = editadmin.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {
                                temp.Address = editadmin.Address;
                                temp.Mobile = editadmin.Mobile;
                                temp.LoginUserID = editadmin.LoginUserID;
                                temp.FullName = editadmin.FullName;

                                databaseModel.SaveChanges();
                                return true;
                            }
                        }

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

        //--------------------   Manager -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool AddManager(ManagerRegistration manager)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var NewUser = new UserLogin();
                    NewUser.UserName = manager.UserName;
                    NewUser.Password = manager.Password;
                    NewUser.UserType = 2;
                    if (AgroExpressDBAccess.AddUserLoginInfo(NewUser))
                    {
                        var Duser = AgroExpressDBAccess.IsUserExist(manager.UserName);

                        var salepoints = manager.SelectedSalePoints;
                        Char delimiter = ',';
                        string[] split = salepoints.Split(delimiter);

                        string salepointname;
                        bool isinserted = false;


                        for (var i = 0; i < split.Length; i++)
                        {

                            SalePointRelation relation = new SalePointRelation();

                            salepointname = split[i];

                            var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                            if (salepointdata != null)
                            {
                                relation.SalePointId = salepointdata.PKSalePointID;
                                relation.UserLoginId = Duser.PkUserID;

                                if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                {
                                    if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                    {
                                        isinserted = true;
                                    }
                                }



                            }
                        }

                        if (isinserted)
                        {
                            if (Duser != null)
                            {
                                var managerdata = new Manager();
                                managerdata.FullName = manager.FullName;
                                managerdata.Address = manager.Address;
                                managerdata.Email = manager.Email;
                                managerdata.Mobile = manager.Mobile;
                                managerdata.LoginUserID = Duser.PkUserID;
                                managerdata.IsDeleted = false;

                                databaseModel.Managers.Add(managerdata);
                                databaseModel.SaveChanges();
                                return true;
                            }
                        }
                    }

                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdateManager(ManagerEdit editmanager)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.Managers.Where(a => a.PKManagerId == editmanager.PKManagerId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editmanager.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editmanager.UserName;
                            updatep.Password = editmanager.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {
                                var Duser = AgroExpressDBAccess.IsUserExist(editmanager.UserName);

                                if (AgroExpressDBAccess.DeleteSalePointRelationById(editmanager.LoginUserID))
                                {
                                    var salepoints = editmanager.SelectedSalePoints;
                                    Char delimiter = ',';
                                    string[] split = salepoints.Split(delimiter);

                                    string salepointname;
                                    bool isinserted = false;


                                    for (var i = 0; i < split.Length; i++)
                                    {

                                        SalePointRelation relation = new SalePointRelation();

                                        salepointname = split[i];

                                        var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                                        if (salepointdata != null)
                                        {
                                            relation.SalePointId = salepointdata.PKSalePointID;
                                            relation.UserLoginId = Duser.PkUserID;

                                            if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                            {
                                                if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                                {
                                                    isinserted = true;
                                                }
                                            }
                                        }
                                    }

                                    if (isinserted)
                                    {
                                        temp.Address = editmanager.Address;
                                        temp.Mobile = editmanager.Mobile;
                                        temp.LoginUserID = editmanager.LoginUserID;
                                        temp.FullName = editmanager.FullName;
                                        temp.Email = editmanager.Email;

                                        databaseModel.SaveChanges();
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    return false;
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
        /// <returns></returns>
        public static List<Manager> GetallEnabledManager()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Managers.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Manager> GetallDisabledManager()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Managers.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableManager(int mid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Managers.Where(a => a.PKManagerId == mid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableManager(int mid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Managers.Where(a => a.PKManagerId == mid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Manager GetManagerByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Managers.Where(a => a.PKManagerId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

        //--------------------  Firm Manager -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool AddFirmManager(FirmManagerRegistration firmmanager)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var NewUser = new UserLogin();
                    NewUser.UserName = firmmanager.UserName;
                    NewUser.Password = firmmanager.Password;
                    NewUser.UserType = 5;
                    if (AgroExpressDBAccess.AddUserLoginInfo(NewUser))
                    {
                        var Duser = AgroExpressDBAccess.IsUserExist(firmmanager.UserName);

                        var salepoints = firmmanager.SelectedSalePoints;
                        Char delimiter = ',';
                        string[] split = salepoints.Split(delimiter);

                        string salepointname;
                        bool isinserted = false;


                        for (var i = 0; i < split.Length; i++)
                        {

                            SalePointRelation relation = new SalePointRelation();

                            salepointname = split[i];

                            var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                            if (salepointdata != null)
                            {
                                relation.SalePointId = salepointdata.PKSalePointID;
                                relation.UserLoginId = Duser.PkUserID;

                                if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                {
                                    if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                    {
                                        isinserted = true;
                                    }
                                }



                            }
                        }

                        if (isinserted)
                        {
                            if (Duser != null)
                            {
                                var firmmanagerdata = new FirmManager();
                                firmmanagerdata.FullName = firmmanager.FullName;
                                firmmanagerdata.Address = firmmanager.Address;
                                firmmanagerdata.Email = firmmanager.Email;
                                firmmanagerdata.Mobile = firmmanager.Mobile;
                                firmmanagerdata.LoginUserID = Duser.PkUserID;
                                firmmanagerdata.IsDeleted = false;

                                databaseModel.FirmManger.Add(firmmanagerdata);
                                databaseModel.SaveChanges();
                                return true;
                            }
                        }
                    }

                    return false;

                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdateFirmManager(FirmManagerEdit editfirmmanager)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.FirmManger.Where(a => a.PKFirmManagerId == editfirmmanager.PKFirmManagerId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editfirmmanager.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editfirmmanager.UserName;
                            updatep.Password = editfirmmanager.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {
                                var Duser = AgroExpressDBAccess.IsUserExist(editfirmmanager.UserName);

                                if (AgroExpressDBAccess.DeleteSalePointRelationById(editfirmmanager.LoginUserID))
                                {
                                    var salepoints = editfirmmanager.SelectedSalePoints;
                                    Char delimiter = ',';
                                    string[] split = salepoints.Split(delimiter);

                                    string salepointname;
                                    bool isinserted = false;


                                    for (var i = 0; i < split.Length; i++)
                                    {

                                        SalePointRelation relation = new SalePointRelation();

                                        salepointname = split[i];

                                        var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                                        if (salepointdata != null)
                                        {
                                            relation.SalePointId = salepointdata.PKSalePointID;
                                            relation.UserLoginId = Duser.PkUserID;

                                            if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                            {
                                                if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                                {
                                                    isinserted = true;
                                                }
                                            }
                                        }
                                    }

                                    if (isinserted)
                                    {
                                        temp.Address = editfirmmanager.Address;
                                        temp.Mobile = editfirmmanager.Mobile;
                                        temp.LoginUserID = editfirmmanager.LoginUserID;
                                        temp.FullName = editfirmmanager.FullName;
                                        temp.Email = editfirmmanager.Email;

                                        databaseModel.SaveChanges();
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                    return false;
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
        /// <returns></returns>
        public static List<FirmManager> GetallEnabledFirmManager()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.FirmManger.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<FirmManager> GetallDisabledFirmManager()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.FirmManger.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableFirmManager(int mid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.FirmManger.Where(a => a.PKFirmManagerId == mid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableFirmManager(int mid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.FirmManger.Where(a => a.PKFirmManagerId == mid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static FirmManager GetFirmManagerByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.FirmManger.Where(a => a.PKFirmManagerId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion
        //--------------------   Delivery Boy -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool AddDeliveryBoy(DeliveryBoyRegistration delivery)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var NewUser = new UserLogin();
                    NewUser.UserName = delivery.UserName;
                    NewUser.Password = delivery.Password;
                    NewUser.UserType = 3;
                    if (AgroExpressDBAccess.AddUserLoginInfo(NewUser))
                    {
                        var Duser = AgroExpressDBAccess.IsUserExist(delivery.UserName);

                        var salepoints = delivery.SelectedSalePoints;
                        Char delimiter = ',';
                        string[] split = salepoints.Split(delimiter);

                        string salepointname;
                        bool isinserted = false;


                        for (var i = 0; i < split.Length; i++)
                        {

                            SalePointRelation relation = new SalePointRelation();

                            salepointname = split[i];

                            var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                            if (salepointdata != null)
                            {
                                relation.SalePointId = salepointdata.PKSalePointID;
                                relation.UserLoginId = Duser.PkUserID;

                                if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                {
                                    if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                    {
                                        isinserted = true;
                                    }
                                }



                            }
                        }

                        if (isinserted)
                        {
                            if (Duser != null)
                            {
                                var deliveryboyt = new DeliveryBoy();
                                deliveryboyt.Address = delivery.Address;
                                deliveryboyt.FullName = delivery.FullName;
                                deliveryboyt.Mobile = delivery.Mobile;
                                deliveryboyt.Email = delivery.Email;
                                deliveryboyt.LoginUserID = Duser.PkUserID;

                                deliveryboyt.IsDeleted = false;

                                databaseModel.DeliveryBoy.Add(deliveryboyt);
                                databaseModel.SaveChanges();

                                return true;
                            }
                        }

                    }


                    return false;

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
        /// <returns></returns>
        public static List<DeliveryBoy> GetallEnabledDeliveryBoy()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.DeliveryBoy.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<DeliveryBoy> GetallDisabledDeliveryBoy()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.DeliveryBoy.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableDeliveryBoy(int did)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.DeliveryBoy.Where(a => a.PKDeliveryBoyId == did).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableDeliveryBoy(int did)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.DeliveryBoy.Where(a => a.PKDeliveryBoyId == did).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static DeliveryBoy GetDeliveryBoyByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.DeliveryBoy.Where(a => a.PKDeliveryBoyId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdateDeliveryBoy(DeliveryBoyEdit editDeliveryBoy)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.DeliveryBoy.Where(a => a.PKDeliveryBoyId == editDeliveryBoy.PKDeliveryBoyId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editDeliveryBoy.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editDeliveryBoy.UserName;
                            updatep.Password = editDeliveryBoy.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {
                                var Duser = AgroExpressDBAccess.IsUserExist(editDeliveryBoy.UserName);

                                if (AgroExpressDBAccess.DeleteSalePointRelationById(editDeliveryBoy.LoginUserID))
                                {
                                    var salepoints = editDeliveryBoy.SelectedSalePoints;
                                    Char delimiter = ',';
                                    string[] split = salepoints.Split(delimiter);

                                    string salepointname;
                                    bool isinserted = false;


                                    for (var i = 0; i < split.Length; i++)
                                    {

                                        SalePointRelation relation = new SalePointRelation();

                                        salepointname = split[i];

                                        var salepointdata = AgroExpressDBAccess.IsSalePointExist(salepointname);

                                        if (salepointdata != null)
                                        {
                                            relation.SalePointId = salepointdata.PKSalePointID;
                                            relation.UserLoginId = Duser.PkUserID;

                                            if (AgroExpressDBAccess.IsSalePointRelationExists(relation) == false)
                                            {
                                                if (AgroExpressDBAccess.AddSalePointRelation(relation))
                                                {
                                                    isinserted = true;
                                                }
                                            }
                                        }
                                    }

                                    if (isinserted)
                                    {
                                        temp.Address = editDeliveryBoy.Address;
                                        temp.Mobile = editDeliveryBoy.Mobile;
                                        temp.LoginUserID = editDeliveryBoy.LoginUserID;
                                        temp.FullName = editDeliveryBoy.FullName;
                                        temp.Email = editDeliveryBoy.Email;

                                        databaseModel.SaveChanges();
                                        return true;
                                    }
                                }
                            }
                        }
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

        //--------------------   Customer -------------------------------------
        #region
        /// <summary>
        /// Check if the Moibile is exist or not
        /// </summary>
        /// <param name="mobileNo"></param>
        /// <returns></returns>
        public static Customer IsMobileExists(string mobileNo)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Where(a => a.Mobile == mobileNo).FirstOrDefault();
                    return v;
                }

            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// Add a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool AddCustomer(CustomerRegistration newCustomer)
        {
            try
            {

                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    int? intIdt = databaseModel.Customers.Max(u => (int?)u.PKCustomerId);
                    if (intIdt == null)
                    {
                        intIdt = 1;
                    }
                    else
                        intIdt = intIdt + 1;

                    string suffix = intIdt.ToString();
                    UserLogin userInfo = new UserLogin();
                    string user_id = "C_" + suffix.PadLeft(5, '0');
                    string password = newCustomer.Mobile.Substring(Math.Max(0, newCustomer.Mobile.Length - 6));
                    userInfo.UserName = user_id;
                    userInfo.Password = password;
                    userInfo.UserType = 4;

                    AddUserLoginInfo(userInfo);
                    databaseModel.SaveChanges();

                    var user = IsUserExist(user_id);

                    Customer customerInfo = new Customer();
                    customerInfo.Address = newCustomer.Address;
                    customerInfo.SubAreaId = newCustomer.SubAreaId;

                    customerInfo.Mobile = newCustomer.Mobile;
                    customerInfo.Rate = newCustomer.Rate;
                    customerInfo.ServiceCharge = newCustomer.ServiceCharge;
                    customerInfo.TotalBill = newCustomer.PreviousBill;
                    customerInfo.TotalPaid = 0;
                    customerInfo.LoginUserID = user.PkUserID;
                    customerInfo.FullName = newCustomer.FullName;
                    customerInfo.IsDeleted = false;
                    customerInfo.SendSMS = newCustomer.SMS;

                    databaseModel.Customers.Add(customerInfo);
                    databaseModel.SaveChanges();
                    var SMSConfig = databaseModel.SMSconfig.FirstOrDefault();
                    if(SMSConfig!=null)
                    {
                        string body = "You have been registered as AgroExpress member. Your User ID is: "+ user_id + " and your Password is: "+password+".Please use this to log in to your account to view your history & place order online.\n Website: www.agroexpressbd.com \nThank you for being with AgroExpress.";
                       
                        SMSSend.SendOneToOneSingleSmsUsingAPI(SMSConfig.APIKey, customerInfo.Mobile,body,"TEXT",SMSConfig.Masking,"Registration");

                    }
                   
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdateCustomer(CustomerEdit editCustomer)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.Customers.Where(a => a.PKCustomerId == editCustomer.PKCustomerId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editCustomer.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editCustomer.UserName;
                            updatep.Password = editCustomer.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {

                                temp.Address = editCustomer.Address;
                                temp.SubAreaId = editCustomer.SubAreaId;

                                temp.Mobile = editCustomer.Mobile;
                                temp.Rate = editCustomer.Rate;
                                temp.ServiceCharge = editCustomer.ServiceCharge;
                                temp.TotalBill = editCustomer.TotalBill;
                                temp.TotalPaid = editCustomer.TotalPaid;
                                temp.LoginUserID = editCustomer.LoginUserID;
                                temp.FullName = editCustomer.FullName;
                                temp.SendSMS = editCustomer.SMS;
                                databaseModel.SaveChanges();
                                return true;
                            }
                        }
                    }
                    return false;
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
        /// <param name="id"></param>
        /// <returns></returns>
        public static Customer GetCustomerByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Where(a => a.PKCustomerId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetallEnabledCustomer()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Include("subarea").Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetallDisabledCustomer()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Include("subarea").Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableCustomer(int customerid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Customers.Where(a => a.PKCustomerId == customerid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableCustomer(int customerid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Customers.Where(a => a.PKCustomerId == customerid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetallEnabledCustomerBySubAreaId(int subareaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Where(a => a.IsDeleted == false && a.SubAreaId == subareaid).ToList();
                    return v;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Customer> GetallEnabledCustomerByAreaId(int subareaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Customers.Include("subarea").Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }

        public static List<CustomerTransactionDetails> GetCustomerTransectionHistory(DateTime startDate, DateTime EndDate, int ID)
        {
            try
            {
                List<CustomerTransactionDetails> li = new List<CustomerTransactionDetails>();
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var sale = databaseModel.Sales.Where(d => d.CustomerId == ID && d.DateTime >= startDate && d.DateTime <= EndDate).ToList();
                    var Billing = databaseModel.BillingHistory.Where(d => d.CustomerId == ID && d.DateTime >= startDate && d.DateTime <= EndDate).ToList();

                    foreach (var tra in sale)
                    {

                        var trDate = tra.DateTime;
                        var trAmount = tra.Amount;
                        var obj = li.FirstOrDefault(x => x.BuyingDate.Date == trDate);
                        //if (obj != null)
                        //{
                        //    if (String.IsNullOrEmpty(obj.ProductName))
                        //    {
                        //        obj.ProductName = tra.product.ProductName + "(" + tra.Amount + " " + tra.product.SellingUnit + ")";
                        //    }
                        //    else
                        //    {
                        //        obj.ProductName = obj.ProductName + "," + tra.product.ProductName + "(" + tra.Amount + " " + tra.product.SellingUnit + ")";
                        //    }
                        //}
                        //else
                        //{
                        li.Add(new CustomerTransactionDetails { BuyingDate = trDate, TotalPaid = 0.0, ProductName = tra.product.ProductName + "(" + tra.Amount + " " + tra.product.SellingUnit + ")" });
                        //}
                    }

                    foreach (var tra in Billing)
                    {
                        var trDate = tra.DateTime;
                        var trAmount = tra.BillPaid;
                        var obj = li.FirstOrDefault(x => x.BuyingDate == trDate);
                        if (obj != null)
                        {
                            obj.TotalPaid += tra.BillPaid;
                        }
                        else
                        {
                            li.Add(new CustomerTransactionDetails { BuyingDate = trDate, ProductName = null, TotalPaid = trAmount });
                        }

                    }

                    return li;
                }
            }
            catch (Exception)
            {
                return null;
            }
        }
        #endregion

        //--------------------   Partner -------------------------------------
        #region


        /// <summary>
        /// 
        /// </summary>
        /// <param name="partner"></param>
        /// <returns></returns>
        public static bool AddPartner(PartnerRegistration partner)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var NewUser = new UserLogin();
                    NewUser.UserName = partner.UserName;
                    NewUser.Password = partner.Password;
                    NewUser.UserType = 6;
                    AgroExpressDBAccess.AddUserLoginInfo(NewUser);

                    var Auser = AgroExpressDBAccess.IsUserExist(partner.UserName);

                    if (Auser != null)
                    {
                        var admint = new Partner();


                        admint.Mobile = partner.Mobile;
                        admint.Email = partner.Email;

                        admint.FullName = partner.FullName;
                        admint.Address = partner.Address;
                        admint.Mobile = partner.Mobile;
                        admint.Email = partner.Email;
                        admint.LoginUserID = Auser.PkUserID;
                        admint.IsDeleted = false;

                        databaseModel.Partners.Add(admint);
                        databaseModel.SaveChanges();
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Partner> GetallPartner()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Partners.ToList();
                    return v;
                }
            }
            catch (Exception ec)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Partner> GetallEnabledPartner()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Partners.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Partner> GetallDisabledPartner()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Partners.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisablePartner(int partnerid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Partners.Where(a => a.PKPartnerId == partnerid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnablePartner(int partnerid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Partners.Where(a => a.PKPartnerId == partnerid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static Partner GetPartnerByID(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Partners.Where(a => a.PKPartnerId == id).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// Update a new Customer
        /// </summary>
        /// <returns></returns>
        public static bool UpdatePartner(PartnerEdit editadmin)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var temp = databaseModel.Partners.Where(a => a.PKPartnerId == editadmin.PKPartnerId).FirstOrDefault();
                    if (temp != null)
                    {
                        var userlogin = AgroExpressDBAccess.GetUserByID(editadmin.LoginUserID);
                        if (userlogin != null)
                        {
                            UpdatePassword updatep = new UpdatePassword();
                            updatep.UserName = editadmin.UserName;
                            updatep.Password = editadmin.Password;
                            updatep.UserType = userlogin.UserType;

                            if (AgroExpressDBAccess.UpdatePassword(updatep))
                            {
                                temp.Address = editadmin.Address;
                                temp.Mobile = editadmin.Mobile;
                                temp.LoginUserID = editadmin.LoginUserID;
                                temp.FullName = editadmin.FullName;

                                databaseModel.SaveChanges();
                                return true;
                            }
                        }

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
        //--------------------   SalePoint -------------------------------------
        #region

        /// <summary>
        /// 
        /// </summary>
        /// <param name="SalepointName"></param>
        /// <returns></returns>

        public static SalePoint IsSalePointExist(string salepointname)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(a => a.SalePointName == salepointname).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool AddSalePoint(SalePoint salepoint)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    databaseModel.SalePoints.Add(salepoint);
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
        /// <returns></returns>
        public static List<SalePoint> GetallEnabledSalePoint()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SalePoint> GetallDisabledSalePoint()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {

                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static SalePoint GetSalePointByName(string salepointname)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(a => a.SalePointName == salepointname).FirstOrDefault();

                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static SalePoint GetSalePointById(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(a => a.PKSalePointID == id).FirstOrDefault();

                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableSalePoint(int salepointid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SalePoints.Where(a => a.PKSalePointID == salepointid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableSalePoint(int salepointid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SalePoints.Where(a => a.PKSalePointID == salepointid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool UpdateSalepoint(EditSalePoint salepoint)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePoints.Where(m => m.PKSalePointID == salepoint.PKSalePointID).FirstOrDefault();
                    if (v != null)
                    {
                        v.SalePointName = salepoint.SalePointName;
                        v.SalePointAddress = salepoint.SalePointAddress;
                        databaseModel.SaveChanges();
                    }

                    return true;

                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<SalePoint> GetSalePointListForUSer(string UserName)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    List<SalePoint> AreaIDList = new List<SalePoint>();
                    var UserInfo = context.UserLoginInfo.Where(a => a.UserName == UserName).FirstOrDefault();
                    if (UserInfo != null)
                    {
                        if (UserInfo.UserType == 2 || UserInfo.UserType == 3 || UserInfo.UserType == 5)
                        {
                            //Manager or firmmnager or deliveryboy
                            AreaIDList = context.SalePointRelations.Include("salepoint").Where(a => a.UserLoginId == UserInfo.PkUserID && a.salepoint.IsDeleted == false).Select(b => b.salepoint).ToList();
                        }
                        else
                        {
                            AreaIDList = context.SalePoints.Where(a => a.IsDeleted == false).ToList();
                        }
                    }


                    return AreaIDList;
                }
            }
            catch
            {
                return null;
            }
        }
        public static List<Sale> GetSaleInfoByDate(DateTime datemin, DateTime datemax)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.Sales.Include("customer").Include("product").Where(a => a.DateTime >= datemin && a.DateTime <= datemax).ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        //--------------------   SalePoint Relation -------------------------------------
        #region



        /// <summary>
        /// 
        /// </summary>
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool AddSalePointRelation(SalePointRelation salepointr)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    databaseModel.SalePointRelations.Add(salepointr);
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
        /// <param name="manager"></param>
        /// <returns></returns>
        public static bool IsSalePointRelationExists(SalePointRelation salepointr)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SalePointRelations.Where(a => a.SalePointId == salepointr.SalePointId && a.UserLoginId == salepointr.UserLoginId).FirstOrDefault();
                    if (v != null)
                        return true;
                    return false;
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
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DeleteSalePointRelationById(int id)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SalePointRelations.Where(a => a.UserLoginId == id).ToList();
                    if (v != null)
                    {
                        foreach (var de in v)
                        {
                            dataModel.SalePointRelations.Remove(de);
                            dataModel.SaveChanges();
                        }

                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public static List<SalePoint> GetSalePointListForUSerId(int UserId)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    List<SalePoint> AreaIDList = new List<SalePoint>();
                    var UserInfo = context.UserLoginInfo.Where(a => a.PkUserID == UserId).FirstOrDefault();
                    if (UserInfo != null)
                    {
                        if (UserInfo.UserType == 2 || UserInfo.UserType == 3 || UserInfo.UserType == 5)
                        {
                            //Manager or firmmnager or deliveryboy
                            AreaIDList = context.SalePointRelations.Include("salepoint").Where(a => a.UserLoginId == UserInfo.PkUserID && a.salepoint.IsDeleted == false).Select(b => b.salepoint).ToList();
                        }
                        else
                        {
                            AreaIDList = context.SalePoints.Where(a => a.IsDeleted == false).ToList();
                        }
                    }


                    return AreaIDList;
                }
            }
            catch
            {
                return null;
            }
        }


        #endregion
        //-----------------------------AREA------------------------------
        #region

        /// <summary>
        /// CHECK Area already exists or not
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static bool isAreaExistById(int areaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.PKAreaId == areaid).FirstOrDefault();

                    return v != null;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        /// <summary>
        /// CHECK Area already exists or not
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static bool isAreaExist(string areaname)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.AreaName == areaname).FirstOrDefault();

                    return v != null;
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
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddArea(Area area)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    databaseModel.Areas.Add(area);
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
        /// <returns></returns>
        public static List<Area> GetallEnabledArea()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Include("salepoint").Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<Area> GetallDisabledArea()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableArea(int areaid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Areas.Where(a => a.PKAreaId == areaid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableArea(int areaid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Areas.Where(a => a.PKAreaId == areaid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static List<Area> GetAreaBySalePointID(int salepointid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.Areas.Where(a => a.SalePointId == salepointid).ToList();
                    return v;
                }
            }
            catch (Exception)
            { return null; }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static Area GetAreaByName(string areaname)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.AreaName == areaname).FirstOrDefault();

                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static int GetSalePointByAreaID(int areaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.PKAreaId == areaid).FirstOrDefault();

                    return v.SalePointId;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }

        /// <summary>
        /// CHECK Area already exists or not
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static Area GetAreaInformationById(int areaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(a => a.PKAreaId == areaid).FirstOrDefault();

                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool UpdateArea(EditArea area)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.Areas.Where(m => m.PKAreaId == area.PKAreaId).FirstOrDefault();
                    if (v != null)
                    {
                        v.AreaName = area.AreaName;
                        v.SalePointId = area.SalePointId;
                        databaseModel.SaveChanges();
                    }

                    return true;

                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        //-----------------------------SUB AREA------------------------------
        #region


        /// <summary>
        /// CHECK Area already exists or not
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static bool isSubAreaExist(string sareaname)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Where(a => a.SubAreaName == sareaname).FirstOrDefault();

                    return v != null;
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
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool AddSubArea(SubArea sarea)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    databaseModel.SubAreas.Add(sarea);
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
        /// <returns></returns>
        public static List<SubArea> GetallEnabledSubArea()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Include("area").Where(a => a.IsDeleted == false).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static List<SubArea> GetallDisabledSubArea()
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Where(a => a.IsDeleted == true).ToList();
                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaID"></param>
        /// <returns></returns>
        public static List<SubArea> GetSubAreaByAreaID(int areaid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SubAreas.Where(a => a.AreaId == areaid).ToList();
                    return v;
                }
            }
            catch (Exception)
            { return null; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static int GetAreaBySubAreaID(int subareaid)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Where(a => a.PKSubAreaId == subareaid).FirstOrDefault();

                    return v.AreaId;
                }
            }
            catch (Exception ex)
            {
                return 0;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaname"></param>
        /// <returns></returns>
        public static SubArea GetSubAreaById(int id)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Where(a => a.PKSubAreaId == id).FirstOrDefault();

                    return v;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool DisableSubArea(int subareaid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SubAreas.Where(a => a.PKSubAreaId == subareaid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = true;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public static bool EnableSubArea(int subareaid)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.SubAreas.Where(a => a.PKSubAreaId == subareaid).FirstOrDefault();
                    if (v != null)
                    {
                        v.IsDeleted = false;
                        dataModel.SaveChanges();
                        return true;
                    }

                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public static bool UpdateSubArea(EditSubArea subarea)
        {
            try
            {
                using (AgroExpressContext databaseModel = new AgroExpressContext())
                {
                    var v = databaseModel.SubAreas.Where(m => m.PKSubAreaId == subarea.PKSubAreaId).FirstOrDefault();
                    if (v != null)
                    {
                        v.SubAreaName = subarea.SubAreaName;
                        v.AreaId = subarea.AreaId;
                        databaseModel.SaveChanges();
                    }

                    return true;

                }

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        //------------------Product-----------------
        #region
        public static Product IsProductExist(string Name)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var v = context.Product.Where(a => a.ProductName == Name).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static Product GetProductByID(int ID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var v = context.Product.Where(a => a.PKProductId == ID).FirstOrDefault();
                    return v;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool AddProduct(Product product)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.Product.Add(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Product> GetAllEnabledProduct()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.Product.Where(a => a.IsDeleted == false).ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static List<Product> SearchProduct(int? ProductID, double? MinimumStock, double? MaximumStock)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = new List<Product>();
                    if (ProductID != null)
                    {
                        List = context.Product.Where(a => a.IsDeleted == false && a.PKProductId == ProductID).ToList();

                    }
                    else if (MinimumStock != null && MaximumStock != null)
                    {
                        List = context.Product.Where(a => a.IsDeleted == false && a.Stock >= MinimumStock && a.Stock <= MaximumStock).ToList();
                    }
                    else if (MaximumStock != null)
                    {
                        List = context.Product.Where(a => a.IsDeleted == false && a.Stock <= MaximumStock).ToList();
                    }
                    else if (MinimumStock != null)
                    {
                        List = context.Product.Where(a => a.IsDeleted == false && a.Stock >= MinimumStock).ToList();
                    }
                    else
                    {
                        List = context.Product.Where(a => a.IsDeleted == false).ToList();
                    }
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static List<Product> GetAllDisabledProduct()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.Product.Where(a => a.IsDeleted == true).ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool UpdateProduct(Product product)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.Product.AddOrUpdate(product);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool DeleteProductByID(int ID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var v = context.Product.Where(a => a.PKProductId == ID).FirstOrDefault();
                    if (v != null)
                        v.IsDeleted = true;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool EnableProductByID(int ID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var v = context.Product.Where(a => a.PKProductId == ID).FirstOrDefault();
                    if (v != null)
                        v.IsDeleted = false;
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<Production> ProductionInfoByDate(DateTime DateMin, DateTime DateMax)
        {
            try
            {
                using (AgroExpressContext contex = new AgroExpressContext())
                {
                    return contex.Productions.Include("product").Where(a => a.Date >= DateMin && a.Date <= DateMax).ToList();

                }
            }
            catch
            {
                return null;
            }
        }

        public static double GetProductStockAmount(int ProductID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var amount = context.Product.Where(a => a.PKProductId == ProductID).FirstOrDefault().Stock;
                    return amount;
                }
            }
            catch
            {
                return 0;
            }
        }

        public static bool AddProductSaleList(List<Sale> SaleRequest, int customerID, int SalpointID, double totalBill, double totalPaid, string OperatorName)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var salehistory = SaleRequest;
                    string SMSBody = "Dear ";
                    context.Sales.AddRange(SaleRequest);
                    context.SaveChanges();
                    var customer = context.Customers.Where(a => a.PKCustomerId == customerID).FirstOrDefault();
                    SMSBody += customer.FullName+"\n";
                    bool firstTime = true;
                    int count = 1;
                    foreach (var sale in SaleRequest)
                    {
                       
                        int productid = sale.ProductId;
                        var product = context.Product.Where(a => a.PKProductId == sale.ProductId).FirstOrDefault();
                        double amount = sale.Amount;
                        var salepointstoock = context.SalePointProductStocks.Where(a => a.ProductId == productid && a.SalePointId == SalpointID).FirstOrDefault();
                        if (salepointstoock != null)
                        {
                            salepointstoock.ProductStock -= amount;
                            context.SaveChanges();
                        }
                        if (firstTime)
                        {
                            SMSBody += "Your order has been delivered.\n";
                            SMSBody += "Product "+count+": "+product.ProductName+" ";
                            SMSBody += "Quantity: " + sale.Amount;
                            firstTime = false;
                            count++;
                        }
                        else
                        {
                            SMSBody += ", Product " + count + ": " + product.ProductName + " ";
                            SMSBody += "Quantity: " + sale.Amount;
                            count++;
                        }

                    }
                   if(!firstTime)
                    {
                        SMSBody += "\nTotal Price: " + totalBill;
                    }
                    if (customer != null)
                    {
                        customer.TotalBill += totalBill;
                        customer.TotalPaid += totalPaid;

                        context.SaveChanges();

                        context.BillingHistory.Add(new BillingHistory { DateTime = System.DateTime.Now, CustomerId = customerID, BillPaid = totalPaid, OperatorName = OperatorName });

                        context.SaveChanges();
                        SMSBody += "\nAmount Paid Today: " + totalPaid;
                        SMSBody += "\nTotal Due: " + (customer.TotalBill- customer.TotalPaid);
                    }
                    SMSBody += "\nThank you for being with AgroExpress.";
                    if (customer.SendSMS)
                    {
                        
                        var SMSConfig = context.SMSconfig.FirstOrDefault();
                        if(string.IsNullOrEmpty( SMSConfig.Masking))
                        {
                            SMSConfig.Masking = "";
                        }
                        if (SMSConfig != null)
                        {
                            string body = SMSBody;
                            int l = body.Length;
                            if(SMSSend.SendOneToOneSingleSmsUsingAPI(SMSConfig.APIKey, customer.Mobile, body, "TEXT", SMSConfig.Masking, "Registration"))
                            {
                                foreach(var saleid in salehistory)
                                {
                                    var sale = context.Sales.Where(a => a.CustomerId == saleid.CustomerId && a.DateTime.Year == saleid.DateTime.Year && a.DateTime.Month == saleid.DateTime.Month && a.DateTime.Day == saleid.DateTime.Day && a.DateTime.Hour == saleid.DateTime.Hour && a.DateTime.Minute == saleid.DateTime.Minute && a.DateTime.Second == saleid.DateTime.Second && a.ProductId == saleid.ProductId).FirstOrDefault();
                                    sale.SMSSent = true;
                                    context.SaveChanges();
                                }
                            }

                        }
                    }
                   
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool AddProductSale(Sale SaleRequest, int customerID, int SalpointID, double totalBill, double totalPaid, string OperatorName)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var salehistory = SaleRequest;
                    string SMSBody = "Dear ";
                    context.Sales.Add(SaleRequest);
                    context.SaveChanges();
                    var customer = context.Customers.Where(a => a.PKCustomerId == customerID).FirstOrDefault();
                    SMSBody += customer.FullName + "\n";
                    bool firstTime = true;
                    int count = 1;
                   

                        int productid = SaleRequest.ProductId;
                        var product = context.Product.Where(a => a.PKProductId == SaleRequest.ProductId).FirstOrDefault();
                        double amount = SaleRequest.Amount;
                        var salepointstoock = context.SalePointProductStocks.Where(a => a.ProductId == productid && a.SalePointId == SalpointID).FirstOrDefault();
                        if (salepointstoock != null)
                        {
                            salepointstoock.ProductStock -= amount;
                            context.SaveChanges();
                        }
                        if (firstTime)
                        {
                            SMSBody += "Your order has been delivered.\n";
                            SMSBody += "Product " + count + ": " + product.ProductName + " ";
                            SMSBody += "Quantity: " + SaleRequest.Amount;
                            firstTime = false;
                            count++;
                        }
                        else
                        {
                            SMSBody += ", Product " + count + ": " + product.ProductName + " ";
                            SMSBody += "Quantity: " + SaleRequest.Amount;
                            count++;
                        }

                
                    if (!firstTime)
                    {
                        SMSBody += "\nTotal Price: " + totalBill;
                    }
                    if (customer != null)
                    {
                        customer.TotalBill += totalBill;
                        customer.TotalPaid += totalPaid;

                        context.SaveChanges();

                        context.BillingHistory.Add(new BillingHistory { DateTime = System.DateTime.Now, CustomerId = customerID, BillPaid = totalPaid, OperatorName = OperatorName });

                        context.SaveChanges();
                        SMSBody += "\nAmount Paid Today: " + totalPaid;
                        SMSBody += "\nTotal Due: " + (customer.TotalBill - customer.TotalPaid);
                    }
                    SMSBody += "\nThank you for being with AgroExpress.";
                    if (customer.SendSMS)
                    {

                        var SMSConfig = context.SMSconfig.FirstOrDefault();
                        if (string.IsNullOrEmpty(SMSConfig.Masking))
                        {
                            SMSConfig.Masking = "";
                        }
                        if (SMSConfig != null)
                        {
                            string body = SMSBody;
                            int l = body.Length;
                            if (SMSSend.SendOneToOneSingleSmsUsingAPI(SMSConfig.APIKey, customer.Mobile, body, "TEXT", SMSConfig.Masking, "Registration"))
                            {
                                
                                    var sale = context.Sales.Where(a => a.CustomerId == salehistory.CustomerId && a.DateTime.Year == salehistory.DateTime.Year && a.DateTime.Month == salehistory.DateTime.Month && a.DateTime.Day == salehistory.DateTime.Day && a.DateTime.Hour == salehistory.DateTime.Hour && a.DateTime.Minute == salehistory.DateTime.Minute && a.DateTime.Second == salehistory.DateTime.Second && a.ProductId == salehistory.ProductId).FirstOrDefault();
                                    sale.SMSSent = true;
                                    context.SaveChanges();
                               
                            }

                        }
                    }

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string AddSalePointProduct(List<SalePointProductConsume> Request)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.SalePointProductConsumes.AddRange(Request);
                    context.SaveChanges();
                    int len = Request.Count;
                    for (int i = 0; i < len; i++)
                    {
                        int Pid = Request[i].ProductId;
                        Double Stock = Request[i].Amount;
                        int Sid = Request[i].SalePointId;
                        var product = context.Product.Where(a => a.PKProductId == Pid).FirstOrDefault();
                        product.Stock -= Stock;
                        var salePoint = context.SalePointProductStocks.Where(a => a.ProductId == Pid && a.SalePointId == Sid).FirstOrDefault();
                        if (salePoint == null)
                        {
                            context.SalePointProductStocks.Add(new SalePointProductStock { ProductId = Pid, SalePointId = Sid, ProductStock = Stock });
                            context.SaveChanges();

                        }
                        else
                        {
                            salePoint.ProductStock += Stock;
                            context.SaveChanges();
                        }

                    }
                    return "yes";
                }
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static double SalePointProductStock(int productId, int selpointID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var amount = context.SalePointProductStocks.Where(a => a.ProductId == productId && a.SalePointId == selpointID).FirstOrDefault().ProductStock;
                    return amount;
                }
            }
            catch
            {
                return 0;
            }
        }


        public static List<SalePointProductStock> GetSalePointProductStock()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var salepoint = context.SalePointProductStocks.Include("product").Include("salepoint").ToList();
                    return salepoint;
                }
            }
            catch
            {
                return null;
            }
        }

        public static bool sellPointProductTranferDellete(int id)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var SalePointproduction = context.SalePointProductConsumes.Where(a => a.PKSalePointProductConsumeId == id).FirstOrDefault();
                    if (SalePointproduction != null)
                    {
                        int productId = SalePointproduction.ProductId;
                        int salePointID = SalePointproduction.SalePointId;

                        var product = context.SalePointProductStocks.Where(a => a.ProductId == productId && a.SalePointId == salePointID).FirstOrDefault();
                        product.ProductStock -= SalePointproduction.Amount;

                        var ProductStock = context.Product.Where(a => a.PKProductId == productId).FirstOrDefault();
                        ProductStock.Stock += SalePointproduction.Amount;
                        context.SalePointProductConsumes.Remove(SalePointproduction);
                    }
                    context.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static List<SalePointProductConsume> GetSellPointProductAdd(DateTime min, DateTime max)
        {
            try
            {
                using (AgroExpressContext contex = new AgroExpressContext())
                {
                    return contex.SalePointProductConsumes.Include("product").Include("salepoint").Where(a => a.Date >= min && a.Date <= max).ToList();
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        //---------------Production------------
        #region
        public static string AddPoductionList(List<Production> ProductionList)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.Productions.AddRange(ProductionList);
                    context.SaveChanges();

                    foreach (var production in ProductionList)
                    {
                        
                        var pro = context.Product.Where(a => a.PKProductId == production.ProductId).FirstOrDefault();
                        if (pro != null)
                        {
                            pro.Stock += production.Amount;
                            context.SaveChanges();
                        }
                    }
                }
                return "yes";
            }
            catch (Exception e)
            {
                return e.Message;
            }
        }

        public static bool ProductionDelete(int id)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var production = context.Productions.Where(a => a.PKProductionId == id).FirstOrDefault();
                    if (production != null)
                    {
                        int productId = production.ProductId;
                        var product = context.Product.Where(a => a.PKProductId == productId).FirstOrDefault();
                        product.Stock -= production.Amount;
                        context.Productions.Remove(production);
                    }
                    context.SaveChanges();

                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static List<Sale> GetSaleHistoryByDate(DateTime date, DateTime date1)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var sale = context.Sales.Where(a => a.DateTime >= date && a.DateTime <= date1 && a.product.ProductName.ToLower() == "milk").ToList();
                    return sale;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        public static List<Sale> GetSaleHistoryByDate(DateTime date)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var sale = context.Sales.Where(a => a.DateTime == date && a.product.ProductName.ToLower() == "milk").ToList();
                    return sale;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        //-------------------- Milk Summary -------------------------------------
        #region
        public static bool AddOrUpdateTotalMilkSummary(DateTime date, double totalmilk)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.SingleOrDefault(a => a.Date == date);

                    if (v != null)
                    {
                        v.TotalProduction = totalmilk;
                        dataModel.SaveChanges();
                    }
                    else
                    {
                        MilkSummary tmp = new MilkSummary();
                        tmp.Date = date;
                        tmp.TotalProduction = totalmilk;
                        tmp.Stuff = 0;
                        tmp.Wastage = 0;
                        tmp.Factory = 0;
                        tmp.CulfMorning = 0;
                        tmp.CulfAfternoon = 0;

                        dataModel.MilkSummary.Add(tmp);
                        dataModel.SaveChanges();

                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool AddMilkSummary(MilkSummaryAdd newentry)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.SingleOrDefault(a => a.Date == newentry.Date);

                    if (v != null)
                    {
                        v.CulfMorning = newentry.CulfMorning;
                        v.CulfAfternoon = newentry.CulfAfternoon;
                        v.Stuff = newentry.Stuff;
                        
                        v.Wastage = newentry.Wastage;

                        dataModel.SaveChanges();
                    }
                    else
                    {
                        MilkSummary tmp = new MilkSummary();
                        tmp.Date = newentry.Date;
                        tmp.TotalProduction = 0;
                        tmp.Stuff = newentry.Stuff;
                        tmp.Wastage = newentry.Wastage;
                        tmp.Factory = 0.0;
                        tmp.CulfMorning = newentry.CulfMorning;
                        tmp.CulfAfternoon = newentry.CulfAfternoon;

                        dataModel.MilkSummary.Add(tmp);
                        dataModel.SaveChanges();

                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static bool EditMilkSummary(MilkSummeryEdit newentry)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.SingleOrDefault(a => a.Date == newentry.Date);

                    if (v != null)
                    {
                        v.CulfMorning = newentry.CulfMorning;
                        v.CulfAfternoon = newentry.CulfAfternoon;
                        v.Stuff = newentry.Stuff;
                        v.Factory = newentry.Factory;
                        v.Wastage = newentry.Wastage;

                        dataModel.SaveChanges();
                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public static bool AddFactoryMilkConsumption(DateTime date,double amount)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.SingleOrDefault(a => a.Date == date.Date);

                    if (v != null)
                    {
                        v.Factory += amount;

                        dataModel.SaveChanges();
                    }
                    else
                    {
                        MilkSummary tmp = new MilkSummary();
                        tmp.Date = date.Date;
                        tmp.TotalProduction = 0;
                        tmp.Stuff = 0;
                        tmp.Wastage = 0;
                        tmp.Factory = amount;
                        tmp.CulfMorning = 0;
                        tmp.CulfAfternoon = 0;

                        dataModel.MilkSummary.Add(tmp);
                        dataModel.SaveChanges();

                    }
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public static MilkSummary GetMilkSummaryByDate(DateTime date)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.Where(a => a.Date == date ).SingleOrDefault();
                    if (v != null)
                    {
                        return v;
                    }
                    return null;
                }
            }

            catch (Exception e)
            {
                return null;
            }
        }
        public static List<MilkSummary> GetMilkSummaryByDate(DateTime date, DateTime date1)
        { 
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    var v = dataModel.MilkSummary.Where(a => a.Date >= date && a.Date<=date1).ToList();
                    if(v!=null)
                    {
                        return v;
                    }
                    return null;
                }
            }

            catch (Exception e)
            {
                return null;
            }
        }

        public static List<MilkSummary> GetAllMilkSummary()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.MilkSummary.ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }
        #endregion

        //--------------------Order -------------------------------------
        #region
        public static bool AddOrder(List<Order> orders)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    foreach (var item in orders)
                    {
                        dataModel.Order.Add(item);
                    }

                    dataModel.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public static bool AddSingleOrder(AddOrder newentry)
        {
            try
            {
                using (AgroExpressContext dataModel = new AgroExpressContext())
                {
                    Order neworder = new Order();

                    neworder.CustomerId = 1;
                    neworder.ProductId = newentry.PKProductId;
                    neworder.OrderPlacingDateTime = newentry.OrderPlacingDateTime;
                    neworder.OrderDateTime = System.DateTime.Now.Date;
                    neworder.Amount = newentry.Amount;

                    dataModel.Order.Add(neworder);
                    dataModel.SaveChanges();

                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Order> GetAllOrder()
        {
            DateTime date = System.DateTime.Now.Date;
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.Order.Include("customer").Include("product").Where(a => a.OrderDateTime == date).ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static List<Order> GetAllNotDeliveredOrder()
        {
            DateTime date = System.DateTime.Now.Date;
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var List = context.Order.Include("customer").Include("product").Where(a => a.OrderDateTime == date && a.IsDelivered == false).ToList();
                    return List;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static void UpdateOrder(int id)
        {
            DateTime date = System.DateTime.Now.Date;
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var order = context.Order.Where(a => a.PKOrderId == id).SingleOrDefault();

                    if (order != null)
                    {
                        order.IsDelivered = true;
                        context.SaveChanges();
                    }
                }
            }
            catch (Exception e)
            {

            }
        }
        #endregion

        //-------------------SMS------------------------------

        #region
        public static string GetSMSBalance()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var smsconfig = context.SMSconfig.FirstOrDefault();
                    if (smsconfig != null)
                    {
                        string sms = AgroExpress.Help.SMSSend.GetBalanceByAPIKey(smsconfig.APIKey);
                        return sms;
                    }

                    else
                    {
                        return "N/A";
                    }


                }
            }
            catch (Exception e)
            {
                return "N/A";
            }
        }
        public static SMSConfiguration GetSMSPrivacyInformation()
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var sms = context.SMSconfig.FirstOrDefault();
                    return sms;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static bool UpdateSMSConfiguration(SMSConfiguration sms)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.SMSconfig.AddOrUpdate(sms);
                    context.SaveChanges();
                    return true;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }
        #endregion

        //-----------------Notification------------------
        #region
        public static bool AddNotificationList(List<Notification> NotiInfo)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    context.Notifications.AddRange(NotiInfo);
                    context.SaveChanges();
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public static List<Notification> GetNotificationByDate(DateTime date)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var notification = context.Notifications.Where(a => a.Date == date.Date).ToList();
                    return notification;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public static bool DeleteNotificationByID(int ID)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    string GroupID = context.Notifications.Where(a => a.PKNotificationId == ID).FirstOrDefault().GroupID;
                    var noti = context.Notifications.Where(a => a.GroupID == GroupID).ToList();
                    if (noti != null)
                        context.Notifications.RemoveRange(noti);
                    context.SaveChanges();
                    return true;
                }
            }
            catch
            {
                return false;
            }

        }

        #endregion

        //-----------------Billing History------------------
        #region
        public static List<BillingHistory> GetBillingInfoByDate(DateTime min, DateTime max)
        {
            try
            {
                using (AgroExpressContext context = new AgroExpressContext())
                {
                    var allbills = context.BillingHistory.Include("customer").Where(a => a.DateTime >= min && a.DateTime <= max).ToList();
                    return allbills;
                }
            }
            catch (Exception ex)
            {
                return null;
            }

        }
        #endregion

    }
}