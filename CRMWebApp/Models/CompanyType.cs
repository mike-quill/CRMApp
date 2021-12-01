using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class CompanyType
	{
		public virtual int ID { get; set; }
		public virtual string Name { get; set; }

		public virtual int Preference { get; set; }
	}
}
