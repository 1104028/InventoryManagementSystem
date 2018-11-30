using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    [Table("AREA")]
    public class Area
    {
        #region
        [Key]
        public int PKAreaId { get; set; }

        [Display(Name = "Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Area Name required")]
        public string AreaName { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
        
        [ForeignKey("salepoint")]
        public int SalePointId { get; set; }
        public virtual SalePoint salepoint { get; set; }

        public virtual ICollection<SubArea> subarea { get; set; }
        #endregion
    }
}