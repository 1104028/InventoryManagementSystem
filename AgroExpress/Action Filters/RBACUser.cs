using AgroExpress.DataLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AgroExpress.DataLayer.Models;
using AgroExpress.Help;
public class RBACUser
{
    public int User_Id { get; set; }
    public int UserType { get; set; }
    public string Username { get; set; }
    private List<string> Permission = new List<string>();

    public RBACUser(string _username)
    {
        this.Username = _username;

        GetUserPermissionList();

    }
    private void GetUserPermissionList()
    {

        UserLogin _user = AgroExpressDBAccess.IsUserExist(this.Username);
        if (_user != null)
        {
            this.UserType = _user.UserType;
            PermissionAssign UserPermission = new PermissionAssign();
            if (this.UserType == 1)
                this.Permission = UserPermission.Admin;
            else if (this.UserType == 2)
                this.Permission = UserPermission.Manager;
            else if (this.UserType == 3)
                this.Permission = UserPermission.DeliveryBoy;
            else if (this.UserType == 4)
                this.Permission = UserPermission.Customer;
            else if (this.UserType == 5)
                this.Permission = UserPermission.FirmManager;
            else if (this.UserType == 6)
                this.Permission = UserPermission.Partner;
        }

    }

    //private void GetDatabaseUserRolesPermissions()
    //{
    //    using (LocationManagementModel _data = new LocationManagementModel())
    //    {
    //        USER _user = _data.USERS.Where(u => u.UserName == this.Username).FirstOrDefault();
    //        if (_user != null)
    //        {
    //            this.User_Id = _user.User_Id;
    //            foreach (ROLE _role in _user.ROLES)
    //            {
    //                UserRole _userRole = new UserRole { Role_Id = _role.Role_Id, RoleName = _role.RoleName };
    //                foreach (PERMISSION _permission in _role.PERMISSIONS)
    //                {
    //                    _userRole.Permissions.Add(new RolePermission { Permission_Id = _permission.Permission_Id, PermissionDescription = _permission.PermissionDescription });
    //                }
    //                this.Roles.Add(_userRole);

    //                if (!this.IsSysAdmin)
    //                    this.IsSysAdmin = _role.IsSysAdmin;
    //            }
    //        }
    //    }
    //}

    public bool HasPermission(string requiredPermission)
    {
        for(int i=0;i<Permission.Count;i++)
        {
            if (Permission[i] == requiredPermission)
                return true;
        }
        return false;
       
    }

    //public bool HasRole(string role)
    //{
    //    return (Roles.Where(p => p.RoleName == role).ToList().Count > 0);
    //}

    //public bool HasRoles(string roles)
    //{
    //    bool bFound = false;
    //    string[] _roles = roles.ToLower().Split(';');
    //    foreach (UserRole role in this.Roles)
    //    {
    //        try
    //        {
    //            bFound = _roles.Contains(role.RoleName.ToLower());
    //            if (bFound)
    //                return bFound;
    //        }
    //        catch (Exception)
    //        {
    //        }
    //    }
    //    return bFound;
    //}
}

//public class UserRole
//{
//    public int Role_Id { get; set; }
//    public string RoleName { get; set; }
//    public List<RolePermission> Permissions = new List<RolePermission>();
//}

//public class RolePermission
//{
//    public int Permission_Id { get; set; }
//    public string PermissionDescription { get; set; }
//}