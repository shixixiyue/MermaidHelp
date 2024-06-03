using FineUICoreEx;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace MyFineUIEx
{
    [HtmlTargetElementAttribute("f:Panel")]
    [RestrictChildrenAttribute("Items", "Content", "Tools", "Toolbars", "Listeners", "Attributes", "IFrameAttributes")]
    public class MyPanelTagHelperEx : PanelTagHelperEx
    {
        /// <summary>
        /// 不显示边框和头
        /// </summary>
        [HtmlAttributeName("NoBorderAndHeader")]
        public bool NoBorderAndHeader { set; get; } = true;

        /// <summary>
        /// 标签处理前执行
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected override void PreProcess(TagHelperContext context, TagHelperOutput output)
        {
            if (NoBorderAndHeader)
            {
                this.Source.ShowBorder = false;
                this.Source.ShowHeader = false;
            }
            base.PreProcess(context, output);
        }
    }
}
