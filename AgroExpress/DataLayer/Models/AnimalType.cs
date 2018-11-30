using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class AnimalType
    {
        [Key]
        public int PKAnimalTypeId { get; set; }

        [Display(Name = "Animal Type Name")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Animal Type Name required")]
        public string AnimalTypeName { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }

        public virtual ICollection<AnimalInformation> animalinfo { get; set; }
    }
}