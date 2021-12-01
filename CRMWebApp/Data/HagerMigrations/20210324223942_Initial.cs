using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRMWebApp.Data.HagerMigrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "CRM");

            migrationBuilder.CreateTable(
                name: "Announcements",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    Content = table.Column<string>(nullable: false),
                    PermissionLevel = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Announcements", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "BillingTerms",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    BillingPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BillingTerms", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Categories",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CategoryPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ContractorTypes",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Preference = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContractorTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Countries",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    CountryPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Countries", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Currencies",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    CurrencyPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Currencies", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CustomerTypes",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Preference = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "EmploymentTypes",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    EmploymentPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmploymentTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "JobPositions",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    JobPreference = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPositions", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "VendorTypes",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Preference = table.Column<int>(nullable: false),
                    Name = table.Column<string>(maxLength: 40, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VendorTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Provinces",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 40, nullable: false),
                    ProvincePreference = table.Column<int>(nullable: false),
                    CountryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Provinces", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Provinces_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "CRM",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Companies",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(nullable: false),
                    Location = table.Column<string>(maxLength: 100, nullable: true),
                    CreditCheck = table.Column<bool>(nullable: false),
                    Phone = table.Column<string>(maxLength: 10, nullable: true),
                    Website = table.Column<string>(maxLength: 100, nullable: true),
                    BillingAddress1 = table.Column<string>(maxLength: 100, nullable: true),
                    BillingAddress2 = table.Column<string>(maxLength: 100, nullable: true),
                    BillingCity = table.Column<string>(maxLength: 100, nullable: true),
                    BillingPostalCode = table.Column<string>(nullable: true),
                    ShippingAddress1 = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingAddress2 = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingCity = table.Column<string>(maxLength: 100, nullable: true),
                    ShippingPostalCode = table.Column<string>(nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    BillingTermID = table.Column<int>(nullable: true),
                    CurrencyID = table.Column<int>(nullable: true),
                    BillingProvinceID = table.Column<int>(nullable: true),
                    ShippingProvinceID = table.Column<int>(nullable: true),
                    BillingCountryID = table.Column<int>(nullable: true),
                    ShippingCountryID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Companies", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_BillingCountryID",
                        column: x => x.BillingCountryID,
                        principalSchema: "CRM",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_BillingProvinceID",
                        column: x => x.BillingProvinceID,
                        principalSchema: "CRM",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_BillingTerms_BillingTermID",
                        column: x => x.BillingTermID,
                        principalSchema: "CRM",
                        principalTable: "BillingTerms",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Currencies_CurrencyID",
                        column: x => x.CurrencyID,
                        principalSchema: "CRM",
                        principalTable: "Currencies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Countries_ShippingCountryID",
                        column: x => x.ShippingCountryID,
                        principalSchema: "CRM",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Companies_Provinces_ShippingProvinceID",
                        column: x => x.ShippingProvinceID,
                        principalSchema: "CRM",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    AddressLine1 = table.Column<string>(maxLength: 100, nullable: true),
                    AddressLine2 = table.Column<string>(maxLength: 100, nullable: true),
                    City = table.Column<string>(maxLength: 100, nullable: true),
                    PostalCode = table.Column<string>(nullable: true),
                    CellPhone = table.Column<string>(maxLength: 10, nullable: true),
                    HomePhone = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    DOB = table.Column<DateTime>(nullable: true),
                    Wage = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    Expense = table.Column<decimal>(type: "decimal(9,2)", nullable: true),
                    DateJoined = table.Column<DateTime>(nullable: true),
                    DateInactive = table.Column<DateTime>(nullable: true),
                    KeyFob = table.Column<string>(nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    IsUser = table.Column<bool>(nullable: false),
                    EmergencyContactName = table.Column<string>(maxLength: 100, nullable: true),
                    EmergencyContactPhone = table.Column<string>(maxLength: 10, nullable: true),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    ProvinceID = table.Column<int>(nullable: true),
                    CountryID = table.Column<int>(nullable: true),
                    JobPositionID = table.Column<int>(nullable: true),
                    EmploymentTypeID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Employees_Countries_CountryID",
                        column: x => x.CountryID,
                        principalSchema: "CRM",
                        principalTable: "Countries",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_EmploymentTypes_EmploymentTypeID",
                        column: x => x.EmploymentTypeID,
                        principalSchema: "CRM",
                        principalTable: "EmploymentTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_JobPositions_JobPositionID",
                        column: x => x.JobPositionID,
                        principalSchema: "CRM",
                        principalTable: "JobPositions",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Provinces_ProvinceID",
                        column: x => x.ProvinceID,
                        principalSchema: "CRM",
                        principalTable: "Provinces",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyContractorTypes",
                schema: "CRM",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    ContractorTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyContractorTypes", x => new { x.CompanyID, x.ContractorTypeID });
                    table.ForeignKey(
                        name: "FK_CompanyContractorTypes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "CRM",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyContractorTypes_ContractorTypes_ContractorTypeID",
                        column: x => x.ContractorTypeID,
                        principalSchema: "CRM",
                        principalTable: "ContractorTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyCustomerTypes",
                schema: "CRM",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    CustomerTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyCustomerTypes", x => new { x.CompanyID, x.CustomerTypeID });
                    table.ForeignKey(
                        name: "FK_CompanyCustomerTypes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "CRM",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyCustomerTypes_CustomerTypes_CustomerTypeID",
                        column: x => x.CustomerTypeID,
                        principalSchema: "CRM",
                        principalTable: "CustomerTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CompanyVendorTypes",
                schema: "CRM",
                columns: table => new
                {
                    CompanyID = table.Column<int>(nullable: false),
                    VendorTypeID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyVendorTypes", x => new { x.CompanyID, x.VendorTypeID });
                    table.ForeignKey(
                        name: "FK_CompanyVendorTypes_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "CRM",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyVendorTypes_VendorTypes_VendorTypeID",
                        column: x => x.VendorTypeID,
                        principalSchema: "CRM",
                        principalTable: "VendorTypes",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Contacts",
                schema: "CRM",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FirstName = table.Column<string>(maxLength: 50, nullable: false),
                    LastName = table.Column<string>(maxLength: 50, nullable: false),
                    JobTitle = table.Column<string>(maxLength: 50, nullable: true),
                    CellPhone = table.Column<string>(maxLength: 10, nullable: true),
                    WorkPhone = table.Column<string>(maxLength: 10, nullable: true),
                    Email = table.Column<string>(maxLength: 255, nullable: true),
                    Active = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(maxLength: 500, nullable: true),
                    CompanyID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contacts", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Contacts_Companies_CompanyID",
                        column: x => x.CompanyID,
                        principalSchema: "CRM",
                        principalTable: "Companies",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ContactCategories",
                schema: "CRM",
                columns: table => new
                {
                    ContactID = table.Column<int>(nullable: false),
                    CategoryID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContactCategories", x => new { x.ContactID, x.CategoryID });
                    table.ForeignKey(
                        name: "FK_ContactCategories_Categories_CategoryID",
                        column: x => x.CategoryID,
                        principalSchema: "CRM",
                        principalTable: "Categories",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ContactCategories_Contacts_ContactID",
                        column: x => x.ContactID,
                        principalSchema: "CRM",
                        principalTable: "Contacts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingCountryID",
                schema: "CRM",
                table: "Companies",
                column: "BillingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingProvinceID",
                schema: "CRM",
                table: "Companies",
                column: "BillingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_BillingTermID",
                schema: "CRM",
                table: "Companies",
                column: "BillingTermID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_CurrencyID",
                schema: "CRM",
                table: "Companies",
                column: "CurrencyID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingCountryID",
                schema: "CRM",
                table: "Companies",
                column: "ShippingCountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_ShippingProvinceID",
                schema: "CRM",
                table: "Companies",
                column: "ShippingProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Companies_Name_Location",
                schema: "CRM",
                table: "Companies",
                columns: new[] { "Name", "Location" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyContractorTypes_ContractorTypeID",
                schema: "CRM",
                table: "CompanyContractorTypes",
                column: "ContractorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyCustomerTypes_CustomerTypeID",
                schema: "CRM",
                table: "CompanyCustomerTypes",
                column: "CustomerTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyVendorTypes_VendorTypeID",
                schema: "CRM",
                table: "CompanyVendorTypes",
                column: "VendorTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ContactCategories_CategoryID",
                schema: "CRM",
                table: "ContactCategories",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_Contacts_CompanyID",
                schema: "CRM",
                table: "Contacts",
                column: "CompanyID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CountryID",
                schema: "CRM",
                table: "Employees",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                schema: "CRM",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmploymentTypeID",
                schema: "CRM",
                table: "Employees",
                column: "EmploymentTypeID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_JobPositionID",
                schema: "CRM",
                table: "Employees",
                column: "JobPositionID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ProvinceID",
                schema: "CRM",
                table: "Employees",
                column: "ProvinceID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_CountryID",
                schema: "CRM",
                table: "Provinces",
                column: "CountryID");

            migrationBuilder.CreateIndex(
                name: "IX_Provinces_Name_CountryID",
                schema: "CRM",
                table: "Provinces",
                columns: new[] { "Name", "CountryID" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Announcements",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "CompanyContractorTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "CompanyCustomerTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "CompanyVendorTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "ContactCategories",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "ContractorTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "CustomerTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "VendorTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Categories",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Contacts",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "EmploymentTypes",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "JobPositions",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Companies",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Provinces",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "BillingTerms",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Currencies",
                schema: "CRM");

            migrationBuilder.DropTable(
                name: "Countries",
                schema: "CRM");
        }
    }
}
