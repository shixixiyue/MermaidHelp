using FineUICore;
using FineUICoreEx;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyFineUIEx
{
    [HtmlTargetElement("f:Tool")]
    [RestrictChildren("Menu", new string[] { "Listeners", "Attributes" })]
    public class ToolEx : ToolTagHelperEx
    {
        /// <summary>
        /// 图标位置， 只支持右侧 或 左侧
        /// </summary>
        [HtmlAttributeName("IconAlign")]
        public IconAlign IconAlign { get; set; }

        protected override void PreProcess(TagHelperContext context, TagHelperOutput output)
        {
            StringBuilder sb = new();
            if (IconAlign == IconAlign.Right)
            {
                sb.Append($"$('#{this.Source.ID} i').before($('#{this.Source.ID} span'));");
            }
            PageContext.RegisterStartupScript(sb.ToString());
            base.PreProcess(context, output);
        }
    }
}
