using FineUICoreEx;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFineUIEx
{
    [HtmlTargetElement("Tools")]
    [RestrictChildren("f:Tool", new string[] { "f:ToolSeparator", "f:ToolbarSeparator" })]
    public class ToolsEx : ToolsTagHelperEx
    {
    }
}