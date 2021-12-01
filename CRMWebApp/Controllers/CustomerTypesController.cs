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
    public class CustomerTypesController : Controller
    {
        private readonly HagerDbContext _context;

        public CustomerTypesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: CustomerTypes
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "CustomerTypesTab" });
        }

        public IActionResult Sort()
        {
            return View(_context.CustomerTypes.OrderBy(p => p.Preference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] customerId)
        {
            int newpreference = 1;
            foreach (int id in customerId)
            {
                var customer = _context.CustomerTypes.Find(id);
                customer.Preference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "CustomerTypesTab" });
        }
        // GET: CustomerTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        // GET: CustomerTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomerTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin, Supervisor")]
        public async Task<IActionResult> Create([Bind("ID,Name")] CustomerType customerType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(customerType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CustomerTypeTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(customerType);
        }

        // GET: CustomerTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes.FindAsync(id);
            if (customerType == null)
            {
                return NotFound();
            }
            return View(customerType);
        }

        // POST: CustomerTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] CustomerType customerType)
        {
            var customerTypeToUpdate = await _context.CustomerTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (customerTypeToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<CustomerType>(customerTypeToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CustomerTypesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerTypeExists(customerTypeToUpdate.ID))
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
            return View(customerTypeToUpdate);
        }

        // GET: CustomerTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customerType = await _context.CustomerTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (customerType == null)
            {
                return NotFound();
            }

            return View(customerType);
        }

        // POST: CustomerTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customerType = await _context.CustomerTypes.FindAsync(id);
            try
            {
                _context.CustomerTypes.Remove(customerType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "CustomerTypesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Customer Type.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(customerType);
        }

        private bool CustomerTypeExists(int id)
        {
            return _context.CustomerTypes.Any(e => e.ID == id);
        }
    }
}
