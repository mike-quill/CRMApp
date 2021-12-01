using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CRMWebApp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CRMWebApp.Controllers
{
    [Authorize(Roles = "Admin, Supervisor")]
    public class LookupsController : Controller
    {
        private readonly HagerDbContext _context;

        public LookupsController(HagerDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string Tab)
        {
            ///Note: select the tab you want to load by passing in
            ///the ID of the tab such as BillingTermsTab, CategoriesTab
            ///or ContractorTypesTab
            ViewData["Tab"] = Tab;
            return View();
        }

        public PartialViewResult BillingTerms()
        {
            ViewData["BillingTermsID"] = new
                SelectList(_context.BillingTerms
                .OrderBy(a => a.BillingPreference), "ID", "Name");
            return PartialView("_BillingTerms");
        }
        public PartialViewResult Categories()
        {
            ViewData["CategoriesID"] = new
                SelectList(_context.Categories
                .OrderBy(a => a.CategoryPreference), "ID", "Name");
            return PartialView("_Categories");
        }
        public PartialViewResult ContractorTypes()
        {
            ViewData["ContractorTypesID"] = new
                SelectList(_context.ContractorTypes
                .OrderBy(a => a.Preference), "ID", "Name");
            return PartialView("_ContractorTypes");
        }

        public PartialViewResult Countries()
        {
            ViewData["CountriesID"] = new
                SelectList(_context.Countries
                .OrderBy(a => a.CountryPreference), "ID", "Name");
            return PartialView("_Countries");
        }

        public PartialViewResult Currencies()
        {
            ViewData["CurrenciesID"] = new
                SelectList(_context.Currencies
                .OrderBy(a => a.CurrencyPreference), "ID", "Name");
            return PartialView("_Currencies");
        }

        public PartialViewResult CustomerTypes()
        {
            ViewData["CustomerTypesID"] = new
                SelectList(_context.CustomerTypes
                .OrderBy(a => a.Preference), "ID", "Name");
            return PartialView("_CustomerTypes");
        }

        public PartialViewResult EmploymentTypes()
        {
            ViewData["EmploymentTypesID"] = new
                SelectList(_context.EmploymentTypes
                .OrderBy(a => a.EmploymentPreference), "ID", "Name");
            return PartialView("_EmploymentTypes");
        }

        public PartialViewResult JobPositions()
        {
            ViewData["JobPositionsID"] = new
                SelectList(_context.JobPositions
                .OrderBy(a => a.JobPreference), "ID", "Name");
            return PartialView("_JobPositions");
        }

        public PartialViewResult Provinces()
        {
            ViewData["CountryID"] = new
                SelectList(_context.Countries
                .OrderBy(a => a.Name), "ID", "Name");
            ViewData["ProvincesID"] = new
                SelectList(_context.Provinces
                .OrderBy(a => a.Name), "ID", "Name");
            return PartialView("_Provinces");
        }

        public PartialViewResult VendorTypes()
        {
            ViewData["VendorTypesID"] = new
                SelectList(_context.VendorTypes
                .OrderBy(a => a.Preference), "ID", "Name");
            return PartialView("_VendorTypes");
        }


    }
}
