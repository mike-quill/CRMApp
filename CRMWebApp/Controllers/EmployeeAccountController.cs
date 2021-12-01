using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMWebApp.Data;
using CRMWebApp.Models;
using CRMWebApp.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CRMWebApp.Controllers
{
    [Authorize]
    public class EmployeeAccountController : Controller
    {
        //Specialized controller just used to allow an 
        //Authenticated user to maintain their own  account details.

        private readonly HagerDbContext _context;

        public EmployeeAccountController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: EmployeeAccount
        public IActionResult Index()
        {
            return RedirectToAction(nameof(Details));
        }

        // GET: EmployeeAccount/Details/5
        public async Task<IActionResult> Details()
        {

            var employee = await _context.Employees
               .Include(c => c.Country)
               .Include(c => c.Province)
               .Include(c=>c.JobPosition)
               .Where(c => c.Email == User.Identity.Name)
               .FirstOrDefaultAsync();
            if (employee == null)
            {
                return RedirectToAction(nameof(Create));
            }
            PopulateDropDownLists();
            return View(employee);
        }

        // GET: EmployeeAccount/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: EmployeeAccount/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,AddressLine1,AddressLine2,PostalCode,CellPhone,HomePhone,EmergencyContactName,EmergencyContactPhone,Email,CountryID,ProvinceID,JobPositionID")] Employee employee)
        {

            employee.Email = User.Identity.Name;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employee);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(employee.FullName);
                    return RedirectToAction(nameof(Details));
                } else
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

        // GET: EmployeeAccount/Edit/5
        public async Task<IActionResult> Edit()
        {
            var employee = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Province)
                .Where(c => c.Email == User.Identity.Name)
                .FirstOrDefaultAsync();
            if (employee == null)
            {
                return RedirectToAction(nameof(Create));
            }
            PopulateDropDownLists();
            return View(employee);
        }

        // POST: EmployeeAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)
        {
            var employeeToUpdate = await _context.Employees
                .Include(e => e.Country)
                .Include(e => e.Province)
                .FirstOrDefaultAsync(m => m.ID == id);

            if (await TryUpdateModelAsync<Employee>(employeeToUpdate, "",
                c => c.FirstName, c => c.LastName, c => c.AddressLine1, c => c.AddressLine2,
                c => c.PostalCode, c => c.CellPhone, c => c.HomePhone, c => c.EmergencyContactName,
                c => c.EmergencyContactPhone, c => c.CountryID, c => c.ProvinceID, c => c.JobPosition))
            {
                try
                {
                    _context.Update(employeeToUpdate);
                    await _context.SaveChangesAsync();
                    UpdateUserNameCookie(employeeToUpdate.FullName);
                    return RedirectToAction(nameof(Details));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employeeToUpdate.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Unable to save changes. The record you attempted to edit "
                                + "was modified by another user after you received your values.  You need to go back and try your edit again.");
                    }
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Something went wrong in the database.");
                }
            }
            PopulateDropDownLists(employeeToUpdate.Country, employeeToUpdate.Province);
            return View(employeeToUpdate);

        }

        //// GET: EmployeeAccount/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var employee = await _context.Employees
        //        .FirstOrDefaultAsync(m => m.ID == id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(employee);
        //}

        //// POST: EmployeeAccount/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var employee = await _context.Employees.FindAsync(id);
        //    _context.Employees.Remove(employee);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        private void UpdateUserNameCookie(string userName)
        {
            CookieHelper.CookieSet(HttpContext, "userName", userName, 960);
        }

        private SelectList CountrySelectList(int? id)
        {
            var dQuery = from c in _context.Countries
                         orderby c.CountryPreference
                         select c;
            return new SelectList(dQuery, "ID", "Name", id);
        }

        private SelectList ProvinceSelectList(int? id)
        {
            var dQuery = from p in _context.Provinces
                         orderby p.ProvincePreference
                         select p;
            return new SelectList(dQuery, "ID", "Name", id);
        }

        private SelectList JobPositionSelectList(int? id)
        {
            var dQuery = from j in _context.JobPositions
                         orderby j.JobPreference
                         select j;
            return new SelectList(dQuery, "ID", "Name", id);
        }

        private void PopulateDropDownLists(Country country = null, Province province = null, JobPosition job = null)
        {
            ViewData["CountryID"] = CountrySelectList(country?.ID);
            ViewData["ProvinceID"] = ProvinceSelectList(province?.ID);
            ViewData["JobPositionID"] = JobPositionSelectList(job?.ID);
        }

        private bool EmployeeExists(int id)
        {
            return _context.Employees.Any(e => e.ID == id);
        }
    }
}
