using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRMWebApp.Data;
using CRMWebApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using OfficeOpenXml;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using CRMWebApp.Utility;
using CRMWebApp.ViewModels;
using Microsoft.EntityFrameworkCore.Storage;

namespace CRMWebApp.Controllers
{
	[Authorize]
	public class CompaniesController : Controller
	{
		public enum CompanyTypeLists
		{
			ContractorType,
			CustomerType,
			VendorType
		}

		private readonly HagerDbContext _context;
		private readonly ApplicationDbContext _identityContext;

		public CompaniesController(HagerDbContext context, ApplicationDbContext identityContext)
		{
			_context = context;
			_identityContext = identityContext;
		}

		// GET: Companies
		public async Task<IActionResult> Index(string SearchString, int? VendorTypeID, int? CustomerTypeID, int? ContractorTypeID, int? page, int? pageSizeID, bool showInactive, bool showCustomer, bool showVendor, bool showContractor,
			string actionButton, string sortDirection = "asc", string sortField = "Name")
		{
			PopulateDropDownLists();    //Data for Filter DDL
			ViewData["Filtering"] = "";  //Assume not filtering

			var companies = from p in _context.Companies
				 .Include(c => c.BillingCountry)
				.Include(c => c.BillingProvince)
				.Include(c => c.BillingTerm)
				.Include(c => c.Currency)
				.Include(c => c.ShippingCountry)
				.Include(c => c.ShippingProvince)
				.Include(c => c.CompanyContractorTypes).ThenInclude(c => c.ContractorType)
				.Include(c => c.CompanyCustomerTypes).ThenInclude(c => c.CustomerType)
				.Include(c => c.CompanyVendorTypes).ThenInclude(c => c.VendorType)
							select p;

			CookieHelper.CookieSet(HttpContext, "CompaniesURL", "", -1);

			//Add as many filters as needed
			if (VendorTypeID.HasValue)
			{
				companies = companies.Where(p => p.CompanyVendorTypes.Any(x => x.VendorTypeID == VendorTypeID));
				ViewData["Filtering"] = " show";
			}
			if (CustomerTypeID.HasValue)
			{
				companies = companies.Where(p => p.CompanyCustomerTypes.Any(x => x.CustomerTypeID == CustomerTypeID));
				ViewData["Filtering"] = " show";
			}
			if (ContractorTypeID.HasValue)
			{
				companies = companies.Where(p => p.CompanyContractorTypes.Any(x => x.ContractorTypeID == ContractorTypeID));
				ViewData["Filtering"] = " show";
			}

			if (!String.IsNullOrEmpty(SearchString))
			{
				companies = companies.Where(p => p.Name.ToUpper().Contains(SearchString.ToUpper()));
				ViewData["Filtering"] = " show";
			}
			if (showInactive == false)//If check box is not checked show only active employees
			{
				companies = companies.Where(p => p.Active == true);
			}
			else
			{
				ViewData["Filtering"] = " show";
			}
			if (showCustomer == true)
			{
				ViewData["Filtering"] = " show";
				companies = companies.Where(p => p.CompanyCustomerTypes.Count > 0);
			}
			if (showContractor == true)
            {
                ViewData["Filtering"] = " show";
                companies = companies.Where(p => p.CompanyContractorTypes.Count > 0);
            }
			if (showVendor == true)
			{
				ViewData["Filtering"] = " show";
				companies = companies.Where(p => p.CompanyVendorTypes.Count > 0);
			}

			if (!String.IsNullOrEmpty(actionButton)) //Form Submitted so lets sort!
			{
				page = 1;//Reset page to start

				if (actionButton != "Filter")//Change of sort is requested
				{
					if (actionButton == sortField) //Reverse order on same field
					{
						sortDirection = sortDirection == "asc" ? "desc" : "asc";
					}
					sortField = actionButton;//Sort by the button clicked
				}
			}
			//Now we know which field and direction to sort by
			if (sortField == "Name")
			{
				if (sortDirection == "asc")
				{
					companies = companies
						.OrderBy(p => p.Name);
				}
				else
				{
					companies = companies
						.OrderByDescending(p => p.Name);
				}
			}
			else if (sortField == "Location")
			{
				if (sortDirection == "asc")
				{
					companies = companies
						.OrderBy(p => p.Location);
				}
				else
				{
					companies = companies
						.OrderByDescending(p => p.Location);
					
				}
			}
			//Set sort for next time
			ViewData["sortField"] = sortField;
			ViewData["sortDirection"] = sortDirection;

			//Handle Paging
			int pageSize;//This is the value we will pass to PaginatedList
			if (pageSizeID.HasValue)
			{
				//Value selected from DDL so use and save it to Cookie
				pageSize = pageSizeID.GetValueOrDefault();
				CookieHelper.CookieSet(HttpContext, "pageSizeValue", pageSize.ToString(), 30);
			}
			else
			{
				//Not selected so see if it is in Cookie
				pageSize = Convert.ToInt32(HttpContext.Request.Cookies["pageSizeValue"]);
			}
			pageSize = (pageSize == 0) ? 5 : pageSize;//Neither Selected or in Cookie so go with default
			ViewData["pageSizeID"] =
				new SelectList(new[] { "3", "5", "10", "20", "30", "40", "50", "100", "500" }, pageSize.ToString());
			var pagedData = await PaginatedList<Company>.CreateAsync(companies.AsNoTracking(), page ?? 1, pageSize);

			return View(pagedData);
		}

