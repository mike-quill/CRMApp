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
    public class CategoriesController : Controller
    {
        private readonly HagerDbContext _context;

        public CategoriesController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: Categories
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
        }

        public IActionResult Sort()
        {
            return View(_context.Categories.OrderBy(p => p.CategoryPreference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] catId)
        {
            int newpreference = 1;
            foreach (int id in catId)
            {
                var categories = _context.Categories.Find(id);
                categories.CategoryPreference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(category);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] Category category)
        {
            var categoryToUpdate = await _context.Categories
               .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (categoryToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<Category>(categoryToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CategoryExists(categoryToUpdate.ID))
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
            return View(categoryToUpdate);
        }

        // GET: Categories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _context.Categories
                .FirstOrDefaultAsync(m => m.ID == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            try
            {
                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "CategoriesTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Category.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(category);
        }


        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.ID == id);
        }
    }
}
