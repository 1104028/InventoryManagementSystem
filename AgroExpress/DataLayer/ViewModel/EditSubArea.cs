using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class EditSubArea
    {
        public int PKSubAreaId { get; set; }

        [Display(Name = "Sub Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Area Name required")]
        public string SubAreaName { get; set; }


        public IEnumerable<SelectListItem> arealist { get; set; }
        public int AreaId { get; set; }
    }
}