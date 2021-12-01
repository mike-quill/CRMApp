using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class CustomerType : CompanyType
	{
		public CustomerType()
		{
			CompanyCustomerTypes = new HashSet<CompanyCustomerType>();
		}

		[Required(ErrorMessage = "You cannot leave Name of the Customer Type blank.")]
		[StringLength(40, ErrorMessage = "Name cannot be more than 40 characters long.")]
		public override string Name { get; set; }

		public virtual ICollection<CompanyCustomerType> CompanyCustomerTypes { get; set; }

	}
}
