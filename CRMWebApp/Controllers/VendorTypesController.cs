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
    public class VendorTypesController : Controller
    {
        private readonly HagerDbContext _context;

        public VendorTypesController(HagerDbContext context)
        {
            _context = context;
        }
        public IActionResult Sort()
        {
            return View(_context.VendorTypes.OrderBy(p => p.Preference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] vendorId)
        {
            int newpreference = 1;
            foreach (int id in vendorId)
            {
                var vendor = _context.VendorTypes.Find(id);
                vendor.Preference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "VendorTypesTab" });
        }
        // GET: VendorTypes
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "VendorTypesTab" });
        }

        // GET: VendorTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }

        // GET: VendorTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: VendorTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] VendorType vendorType)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(vendorType);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "VendorTypesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(vendorType);
        }

        // GET: VendorTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes.FindAsync(id);
            if (vendorType == null)
            {
                return NotFound();
            }
            return View(vendorType);
        }

        // POST: VendorTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] VendorType vendorType)
        {
            var vendorTypeToUpdate = await _context.VendorTypes
               .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (vendorTypeToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<VendorType>(vendorTypeToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "VendorTypesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VendorTypeExists(vendorTypeToUpdate.ID))
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
            return View(vendorTypeToUpdate);
        }

        // GET: VendorTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vendorType = await _context.VendorTypes
                .FirstOrDefaultAsync(m => m.ID == id);
            if (vendorType == null)
            {
                return NotFound();
            }

            return View(vendorType);
        }

        // POST: VendorTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vendorType = await _context.VendorTypes.FindAsync(id);
            try
            {
                _context.VendorTypes.Remove(vendorType);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "VendorTypesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Vendor Type");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(vendorType);
        }

        private bool VendorTypeExists(int id)
        {
            return _context.VendorTypes.Any(e => e.ID == id);
        }
    }
}
