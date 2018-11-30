using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AgroExpress.DataLayer.Models;
using System.Web.Mvc;
using System.ComponentModel.DataAnnotations;
namespace AgroExpress.DataLayer.ViewModel
{
    public class AnimalList
    {
        public IEnumerable<SelectListItem> AnimalTypes { get; set; }
        public int? AnimalTypeID { get; set; }

        public IEnumerable<SelectListItem> GenderOptions { get; set; }
        public string Gender { get; set; }

        public IEnumerable<SelectListItem> AnimalCodes { get; set; }
        public int? AnimalID { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EntryDateMin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? EntryDateMax { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExitDateMin { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? ExitDateMax { get; set; }



        public List<AnimalInformation> searchResult { get; set; }
    }
}