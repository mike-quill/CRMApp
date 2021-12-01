using CRMWebApp.Data;
using CRMWebApp.Models;
using CRMWebApp.Utility;
using CRMWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Controllers
{
	[Authorize]
    public class ContactsController : Controller
    {
        private readonly HagerDbContext _context;
        private readonly ApplicationDbContext _identityContext;

        public ContactsController(HagerDbContext context, ApplicationDbContext identityContext)
        {
            _context = context;
            _identityContext = identityContext;
        }

        // GET: Contacts
        public async Task<IActionResult> Index(string SearchString, string SearchStringCompany, int? companyID, int? categoryID, int? page, int? pageSizeID, bool showInactive, bool showCustomer, bool showVendor, bool showContractor,
            string actionButton, string sortDirection = "asc", string sortField = "Contact")
        {
            PopulateDropDownLists();    //Data for Filter DDL
            ViewData["Filtering"] = "";  //Assume not filtering

            var contacts = from p in _context.Contacts
                .Include(c => c.Company)
                .Include(c => c.ContactCategories)
                .ThenInclude(c => c.Category)
                select p;

            CookieHelper.CookieSet(HttpContext, "ContactsURL", "", -1);

            //Add as many filters as needed
            if (companyID.HasValue)
            {
                contacts = contacts.Where(p => p.CompanyID == companyID);
                ViewData["Filtering"] = " show";
            }

            if(categoryID.HasValue)
            {
                contacts = contacts.Where(p=>p.ContactCategories.Any(x=>x.CategoryID == categoryID));
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(SearchString))
            {
                contacts = contacts.Where(p => p.LastName.ToUpper().Contains(SearchString.ToUpper())
                       || p.FirstName.ToUpper().Contains(SearchString.ToUpper()));
                ViewData["Filtering"] = " show";
            }

            if (!String.IsNullOrEmpty(SearchStringCompany))
            {
                contacts = contacts.Where(p => p.Company.Name.ToUpper().Contains(SearchStringCompany.ToUpper()));
                ViewData["Filtering"] = " show";
            }


            if (showInactive == false)//If check box is not checked show only active contacts
            {
                contacts = contacts.Where(p => p.Active == true);
            }
            else
            {
                ViewData["Filtering"] = " show";
            }

            if (showCustomer == true)
            {
                ViewData["Filtering"] = " show";
                contacts = contacts.Where(p => p.Company.CompanyCustomerTypes.Count > 0);
            }

            if (showVendor == true)
            {
                ViewData["Filtering"] = " show";
                contacts = contacts.Where(p => p.Company.CompanyVendorTypes.Count > 0);
            }

            if (showContractor == true)
            {
                ViewData["Filtering"] = " show";
                contacts = contacts.Where(p => p.Company.CompanyContractorTypes.Count > 0);
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
            if (sortField == "First Name")
            {
                if (sortDirection == "asc")
                {
                    contacts = contacts
                        .OrderBy(p => p.FirstName);
                }
                else
                {
                    contacts = contacts
                        .OrderByDescending(p => p.FirstName);
                }
            }
            if (sortField == "Last Name")
            {
                if (sortDirection == "asc")
                {
                    contacts = contacts
                        .OrderBy(p => p.LastName);
                }
                else
                {
                    contacts = contacts
                        .OrderByDescending(p => p.LastName);
                        
                }
            }
            if (sortField == "Job Title")
            {
                if (sortDirection == "asc")
                {
                    contacts = contacts
                        .OrderBy(p => p.JobTitle);
                }
                else
                {
                    contacts = contacts
                        .OrderByDescending(p => p.JobTitle);
                        
                }
            }
            if (sortField == "Company")
            {
                if (sortDirection == "asc")
                {
                    contacts = contacts
                        .OrderBy(p => p.Company);
                }
                else
                {
                    contacts = contacts
                        .OrderByDescending(p => p.Company);
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
            var pagedData = await PaginatedList<Contact>.CreateAsync(contacts.AsNoTracking(), page ?? 1, pageSize);

            return View(pagedData);
        }

        // GET: Contacts/Details/5
        public async Task<IActionResult> Details(int? id, string returnURL)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(c => c.Company)
                .Include(c=>c.ContactCategories).ThenInclude(m=>m.Category)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            if (User.IsInRole("Admin"))
            {
                return View("DetailAdmin", contact);
            }
            else
            {
                return View(contact);
            };
        }

        
        public JsonResult GetCompanies(string term)
        {
            var result = (from p in _context.Companies.OrderBy(p => p.Name)
                          where (p.Name + (String.IsNullOrEmpty(p.Location) ? "" : " - " + p.Location)).StartsWith(term)
                          select new { label = p.Summary, id= p.ID}).ToList();
            return Json(result);
        }

        // GET: Contacts/Create
        [Authorize(Roles = "Admin, Supervisor")]
        public IActionResult Create(int? companyID)
        {

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            Contact contact = new Contact();
            
            if(companyID != null)
            {
                var company = _context.Companies.Where(a => a.ID == companyID).FirstOrDefault();
                PopulateDropDownLists(companyID.Value);
                contact.CompanyID = companyID.Value;
                contact.Company = company;
                
            }
            else
            {
                PopulateDropDownLists();
            }
            PopulateAssignedCategoryData(contact);
            return View(contact);
        }



        // POST: Contacts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,JobTitle,CellPhone,WorkPhone,Email,Active,Notes,CompanyID")] Contact contact, string[] selectedOptions, string finish, string add)
        {

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            try
            {
                UpdateContactCategories(selectedOptions, contact);
                if (ModelState.IsValid)
                {
                    _context.Add(contact);
                    await _context.SaveChangesAsync();
                    if(!String.IsNullOrEmpty(finish))
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    if (!String.IsNullOrEmpty(add))
                    {
                        return RedirectToAction("Create", "Contacts", new { companyID = contact.CompanyID });
                    }
                }
            }
            catch (RetryLimitExceededException /* dex */)
            {
                ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateDropDownLists();
            PopulateAssignedCategoryData(contact);
            return View(contact);
        }

        // GET: Contacts/Edit/5
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> Edit(int? id, string returnURL)
        {
            if (id == null)
            {
                return NotFound();
            }

            //Get the URL of the page that send us here
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(m => m.Company)
                .Include(c => c.ContactCategories)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(e => e.ID == id);
            if (contact == null)
            {
                return NotFound();
            }
            PopulateAssignedCategoryData(contact);
            PopulateDropDownLists(contact.Company.ID);
            return View(contact);
        }

        // POST: Contacts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> Edit(int id, bool IsUser, bool Active, string returnURL, string[] selectedOptions) //[Bind("ID,FirstName,LastName,JobTitle,CellPhone,WorkPhone,Email,Active,Notes,CompanyID")] Contact contact)
        {

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contactToUpdate = await _context.Contacts
                .Include(m => m.Company)
                .Include(c => c.ContactCategories)
                .ThenInclude(c => c.Category)
                .SingleOrDefaultAsync(e => e.ID == id);

            if (contactToUpdate == null)
            {
                return NotFound();
            }

            UpdateContactCategories(selectedOptions, contactToUpdate);

            if (await TryUpdateModelAsync<Contact>(contactToUpdate, "", 
                c => c.FirstName, 
                c => c.LastName,
                c => c.JobTitle,
                c => c.CellPhone,
                c => c.WorkPhone,
                c => c.Email,
                c => c.Active,
                c => c.Notes,
                c => c.CompanyID
                ))
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
                        return RedirectToAction("Details", new { contactToUpdate.ID });
                    }
                }
                catch (RetryLimitExceededException /* dex */)
                {
                    ModelState.AddModelError("", "Unable to save changes after multiple attempts. Try again, and if the problem persists, see your system administrator.");
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ContactExists(contactToUpdate.ID))
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
            PopulateAssignedCategoryData(contactToUpdate);
            PopulateDropDownLists(contactToUpdate.Company.ID);
            return View(contactToUpdate);
        }

        // GET: Contacts/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");

            var contact = await _context.Contacts
                .Include(c => c.Company)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (contact == null)
            {
                return NotFound();
            }

            return View(contact);
        }

        // POST: Contacts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            ViewData["returnURL"] = MaintainURL.ReturnURL(HttpContext, "Contacts");
            var contact = await _context.Contacts.FindAsync(id);
            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ContactExists(int id)
        {
            return _context.Contacts.Any(e => e.ID == id);
        }

        private SelectList ContactCategorySelectList(int? id)
        {
            var dQuery = from j in _context.Categories
                         orderby j.CategoryPreference
                         select j;
            return new SelectList(dQuery, "ID", "Name", id);
        }
        private SelectList CompanySelectList(int? id)
        {
            var dQuery = from j in _context.Companies
                         orderby j.Name
                         select j;
            return new SelectList(dQuery, "ID", "Summary", id);
        }

        private void PopulateDropDownLists(int companyID = 0)
        {
            ViewData["CompanyID"] = CompanySelectList(companyID);
            ViewData["CategoryID"] = ContactCategorySelectList(null);
        }

        private void PopulateAssignedCategoryData(Contact contact)
        {
            var allOptions = _context.Categories;
            var currentOptionsHS = new HashSet<int>(contact.ContactCategories.Select(b => b.CategoryID));
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
                        Preference = s.CategoryPreference
                    });
                }
                else
                {
                    available.Add(new ListOptionVM
                    {
                        ID = s.ID,
                        DisplayText = s.Name,
						Preference = s.CategoryPreference
                    });
                }
            }

            ViewData["selOpts"] = new MultiSelectList(selected.OrderBy(s => s.Preference), "ID", "DisplayText");
            ViewData["availOpts"] = new MultiSelectList(available.OrderBy(s => s.Preference), "ID", "DisplayText");
        }
        private void UpdateContactCategories(string[] selectedOptions, Contact contactToUpdate)
        {
            if (selectedOptions == null)
            {
                contactToUpdate.ContactCategories = new List<ContactCategory>();
                return;
            }

            var selectedOptionsHS = new HashSet<string>(selectedOptions);
            var currentOptionsHS = new HashSet<int>(contactToUpdate.ContactCategories.Select(b => b.CategoryID));
            foreach (var c in _context.Categories)
            {
                if (selectedOptionsHS.Contains(c.ID.ToString()))
                {
                    if (!currentOptionsHS.Contains(c.ID))
                    {
                        contactToUpdate.ContactCategories.Add(new ContactCategory
                        {
                            CategoryID = c.ID,
                            ContactID = contactToUpdate.ID
                        });
                    }
                }
                else
                {
                    if (currentOptionsHS.Contains(c.ID))
                    {
                        ContactCategory categoryToRemove = contactToUpdate.ContactCategories.SingleOrDefault(d => d.CategoryID == c.ID);
                        _context.Remove(categoryToRemove);
                    }
                }
            }
        }


    }
}
