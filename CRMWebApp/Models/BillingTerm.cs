using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class BillingTerm
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Cannot be left blank")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        [Display(Name = "Billing Term Name")]
        public string Name { get; set; }

        public int BillingPreference { get; set; }
    }
}
