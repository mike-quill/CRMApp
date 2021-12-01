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

namespace CRMWebApp.Controllers
{
    [Authorize(Roles = "Admin, Supervisor")]
    public class EmploymentTypesController : Controller
    {
        private readonly HagerDbContext _context;

        public EmploymentTypesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: EmploymentTypes
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "EmploymentTypesTab" });
        }

        public IActionResult Sort()
        {
            return View(_context.EmploymentTypes.OrderBy(p => p.EmploymentPreference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] employmentId)
        {
            int newpreference = 1;
            foreach (int id in employmentId)
            {
                var employment = _context.EmploymentTypes.Find(id);
                employment.EmploymentPreference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "EmploymentTypesTab" });
        }

        // GET: EmploymentTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentType = await _context.EmploymentTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employmentType == null)
            {
                return NotFound();
            }

            return View(employmentType);
        }

        // GET: EmploymentTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: EmploymentTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] EmploymentType employmentType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(employmentType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "EmploymentTypesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(employmentType);
        }

        // GET: EmploymentTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentType = await _context.EmploymentTypes.FindAsync(id);
            if (employmentType == null)
            {
                return NotFound();
            }
            return View(employmentType);
        }

        // POST: EmploymentTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] EmploymentType employmentType)
        {
            var employmentTypeToUpdate = await _context.EmploymentTypes
               .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (employmentTypeToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<EmploymentType>(employmentTypeToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "EmploymentTypesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmploymentTypeExists(employmentTypeToUpdate.ID))
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
            return View(employmentTypeToUpdate);
        }

        // GET: EmploymentTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employmentType = await _context.EmploymentTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (employmentType == null)
            {
                return NotFound();
            }

            return View(employmentType);
        }

        // POST: EmploymentTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var employmentType = await _context.EmploymentTypes.FindAsync(id);
            try
            {
                _context.EmploymentTypes.Remove(employmentType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "EmploymentTypesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Employment Type");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(employmentType);
        }

        private bool EmploymentTypeExists(int id)
        {
            return _context.EmploymentTypes.Any(e => e.ID == id);
        }
    }
}
