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
	public class ContractorTypesController : Controller
	{
		private readonly HagerDbContext _context;

		public ContractorTypesController(HagerDbContext context)
		{
			_context = context;
		}

		// GET: ContractorTypes
		public IActionResult Index()
		{
			return RedirectToAction("Index", "Lookups", new { Tab = "ContractorTypesTab" });
		}

		public IActionResult Sort()
		{
			return View(_context.ContractorTypes.OrderBy(p => p.Preference).ToList());
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Sort(int[] contractId)
		{
			int newpreference = 1;
			foreach (int id in contractId)
			{
				var contract = _context.ContractorTypes.Find(id);
				contract.Preference = newpreference;
				_context.SaveChanges();
				newpreference += 1;
			}
			return RedirectToAction("Index", "Lookups", new { Tab = "ContractorTypesTab" });
		}

		// GET: ContractorTypes/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var contractorType = await _context.ContractorTypes
				.FirstOrDefaultAsync(m => m.ID == id);
			if (contractorType == null)
			{
				return NotFound();
			}

			return View(contractorType);
		}

		// GET: ContractorTypes/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: ContractorTypes/Create
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("ID,Name")] ContractorType contractorType)
		{
			try
			{
				if (ModelState.IsValid)
				{
					_context.Add(contractorType);
					await _context.SaveChangesAsync();
					return RedirectToAction("Index", "Lookups", new { Tab = "ContractorTypesTab" });
				}
			}
			catch (DbUpdateException)
			{
				ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
			}

			return View(contractorType);
		}

		// GET: ContractorTypes/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var contractorType = await _context.ContractorTypes.FindAsync(id);
			if (contractorType == null)
			{
				return NotFound();
			}
			return View(contractorType);
		}

		// POST: ContractorTypes/Edit/5
		// To protect from overposting attacks, enable the specific properties you want to bind to, for 
		// more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id)//, [Bind("ID,Name")] ContractorType contractorType)
		{
			var contractorTypeToUpdate = await _context.ContractorTypes
				.FirstOrDefaultAsync(m => m.ID == id);
			//Check that you got it or exit with a not found error
			if (contractorTypeToUpdate == null)
			{
				return NotFound();
			}

			if (await TryUpdateModelAsync<ContractorType>(contractorTypeToUpdate, "",
				d => d.Name))
			{
				try
				{
					await _context.SaveChangesAsync();
					return RedirectToAction("Index", "Lookups", new { Tab = "ContractorTypesTab" });
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!ContractorTypeExists(contractorTypeToUpdate.ID))
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
			return View(contractorTypeToUpdate);
		}

		// GET: ContractorTypes/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var contractorType = await _context.ContractorTypes
				.FirstOrDefaultAsync(m => m.ID == id);
			if (contractorType == null)
			{
				return NotFound();
			}

			return View(contractorType);
		}

		// POST: ContractorTypes/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var contractorType = await _context.ContractorTypes.FindAsync(id);
			try
			{
				_context.ContractorTypes.Remove(contractorType);
				await _context.SaveChangesAsync();
				return RedirectToAction("Index", "Lookups", new { Tab = "ContractorTypesTab" });
			}
			catch (DbUpdateException dex)
			{
				if (dex.GetBaseException().Message.Contains("FOREIGN KEY constraint failed"))
				{
					ModelState.AddModelError("", "Unable to delete Contractor Types.");
				}
				else
				{
					ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
				}
			}
			return View(contractorType);
		}

		private bool ContractorTypeExists(int id)
		{
			return _context.ContractorTypes.Any(e => e.ID == id);
		}
	}
}
