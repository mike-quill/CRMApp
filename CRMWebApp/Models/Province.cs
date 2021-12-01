using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class Province
    {

        public Province()
        {
            Employees = new HashSet<Employee>();
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Cannot be left blank")]
        [StringLength(40, ErrorMessage = "Name cannot be more than 40 characters long.")]
        [Display(Name = "Province/State")]
        [DisplayFormat(NullDisplayText = "None")]
        public string Name { get; set; }

        public int ProvincePreference { get; set; }

        [Required(ErrorMessage = "You must select the Country.")]
        [Display(Name="Country")]
        public int CountryID { get; set; }

        public Country Country { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
    }
}
