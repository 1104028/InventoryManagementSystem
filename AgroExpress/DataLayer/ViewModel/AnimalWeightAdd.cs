using AgroExpress.DataLayer.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalWeightAdd
    {
        #region

        [Display(Name = "AnimalCategory")]
        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public int? AnimalTypeId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        public DateTime SearchDate { get; set; }

        public List<AnimalWeights> animalweightlist { get; set; }
        #endregion
    }
}