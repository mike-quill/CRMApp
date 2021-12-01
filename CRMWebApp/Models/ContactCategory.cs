using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class ContactCategory
	{
		public int ContactID { get; set; }

		public Contact Contact { get; set; }

		public int CategoryID { get; set; }

		public Category Category { get; set; }
	}
}
