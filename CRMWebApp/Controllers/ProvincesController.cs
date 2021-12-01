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
    public class ProvincesController : Controller
    {
        private readonly HagerDbContext _context;

        public ProvincesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: Provinces
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
        }
        //public IActionResult Sort()
        //{
        //    return View(_context.Provinces.OrderBy(p => p.ProvincePreference).ToList());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Sort(int[] provinceId)
        //{
        //    int newpreference = 1;
        //    foreach (int id in provinceId)
        //    {
        //        var province = _context.Provinces.Find(id);
        //        province.ProvincePreference = newpreference;
        //        _context.SaveChanges();
        //        newpreference += 1;
        //    }
        //    return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
        //}

        // GET: Provinces/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // GET: Provinces/Create
        public IActionResult Create()
        {
            PopulateDropDownLists();
            return View();
        }

        // POST: Provinces/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name,CountryID")] Province province)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(province);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            PopulateDropDownLists();
            return View(province);
        }

        // GET: Provinces/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces.FindAsync(id);
            if (province == null)
            {
                return NotFound();
            }
            PopulateDropDownLists();
            return View(province);
        }

        // POST: Provinces/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] Province province)
        {
            var provinceToUpdate = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (provinceToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Province>(provinceToUpdate, "",
                d => d.Name, d => d.CountryID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProvinceExists(provinceToUpdate.ID))
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
            PopulateDropDownLists();
            return View(provinceToUpdate);
        }

        // GET: Provinces/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var province = await _context.Provinces
                .Include(p => p.Country)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (province == null)
            {
                return NotFound();
            }

            return View(province);
        }

        // POST: Provinces/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var province = await _context.Provinces.FindAsync(id);
            try
            {
                _context.Provinces.Remove(province);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "ProvincesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Province.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(province);
        }

        private SelectList CountrySelectList(int? id)
        {
            var dQuery = from c in _context.Countries
                         orderby c.CountryPreference
                         select c;
            return new SelectList(dQuery, "ID", "Name", id);
        }

        private void PopulateDropDownLists(Country country = null)

        {
            ViewData["CountryID"] = CountrySelectList(country?.ID);
        }


        private bool ProvinceExists(int id)
        {
            return _context.Provinces.Any(e => e.ID == id);
        }
    }
}
