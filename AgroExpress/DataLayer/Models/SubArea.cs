using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class SubArea
    {
        #region
        [Key]
        public int PKSubAreaId { get; set; }

        [Display(Name = "Sub Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Sub Area Name required")]
        public string SubAreaName { get; set; }

        [ForeignKey("area")]
        public int AreaId { get; set; }
        public virtual Area area { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        

        public virtual ICollection<Customer> customer { get; set; }
        #endregion
    }
}