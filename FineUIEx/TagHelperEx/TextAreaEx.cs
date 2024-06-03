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
    [HtmlTargetElementAttribute("f:TextArea")]
    [RestrictChildrenAttribute("Listeners", new string[] { "Attributes" })]
    public class TextAreaEx : TextAreaTagHelperEx
    {
        [HtmlAttributeName("OnEnter")]
        public string OnEnter { get; set; } = string.Empty;

        protected override void PreProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (!string.IsNullOrEmpty(OnEnter))
            {
                string js = @$"F.doPostBack({{
	                eventTarget: '{Source.ID}',
	                eventArgument: 'enter',
	                url: '?handler={OnEnter}',
	                disableControl : '{Source.ID}',
	            }});";
                this.Source.Listeners.Add(new Listener() { Handler = js, EventName = "enter" });
            }
            base.PreProcess(context, output);
        }
    }
}
