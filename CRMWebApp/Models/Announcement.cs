using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class Announcement
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "You cannot leave the Title blank.")]
        [StringLength(50, ErrorMessage = "Title cannot be more than 50 characters long.")]
        public string Title { get; set; }

        [Required(ErrorMessage = "You cannot leave the Content blank.")]
        public string Content { get; set; }

        [Display(Name = "Permission Level")]
        [Required(ErrorMessage = "You cannot leave the Permission Level blank.")]
        public int PermissionLevel { get; set; }

        [Required(ErrorMessage = "You cannot leave the Date Created blank.")]
        [Display(Name = "Date Created")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd h:mm tt}", ApplyFormatInEditMode = true)]
        public DateTime DateCreated { get; set; }
    }
}