		// GET: Companies/Details/5
		public async Task<IActionResult> Details(int? id, string returnURL)
		{
			if (id == null)
			{
				return NotFound();
			}

			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

			var company = await _context.Companies
				.Include(c => c.BillingCountry)
				.Include(c => c.BillingProvince)
				.Include(c => c.BillingTerm)
				.Include(c => c.Currency)
				.Include(c => c.ShippingCountry)
				.Include(c => c.ShippingProvince)
				.Include(c => c.Contacts)
				.Include(m => m.CompanyContractorTypes)
				.ThenInclude(m => m.ContractorType)
				.Include(m => m.CompanyCustomerTypes)
				.ThenInclude(m => m.CustomerType)
				.Include(m => m.CompanyVendorTypes)
				.ThenInclude(m => m.VendorType)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (company == null)
			{
				return NotFound();
			}
			if (User.IsInRole("Admin"))
			{
				return View("DetailAdmin", company);
			}
			else
			{
				return View(company);
			};
		}

		// GET: Companies/Create
		[Authorize(Roles = "Admin, Supervisor")]
		public IActionResult Create()
		{
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");
			Company company = new Company();
			PopulateAssignedTypeData<ContractorType>(company, CompanyTypeLists.ContractorType, _context.ContractorTypes, "contractorSelOpts", "contractorAvailOpts");
			PopulateAssignedTypeData<CustomerType>(company, CompanyTypeLists.CustomerType, _context.CustomerTypes, "customerSelOpts", "customerAvailOpts");
			PopulateAssignedTypeData<VendorType>(company, CompanyTypeLists.VendorType, _context.VendorTypes, "vendorSelOpts", "vendorAvailOpts");
			PopulateDropDownLists();
			return View();
		}

