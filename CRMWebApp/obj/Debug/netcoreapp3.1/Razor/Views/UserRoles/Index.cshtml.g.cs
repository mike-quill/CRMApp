#pragma checksum "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "107bda1d5332a13e302df7bd45130958f88681d9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_UserRoles_Index), @"mvc.1.0.view", @"/Views/UserRoles/Index.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"107bda1d5332a13e302df7bd45130958f88681d9", @"/Views/UserRoles/Index.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4052a7ef74a518b5d1cf2302cab0a0a758acd4fd", @"/Views/_ViewImports.cshtml")]
    public class Views_UserRoles_Index : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<CRMWebApp.ViewModels.UserVM>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\n");
#nullable restore
#line 3 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
  
	ViewBag.Title = "Index";

#line default
#line hidden
#nullable disable
            WriteLiteral("\n<h1>User Role Assignments</h1>\n\n<table class=\"table\">\n\t<tr>\n\t\t<th>\n\t\t\t");
#nullable restore
#line 12 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\t\t</th>\n\t\t<th>\n\t\t\t");
#nullable restore
#line 15 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
       Write(Html.DisplayNameFor(model => model.userRoles));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\t\t</th>\n\t\t<th></th>\n\t</tr>\n\n");
#nullable restore
#line 20 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
     foreach (var item in Model)
	{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t<tr>\n\t\t\t<td>\n\t\t\t\t");
#nullable restore
#line 24 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
           Write(Html.DisplayFor(modelItem => item.UserName));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\t\t\t</td>\n\t\t\t<td>\n");
#nullable restore
#line 27 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
                  
					foreach (var r in item.userRoles)
					{

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t\t\t\t");
            WriteLiteral("  ");
#nullable restore
#line 30 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
                       Write(r);

#line default
#line hidden
#nullable disable
            WriteLiteral(" <br />\n");
#nullable restore
#line 31 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
					}
				

#line default
#line hidden
#nullable disable
            WriteLiteral("\t\t\t</td>\n\t\t\t<td>\n\t\t\t\t");
#nullable restore
#line 35 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
           Write(Html.ActionLink("Edit", "Edit", new { id = item.Id }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\n\t\t\t</td>\n\t\t</tr>\n");
#nullable restore
#line 38 "C:\Users\Mike\Documents\GitHub\CRMApp\CRMWebApp\Views\UserRoles\Index.cshtml"
	}

#line default
#line hidden
#nullable disable
            WriteLiteral("\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<CRMWebApp.ViewModels.UserVM>> Html { get; private set; }
    }
}
#pragma warning restore 1591
