using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgroExpress.DataLayer.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalWeightList
    {
        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public int? AnimalTypeID { get; set; }

        public IEnumerable<SelectListItem> GenderOptions { get; set; }
        public string Gender { get; set; }

        public IEnumerable<SelectListItem> AnimalCodes { get; set; }
        public int? AnimalID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateMin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateMax { get; set; }

        public double? weightMin { get; set; }

        public double? weightMax { get; set; }

        public List<AnimalWeightInfoView> searchResult { get; set; }
    }
}