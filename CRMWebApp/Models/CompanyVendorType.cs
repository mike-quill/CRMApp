using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class CompanyVendorType
	{
		public int CompanyID { get; set; }
		public Company Company { get; set; }

		public int VendorTypeID { get; set; }
		public VendorType VendorType { get; set; }
	}
}
