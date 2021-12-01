using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class EmploymentType
	{
		public EmploymentType()
		{
			Employees = new HashSet<Employee>();
		}

		public int ID { get; set; }

		[Display(Name = "Employment Type")]
		[Required(ErrorMessage = "You cannot leave Name of the Employment Type blank.")]
		[StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
		[DisplayFormat(NullDisplayText = "None")]
		public string Name { get; set; }

		public int EmploymentPreference { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }
	}
}
