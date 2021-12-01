using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class JobPosition
	{
		public JobPosition()
		{
			Employees = new HashSet<Employee>();
		}

		public int ID { get; set; }

		[Required(ErrorMessage = "You cannot leave Name of the Job Position blank.")]
		[DisplayFormat(NullDisplayText = "None")]
		[Display(Name = "Job Position")]
		[StringLength(50, ErrorMessage = "Name cannot be more than 50 characters long.")]
		public string Name { get; set; }

		public int JobPreference { get; set; }

		public virtual ICollection<Employee> Employees { get; set; }
	}
}
