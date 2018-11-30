using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgroExpress.DataLayer.Models
{
    public class UserLogin
    {
        #region
        [Key]
        public int PkUserID { get; set; }


        [Display(Name = "User Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Name required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Minimum 6 characters required")]
        public string Password { get; set; }


        [Display(Name = "User Type")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "User Type required")]
        public int UserType { get; set; }

       

        public virtual ICollection<Customer> customer { get; set; }
        public virtual ICollection<Admin> admin { get; set; }
        public virtual ICollection<DeliveryBoy> deliveryboys { get; set; }
        public virtual ICollection<Manager> manager { get; set; }
        public virtual ICollection<FirmManager> firmmanager { get; set; }
        public virtual ICollection<Partner> partner { get; set; }


        #endregion
    }
}