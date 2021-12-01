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
    public class JobPositionsController : Controller
    {
        private readonly HagerDbContext _context;

        public JobPositionsController(HagerDbContext context)
        {
            _context = context;
        }

        // GET: JobPositions
        public IActionResult Index()
        {
            return RedirectToAction("Index", "Lookups", new { Tab = "JobPositionsTab" });
        }

        public IActionResult Sort()
        {
            return View(_context.JobPositions.OrderBy(p => p.JobPreference).ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Sort(int[] jobId)
        {
            int newpreference = 1;
            foreach (int id in jobId)
            {
                var job = _context.JobPositions.Find(id);
                job.JobPreference = newpreference;
                _context.SaveChanges();
                newpreference += 1;
            }
            return RedirectToAction("Index", "Lookups", new { Tab = "JobPositionsTab" });
        }

        // GET: JobPositions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // GET: JobPositions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: JobPositions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Name")] JobPosition jobPosition)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(jobPosition);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "JobPositionsTab" });
                }
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }

            return View(jobPosition);
        }

        // GET: JobPositions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions.FindAsync(id);
            if (jobPosition == null)
            {
                return NotFound();
            }
            return View(jobPosition);
        }

        // POST: JobPositions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] JobPosition jobPosition)
        {
            var jobPositionToUpdate = await _context.JobPositions
                .FirstOrDefaultAsync(m => m.ID == id);
            //Check that you got it or exit with a not found error
            if (jobPositionToUpdate == null)
            {
                return NotFound();
            }

            if (await TryUpdateModelAsync<JobPosition>(jobPositionToUpdate, "",
                d => d.Name))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "Lookups", new { Tab = "JobPositionsTab" });
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!JobPositionExists(jobPositionToUpdate.ID))
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
            return View(jobPositionToUpdate);
        }

        // GET: JobPositions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var jobPosition = await _context.JobPositions
                .FirstOrDefaultAsync(m => m.ID == id);
            if (jobPosition == null)
            {
                return NotFound();
            }

            return View(jobPosition);
        }

        // POST: JobPositions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var jobPosition = await _context.JobPositions.FindAsync(id);
            try
            {
                _context.JobPositions.Remove(jobPosition);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", "Lookups", new { Tab = "JobPositionsTab" });
            }
            catch (DbUpdateException dex)
            {
                if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
                {
                    ModelState.AddModelError("", "Unable to delete Job Position.");
                }
                else
                {
                    ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
                }
            }
            return View(jobPosition);
        }

        private bool JobPositionExists(int id)
        {
            return _context.JobPositions.Any(e => e.ID == id);
        }
    }
}
