using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class CompanyContractorType
	{
		public int CompanyID { get; set; }
		public Company Company { get; set; }

		public int ContractorTypeID { get; set; }
		public ContractorType ContractorType { get; set; }
	}
}
