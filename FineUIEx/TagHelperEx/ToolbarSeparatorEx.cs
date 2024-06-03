using FineUICore;
using FineUICoreEx;
using FineUICoreEx.BaseEx;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MyFineUIEx
{
    public class ToolbarSeparatorEx : ITagHelperEx<ToolbarSeparatorTagHelperEx>
    {
        public void BeforProcess(ToolbarSeparatorTagHelperEx tagHelper, TagHelperContext context, TagHelperOutput output, TagHelperExFun method)
        {
            var currentPath = method.GetCurrentPath();
            var reverse = currentPath.AsEnumerable().Reverse();
            //判断是不是在 ToolsEx 里
            if (currentPath.Any(m => m.GetType() == typeof(ToolsEx)))
            {
                var nextItem = reverse.SkipWhile(m => m.GetType() == typeof(ToolsEx)).First();
                var item = nextItem as PanelBaseTagHelper;
                var newtool = new Tool();
                item.Source.Tools.Add(newtool);
                StringBuilder sb = new();
                sb.Append($"$('#{newtool.ID}').replaceWith('<div class=\"f-toolbar-separator f-toolbar-item f-cmp f-widget\" style=\"display: initial;\"></div>');F.ui.{tagHelper.ID}.remove();$('#{tagHelper.Source.WrapperID}').remove();");
                PageContext.RegisterStartupScript(sb.ToString());
            }
        }
    }

    //public class ToolSeparato : Tool
    //{
    //    private readonly string panelId;

    //    public ToolSeparato(string panelId)
    //    {
    //        this.panelId = panelId;
    //    }

    //    protected override void OnFirstPreRender()
    //    {
    //        //base.OnFirstPreRender();
    //        var id = this.GetType().GetMethod("A", BindingFlags.NonPublic | BindingFlags.Instance, null, Type.EmptyTypes, null)?.Invoke(this, null);
    //        StringBuilder sb = new();
    //        sb.Append($"var {id}=new F.ToolbarSeparator({{style: 'display: initial;'}});");
    //        AddStartupScript(sb.ToString());
    //    }
    //}

    //[HtmlTargetElement("f:ToolSeparator")]
    //public class ToolSeparator : ITagHelper
    //{
    //    public int Order => 0;

    //    public void Init(TagHelperContext context)
    //    {
    //        // Initialization logic if needed
    //    }

    //    public async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
    //    {
    //        // Set the tag name to div
    //        output.TagName = "div";

    //        // Add the necessary attributes
    //        output.Attributes.SetAttribute("class", "f-toolbar-separator f-toolbar-item f-cmp f-widget");
    //        output.Attributes.SetAttribute("style", "display: initial;");

    //        // Clear the content inside the tag
    //        output.Content.Clear();

    //        // Optionally, if you need to process child content
    //        var childContent = await output.GetChildContentAsync();
    //        output.Content.SetHtmlContent(childContent);
    //    }
    //}
}
