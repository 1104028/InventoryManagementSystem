using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AddArea
    {
        [Display(Name = "Area Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Area Name required")]
        public string AreaName { get; set; }


        public IEnumerable<SelectListItem> salepointlist { get; set; }
        public int SalePointId { get; set; }

    }
}