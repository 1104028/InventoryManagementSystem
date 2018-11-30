using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;

//Get requesting user's roles/permissions from database tables...      
public static class RBAC_ExtendedMethods
{
    //public static bool HasRole(this ControllerBase controller, string role)
    //{
    //    bool bFound = false;
    //    try
    //    {
    //        //Check if the requesting user has the specified role...
    //        bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRole(role);
    //    }
    //    catch { }
    //    return bFound;
    //}

    //public static bool HasRoles(this ControllerBase controller, string roles)
    //{
    //    bool bFound = false;
    //    try
    //    {
    //        //Check if the requesting user has any of the specified roles...
    //        //Make sure you separate the roles using ; (ie "Sales Manager;Sales Operator"
    //        bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasRoles(roles);
    //    }
    //    catch { }
    //    return bFound;
    //}

    public static bool HasPermission(this ControllerBase controller, string permission)
    {
        bool bFound = false;
        try
        {
            //Check if the requesting user has the specified application permission...
            bFound = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).HasPermission(permission);
        }
        catch { }
        return bFound;
    }

    public static bool IsSysAdmin(this ControllerBase controller)
    {
        bool bIsSysAdmin = false;
        try
        {
            //Check if the requesting user has the System Administrator privilege...
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 1)
                return bIsSysAdmin = true;
        }
        catch { }
        return bIsSysAdmin;
    }
    public static bool IsManager(this ControllerBase controller)
    {
        bool bIspartner = false;
        try
        {
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 2)
                return bIspartner = true;
        }
        catch { }
        return bIspartner;
    }
    public static bool IsDeliveryBoy(this ControllerBase controller)
    {
        bool bIspartner = false;
        try
        {
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 3)
                return bIspartner = true;
        }
        catch { }
        return bIspartner;
    }
    public static bool IsCustomer(this ControllerBase controller)
    {
        bool bIspartner = false;
        try
        {
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 4)
                return bIspartner = true;
        }
        catch { }
        return bIspartner;
    }
    public static bool IsFirmManager(this ControllerBase controller)
    {
        bool bIspartner = false;
        try
        {
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 5)
                return bIspartner = true;
        }
        catch { }
        return bIspartner;
    }
    public static bool IsPartner(this ControllerBase controller)
    {
        bool bIspartner = false;
        try
        {
            int typeID = new RBACUser(controller.ControllerContext.HttpContext.User.Identity.Name).UserType;
            if (typeID == 6)
                return bIspartner = true;
        }
        catch { }
        return bIspartner;
    }
    public static string SMSBalance(this ControllerBase controller)
    {
        return AgroExpress.DataLayer.AgroExpressDBAccess.GetSMSBalance();
    }

}
