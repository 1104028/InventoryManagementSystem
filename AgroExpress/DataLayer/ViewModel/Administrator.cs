using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.ViewModel
{
    public class Administrator
    {

      //  public List<Vaccination> vaccinelist { get; set; }
      //  public List<MedicationList> medicinelist { get; set; }
       // public List<CowHeat> cowheatlist { get; set; }

        public List<Order> orderlist { get; set; }
        public List<ProductandSaleHistory> productandsale { get; set; }
        public List<CustomerTransactionDetails> TransactionHistory { get; set; }
        public List<Notification> Notification { get; set; }

        public string CustomerName { get; set; }
    }
}