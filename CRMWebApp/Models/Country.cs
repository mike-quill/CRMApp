using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
    public class Country
    {

        public Country()
		{
            Employees = new HashSet<Employee>();
            Provinces = new HashSet<Province>();
        }

        public int ID { get; set; }

        [Display(Name = "Country")]
        [Required(ErrorMessage = "You cannot leave Country Name blank.")]
        [StringLength(100, ErrorMessage = "Name cannot be more than 100 characters long.")]
        [DisplayFormat(NullDisplayText = "None")]
        public string Name { get; set; }
        public int CountryPreference { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<Province> Provinces { get; set; }
    }
}