		// POST: Companies/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Supervisor")]
		public async Task<IActionResult> Create([Bind("ID,Name,Location,CreditCheck,Phone,Website,BillingAddress1,BillingAddress2,BillingCity,BillingPostalCode,ShippingAddress1,ShippingAddress2,ShippingCity,ShippingPostalCode,Active,Notes,BillingTermID,CurrencyID,BillingProvinceID,ShippingProvinceID,BillingCountryID,ShippingCountryID")] Company company, string[] selectedContractorOptions, string[] selectedCustomerOptions, string[] selectedVendorOptions, string finish, string add)
		{

			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

			try
			{
				UpdateCompanyTypes<ContractorType>(selectedContractorOptions, company, CompanyTypeLists.ContractorType, _context.ContractorTypes);
				UpdateCompanyTypes<CustomerType>(selectedCustomerOptions, company, CompanyTypeLists.CustomerType, _context.CustomerTypes);
				UpdateCompanyTypes<VendorType>(selectedVendorOptions, company, CompanyTypeLists.VendorType, _context.VendorTypes);
				if (ModelState.IsValid)
				{
					_context.Add(company);
					await _context.SaveChangesAsync();
					if(!String.IsNullOrEmpty(finish))
                    {
						return RedirectToAction(nameof(Index));
					}
                    if(!String.IsNullOrEmpty(add))
                    {
						return RedirectToAction("Create", "Contacts",  new { companyID = company.ID });
					}
				}
				else
				{
					ModelState.AddModelError("", "Model error occurred.");
				}
			}
			catch (DbUpdateException ex)
			{
				if(ex.InnerException.Message.Contains("UNIQUE"))
				{
					ModelState.AddModelError("", "Could not create. Company with Name and Location already exists.");
				} else
				{
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
				
				
			}
			PopulateAssignedTypeData<ContractorType>(company, CompanyTypeLists.ContractorType, _context.ContractorTypes, "contractorSelOpts", "contractorAvailOpts");
			PopulateAssignedTypeData<CustomerType>(company, CompanyTypeLists.CustomerType, _context.CustomerTypes, "customerSelOpts", "customerAvailOpts");
			PopulateAssignedTypeData<VendorType>(company, CompanyTypeLists.VendorType, _context.VendorTypes, "vendorSelOpts", "vendorAvailOpts");
			PopulateDropDownLists();
			return View(company);
		}

		// GET: Companies/Edit/5
		[Authorize(Roles = "Admin, Supervisor")]
		public async Task<IActionResult> Edit(int? id, string returnURL)
		{
			if (id == null)
			{
				return NotFound();
			}

			//Get the URL of the page that send us here
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

			var company = await _context.Companies
				.Include(m => m.BillingCountry)
				.Include(m => m.BillingProvince)
				.Include(m => m.ShippingCountry)
				.Include(m => m.ShippingProvince)
				.Include(m => m.BillingTerm)
				.Include(m => m.Currency)
				.Include(m => m.CompanyContractorTypes)
				.ThenInclude(m => m.ContractorType)
				.Include(m => m.CompanyCustomerTypes)
				.ThenInclude(m => m.CustomerType)
				.Include(m => m.CompanyVendorTypes)
				.ThenInclude(m => m.VendorType)
				.SingleOrDefaultAsync(e => e.ID == id);
			if (company == null)
			{
				return NotFound();
			}
			PopulateAssignedTypeData<ContractorType>(company, CompanyTypeLists.ContractorType, _context.ContractorTypes, "contractorSelOpts", "contractorAvailOpts");
			PopulateAssignedTypeData<CustomerType>(company, CompanyTypeLists.CustomerType, _context.CustomerTypes, "customerSelOpts", "customerAvailOpts");
			PopulateAssignedTypeData<VendorType>(company, CompanyTypeLists.VendorType, _context.VendorTypes, "vendorSelOpts", "vendorAvailOpts");
			PopulateDropDownLists(company.BillingTerm, company.Currency,
				company.ShippingProvince, company.BillingProvince, company.ShippingCountry, company.BillingCountry);
			return View(company);
		}

		// POST: Companies/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin, Supervisor")]
		public async Task<IActionResult> Edit(int id, bool IsUser, bool Active, string returnURL, string[] selectedContractorOptions, string[] selectedCustomerOptions, string[] selectedVendorOptions)
		{
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

			var companyToUpdate = await _context.Companies
				.Include(m => m.BillingCountry)
				.Include(m => m.BillingProvince)
				.Include(m => m.ShippingCountry)
				.Include(m => m.ShippingProvince)
				.Include(m => m.BillingTerm)
				.Include(m => m.Currency)
				.Include(m => m.CompanyContractorTypes)
				.ThenInclude(m => m.ContractorType)
				.Include(m => m.CompanyCustomerTypes)
				.ThenInclude(m => m.CustomerType)
				.Include(m => m.CompanyVendorTypes)
				.ThenInclude(m => m.VendorType)
				.SingleOrDefaultAsync(e => e.ID == id);

			if (companyToUpdate == null)
			{
				return NotFound();
			}

			UpdateCompanyTypes<ContractorType>(selectedContractorOptions, companyToUpdate, CompanyTypeLists.ContractorType, _context.ContractorTypes);
			UpdateCompanyTypes<CustomerType>(selectedCustomerOptions, companyToUpdate, CompanyTypeLists.CustomerType, _context.CustomerTypes);
			UpdateCompanyTypes<VendorType>(selectedVendorOptions, companyToUpdate, CompanyTypeLists.VendorType, _context.VendorTypes);

			if (await TryUpdateModelAsync<Company>(companyToUpdate, "",
							c => c.BillingAddress1, c => c.BillingAddress2, c => c.BillingCity, c => c.BillingCountry, c => c.BillingPostalCode, c => c.BillingProvince,
							c => c.BillingTerm, c => c.Location, c => c.Name, c => c.Phone, c => c.Website, c => c.BillingPostalCode,
							c => c.ShippingAddress1, c => c.ShippingAddress2, c => c.ShippingCity, c => c.ShippingPostalCode, c => c.Active, c => c.Notes, c => c.BillingTermID,
							c => c.CurrencyID, c => c.BillingProvinceID, c => c.ShippingProvinceID, c => c.BillingCountryID))
			{
				try
				{
					await _context.SaveChangesAsync();
					//If no referrer then go back to index
					if (String.IsNullOrEmpty(returnURL))
					{
						return RedirectToAction(nameof(Index));
					}
					else
					{
						return RedirectToAction("Details", new { companyToUpdate.ID });
					}
				}
				catch (RetryLimitExceededException /* dex */)
				{
					ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!CompanyExists(companyToUpdate.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				catch (DbUpdateException)
				{
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			PopulateAssignedTypeData<ContractorType>(companyToUpdate, CompanyTypeLists.ContractorType, _context.ContractorTypes, "contractorSelOpts", "contractorAvailOpts");
			PopulateAssignedTypeData<CustomerType>(companyToUpdate, CompanyTypeLists.CustomerType, _context.CustomerTypes, "customerSelOpts", "customerAvailOpts");
			PopulateAssignedTypeData<VendorType>(companyToUpdate, CompanyTypeLists.VendorType, _context.VendorTypes, "vendorSelOpts", "vendorAvailOpts");
			PopulateDropDownLists(companyToUpdate.BillingTerm, companyToUpdate.Currency,
				companyToUpdate.ShippingProvince, companyToUpdate.BillingProvince, companyToUpdate.ShippingCountry, companyToUpdate.BillingCountry);
			return View(companyToUpdate);
		}

		// GET: Companies/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");

			var company = await _context.Companies
				.Include(c => c.BillingCountry)
				.Include(c => c.BillingProvince)
				.Include(c => c.BillingTerm)
				.Include(c => c.Currency)
				.Include(c => c.ShippingCountry)
				.Include(c => c.ShippingProvince)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (company == null)
			{
				return NotFound();
			}

			return View(company);
		}

		// POST: Companies/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Companies");
			var company = await _context.Companies.FindAsync(id);
			_context.Companies.Remove(company);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private SelectList VendorTypeSelectList(int? id)
		{
			var dQuery = from j in _context.VendorTypes
						 orderby j.Preference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private SelectList CustomerTypeSelectList(int? id)
		{
			var dQuery = from j in _context.CustomerTypes
						 orderby j.Preference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private SelectList ContractorSelectList(int? id)
		{
			var dQuery = from j in _context.ContractorTypes
						 orderby j.Preference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private SelectList BillingTermSelectList(int? id)
		{
			var dQuery = from j in _context.BillingTerms
						 orderby j.BillingPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}
		private SelectList CurrencySelectList(int? id)
		{
			var dQuery = from j in _context.Currencies
						 orderby j.CurrencyPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}
		private SelectList BillingProvinceSelectList(int? CountryID, int? selectedId)
		{
			var query = from p in _context.Provinces.Include(c => c.Country)
						select p;
			if (CountryID.HasValue)
			{
				query = query.Where(c => c.CountryID == CountryID);
			}
			return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", selectedId);
		}
		private SelectList ShippingProvinceSelectList(int? CountryID, int? selectedId)
		{
			var query = from p in _context.Provinces.Include(c => c.Country)
						select p;
			if (CountryID.HasValue)
			{
				query = query.Where(c => c.CountryID == CountryID);
			}
			return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", selectedId);
		}
		private SelectList BillingCountrySelectList(int? id)
		{
			var dQuery = from j in _context.Countries
						 orderby j.CountryPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}
		private SelectList ShippingCountrySelectList(int? id)
		{
			var dQuery = from j in _context.Countries
						 orderby j.CountryPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private void PopulateDropDownLists(BillingTerm billingTerm = null, Currency currency = null, Province billProvince = null, Province shipProvince = null, Country billCountry = null, Country shipCountry = null)

		{
			ViewData["VendorTypeID"] = VendorTypeSelectList(null);
			ViewData["ContractorTypeID"] = ContractorSelectList(null);
			ViewData["CustomerTypeID"] = CustomerTypeSelectList(null);
			ViewData["BillingTermID"] = BillingTermSelectList(billingTerm?.ID);
			ViewData["CurrencyID"] = CurrencySelectList(currency?.ID);
			ViewData["BillingProvinceID"] = BillingProvinceSelectList(billCountry?.ID, billProvince?.ID);
			ViewData["ShippingProvinceID"] = ShippingProvinceSelectList(shipCountry?.ID, shipProvince?.ID);
			ViewData["BillingCountryID"] = BillingCountrySelectList(billCountry?.ID);
			ViewData["ShippingCountryID"] = ShippingCountrySelectList(shipCountry?.ID);
		}

		public JsonResult GetProvinces(int? ID)
		{
			return Json(BillingProvinceSelectList(ID, null));
		}

		private bool CompanyExists(int id)
		{
			return _context.Companies.Any(e => e.ID == id);
		}

		private void PopulateAssignedTypeData<T>(Company company, CompanyTypeLists listType, DbSet<T> allOptions, string selOptsViewDataName, string availOptsViewDataName) where T : CompanyType
		{
			var currentOptionsHS = new HashSet<int>();
			switch (listType)
			{
				case CompanyTypeLists.ContractorType:
					{
						currentOptionsHS = new HashSet<int>(company.CompanyContractorTypes.Select(b => b.ContractorTypeID));
						break;
					}
				case CompanyTypeLists.CustomerType:
					{
						currentOptionsHS = new HashSet<int>(company.CompanyCustomerTypes.Select(b => b.CustomerTypeID));
						break;
					}
				case CompanyTypeLists.VendorType:
					{
						currentOptionsHS = new HashSet<int>(company.CompanyVendorTypes.Select(b => b.VendorTypeID));
						break;
					}
				default: break;
			}

			var selected = new List<ListOptionVM>();
			var available = new List<ListOptionVM>();
			foreach (var s in allOptions)
			{
				if (currentOptionsHS.Contains(s.ID))
				{
					selected.Add(new ListOptionVM
					{
						ID = s.ID,
						DisplayText = s.Name,
						Preference = s.Preference
					});
				}
				else
				{
					available.Add(new ListOptionVM
					{
						ID = s.ID,
						DisplayText = s.Name,
						Preference = s.Preference
					});
				}
			}

			ViewData[selOptsViewDataName] = new MultiSelectList(selected.OrderBy(s => s.Preference), "ID", "DisplayText");
			ViewData[availOptsViewDataName] = new MultiSelectList(available.OrderBy(s => s.Preference), "ID", "DisplayText");
		}
		private void UpdateCompanyTypes<T>(string[] selectedOptions, Company companyToUpdate, CompanyTypeLists listType, DbSet<T> allOptions) where T : CompanyType
		{
			var currentOptionsHS = new HashSet<int>();
			switch (listType)
			{
				case CompanyTypeLists.ContractorType:
					{
						if (selectedOptions == null)
						{
							companyToUpdate.CompanyContractorTypes = new List<CompanyContractorType>();
							return;
						}
						currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyContractorTypes.Select(b => b.ContractorTypeID));
						break;
					}
				case CompanyTypeLists.CustomerType:
					{
						if (selectedOptions == null)
						{
							companyToUpdate.CompanyCustomerTypes = new List<CompanyCustomerType>();
							return;
						}
						currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyCustomerTypes.Select(b => b.CustomerTypeID));
						break;
					}
				case CompanyTypeLists.VendorType:
					{
						if (selectedOptions == null)
						{
							companyToUpdate.CompanyVendorTypes = new List<CompanyVendorType>();
							return;
						}
						currentOptionsHS = new HashSet<int>(companyToUpdate.CompanyVendorTypes.Select(b => b.VendorTypeID));
						break;
					}
				default: break;
			}

			var selectedOptionsHS = new HashSet<string>(selectedOptions);

			foreach (var s in allOptions)
			{
				if (selectedOptionsHS.Contains(s.ID.ToString()))
				{
					if (!currentOptionsHS.Contains(s.ID))
					{
						switch (listType)
						{
							case CompanyTypeLists.ContractorType:
								{
									companyToUpdate.CompanyContractorTypes.Add(new CompanyContractorType
									{
										ContractorTypeID = s.ID,
										CompanyID = companyToUpdate.ID
									});
									break;
								}
							case CompanyTypeLists.CustomerType:
								{
									companyToUpdate.CompanyCustomerTypes.Add(new CompanyCustomerType
									{
										CustomerTypeID = s.ID,
										CompanyID = companyToUpdate.ID
									});
									break;
								}
							case CompanyTypeLists.VendorType:
								{
									companyToUpdate.CompanyVendorTypes.Add(new CompanyVendorType
									{
										VendorTypeID = s.ID,
										CompanyID = companyToUpdate.ID
									});
									break;
								}
							default: break;
						}

					}
				}
				else
				{
					if (currentOptionsHS.Contains(s.ID))
					{
						switch (listType)
						{
							case CompanyTypeLists.ContractorType:
								{
									CompanyContractorType typeToRemove = companyToUpdate.CompanyContractorTypes.SingleOrDefault(d => d.ContractorTypeID == s.ID);
									_context.Remove(typeToRemove);
									break;
								}
							case CompanyTypeLists.CustomerType:
								{
									CompanyCustomerType typeToRemove = companyToUpdate.CompanyCustomerTypes.SingleOrDefault(d => d.CustomerTypeID == s.ID);
									_context.Remove(typeToRemove);
									break;
								}
							case CompanyTypeLists.VendorType:
								{
									CompanyVendorType typeToRemove = companyToUpdate.CompanyVendorTypes.SingleOrDefault(d => d.VendorTypeID == s.ID);
									_context.Remove(typeToRemove);
									break;
								}
							default: break;
						}


					}
				}
			}
		}

		[HttpPost]
		[Authorize(Roles = "Admin, Supervisor")]
		public async Task<IActionResult> InsertFromExcel(IFormFile theExcel)
		{
			if (theExcel != null)
			{
				ExcelPackage excel;
				using (var memoryStream = new MemoryStream())
				{
					await theExcel.CopyToAsync(memoryStream);
					excel = new ExcelPackage(memoryStream);
				}

				var workSheet = excel.Workbook.Worksheets[0];
				var start = workSheet.Dimension.Start;
				var end = workSheet.Dimension.End;

				List<Company> companies = new List<Company>();
				StringBuilder sb = new StringBuilder();

				int totalRows = end.Row - start.Row;

				for (int row = start.Row + 1; row <= end.Row; row++)
				{
					try
					{
						Company c = new Company();
						if (_context.Companies.Any(e => e.Name.ToLower() == workSheet.Cells[row, 1].Text.ToLower()) &&
							_context.Companies.Any(e => e.Location.ToLower() == workSheet.Cells[row, 2].Text.ToLower()))
						{
							throw new Exception("Company at this Location exists within db already: " + workSheet.Cells[row, 1].Text + " at: " + workSheet.Cells[row, 2].Text);
						}
						else
						{
							c.Name = workSheet.Cells[row, 1].Text;
							c.Location = workSheet.Cells[row, 2].Text;
						}

						c.CreditCheck = (workSheet.Cells[row, 3].Text == "1");

						// If Billing Terms column not empty
						if(!String.IsNullOrEmpty(workSheet.Cells[row, 4].Text))
						{
							// Try to get billing term id
							int? billingTermID = _context.BillingTerms.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 4].Text.ToLower()))?.ID;
							// Column not empty, but no match found
							if (billingTermID == null)
							{
								sb.AppendLine("#########################################");
								sb.AppendLine("Billing Term: " + workSheet.Cells[row, 4].Text + " not found.");
								sb.AppendLine("NON FATAL ERROR, ROW NUMBER: " + row);
							}
							else
							{
								// Assign found id
								c.BillingTermID = billingTermID;
							}
						}



						// If Currency column not empty
						if (!String.IsNullOrEmpty(workSheet.Cells[row, 5].Text))
						{
							// Try to get currency id
							int? currencyID = _context.Currencies.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 5].Text.ToLower()))?.ID;
							// Column not empty, but no match found
							if (currencyID == null)
							{
								sb.AppendLine("#########################################");
								sb.AppendLine("Currency: " + workSheet.Cells[row, 5].Text + " not found.");
								sb.AppendLine("NON FATAL ERROR, ROW NUMBER: " + row);
							}
							else
							{
								// Assign found id
								c.CurrencyID = currencyID;
							}
						}

						string phone = Regex.Replace(workSheet.Cells[row, 6].Text, "[^0-9]", "");
						if (phone.Length == 11 && phone[0] == '1')
						{
							c.Phone = phone.Remove(0, 1);
						}
						else
						{
							c.Phone = phone;
						}
						c.Email = String.IsNullOrEmpty(workSheet.Cells[row, 7].Text) ? null : workSheet.Cells[row, 7].Text;
						c.Website = String.IsNullOrEmpty(workSheet.Cells[row, 8].Text) ? null : workSheet.Cells[row, 8].Text;


						////////////////

						c.BillingAddress1 = workSheet.Cells[row, 9].Text;
						c.BillingAddress2 = workSheet.Cells[row, 10].Text;
						c.BillingCity = workSheet.Cells[row, 11].Text;


						int? billingCountryID = null;
						int? billingProvinceID;
						// If country column is not empty
						if (!String.IsNullOrEmpty(workSheet.Cells[row, 14].Text))
						{
							// Try to get ID from name
							billingCountryID = _context.Countries.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 14].Text.ToLower()))?.ID;
							// Coult not find from given name, throw error
							if (billingCountryID == null)
							{
								throw new Exception("Billing Country not found: " + workSheet.Cells[row, 14].Text);
							}
							// Else an ID was found...
							else
							{
								// If the province column is not empty
								if (!String.IsNullOrEmpty(workSheet.Cells[row, 12].Text))
								{
									// Try to get province ID that matches name and parent country
									billingProvinceID = _context.Provinces.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 12].Text.ToLower()) && p.CountryID == billingCountryID)?.ID;
									// If no match is found, throw error. Should protect against mismatched provinces and countries. ie. Louisiana, Canada
									if (billingProvinceID == null)
									{
										throw new Exception("Billing Province not found: " + workSheet.Cells[row, 12].Text + ", in Billing Country: " + workSheet.Cells[row, 14].Text);
									}
									else
									{
										// Else we found a country ID and province ID that matches country, assign both values to company
										c.BillingCountryID = billingCountryID;
										c.BillingProvinceID = billingProvinceID;
									}
								}
								else
								{
									// Province is empty, but we still have country so assign it
									c.BillingCountryID = billingCountryID;
								}
							}
						}
						else
						{
							// Country column is empty but province is not
							if (!String.IsNullOrEmpty(workSheet.Cells[row, 12].Text))
							{
								// Assign first province found that matches name
								c.BillingProvinceID = _context.Provinces.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 12].Text.ToLower()))?.ID;
							}
						}

						c.BillingPostalCode = workSheet.Cells[row, 13].Text;
						//////////////////
						c.ShippingAddress1 = workSheet.Cells[row, 15].Text;
						c.ShippingAddress2 = workSheet.Cells[row, 16].Text;
						c.ShippingCity = workSheet.Cells[row, 17].Text;


						int? shippingCountryID = null;
						int? shippingProvinceID;
						// If country column is not empty
						if (!String.IsNullOrEmpty(workSheet.Cells[row, 20].Text))
						{
							// Try to get ID from name
							shippingCountryID = _context.Countries.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 20].Text.ToLower()))?.ID;
							// Coult not find from given name, throw error
							if (shippingCountryID == null)
							{
								throw new Exception("Shipping Country not found: " + workSheet.Cells[row, 20].Text);
							}
							// Else an ID was found...
							else
							{
								// If the province column is not empty
								if (!String.IsNullOrEmpty(workSheet.Cells[row, 18].Text))
								{
									// Try to get province ID that matches name and parent country
									shippingProvinceID = _context.Provinces.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 18].Text.ToLower()) && p.CountryID == shippingCountryID)?.ID;
									// If no match is found, throw error. Should protect against mismatched provinces and countries. ie. Louisiana, Canada
									if (shippingProvinceID == null)
									{
										throw new Exception("Shipping Province not found: " + workSheet.Cells[row, 18].Text + ", in Shipping Country: " + workSheet.Cells[row, 20].Text);
									}
									else
									{
										// Else we found a country ID and province ID that matches country, assign both values to company
										c.ShippingCountryID = shippingCountryID;
										c.ShippingProvinceID = shippingProvinceID;
									}
								}
								else
								{
									// Province is empty, but we still have country so assign it
									c.ShippingCountryID = shippingCountryID;
								}
							}
						}
						else
						{
							// Country column is empty but province is not
							if (!String.IsNullOrEmpty(workSheet.Cells[row, 18].Text))
							{
								// Assign first province found that matches name
								c.ShippingProvinceID = _context.Provinces.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 18].Text.ToLower()))?.ID;
							}
						}

						c.ShippingPostalCode = workSheet.Cells[row, 19].Text;


						////////////////////////////
						// Customer Types
						string customerTypes = workSheet.Cells[row, 22].Text;
						if(!String.IsNullOrEmpty(customerTypes))
						{
							string[] customerTypesList = customerTypes.Split(",");
							foreach (string custType in customerTypesList)
							{
								int? custTypeID = _context.CustomerTypes.FirstOrDefault(p => p.Name.ToLower().Contains(custType.TrimStart().ToLower()))?.ID;
								if (custTypeID != null)
								{
									c.CompanyCustomerTypes.Add(new CompanyCustomerType()
									{
										CustomerTypeID = (int)custTypeID,
										Company = c
									});
								}
								else
								{
									sb.AppendLine("#########################################");
									sb.AppendLine("Customer Type: " + custType.Trim() + " not found.");
									sb.AppendLine("NON FATAL ERROR ROW NUMBER: " + row);
								}

							}
						}

						


						////////////////////////////
						// Contractor Types
						string contractorTypes = workSheet.Cells[row, 26].Text;
						if (!String.IsNullOrEmpty(contractorTypes))
						{
							string[] contractorTypesList = contractorTypes.Split(",");
							foreach (string contType in contractorTypesList)
							{
								int? contTypeID = _context.ContractorTypes.FirstOrDefault(p => p.Name.ToLower().Contains(contType.TrimStart().ToLower()))?.ID;
								if (contTypeID != null)
								{
									c.CompanyContractorTypes.Add(new CompanyContractorType()
									{
										ContractorTypeID = (int)contTypeID,
										Company = c
									});
								}
								else
								{
									sb.AppendLine("#########################################");
									sb.AppendLine("Contractor Type: " + contType.Trim() + " not found.");
									sb.AppendLine("NON FATAL ERROR ROW NUMBER: " + row);
								}

							}
						}
						


						////////////////////////////
						// Vendor Types
						string vendorTypes = workSheet.Cells[row, 24].Text;
						if (!String.IsNullOrEmpty(vendorTypes))
						{
							string[] vendorTypesList = vendorTypes.Split(",");
							foreach (string vendType in vendorTypesList)
							{
								int? vendTypeID = _context.VendorTypes.FirstOrDefault(p => p.Name.ToLower().Contains(vendType.TrimStart().ToLower()))?.ID;
								if (vendTypeID != null)
								{
									c.CompanyVendorTypes.Add(new CompanyVendorType()
									{
										VendorTypeID = (int)vendTypeID,
										Company = c
									});
								}
								else
								{
									sb.AppendLine("#########################################");
									sb.AppendLine("Vendor Type: " + vendType.Trim() + " not found.");
									sb.AppendLine("NON FATAL ERROR, ROW NUMBER: " + row);
								}
							}
						}
						


						///////////////////////////

						c.Active = (workSheet.Cells[row, 27].Text == "1");
						c.Notes = workSheet.Cells[row, 28].Text;

						companies.Add(c);
					}
					catch (Exception ex)
					{
						sb.AppendLine("#########################################");
						sb.AppendLine(ex.Message);
						sb.AppendLine("THIS ERROR PREVENTED INSERTING, ROW NUMBER: " + row);
					}

				}
				
				if (sb.Length > 0)
				{
					TempData["ImportErrors"] = sb.ToString();
				}
				int before = _context.Companies.Count();
				_context.Companies.AddRange(companies);
				_context.SaveChanges();
				int rows = _context.Companies.Count() - before;
				TempData["Success"] = rows.ToString() + "/" + totalRows.ToString() + " inserted.";

			}

			return RedirectToAction("Index", "Companies");
		}

		public IActionResult GetImportLog()
		{
			this.Response.Headers["content-disposition"] = "attachment;filename=ImportLog.txt";
			return File(Encoding.UTF8.GetBytes(TempData["ImportErrors"].ToString()), "text/plain");
		}

		public JsonResult GetCompaniesJSON()
		{
			List<ListOptionVM> companies = new List<ListOptionVM>();
			foreach(Company c in _context.Companies)
			{
				companies.Add(new ListOptionVM
				{
					ID = c.ID,
					DisplayText = c.Summary
				});
			}
			return Json(companies);
		}
	}
}
