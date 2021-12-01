using CRMWebApp.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Utility
{
	public class MergeHelper
	{
		public MergeHelper()
		{
			CompanySelectList = new List<SelectListItem>();
			CompanyMergeList = new List<Company>();
		}
		public List<int> SelectedCompanyIDs { get; set; }
		public IEnumerable<SelectListItem> CompanySelectList { get; set; }
		public IEnumerable<Company> CompanyMergeList { get; set; }

		public int ID { get; set; }
		public string Name { get; set; }
	}
}