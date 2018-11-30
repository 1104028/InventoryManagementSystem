using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AgroExpress.DataLayer.Models
{
    public class Notification
    {
        #region
        [Key]
        public int PKNotificationId { get; set; }

        [Display(Name = "Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Date required")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }

        [Display(Name = "Notification Message")]
        [StringLength(350, ErrorMessage = "Notification can be at most 350 characters")]
        public string NotificationMessage { get; set; }

        [DefaultValue(false)]
        public bool IsAcknowledged { get; set; }

        public string GroupID { get; set; }
        #endregion
    }
}