using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;


public class PermissionAssign
{
    public List<string> Customer = new List<string>();
    public List<string> Manager = new List<string>();
    public List<string> DeliveryBoy = new List<string>();
    public List<string> Admin = new List<string>();
    public List<string> FirmManager = new List<string>();
    public List<string> Partner = new List<string>();

    public PermissionAssign()
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


        //-------------  customer ----------------------------
        #region

        Customer.Add("Home-Logout");
        Customer.Add("Home-ChangePassword");
        Customer.Add("Order-SingleOrderAdd");
        Customer.Add("Customer-TransactionHistory");
        Customer.Add("Administrator-Index");
        #endregion

        //-------------  manager ----------------------------
        #region

        Manager.Add("Home-Logout");
        Manager.Add("Animal-List");
        Manager.Add("Customer-EnabledCustomer");
        Manager.Add("Product-Sale");
        Manager.Add("Product-SingleSale");
        Manager.Add("Product-SaleHistory");
        Manager.Add("Production-Factory");
        Manager.Add("Production-FactoryList");
        Manager.Add("Production-Milk");
        Manager.Add("Production-MilkSummary");
        Manager.Add("Production-MilksummaryAdd");
        Manager.Add("SalePointProduct-Add");
        Manager.Add("SalePointProduct-Stock");
        Manager.Add("SalePointProduct-TransferList");
        Manager.Add("Administrator-Index");
        Manager.Add("Product-BillHistory");
        Manager.Add("Home-ChangePassword");
        Manager.Add("");
        #endregion

        //-------------  Delivery boy ----------------------------
        #region

        DeliveryBoy.Add("Home-Logout");
        DeliveryBoy.Add("Product-Sale");
        DeliveryBoy.Add("Product-SingleSale");
        DeliveryBoy.Add("Administrator-Index");
        DeliveryBoy.Add("Home-ChangePassword");
        #endregion

        //-------------  Firm Manager ----------------------------
        #region

        FirmManager.Add("Home-Logout");
        FirmManager.Add("Animal-WeightAdd");
        FirmManager.Add("Animal-WeightList");
        FirmManager.Add("Animal-CowHeatAdd");
        FirmManager.Add("Animal-CowHeatList");
        FirmManager.Add("Animal-MedicineAdd");
        FirmManager.Add("Animal-MedicationList");
        FirmManager.Add("Animal-VaccineAdd");
        FirmManager.Add("Animal-VaccineList");
        FirmManager.Add("Product-Sale");
        FirmManager.Add("Product-SingleSale");
        FirmManager.Add("Product-SaleHistory");
        FirmManager.Add("Production-Milk");
        FirmManager.Add("Production-MilkProductionList");
        FirmManager.Add("Administrator-Index");
        FirmManager.Add("Product-BillHistory");
        FirmManager.Add("Home-ChangePassword");
        FirmManager.Add("AnimalTypeList-Production");
        #endregion

        //-------------  Partner ----------------------------
        #region

        Partner.Add("Home-Logout");
        Partner.Add("Animal-List");
        Partner.Add("Animal-DisabledAnimalList");
        Partner.Add("Animal-WeightList");
        Partner.Add("Animal-CowHeatList");
        Partner.Add("Animal-MedicationList");
        Partner.Add("Animal-VaccineList");
        Partner.Add("Product-Index");
        Partner.Add("Product-SaleHistory");
        Partner.Add("Administrator-Index");
        Partner.Add("Product-BillHistory");
        Partner.Add("Home-ChangePassword");
        Partner.Add("Production-FactoryList");
        Partner.Add("SalePointProduct-Stock");
        Partner.Add("SalePointProduct-TransferList");
        Partner.Add("Production-MilkSummary");
        #endregion



    }
}