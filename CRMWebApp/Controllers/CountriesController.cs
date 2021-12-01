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
    public class CountriesController : Controller
    {
        private readonly HagerDbContext _context;

        public CountriesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: Countries
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "CountriesTab" });
        }

        //public IActionResult Sort()
        //{
        //    return View(_context.Countries.OrderBy(p => p.CountryPreference).ToList());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Sort(int[] countryId)
        //{
        //    int newpreference = 1;
        //    foreach (int id in countryId)
        //    {
        //        var country = _context.Countries.Find(id);
        //        country.CountryPreference = newpreference;
        //        _context.SaveChanges();
        //        newpreference += 1;
        //    }
        //    return RedirectToAction("Index", "Lookups", new { Tab = "CountriesTab" });
        //}

        // GET: Countries/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // GET: Countries/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Countries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Country country)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(country);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CountriesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(country);
        }

        // GET: Countries/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries.FindAsync(id);
            if (country == null)
            {
                return NotFound();
            }
            return View(country);
        }

        // POST: Countries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] Country country)
        {
            var countryToUpdate = await _context.Countries
                .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (countryToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Country>(countryToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CountriesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CountryExists(countryToUpdate.ID))
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
            return View(countryToUpdate);
        }

        // GET: Countries/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var country = await _context.Countries
                .FirstOrDefaultAsync(m => m.ID == id);
            if (country == null)
            {
                return NotFound();
            }

            return View(country);
        }

        // POST: Countries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var country = await _context.Countries.FindAsync(id);
            try
            {
                _context.Countries.Remove(country);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "CountriesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Country.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(country);
        }

        private bool CountryExists(int id)
        {
            return _context.Countries.Any(e => e.ID == id);
        }
    }
}
