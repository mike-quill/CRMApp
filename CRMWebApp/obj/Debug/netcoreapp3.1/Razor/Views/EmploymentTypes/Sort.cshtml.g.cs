#pragma checksum "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "6b5f26b651e2b66af087dc58414f9dcca78ddcf8"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_EmploymentTypes_Sort), @"mvc.1.0.view", @"/Views/EmploymentTypes/Sort.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"6b5f26b651e2b66af087dc58414f9dcca78ddcf8", @"/Views/EmploymentTypes/Sort.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4052a7ef74a518b5d1cf2302cab0a0a758acd4fd", @"/Views/_ViewImports.cshtml")]
    public class Views_EmploymentTypes_Sort : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EmploymentType>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Lookups", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-route-Tab", "EmploymentTypesTab", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
  
	ViewData["Title"] = "Sort";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 6 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
 using (Html.BeginForm("Sort", "EmploymentTypes", FormMethod.Post))
{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t<h3>Drag and Drop to Reorder List</h3>\n\t<table id=\"EmploymentType\" class=\"sortable table w-auto table-bordered\">\n\t\t<thead class=\"thead-light\">\n\t\t<thead>\n\t\t\t<tr>\n\t\t\t\t<th>ID</th>\n\t\t\t\t<th>Name</th>\n\t\t\t</tr>\n\t\t</thead>\n\t\t<tbody>\n");
#nullable restore
#line 18 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
             foreach (EmploymentType employment in Model)
			{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t<tr>\n\t\t\t\t\t<td>\n\t\t\t\t\t\t");
#nullable restore
#line 22 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
                   Write(employment.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\t\t\t\t\t\t<input type=\"hidden\" name=\"employmentId\"");
            BeginWriteAttribute("value", " value=\"", 499, "\"", 521, 1);
#nullable restore
#line 23 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
WriteAttributeValue("", 507, employment.ID, 507, 14, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" />\n\t\t\t\t\t</td>\n\t\t\t\t\t<td>");
#nullable restore
#line 25 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
                   Write(employment.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n\t\t\t\t</tr>\n");
#nullable restore
#line 27 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
			}

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t</tbody>\n\n\t</table>\n\t<br />\n\t<input type=\"submit\" value=\"Update Preference\" class=\"btn ml-0\" />\n\t<div>\n\t\t");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "6b5f26b651e2b66af087dc58414f9dcca78ddcf86327", async() => {
                WriteLiteral("Back to Lookup List");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-Tab", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["Tab"] = (string)__tagHelperAttribute_2.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n\t</div>\n\t<script type=\"text/javascript\">\n\t\t$(\".sortable tbody\").sortable();\n\t\t$(\".sortable tbody\").disableSelection();\n\t</script>\n");
#nullable restore
#line 40 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\EmploymentTypes\Sort.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\n\n\n\n\n\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EmploymentType>> Html { get; private set; }
    }
}
#pragma warning restore 1591