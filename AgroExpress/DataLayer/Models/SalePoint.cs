using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SalePoint
    {
        #region
        [Key]
        public int PKSalePointID { get; set; }

        [Display(Name = "Sale Point Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sale Point Name required")]
        public string SalePointName { get; set; }


        [Display(Name = "Sale Point Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sale Point Address required")]
        [MaxLength(200, ErrorMessage = "Address can be Maximum 200 characters")]
        public string SalePointAddress { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual ICollection<Manager> manager { get; set; }
        public virtual ICollection<DeliveryBoy> deliveryboy { get; set; }
        public virtual ICollection<Area> area { get; set; }
        public virtual ICollection<Sale> sale { get; set; }
        public virtual ICollection<BillingHistory> billinghistory { get; set; }
        public virtual ICollection<SalePointProductConsume> salepointmilksummary { get; set; }
        public virtual ICollection<SalePointProductStock> salepoinproduct { get; set; }
        #endregion
    }
}