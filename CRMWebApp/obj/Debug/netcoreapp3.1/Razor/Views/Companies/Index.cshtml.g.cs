#pragma checksum "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f3d077919f946c1b5bfb454d731a1f15002acf3d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Companies_Index), @"mvc.1.0.view", @"/Views/Companies/Index.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\_ViewImports.cshtml"
using CRMWebApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\_ViewImports.cshtml"
using CRMWebApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f3d077919f946c1b5bfb454d731a1f15002acf3d", @"/Views/Companies/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4052a7ef74a518b5d1cf2302cab0a0a758acd4fd", @"/Views/_ViewImports.cshtml")]
    public class Views_Companies_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CRMWebApp.Models.Company>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Create", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-outline-dark "), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_IndexDesktop", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_IndexMobile", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("name", "_PagingNavBar", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "get", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/CompanyTemplate.xlsx"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("target", new global::Microsoft.AspNetCore.Html.HtmlString(""), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "InsertFromExcel", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_10 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Companies", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_11 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_12 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("enctype", new global::Microsoft.AspNetCore.Html.HtmlString("multipart/form-data"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
  
	ViewData["Title"] = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>Companies</h1>\n");
#nullable restore
#line 8 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
 if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3d077919f946c1b5bfb454d731a1f15002acf3d8303", async() => {
                WriteLiteral("<button type=\"button\" class=\"btn mb-2\">Create New Company</button>");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
#nullable restore
#line 11 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
}

#line default
#line hidden
#nullable disable
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3d077919f946c1b5bfb454d731a1f15002acf3d9680", async() => {
                WriteLiteral("\n    <input type=\"hidden\" name=\"sortDirection\"");
                BeginWriteAttribute("value", " value=\"", 344, "\"", 378, 1);
#nullable restore
#line 13 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
WriteAttributeValue("", 352, ViewData["sortDirection"], 352, 26, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\n    <input type=\"hidden\" name=\"sortField\"");
                BeginWriteAttribute("value", " value=\"", 424, "\"", 454, 1);
#nullable restore
#line 14 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
WriteAttributeValue("", 432, ViewData["sortField"], 432, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
    <div class=""form-horizontal"">
        <button class=""btn mb-2 collapsed"" type=""button"" data-toggle=""collapse"" id=""filterToggle"" data-target=""#collapseFilter"" aria-expanded=""false"" aria-controls=""collapseFilter"">
            Filter/Search
        </button>
        <div");
                BeginWriteAttribute("class", " class=\"", 731, "\"", 772, 2);
                WriteAttributeValue("", 739, "collapse", 739, 8, true);
#nullable restore
#line 19 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
WriteAttributeValue(" ", 747, ViewData["Filtering"], 748, 24, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" id=""collapseFilter"">
            <div class=""card card-body bg-light mb-2"">
                <div class=""form-row"">
                    <div class=""col-md-4 col-sm-6"">
                        <div class=""form-group"">
                            <label class=""control-label"">Select by Contractor Type:</label>
                            ");
#nullable restore
#line 25 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.DropDownList("ContractorTypeID", null, "All Contractor Types", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                        </div>\n                        <div class=\"form-group\">\n                            <label class=\"control-label\">Select by Customer Type:</label>\n                            ");
#nullable restore
#line 29 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.DropDownList("CustomerTypeID", null, "All Customer Types", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                        </div>\n                        <div class=\"form-group\">\n                            <label class=\"control-label\">Select by Vendor Type:</label>\n                            ");
#nullable restore
#line 33 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.DropDownList("VendorTypeID", null, "All Vendor Types", htmlAttributes: new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>
                    </div>
                    <div class=""col-md-4 col-sm-6"">
                        <div class=""form-group"">
                            <label class=""control-label"">Search Company Name:</label>
                            ");
#nullable restore
#line 39 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.TextBox("SearchString", null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>
                    </div>
                </div>
                <div class=""form-row"">
                    <div class=""col-md-10"">
                        <div class=""form-group"">
                            <span>Show Inactive Companies:</span>
                            ");
#nullable restore
#line 47 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.CheckBox("showInactive", false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                            &nbsp; &nbsp;\n                            <span>Show Contractors:</span>\n                            ");
#nullable restore
#line 50 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.CheckBox("showContractor", false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                            &nbsp; &nbsp;\n                            <span>Show Customers:</span>\n                            ");
#nullable restore
#line 53 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.CheckBox("showCustomer", false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\n                            &nbsp; &nbsp;\n                            <span>Show Vendors:</span>\n                            ");
#nullable restore
#line 56 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                       Write(Html.CheckBox("showVendor", false));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"
                        </div>
                    </div>
                </div>
                <div class=""form-row"">
                    <div class=""col-md-6  align-self-end"">
                        <div class=""form-group"">
                            <input type=""submit"" name=""actionButton"" value=""Filter"" class=""btn btn-outline-primary"" />
                            ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3d077919f946c1b5bfb454d731a1f15002acf3d16043", async() => {
                    WriteLiteral("Clear");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n                        </div>\n                    </div>\n                </div>\n            </div>\n        </div>\n    </div>\n    ");
#nullable restore
#line 71 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
Write(TempData["Success"]);

#line default
#line hidden
#nullable disable
                WriteLiteral("\n");
#nullable restore
#line 72 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
     if (TempData.Peek("ImportErrors") != null)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
   Write(Html.ActionLink("Download log to find out why.", "GetImportLog", "Companies"));

#line default
#line hidden
#nullable disable
#nullable restore
#line 74 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
                                                                                      ;
    }

#line default
#line hidden
#nullable disable
                WriteLiteral("\n    <div class=\"company-index-desktop\">\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f3d077919f946c1b5bfb454d731a1f15002acf3d18517", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    </div>\n    <div class=\"company-index-mobile\">\n        ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f3d077919f946c1b5bfb454d731a1f15002acf3d19754", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n    </div>\n\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("partial", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "f3d077919f946c1b5bfb454d731a1f15002acf3d20947", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.PartialTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_PartialTagHelper.Name = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_6.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_6);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\n");
#nullable restore
#line 87 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
  
	if (User.IsInRole("Admin") || User.IsInRole("Supervisor"))
	{

#line default
#line hidden
#nullable disable
            WriteLiteral(@"		<button class=""btn mb-2"" type=""button"" data-toggle=""collapse"" id=""filterToggle"" data-target=""#collapseFilter2"" aria-expanded=""false"" aria-controls=""collapseFilter"">
			Upload Batch Companies
		</button>
		<div class=""collapse"" id=""collapseFilter2"">
			<div class=""card card-body bg-light mb-2"">
				");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3d077919f946c1b5bfb454d731a1f15002acf3d24072", async() => {
                WriteLiteral("\n\t\t\t\t\t<div>\n\t\t\t\t\t\t");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "f3d077919f946c1b5bfb454d731a1f15002acf3d24358", async() => {
                    WriteLiteral("Download Template");
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_8);
                BeginWriteTagHelperAttribute();
                __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
                __tagHelperExecutionContext.AddHtmlAttribute("_blank\"\"", Html.Raw(__tagHelperStringValueBuffer), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.Minimized);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\n\t\t\t\t\t</div>\n\t\t\t\t\t<div class=\"form-group\">\n\t\t\t\t\t\t<label class=\"control-label\">Upload Excel File:</label>\n\t\t\t\t\t\t<input required type=\"file\"");
                BeginWriteAttribute("class", " class=\"", 4696, "\"", 4704, 0);
                EndWriteAttribute();
                WriteLiteral(" name=\"theExcel\" /><br />\n\t\t\t\t\t</div>\n\t\t\t\t\t<div class=\"form-group\">\n\t\t\t\t\t\t<input type=\"submit\" value=\"Upload Data\" class=\"btn m-0\" />\n\t\t\t\t\t</div>\n\t\t\t\t");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_9.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_9);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Controller = (string)__tagHelperAttribute_10.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_10);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_11.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_11);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_12);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t\t\t</div>\n\t\t</div>\n");
#nullable restore
#line 109 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Companies\Index.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
            DefineSection("Scripts", async() => {
                WriteLiteral("\n\t<script type=\"text/javascript\">\n\t\t$(function () {\n\t\t\t$(\'[data-toggle=\"popover\"]\').popover();\n\t\t});\n\t</script>\n\n");
                WriteLiteral("\n\n");
            }
            );
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CRMWebApp.Models.Company>> Html { get; private set; }
    }
}
#pragma warning restore 1591