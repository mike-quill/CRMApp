using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Models
{
	public class Company
	{

		public Company()
		{
			Active = true;
			Contacts = new HashSet<Contact>();
			CompanyContractorTypes = new HashSet<CompanyContractorType>();
			CompanyCustomerTypes = new HashSet<CompanyCustomerType>();
			CompanyVendorTypes = new HashSet<CompanyVendorType>();
		}

		public int ID { get; set; }

		[Display(Name = "Company")]
		public string Summary
		{
			get
			{
				return this.Name + (String.IsNullOrEmpty(this.Location) ? "" : " - " + this.Location);
			}
		}

		[Display(Name = "Phone")]
		public string PhoneNumber
		{
			get
			{
				if (String.IsNullOrEmpty(Phone))
				{
					return "";
				}
				else
				{
					return "(" + Phone.Substring(0, 3) + ") " + Phone.Substring(3, 3) + "-" + Phone.Substring(6, 4);
				}
			}
		}

		[Display(Name = "Company Name")]
		[Required(ErrorMessage = "You cannot leave the Company Name blank.")]
		public string Name { get; set; }

		[StringLength(100, ErrorMessage = "Location cannot be more than 100 characters long.")]
		[DisplayFormat(ConvertEmptyStringToNull = false)]
		public string Location { get; set; }

		[Display(Name = "Credit Check")]
		public bool CreditCheck { get; set; }

		[Display(Name = "Phone #")]
		[RegularExpression("^\\d{10}$", ErrorMessage = "Please enter a valid 10-digit phone number (no spaces).")]
		[DataType(DataType.PhoneNumber)]
		[StringLength(10)]
		public string Phone { get; set; }

		[StringLength(100, ErrorMessage = "Billing Address Line 1 cannot be more than 100 characters long.")]
		public string Website { get; set; }

		[Display(Name = "Billing Address Line 1")]
		[StringLength(100, ErrorMessage = "Billing Address Line 1 cannot be more than 100 characters long.")]
		public string BillingAddress1 { get; set; }

		[Display(Name = "Billing Address Line 2")]
		[StringLength(100, ErrorMessage = "Billing Address Line 2 cannot be more than 100 characters long.")]
		public string BillingAddress2 { get; set; }

		[Display(Name = "Billing City")]
		[StringLength(100, ErrorMessage = "Billing City cannot be more than 100 characters long.")]
		public string BillingCity { get; set; }

		[Display(Name = "Billing Postal/Zip Code")]
		[RegularExpression("^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$|^[0-9]{5}$", ErrorMessage = "Please enter a valid Postal Code in the A1A 1A1 format or 5 digit Zip Code.")]
		public string BillingPostalCode { get; set; }

		[Display(Name = "Shipping Address Line 1")]
		[StringLength(100, ErrorMessage = "Shipping Address Line 1 cannot be more than 100 characters long.")]
		public string ShippingAddress1 { get; set; }

		[Display(Name = "Shipping Address Line 2")]
		[StringLength(100, ErrorMessage = "Shipping Address Line 2 cannot be more than 100 characters long.")]
		public string ShippingAddress2 { get; set; }

		[Display(Name = "Shipping City")]
		[StringLength(100, ErrorMessage = "Shipping City cannot be more than 100 characters long.")]
		public string ShippingCity { get; set; }

		[Display(Name = "Shipping Postal/Zip Code")]
		[RegularExpression("^(?!.*[DFIOQU])[A-VXY][0-9][A-Z] ?[0-9][A-Z][0-9]$|^[0-9]{5}$", ErrorMessage = "Please enter a valid Postal Code in the A1A 1A1 format or 5 digit Zip Code.")]
		public string ShippingPostalCode { get; set; }

		[StringLength(255)]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

		public bool Active { get; set; }

		[StringLength(500, ErrorMessage = "Company Notes cannot be more than 500 characters long.")]
		public string Notes { get; set; }

		[Display(Name = "Billing Terms")]
		public int? BillingTermID { get; set; }

		public BillingTerm BillingTerm { get; set; }

		[Display(Name = "Currency")]
		public int? CurrencyID { get; set; }

		public Currency Currency { get; set; }

		[Display(Name = "Billing Province")]
		public int? BillingProvinceID { get; set; }
		public Province BillingProvince { get; set; }

		[Display(Name = "Shipping Province")]
		public int? ShippingProvinceID { get; set; }

		public Province ShippingProvince { get; set; }

		[Display(Name = "Billing Country")]
		public int? BillingCountryID { get; set; }

		public Country BillingCountry { get; set; }

		[Display(Name = "Shipping Country")]
		public int? ShippingCountryID { get; set; }

		public Country ShippingCountry { get; set; }

		public virtual ICollection<Contact> Contacts { get; set; }

		public virtual ICollection<CompanyContractorType> CompanyContractorTypes { get; set; }

		public virtual ICollection<CompanyCustomerType> CompanyCustomerTypes { get; set; }

		public virtual ICollection<CompanyVendorType> CompanyVendorTypes { get; set; }

	}
}
