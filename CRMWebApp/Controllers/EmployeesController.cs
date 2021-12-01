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
using Microsoft.AspNetCore.Identity;
using CRMWebApp.ViewModels;

namespace CRMWebApp.Controllers
{
	[Authorize]
	public class EmployeesController : Controller
	{
		private readonly HagerDbContext _context;
		private readonly ApplicationDbContext _identityContext;
		private readonly UserManager<IdentityUser> _userManager;

		public EmployeesController(HagerDbContext context, ApplicationDbContext identityContext, UserManager<IdentityUser> userManager)
		{
			_context = context;
			_identityContext = identityContext;
			_userManager = userManager;
		}

		// GET: Employees
		public async Task<IActionResult> Index(string SearchString, int? JobPositionID, bool showInactive,
			 int? page, int? pageSizeID, string actionButton, string sortDirection = "asc", string sortField = "Employee")
		{
			PopulateDropDownLists();    //Data for Filter DDL
			ViewData["Filtering"] = "";  //Assume not filtering

			var employees = from p in _context.Employees
				 .Include(p => p.JobPosition)
				 .Include(p => p.EmploymentType)
							select p;

			CookieHelper.CookieSet(HttpContext, "EmployeesURL", "", -1);

			//Add as many filters as needed
			if (JobPositionID.HasValue)
			{
				employees = employees.Where(p => p.JobPositionID == JobPositionID);
				ViewData["Filtering"] = " show";
			}

			if (!String.IsNullOrEmpty(SearchString))
			{
				employees = employees.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper())
									   || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
				ViewData["Filtering"] = " show";
			}

