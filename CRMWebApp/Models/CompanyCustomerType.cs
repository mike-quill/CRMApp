using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class CompanyCustomerType
	{
		public int CompanyID { get; set; }
		public Company Company { get; set; }

		public int CustomerTypeID { get; set; }
		public CustomerType CustomerType { get; set; }
	}
}
