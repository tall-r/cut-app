#pragma checksum "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "470de087051e6df43392f435b797c2604bebf3b3"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Settings), @"mvc.1.0.view", @"/Views/Home/Settings.cshtml")]
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
#line 1 "/Users/filippk/projects/Cut/Cut.Web/Views/_ViewImports.cshtml"
using Cut.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/filippk/projects/Cut/Cut.Web/Views/_ViewImports.cshtml"
using Cut.Web.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"470de087051e6df43392f435b797c2604bebf3b3", @"/Views/Home/Settings.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"22000192460ea686adcd2b5ab3c47e52cc9e2b92", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_Settings : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<Cut.Data.Settings>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
  
    ViewData["Title"] = "Настройки";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"text-left\">\r\n    <h1 class=\"text-center\">");
#nullable restore
#line 7 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
                       Write(ViewData["Title"]);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h1>\r\n");
#nullable restore
#line 8 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
 using(Html.BeginForm("SaveSettings", "Home", null, FormMethod.Post, null, new { @class = "row g-3"} )) {

#line default
#line hidden
#nullable disable
            WriteLiteral("    <div class=\"col-md-9\">\r\n        ");
#nullable restore
#line 10 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.LabelFor(m => m.BladeWidth, "Толщина диска пилы (мм)", new { @class = "form-label" } ));

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n    </div>\r\n    <div class=\"col-md-3\" style=\"width:60px;\">\r\n        ");
#nullable restore
#line 13 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.EditorFor(m => m.BladeWidth, null, new { @class = "form-control"}));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        ");
#nullable restore
#line 16 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.LabelFor(m => m.MaxCutOff, "Максимально допустимый остаток доски при распиле (мм)", new { @class = "form-label" }));

#line default
#line hidden
#nullable disable
            WriteLiteral(" \r\n    </div>\r\n    <div class=\"col-md-3\" style=\"width:60px;\">\r\n        ");
#nullable restore
#line 19 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.EditorFor(m => m.MaxCutOff, null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-md-9\">\r\n        ");
#nullable restore
#line 22 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.LabelFor(m => m.DefaultPlankLength, "Стандартная длина доски (мм)", new { @class = "form-label" } ));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-md-3\" style=\"width:60px;\">\r\n        ");
#nullable restore
#line 25 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
   Write(Html.EditorFor(m => m.DefaultPlankLength, null, new { @class = "form-control" }));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n    </div>\r\n    <div class=\"col-12 text-center\"><button type=\"submit\" class=\"col-3\">Сохранить</button></div>\r\n");
#nullable restore
#line 28 "/Users/filippk/projects/Cut/Cut.Web/Views/Home/Settings.cshtml"
}

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<Cut.Data.Settings> Html { get; private set; }
    }
}
#pragma warning restore 1591
