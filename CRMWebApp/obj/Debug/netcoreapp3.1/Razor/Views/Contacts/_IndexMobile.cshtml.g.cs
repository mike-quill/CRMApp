#pragma checksum "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "423e4dbbf2ec35b9ceaefac78b2d3818ddcb35a7"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Contacts__IndexMobile), @"mvc.1.0.view", @"/Views/Contacts/_IndexMobile.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"423e4dbbf2ec35b9ceaefac78b2d3818ddcb35a7", @"/Views/Contacts/_IndexMobile.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4052a7ef74a518b5d1cf2302cab0a0a758acd4fd", @"/Views/_ViewImports.cshtml")]
    public class Views_Contacts__IndexMobile : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CRMWebApp.Models.Contact>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Edit", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Details", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral(@"
<table id=""collapse-table"" class=""table"">
    <thead>
        <tr>
            <th>
                <input type=""submit"" name=""actionButton"" value=""Name"" class=""btn-link"" />
            </th>

            <th>
                <input type=""submit"" disabled=""disabled"" value=""Phone Numbers"" class=""btn-link"" />
            </th>
            <th>
                <input type=""submit"" name=""actionButton"" value=""Company"" class=""btn-link"" />
            </th>
        </tr>
    </thead>
    <tbody>
");
#nullable restore
#line 19 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
         foreach (var item in Model)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr data-toggle=\"collapse\" data-target=\"#collapsed-row-");
#nullable restore
#line 21 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                              Write(item.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("\" class=\"collapsed collapsible-row\">\n                <td>");
#nullable restore
#line 22 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
               Write(Html.DisplayFor(modelItem => item.FullName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n                <td>\n");
#nullable restore
#line 24 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                      
                        bool showingCell = !string.IsNullOrEmpty(item.CellPhoneNumber);

                        if (showingCell)
                        {

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            WriteLiteral("Cell: ");
#nullable restore
#line 29 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                               Write(Html.DisplayFor(modelItem => item.CellPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 30 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                        }

                        if (!string.IsNullOrEmpty(item.WorkPhoneNumber))
                        {
                            if (showingCell)
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                <br />\n");
#nullable restore
#line 37 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                            }


#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            WriteLiteral("Work: ");
#nullable restore
#line 39 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                               Write(Html.DisplayFor(modelItem => item.WorkPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n");
#nullable restore
#line 40 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                        }
                    

#line default
#line hidden
#nullable disable
            WriteLiteral("                </td>\n                <td>");
#nullable restore
#line 43 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
               Write(Html.DisplayFor(modelItem => item.Company.Name));

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\n            </tr>\n            <tr>\n                <td colspan=\"4\">\n                    <!-- Collapse on the div because animations do not work on the td -->\n                    <div");
            BeginWriteAttribute("id", " id=\"", 1742, "\"", 1769, 2);
            WriteAttributeValue("", 1747, "collapsed-row-", 1747, 14, true);
#nullable restore
#line 48 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
WriteAttributeValue("", 1761, item.ID, 1761, 8, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"collapse collapsed-row\" data-parent=\"#collapse-table\">\n                        <div class=\"details-container\">\n                            <ul>\n                                <li><span>");
#nullable restore
#line 51 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 51 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                               Write(Html.DisplayFor(modelItem => item.FirstName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 52 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 52 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                              Write(Html.DisplayFor(modelItem => item.LastName));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 53 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.JobTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 53 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                              Write(Html.DisplayFor(modelItem => item.JobTitle));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 54 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.CellPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 54 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                                     Write(Html.DisplayFor(modelItem => item.CellPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 55 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.WorkPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 55 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                                     Write(Html.DisplayFor(modelItem => item.WorkPhoneNumber));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 56 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 56 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 57 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 57 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                            Write(Html.DisplayFor(modelItem => item.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 58 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.Notes));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 58 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                           Write(Html.DisplayFor(modelItem => item.Notes));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                                <li><span>");
#nullable restore
#line 59 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                     Write(Html.DisplayNameFor(modelItem => item.Company));

#line default
#line hidden
#nullable disable
            WriteLiteral(":</span> ");
#nullable restore
#line 59 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                                                             Write(Html.DisplayFor(modelItem => item.Company.Summary));

#line default
#line hidden
#nullable disable
            WriteLiteral("</li>\n                            </ul>\n                        </div>\n");
#nullable restore
#line 62 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                          
                            if (User.IsInRole("Admin"))
                            {

#line default
#line hidden
#nullable disable
            WriteLiteral("                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "423e4dbbf2ec35b9ceaefac78b2d3818ddcb35a714906", async() => {
                WriteLiteral("Edit");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 65 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                       WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(" ");
            WriteLiteral(" |\n");
#nullable restore
#line 66 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                            }

#line default
#line hidden
#nullable disable
            WriteLiteral("                            ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "423e4dbbf2ec35b9ceaefac78b2d3818ddcb35a717358", async() => {
                WriteLiteral(" Details ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_1.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_1);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 67 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
                                                      WriteLiteral(item.ID);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\n");
            WriteLiteral("                    </div>\n                </td>\n            </tr>\n");
#nullable restore
#line 72 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\Contacts\_IndexMobile.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\n</table>\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CRMWebApp.Models.Contact>> Html { get; private set; }
    }
}
#pragma warning restore 1591
