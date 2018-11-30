using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("CUSTOMER")]
    public class Customer
    {
        #region
        [Key]
        public int PKCustomerId { get; set; }

        [Display(Name = "Customer Full Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Full Name required")]
        public string FullName { get; set; }

        [Display(Name = "Customer Address")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Customer Address required")]
        [MaxLength(250, ErrorMessage = "Address can be Maximum 250 characters")]
        public string Address { get; set; }

        [Display(Name = "Mobile Number")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Mobile Number required")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Mobile Number must be 11 characters")]
        [RegularExpression("^[0-9]*$", ErrorMessage = "Mobile Number must be numeric")]
        public string Mobile { get; set; }

        [Column(Order = 1)]
        [ForeignKey("subarea")]
        [Display(Name = "Sub Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public int SubAreaId { get; set; }
        public virtual SubArea subarea { get; set; }

        [Column(Order = 2)]
        [ForeignKey("userLogin")]
        public int LoginUserID { get; set; }
        public virtual UserLogin userLogin { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Rate")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Milk Rate Per Litre required")]
        public double Rate { get; set; }

        [DefaultValue(0)]
        [Display(Name = "Service Charge")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Service Charge required")]
        public double ServiceCharge { get; set; }

        [Display(Name = "Total Bill")]
        public double TotalBill { get; set; }

        [Display(Name = "Total paid")]
        public double TotalPaid { get; set; }

        

        [DefaultValue("false")]
        public bool IsDeleted { get; set; }

        [DefaultValue("false")]
        public bool SendSMS { get; set; }

        public virtual ICollection<Sale> sale { get; set; }
        public virtual ICollection<BillingHistory> billinghistory { get; set; }
        #endregion
    }
}