			if (showInactive == false)//If check box is not checked show only active employees
			{
				employees = employees.Where(p => p.Active == true);
			}
			else
			{
				ViewData["Filtering"] = " show";
			}
			//Before we sort, see if we have called for a change of filtering or sorting
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
			if (sortField == "First Name")
			{
				if (sortDirection == "asc")
				{
					employees = employees
						.OrderBy(p => p.FirstName);
				}
				else
				{
					employees = employees
						.OrderByDescending(p => p.FirstName);
				}
			}
			else if (sortField == "Last Name")
			{
				if (sortDirection == "asc")
				{
					employees = employees
						.OrderBy(p => p.LastName);

				}
				else
				{
					employees = employees
						.OrderByDescending(p => p.LastName);

				}
			}
			else if (sortField == "Email")
			{
				if (sortDirection == "asc")
				{
					employees = employees
						.OrderBy(p => p.Email);

				}
				else
				{
					employees = employees
						.OrderByDescending(p => p.Email);
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
			var pagedData = await PaginatedList<Employee>.CreateAsync(employees.AsNoTracking(), page ?? 1, pageSize);

			return View(pagedData);
		}


		// GET: Employees/Details/5
		public async Task<IActionResult> Details(int? id, string returnURL)
		{
			if (id == null)
			{
				return NotFound();
			}

			////Get the URL of the page that send us here
			//if (String.IsNullOrEmpty(returnURL))
			//{
			//	returnURL = Request.Headers["Referer"].ToString();
			//}
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

			var employee = await _context.Employees
				.Include(m => m.Country)
				.Include(m => m.Province)
				.Include(m => m.JobPosition)
				.Include(m => m.EmploymentType)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (employee == null)
			{
				return NotFound();
			}

			if (User.IsInRole("Admin"))
			{
				return View("DetailAdmin", employee);
			}
			else
			{
				return View(employee);
			}
		}

		// GET: Employee/Create
		[Authorize(Roles = "Admin")]
		public IActionResult Create()
		{
			PopulateDropDownLists();
			return View();
		}

		// POST: Employee/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[Authorize(Roles = "Admin")]
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,CountryID,ProvinceID,JobPositionID,EmploymentTypeID,AddressLine1,AddressLine2,City,PostalCode,CellPhone,HomePhone,DOB,Wage,Expense,DateJoined,Active,KeyFob,EmergencyContactName,EmergencyContactPhone,Notes")] Employee employee, bool register = false)
		{

			try
			{
				if (ModelState.IsValid)
				{
					if (!String.IsNullOrEmpty(employee.Email))
					{
						var acc = _identityContext.Users.Where(a => a.Email.ToLower() == employee.Email.ToLower()).FirstOrDefault();
						if (acc != null)
						{
							employee.IsUser = true;
						}
					}

					_context.Add(employee);
					await _context.SaveChangesAsync();
					if (register)
					{
						return RedirectToPage("/Account/Register", new { area = "Identity", email = employee.Email });
					}
					return RedirectToAction("Details", new { employee.ID });
				}
				else
				{
					ModelState.AddModelError("", "Model error occurred.");
				}
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}
			PopulateDropDownLists();
			return View(employee);
		}

		// GET: Employees/Edit/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int? id, string returnURL)
		{
			if (id == null)
			{
				return NotFound();
			}

			//Get the URL of the page that send us here
			if (String.IsNullOrEmpty(returnURL))
			{
				returnURL = Request.Headers["Referer"].ToString();
			}
			ViewData["returnURL"] = returnURL;

			var employee = await _context.Employees
				.Include(m => m.Country)
				.Include(m => m.Province)
				.Include(m => m.JobPosition)
				.Include(m => m.EmploymentType)
				.SingleOrDefaultAsync(e => e.ID == id);
			if (employee == null)
			{
				return NotFound();
			}
			PopulateDropDownLists(employee.Country, employee.Province, employee.JobPosition, employee.EmploymentType);
			if (!String.IsNullOrEmpty(employee.Email))
			{
				var user = await _userManager.FindByEmailAsync(employee.Email);
				if (user != null)
				{
					IList<String> roles = await _userManager.GetRolesAsync(user);
					ViewData["employeeRoles"] = roles;
				}
			}


			return View(employee);
		}

		// POST: Employees/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Edit(int id, bool IsUser, bool Active, string returnURL)
		{

			ViewData["returnURL"] = returnURL;

			var employeeToUpdate = await _context.Employees
				.Include(m => m.Country)
				.Include(m => m.Province)
				.Include(m => m.JobPosition)
				.Include(m => m.EmploymentType)
				.FirstOrDefaultAsync(m => m.ID == id);

			if (employeeToUpdate == null)
			{
				return NotFound();
			}

			//Check to see if you are making them inactive
			if (employeeToUpdate.IsUser == true && IsUser == false)
			{
				//This deletes the user's login from the security system
				await DeleteIdentityUser(employeeToUpdate.Email);
			}

			if (employeeToUpdate.Active == true && Active == false)
			{
				employeeToUpdate.DateInactive = DateTime.Now;
			}
			else if (employeeToUpdate.Active == false && Active == true)
			{
				employeeToUpdate.DateInactive = null;
			}

			if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
							c => c.FirstName, c => c.LastName, c => c.AddressLine1, c => c.AddressLine2,
							c => c.PostalCode, c => c.City, c => c.CellPhone, c => c.HomePhone, c => c.Email, c => c.EmergencyContactName,
							c => c.EmergencyContactPhone, c => c.Active, c => c.IsUser, c => c.KeyFob, c => c.JobPositionID,
							c => c.CountryID, c => c.ProvinceID, c => c.EmploymentTypeID))
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
						return RedirectToAction("Details", new { employeeToUpdate.ID, returnURL });
					}
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!EmployeeExists(employeeToUpdate.ID))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
			}
			PopulateDropDownLists(employeeToUpdate.Country, employeeToUpdate.Province, employeeToUpdate.JobPosition, employeeToUpdate.EmploymentType);
			return View(employeeToUpdate);
		}

		// GET: Employees/Delete/5
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");

			var employee = await _context.Employees
				.Include(m => m.Country)
				.Include(m => m.Province)
				.Include(m => m.JobPosition)
				.Include(m => m.EmploymentType)
				.FirstOrDefaultAsync(m => m.ID == id);
			if (employee == null)
			{
				return NotFound();
			}

			return View(employee);
		}

		// POST: Employees/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		[Authorize(Roles = "Admin")]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Employees");
			var employee = await _context.Employees.FindAsync(id);
			if (employee.IsUser && !String.IsNullOrEmpty(employee.Email))
			{
				//This deletes the user's login from the security system
				await DeleteIdentityUser(employee.Email);
			}
			_context.Employees.Remove(employee);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		[HttpPost]
		[Authorize(Roles = "Admin")]
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

				List<Employee> employees = new List<Employee>();
				StringBuilder sb = new StringBuilder();

				int totalRows = end.Row - start.Row;

				for (int row = start.Row + 1; row <= end.Row; row++)
				{
					try
					{
						Employee e = new Employee();
						if (_context.Employees.Any(e => e.FirstName == workSheet.Cells[row, 1].Text &&
							e.LastName == workSheet.Cells[row, 2].Text &&
							e.AddressLine1 == workSheet.Cells[row, 9].Text &&
							e.AddressLine2 == workSheet.Cells[row, 10].Text))
						{
							throw new Exception("Name at this address exists within db already: " + workSheet.Cells[row, 1].Text + " " + workSheet.Cells[row, 2].Text + " at: " + workSheet.Cells[row, 9].Text);
						}
						else
						{
							e.FirstName = workSheet.Cells[row, 1].Text;
							e.LastName = workSheet.Cells[row, 2].Text;
							e.AddressLine1 = workSheet.Cells[row, 9].Text;
							e.AddressLine2 = workSheet.Cells[row, 10].Text;
						}

						if (_context.Employees.Any(e => e.Email == workSheet.Cells[row, 3].Text))
						{
							throw new Exception("Email exists within db already: " + workSheet.Cells[row, 3].Text);
						}
						else
						{
							e.Email = String.IsNullOrEmpty(workSheet.Cells[row, 3].Text) ? null : workSheet.Cells[row, 3].Text;
						}

						e.HomePhone = Regex.Replace(workSheet.Cells[row, 4].Text, "[^0-9]", "");
						e.CellPhone = Regex.Replace(workSheet.Cells[row, 5].Text, "[^0-9]", "");
						e.DOB = String.IsNullOrEmpty(workSheet.Cells[row, 6].Text) ? (DateTime?)null : DateTime.Parse(workSheet.Cells[row, 6].Text);
						e.Wage = String.IsNullOrEmpty(workSheet.Cells[row, 7].Text) ? 0m : Decimal.Parse(workSheet.Cells[row, 7].Text);
						e.Expense = String.IsNullOrEmpty(workSheet.Cells[row, 8].Text) ? 0m : Decimal.Parse(workSheet.Cells[row, 8].Text);
						e.City = workSheet.Cells[row, 11].Text;
						e.PostalCode = workSheet.Cells[row, 12].Text;
						e.ProvinceID = _context.Provinces.FirstOrDefault(p => p.Name.Contains(workSheet.Cells[row, 13].Text))?.ID;
						e.Province = _context.Provinces.FirstOrDefault(p => p.Name.Contains(workSheet.Cells[row, 13].Text));
						e.CountryID = _context.Countries.FirstOrDefault(c => c.Name.Contains(workSheet.Cells[row, 14].Text))?.ID;
						e.Country = _context.Countries.FirstOrDefault(c => c.Name.Contains(workSheet.Cells[row, 14].Text));


						// If Job Position column not empty
						if (!String.IsNullOrEmpty(workSheet.Cells[row, 15].Text))
						{
							// Try to get job position id
							int? jobPosID = _context.JobPositions.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 15].Text.ToLower()))?.ID;
							// Column not empty, but no match found
							if (jobPosID == null)
							{
								sb.AppendLine("#########################################");
								sb.AppendLine("Job Position: " + workSheet.Cells[row, 15].Text + " not found.");
								sb.AppendLine("NON FATAL ERROR, ROW NUMBER: " + row);
							}
							else
							{
								// Assign found id
								e.JobPositionID = jobPosID;
							}
						}
						//e.JobPositionID = _context.JobPositions.FirstOrDefault(p => p.Name.Contains(workSheet.Cells[row, 15].Text))?.ID;
						//e.JobPosition = _context.JobPositions.FirstOrDefault(p => p.Name.Contains(workSheet.Cells[row, 15].Text));


						// If Employment Type column not empty
						if (!String.IsNullOrEmpty(workSheet.Cells[row, 16].Text))
						{
							// Try to get employment type id
							int? empTypeID = _context.EmploymentTypes.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 16].Text.ToLower()))?.ID;
							// Column not empty, but no match found
							if (empTypeID == null)
							{
								sb.AppendLine("#########################################");
								sb.AppendLine("Employment Type: " + workSheet.Cells[row, 16].Text + " not found.");
								sb.AppendLine("NON FATAL ERROR, ROW NUMBER: " + row);
							}
							else
							{
								// Assign found id
								e.EmploymentTypeID = empTypeID;
							}
						}
						//e.EmploymentTypeID = _context.EmploymentTypes.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 16].Text.ToLower()))?.ID;
						//e.EmploymentType = _context.EmploymentTypes.FirstOrDefault(p => p.Name.ToLower().Contains(workSheet.Cells[row, 16].Text.ToLower()));


						e.DateJoined = String.IsNullOrEmpty(workSheet.Cells[row, 17].Text) ? (DateTime?)null : DateTime.Parse(workSheet.Cells[row, 17].Text);
						e.Active = (workSheet.Cells[row, 20].Text == "1");
						e.EmergencyContactName = workSheet.Cells[row, 22].Text;
						e.EmergencyContactPhone = Regex.Replace(workSheet.Cells[row, 23].Text, "[^0-9]", "");
						e.KeyFob = workSheet.Cells[row, 24].Text;
						var acc = _identityContext.Users.Where(a => a.Email == e.Email).FirstOrDefault();
						if (acc != null)
						{
							e.IsUser = true;
						}
						else
						{
							e.IsUser = false;
						}

						employees.Add(e);
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
				int before = _context.Employees.Count();
				_context.Employees.AddRange(employees);
				_context.SaveChanges();
				int rows = _context.Employees.Count() - before;
				TempData["Success"] = rows.ToString() + "/" + totalRows.ToString() + " inserted.";


			}

			return RedirectToAction("Index", "Employees");
		}

		public IActionResult GetImportLog()
		{
			this.Response.Headers["content-disposition"] = "attachment;filename=ImportLog.txt";
			return File(Encoding.UTF8.GetBytes(TempData["ImportErrors"].ToString()), "text/plain");
		}

		//Delete GET/POST was deleted as it was not neccessary

		private async Task DeleteIdentityUser(string Email)
		{
			var userToDelete = await _identityContext.Users.Where(u => u.Email == Email).FirstOrDefaultAsync();
			if (userToDelete != null)
			{
				_identityContext.Users.Remove(userToDelete);
				await _identityContext.SaveChangesAsync();
			}
		}
		private SelectList CountrySelectList(int? id)
		{
			var dQuery = from c in _context.Countries
						 orderby c.CountryPreference
						 select c;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private SelectList ProvinceSelectList(int? CountryID, int? selectedId)
		{
			var query = from p in _context.Provinces.Include(c => c.Country)
						select p;
			if (CountryID.HasValue)
			{
				query = query.Where(c => c.CountryID == CountryID);
			}
			return new SelectList(query.OrderBy(p => p.Name), "ID", "Name", selectedId);
		}

		private SelectList JobPositionSelectList(int? id)
		{
			var dQuery = from j in _context.JobPositions
						 orderby j.JobPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private SelectList EmploymentTypeSelectList(int? id)
		{
			var dQuery = from j in _context.EmploymentTypes
						 orderby j.EmploymentPreference
						 select j;
			return new SelectList(dQuery, "ID", "Name", id);
		}

		private void PopulateDropDownLists(Country country = null, Province province = null, JobPosition job = null, EmploymentType employment = null)

		{
			ViewData["CountryID"] = CountrySelectList(country?.ID);
			ViewData["ProvinceID"] = ProvinceSelectList(country?.ID, province?.ID);
			ViewData["JobPositionID"] = JobPositionSelectList(job?.ID);
			ViewData["EmploymentTypeID"] = EmploymentTypeSelectList(employment?.ID);
		}

		[HttpGet]
		public JsonResult GetProvinces(int? ID)
		{
			return Json(ProvinceSelectList(ID, null));
		}

		private bool EmployeeExists(int id)
		{
			return _context.Employees.Any(e => e.ID == id);
		}
	}
}
