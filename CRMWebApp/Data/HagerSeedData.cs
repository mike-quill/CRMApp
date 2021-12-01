using CRMWebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Data
{
	public static class HagerSeedData
	{
		public static void Initialize(IServiceProvider serviceProvider)
		{
			using (var context = new HagerDbContext(
				serviceProvider.GetRequiredService<DbContextOptions<HagerDbContext>>()))
			{
				//Countries
				string[] countries = new string[] { "Canada", "United States", "Mexico" };
				if (!context.Countries.Any())
				{
					int preference = 0;
					foreach (string s in countries)
					{
						preference++;
						Country c = new Country
						{
							Name = s,
							CountryPreference = preference

						};
						context.Countries.Add(c);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Countries
				int[] countryIDs = context.Countries.Select(s => s.ID).ToArray();
				int countryIDCount = countryIDs.Count();

				//Provinces and States for Canada, USA, and Mexico
				string[] canadaProvinces = new string[] { "Alberta", "British Columbia", "Manitoba", "New Brunswick", "Newfoundland and Labrador", "Northwest Territories", "Nova Scotia", "Nunavut", "Ontario", "Prince Edward Island", "Quebec", "Saskatchewan", "Yukon" };
				string[] americanStates = new string[] { "Alabama", "Alaska", "Arizona", "Arkansas", "California", "Colorado", "Connecticut", "Delaware", "Florida", "Georgia", "Hawaii", "Idaho", "Illinois", "Indiana", "Iowa", "Kansas", "Kentucky", "Louisiana", "Maine", "Maryland", "Massachusetts", "Michigan", "Minnesota", "Mississippi", "Missouri", "Montana", "Nebraska", "Nevada", "New Hampshire", "New Jersey", "New Mexico", "New York", "North Carolina", "North Dakota", "Ohio", "Oklahoma", "Oregon", "Pennsylvania", "Rhode Island", "South Carolina", "South Dakota", "Tennessee", "Texas", "Utah", "Vermont", "Virginia", "Washington", "West Virginia", "Wisconsin", "Wyoming" };
				string[] mexicanStates = new string[] { "Aguascalientes", "Baja California", "Baja California Sur", "Campeche", "Chiapas", "Chihuahua", "Coahuila", "Colima", "Mexico City", "Durango", "Guanajuato", "Guerrero", "Hidalgo", "Jalisco", "México", "Michoacán", "Morelos", "Nayarit", "Nuevo León", "Oaxaca", "Puebla", "Querétaro", "Quintana Roo", "San Luis Potosí", "Sinaloa", "Sonora", "Tabasco", "Tamaulipas", "Tlaxcala", "Veracruz", "Yucatán", "Zacatecas" };
				if (!context.Provinces.Any())
				{
					int preference = 0;
					foreach (string s in canadaProvinces)
					{
						preference++;
						Province p = new Province
						{
							Name = s,
							CountryID = context.Countries.Where(c => c.Name == "Canada").FirstOrDefault().ID,
							ProvincePreference = preference
						};
						context.Provinces.Add(p);
					}
					context.SaveChanges();
					foreach (string s in americanStates)
					{
						preference++;
						Province p = new Province
						{
							Name = s,
							CountryID = context.Countries.Where(c => c.Name == "United States").FirstOrDefault().ID,
							ProvincePreference = preference
						};
						context.Provinces.Add(p);
					}
					context.SaveChanges();
					foreach (string s in mexicanStates)
					{
						preference++;
						Province p = new Province
						{
							Name = s,
							CountryID = context.Countries.Where(c => c.Name == "Mexico").FirstOrDefault().ID,
							ProvincePreference = preference
						};
						context.Provinces.Add(p);
					}
					context.SaveChanges();
				}

				//Create collection of the primary keys of the Provinces
				int[] provinceIDs = context.Provinces.Select(s => s.ID).ToArray();
				int provinceIDCount = provinceIDs.Count();



				//Currencies
				string[] currencies = new string[] { "CAD", "USD" };
				if (!context.Currencies.Any())
				{
					int preference = 0;
					foreach (string s in currencies)
					{
						preference++;
						Currency c = new Currency
						{
							Name = s,
							CurrencyPreference = preference

						};
						context.Currencies.Add(c);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Currencies
				int[] currencyIDs = context.Currencies.Select(s => s.ID).ToArray();
				int currencyIDCount = currencyIDs.Count();


				//Customer Type
				string[] customerTypes = new string[] { "Poultry", "Beef", "Pork", "Bakery", "Vegetables & Produce", "Other Food", "Conveyor & Fabrication", "Compressed Gas", "Cryogenic Piping", "Custom Fabrication", "IQF Exhaust", "NFPA Exhaust", "Construction", "Conveyors", "Manifolds", "Plumbing", "Beverage", "HPP", "Cryogenics" };
				if (!context.CustomerTypes.Any())
				{
					int preference = 0;
					foreach (string s in customerTypes)
					{
						preference++;
						CustomerType c = new CustomerType
						{
							Name = s,
							Preference = preference
						};
						context.CustomerTypes.Add(c);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Customer Types
				int[] CustomerTypeIDs = context.CustomerTypes.Select(s => s.ID).ToArray();
				int customerTypeIDCount = CustomerTypeIDs.Count();

				//Vendor Type
				string[] vendorTypes = new string[] { "Conveyor & Fabrication", "Professional Service", "Office Supplies", "Shop Supplies", "Cryogenic", "Cryogenic Parts", "Conveyor Components", "Stainless Steel Raw Material", "Plumbing / Piping", "Transportation", "HVAC & Exhaust Systems", "Outsourced Fabrication & Services", "Electrical Components" };
				if (!context.VendorTypes.Any())
				{
					int preference = 0;
					foreach (string s in vendorTypes)
					{
						preference++;
						VendorType v = new VendorType
						{
							Name = s,
							Preference = preference
						};
						context.VendorTypes.Add(v);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Vendor Types
				int[] VendorTypeIDs = context.VendorTypes.Select(s => s.ID).ToArray();
				int vendorTypeIDCount = VendorTypeIDs.Count();

				//Contractor Type
				string[] contractorTypes = new string[] { "Welding", "Plumbing", "Electrical", "Engineering", "Fabrication", "General Contractor", "Metal Forming", "Metal Cutting" };
				if (!context.ContractorTypes.Any())
				{
					int preference = 0;

					foreach (string s in contractorTypes)
					{
						preference++;
						ContractorType ct = new ContractorType
						{
							Name = s,
							Preference = preference
						};
						context.ContractorTypes.Add(ct);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Contractor Types
				int[] ContractorTypeIDs = context.ContractorTypes.Select(s => s.ID).ToArray();
				int contractorTypeIDCount = ContractorTypeIDs.Count();

				//BillingTerm
				string[] billingTerms = new string[] { "40% down, balance net 15", "40% down, balance net 30", "40% down, balance net 45", "40% down, balance net 90", "Due on receipt", "Net 15", "Net 30", "Net 45", "Net 90" };
				if (!context.BillingTerms.Any())
				{
					int preference = 0;

					foreach (string s in billingTerms)
					{
						preference++;
						BillingTerm b = new BillingTerm
						{
							Name = s,
							BillingPreference = preference
						};
						context.BillingTerms.Add(b);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the BillingTerm
				int[] BillingTermIDs = context.BillingTerms.Select(s => s.ID).ToArray();
				int billingTermIDCount = BillingTermIDs.Count();

				//Job Positions
				string[] jobPositions = new string[] { "Jr. Fabricator", "Fabricator", "Sr. Fabricator", "Foreman", "Apprentice Plumber", "Plumber", "Field Supervisor", "General Labourer", "Shipping Receiving", "Controller", "President", "Vice President", "Jr. Draftsperson", "Mechanical Designer", "Professional Engineer", "Engineering Manager", "Mechanical Estimator/Purchaser", "Estimator", "Sales Manager" };
				if (!context.JobPositions.Any())
				{
					int preference = 0;

					foreach (string s in jobPositions)
					{
						preference++;
						JobPosition c = new JobPosition
						{
							Name = s,
							JobPreference = preference
						};
						context.JobPositions.Add(c);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Job Positions
				int[] JobPositionIDs = context.JobPositions.Select(s => s.ID).ToArray();
				int jobPositionsIDCount = JobPositionIDs.Count();


				//Employment Type
				string[] employmentTypes = new string[] { "Full-time", "Part-time", "Contract", "Seasonal", "Co-op Student" };
				if (!context.EmploymentTypes.Any())
				{
					int preference = 0;

					foreach (string s in employmentTypes)
					{
						preference++;
						EmploymentType e = new EmploymentType
						{
							Name = s,
							EmploymentPreference = preference
						};
						context.EmploymentTypes.Add(e);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Employment Type
				int[] EmploymentTypeIDs = context.EmploymentTypes.Select(s => s.ID).ToArray();
				int employmentTypesIDCount = EmploymentTypeIDs.Count();

				// Categories
				string[] categories = new string[] { "Christmas Card", "Marketing Material", "News letter" };
				if (!context.Categories.Any())
				{
					int preference = 0;

					foreach (string s in categories)
					{
						preference++;
						Category c = new Category
						{
							Name = s,
							CategoryPreference = preference
						};
						context.Categories.Add(c);
					}
					context.SaveChanges();
				}
				//Create collection of the primary keys of the Categories
				int[] CategoryIDs = context.Categories.Select(s => s.ID).ToArray();
				int categoriesIDCount = CategoryIDs.Count();

			}
		}
	}
}
