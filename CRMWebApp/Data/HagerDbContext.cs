using CRMWebApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRMWebApp.Data
{
	public class HagerDbContext : DbContext
	{
        public HagerDbContext(DbContextOptions<HagerDbContext> options)
        : base(options)
        {
        }

        public DbSet<BillingTerm> BillingTerms { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ContractorType> ContractorTypes { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<CustomerType> CustomerTypes { get; set; }
        public DbSet<EmploymentType> EmploymentTypes { get; set; }
        public DbSet<JobPosition> JobPositions { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<VendorType> VendorTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<ContactCategory> ContactCategories { get; set; }
        public DbSet<CompanyContractorType> CompanyContractorTypes { get; set; }
        public DbSet<CompanyCustomerType> CompanyCustomerTypes { get; set; }
        public DbSet<CompanyVendorType> CompanyVendorTypes { get; set; }
        public DbSet<Announcement> Announcements { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("CRM");

            //Add a unique index to the Employee Email
            modelBuilder.Entity<Employee>()
            .HasIndex(a => new { a.Email })
            .IsUnique();

            modelBuilder.Entity<Country>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Province>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.Province)
                .HasForeignKey(p => p.ProvinceID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<JobPosition>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.JobPosition)
                .HasForeignKey(p => p.JobPositionID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<EmploymentType>()
                .HasMany<Employee>(d => d.Employees)
                .WithOne(p => p.EmploymentType)
                .HasForeignKey(p => p.EmploymentTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ContactCategory>()
                .HasKey(t => new { t.ContactID, t.CategoryID});

            modelBuilder.Entity<Category>()
                .HasMany<ContactCategory>(p => p.ContactCategories)
                .WithOne(c => c.Category)
                .HasForeignKey(c => c.CategoryID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyContractorType>()
                .HasKey(t => new { t.CompanyID, t.ContractorTypeID });

            modelBuilder.Entity<ContractorType>()
                .HasMany<CompanyContractorType>(p => p.CompanyContractorTypes)
                .WithOne(c => c.ContractorType)
                .HasForeignKey(c => c.ContractorTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyCustomerType>()
                .HasKey(t => new { t.CompanyID, t.CustomerTypeID });

            modelBuilder.Entity<CustomerType>()
                .HasMany<CompanyCustomerType>(p => p.CompanyCustomerTypes)
                .WithOne(c => c.CustomerType)
                .HasForeignKey(c => c.CustomerTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<CompanyVendorType>()
                .HasKey(t => new { t.CompanyID, t.VendorTypeID});

            modelBuilder.Entity<VendorType>()
                .HasMany<CompanyVendorType>(p => p.CompanyVendorTypes)
                .WithOne(c => c.VendorType)
                .HasForeignKey(c => c.VendorTypeID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index to the Province/Country
            modelBuilder.Entity<Province>()
            .HasIndex(c => new { c.Name, c.CountryID })
            .IsUnique();

            //Add this so you don't get Cascade Delete
            modelBuilder.Entity<Country>()
                .HasMany<Province>(d => d.Provinces)
                .WithOne(p => p.Country)
                .HasForeignKey(p => p.CountryID)
                .OnDelete(DeleteBehavior.Restrict);

            //Add a unique index to the Company Name/Location
            modelBuilder.Entity<Company>()
            .HasIndex(c => new { c.Name, c.Location })
            .IsUnique();
        }
    }
}
