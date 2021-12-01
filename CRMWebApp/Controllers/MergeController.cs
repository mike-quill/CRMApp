using CRMWebApp.Data;
using CRMWebApp.Models;
using CRMWebApp.Utility;
using CRMWebApp.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Web;
using Microsoft.AspNetCore.Http;

namespace CRMWebApp.Controllers
{
	[Authorize]
	public class MergeController : Controller
	{
		private readonly HagerDbContext _context;
		private readonly ApplicationDbContext _identityContext;

		public MergeController(HagerDbContext context, ApplicationDbContext identityContext)
		{
			_context = context;
			_identityContext = identityContext;
		}

		//Index 
		[Authorize(Roles = "Admin")]
		public ActionResult Index()
		{
			if (ModelState.IsValid)
			{
				try
				{
					var model = new MergeHelper
					{
                        SelectedCompanyIDs = new List<int>(),
                        CompanySelectList = GetCompanySelectList()
                    };
					return View(model);
				}
				catch (Exception){ return View(); }
			}
			else { return View(); }
		}

        //Index POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public ActionResult Index(MergeHelper mergeHelper)
        {
            mergeHelper.CompanySelectList = GetCompanySelectList();
            try
            {
                if (mergeHelper.SelectedCompanyIDs != null && mergeHelper.SelectedCompanyIDs.Count() == 2)
                {
                    if (mergeHelper.SelectedCompanyIDs[0] != mergeHelper.SelectedCompanyIDs[1])
                    {
                        List<SelectListItem> selectedItems = mergeHelper.CompanySelectList.Where(c => mergeHelper.SelectedCompanyIDs.Contains(Convert.ToInt32(c.Value))).ToList();
                        foreach (var Company in selectedItems)
                            Company.Selected = true;
                        return RedirectToAction("Merge", "Merge", new { selectedIDs = mergeHelper.SelectedCompanyIDs });
                    }
                }
            }
            catch (InvalidOperationException)
            {
                return RedirectToAction("Index", "Merge");
            }
            return RedirectToAction("Index", "Merge");
        }

        //Merge
        [Authorize(Roles = "Admin")]
        public ActionResult Merge(List<int> selectedIDs)
        {
            if (selectedIDs.Count == 2)
            {
                try
                {
                    var model = new MergeHelper
                    {
                        SelectedCompanyIDs = selectedIDs,
                        CompanyMergeList = GetCompanyMergeList(selectedIDs)
                    };
                    return View(model);
                }
                catch (InvalidOperationException) { return RedirectToAction("Index", "Merge"); }
            }
            else { return RedirectToAction("Index", "Merge"); }
        }

