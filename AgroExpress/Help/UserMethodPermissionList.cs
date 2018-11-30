using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.Help
{
    public class UserMethodPermissionList
    {
        public List<string> User = new List<string>();
        public List<string> Admin = new List<string>();
        
        public UserMethodPermissionList()
        {
            var _controllerTypes = AppDomain.CurrentDomain.GetAssemblies()
               .SelectMany(a => a.GetTypes())
               .Where(t => t != null
                   && t.IsPublic
                   && t.Name.EndsWith("Controller", StringComparison.OrdinalIgnoreCase)
                   && !t.IsAbstract
                   && typeof(IController).IsAssignableFrom(t));

            var _controllerMethods = _controllerTypes.ToDictionary(controllerType => controllerType,
                    controllerType => controllerType.GetMethods(BindingFlags.Public | BindingFlags.Instance)
                    .Where(m => typeof(ActionResult).IsAssignableFrom(m.ReturnType)));

            foreach (var _controller in _controllerMethods)
            {
                string _controllerName = _controller.Key.Name;
                foreach (var _controllerAction in _controller.Value)
                {
                    string _controllerActionName = _controllerAction.Name;
                    if (_controllerName.EndsWith("Controller"))
                    {
                        _controllerName = _controllerName.Substring(0, _controllerName.LastIndexOf("Controller"));
                    }

                    string _permissionDescription = string.Format("{0}-{1}", _controllerName, _controllerActionName);
                    Admin.Add(_permissionDescription);
                }
            }

            User.Add("Camera-Index");
            User.Add("Camera-Details");
            User.Add("Home-Index");
            User.Add("Location-Index");
            User.Add("Location-Details");


        }
        

    }
}