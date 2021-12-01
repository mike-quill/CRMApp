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
    public class BillingTermsController : Controller
    {
        private readonly HagerDbContext _context;

        public BillingTermsController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: BillingTerms
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
        }
        public IActionResult Sort()
        {
            return View(_context.BillingTerms.OrderBy(p => p.BillingPreference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] termId)
        {
            int newpreference = 1;
            foreach (int id in termId)
            {
                var billingTerm = _context.BillingTerms.Find(id);
                billingTerm.BillingPreference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
        }

        // GET: BillingTerms/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billingTerm == null)
            {
                return NotFound();
            }

            return View(billingTerm);
        }

        // GET: BillingTerms/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: BillingTerms/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] BillingTerm billingTerm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(billingTerm);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(billingTerm);
        }

        // GET: BillingTerms/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms.FindAsync(id);
            if (billingTerm == null)
            {
                return NotFound();
            }
            return View(billingTerm);
        }

        // POST: BillingTerms/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] BillingTerm billingTerm)
        {
            var billingTermToUpdate = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (billingTermToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<BillingTerm>(billingTermToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BillingTermExists(billingTermToUpdate.ID))
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
            return View(billingTermToUpdate);
        }

        // GET: BillingTerms/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var billingTerm = await _context.BillingTerms
                .FirstOrDefaultAsync(m => m.ID == id);
            if (billingTerm == null)
            {
                return NotFound();
            }

            return View(billingTerm);
        }

        // POST: BillingTerms/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var billingTerm = await _context.BillingTerms.FindAsync(id);
            try
            {
                _context.BillingTerms.Remove(billingTerm);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "BillingTermsTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Billing Term.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(billingTerm);
        }

        private bool BillingTermExists(int id)
        {
            return _context.BillingTerms.Any(e => e.ID == id);
        }
    }
}