        //Merge POST
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Merge(IFormCollection formC, int mergeIDLeft, int mergeIDRight)
        {
            //int array representing the chosen fields for the merged record
            string[] mergeFields = formC["mergeFields[]"];

            var companyLeft = await _context.Companies.FindAsync(mergeIDLeft);
            var companyRight = await _context.Companies.FindAsync(mergeIDRight);
            var contacts = from f in _context.Contacts
                .Include(m => m.Company)
                .Where(c => c.CompanyID == mergeIDLeft || c.CompanyID == mergeIDRight)
                    select f;

            if (companyLeft == null || companyRight == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                //ID list from view used to get merge list
                List<int> IDList = new List<int>() { mergeIDLeft, mergeIDRight };
                var mergeList = GetCompanyMergeList(IDList).ToList();

                //combined lists of contacts/types between both merge records
                var listContacts = mergeList[0].Contacts.Union(mergeList[1].Contacts).ToList();
                var listCustomerTypes = mergeList[0].CompanyCustomerTypes.Union(mergeList[1].CompanyCustomerTypes).ToList();
                var listContractorTypes = mergeList[0].CompanyContractorTypes.Union(mergeList[1].CompanyContractorTypes).ToList();
                var listVendorTypes = mergeList[0].CompanyVendorTypes.Union(mergeList[1].CompanyVendorTypes).ToList();

                //updating left record with new merge values chosen in the form
                companyLeft.Active = mergeList[Convert.ToInt32(mergeFields[0])].Active;
                companyLeft.CreditCheck = mergeList[Convert.ToInt32(mergeFields[1])].CreditCheck;
                companyLeft.Name = mergeList[Convert.ToInt32(mergeFields[2])].Name;
                companyLeft.Location = mergeList[Convert.ToInt32(mergeFields[3])].Location;
                companyLeft.Phone = mergeList[Convert.ToInt32(mergeFields[4])].Phone;
                companyLeft.Website = mergeList[Convert.ToInt32(mergeFields[5])].Website;
                companyLeft.BillingTermID = mergeList[Convert.ToInt32(mergeFields[6])].BillingTermID;
                companyLeft.BillingCountryID = mergeList[Convert.ToInt32(mergeFields[7])].BillingCountryID;
                companyLeft.BillingProvinceID = mergeList[Convert.ToInt32(mergeFields[8])].BillingProvinceID;
                companyLeft.BillingCity = mergeList[Convert.ToInt32(mergeFields[9])].BillingCity;
                companyLeft.BillingAddress1 = mergeList[Convert.ToInt32(mergeFields[10])].BillingAddress1;
                companyLeft.BillingAddress2 = mergeList[Convert.ToInt32(mergeFields[11])].BillingAddress2;
                companyLeft.BillingPostalCode = mergeList[Convert.ToInt32(mergeFields[12])].BillingPostalCode;
                companyLeft.ShippingCountryID = mergeList[Convert.ToInt32(mergeFields[13])].ShippingCountryID;
                companyLeft.ShippingProvinceID = mergeList[Convert.ToInt32(mergeFields[14])].ShippingProvinceID;
                companyLeft.ShippingCity = mergeList[Convert.ToInt32(mergeFields[15])].ShippingCity;
                companyLeft.ShippingAddress1 = mergeList[Convert.ToInt32(mergeFields[16])].ShippingAddress1;
                companyLeft.ShippingAddress2 = mergeList[Convert.ToInt32(mergeFields[17])].ShippingAddress2;
                companyLeft.ShippingPostalCode = mergeList[Convert.ToInt32(mergeFields[18])].ShippingPostalCode;
                companyLeft.CurrencyID = mergeList[Convert.ToInt32(mergeFields[19])].CurrencyID;
                companyLeft.Notes = mergeList[Convert.ToInt32(mergeFields[20])].Notes;
                    
                //clear lists
                companyLeft.CompanyContractorTypes.Clear();
                companyLeft.CompanyCustomerTypes.Clear();
                companyLeft.CompanyVendorTypes.Clear();
                companyLeft.Contacts.Clear();

                Debug.WriteLine(listContacts.Count());
                Debug.WriteLine(listCustomerTypes.Count());
                Debug.WriteLine(listContractorTypes.Count());
                Debug.WriteLine(listVendorTypes.Count());
                    
                //hashsets to run .contains on
                //for each foreign collection, changes ID to the record destined for merge
                //and repopulates the merge record's collections
                var currentOptionsHS = new HashSet<int>();
                var leftHS = new HashSet<int>();

                //__Contacts__
                currentOptionsHS = new HashSet<int>(listContacts.Select(b => b.ID));
                leftHS = new HashSet<int>(companyLeft.Contacts.Select(b => b.ID));
                foreach (var item in listContacts)
                    item.CompanyID = companyLeft.ID;
                foreach (var s in _context.Contacts)
                {
                    if (currentOptionsHS.Contains(s.ID) && !leftHS.Contains(s.ID))
                        companyLeft.Contacts.Add(listContacts.Find(x => x.ID == s.ID));
                };
                    
                //__CompanyCustomerTypes__
                currentOptionsHS = new HashSet<int>(listCustomerTypes.Select(b => b.CustomerTypeID));
                leftHS = new HashSet<int>(companyLeft.CompanyCustomerTypes.Select(b => b.CustomerTypeID));
                foreach (var s in _context.CustomerTypes)
                {
                    if (currentOptionsHS.Contains(s.ID) && !leftHS.Contains(s.ID))
                    {
                        companyLeft.CompanyCustomerTypes.Add(new CompanyCustomerType
                        {
                            CustomerTypeID = s.ID,
                            CompanyID = companyLeft.ID
                        });
                    }
                };

                //__CompanyContractorTypes__
                currentOptionsHS = new HashSet<int>(listContractorTypes.Select(b => b.ContractorTypeID));
                leftHS = new HashSet<int>(companyLeft.CompanyContractorTypes.Select(b => b.ContractorTypeID));
                foreach (var s in _context.ContractorTypes)
                {
                    if (currentOptionsHS.Contains(s.ID) && !leftHS.Contains(s.ID))
                    {
                        companyLeft.CompanyContractorTypes.Add(new CompanyContractorType
                        {
                            ContractorTypeID = s.ID,
                            CompanyID = companyLeft.ID
                        });
                    }
                };

                //__CompanyVendorTypes__
                currentOptionsHS = new HashSet<int>(listVendorTypes.Select(b => b.VendorTypeID));
                leftHS = new HashSet<int>(companyLeft.CompanyVendorTypes.Select(b => b.VendorTypeID));
                foreach (var s in _context.VendorTypes)
                {
                    if (currentOptionsHS.Contains(s.ID) && !leftHS.Contains(s.ID))
                    {
                        companyLeft.CompanyVendorTypes.Add(new CompanyVendorType
                        {
                            VendorTypeID = s.ID,
                            CompanyID = companyLeft.ID
                        });
                    }
                };
                try
                {
                    //commit changes (redundant code?)
                    _context.Contacts.UpdateRange(contacts);
                    _context.Companies.Update(companyLeft);
                    _context.Companies.Remove(companyRight);
                    await _context.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            return RedirectToAction("Details", "Companies", companyLeft);
        }

        private List<SelectListItem> GetCompanySelectList()
		{
			var dQuery = from j in _context.Companies
						 select j;
			List<SelectListItem> companylist = new List<SelectListItem>();
			companylist.AddRange(new SelectList(dQuery, "ID", "Summary"));
			return companylist;
		}

        private IEnumerable<Company> GetCompanyMergeList(List<int> selectedIDs)
        {
            var companies = from f in _context.Companies
                .Include(c => c.BillingCountry)
                .Include(c => c.BillingProvince)
                .Include(c => c.BillingTerm)
                .Include(c => c.Currency)
                .Include(c => c.ShippingCountry)
                .Include(c => c.ShippingProvince)
                .Include(c => c.Contacts)
                .Include(c => c.CompanyContractorTypes).ThenInclude(c => c.ContractorType)
                .Include(c => c.CompanyCustomerTypes).ThenInclude(c => c.CustomerType)
                .Include(c => c.CompanyVendorTypes).ThenInclude(c => c.VendorType)
                .Where(c => c.ID == selectedIDs[0] || c.ID == selectedIDs[1])
                            select f;
            if (selectedIDs[0] > selectedIDs[1])
                companies.AsQueryable().Reverse();
            return companies;
        }
    }
}