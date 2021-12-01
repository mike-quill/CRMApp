using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class Currency
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Cannot be left blank")]
        [StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
        [Display(Name = "Currency Name")]
        public string Name { get; set; }
        public int CurrencyPreference { get; set; }
    }
}
