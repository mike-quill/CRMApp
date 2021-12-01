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
    public class CurrenciesController : Controller
    {
        private readonly HagerDbContext _context;

        public CurrenciesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: Currencies
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
        }
        //public IActionResult Sort()
        //{
        //    return View(_context.Currencies.OrderBy(p => p.CurrencyPreference).ToList());
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public IActionResult Sort(int[] currencyId)
        //{
        //    int newpreference = 1;
        //    foreach (int id in currencyId)
        //    {
        //        var currency = _context.Currencies.Find(id);
        //        currency.CurrencyPreference = newpreference;
        //        _context.SaveChanges();
        //        newpreference += 1;
        //    }
        //    return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
        //}

        // GET: Currencies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // GET: Currencies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Currencies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Currency currency)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(currency);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(currency);
        }

        // GET: Currencies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies.FindAsync(id);
            if (currency == null)
            {
                return NotFound();
            }
            return View(currency);
        }

        // POST: Currencies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] Currency currency)
        {
            var currencyToUpdate = await _context.Currencies
              .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (currencyToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Currency>(currencyToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CurrencyExists(currencyToUpdate.ID))
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
            return View(currencyToUpdate);
        }

        // GET: Currencies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var currency = await _context.Currencies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (currency == null)
            {
                return NotFound();
            }

            return View(currency);
        }

        // POST: Currencies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var currency = await _context.Currencies.FindAsync(id);
            try
            {
                _context.Currencies.Remove(currency);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "CurrenciesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Currency.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(currency);
        }

        private bool CurrencyExists(int id)
        {
            return _context.Currencies.Any(e => e.ID == id);
        }
    }
}
