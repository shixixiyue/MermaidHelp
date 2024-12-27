
//------------------------------------------------------------------------------
//     这段代码是由没想好生成的。
//     对这个文件的更改可能导致不正确的行为，并且如果代码重新生成，这些更改会丢失.
//     在 .cshtml 文件中添加 //NoFineUIDesigner 注释，以防止生成此文件。
//     或者在 appsettings 中添加 FineUIDesigner:false，以防止生成此文件。
//------------------------------------------------------------------------------
namespace MermaidHelp.Pages
{
   public partial class IndexModel
   {
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 主面板，包含整个页面的布局和内容。<br/>
       /// 该面板设置为视口，具有边框和标题的选项，内部布局使用HBox布局，允许子面板水平排列。
       /// </remarks>
       protected FineUICore.Panel mainPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 左侧面板，包含代码展示区域和输入区域。<br/>
       /// 该面板配置为VBox布局，允许子面板垂直排列，并具有可调整的宽度。
       /// </remarks>
       protected FineUICore.Panel leftPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 代码面板，显示返回的代码。<br/>
       /// 此面板支持自动滚动，允许折叠，并在展开时显示边框。
       /// </remarks>
       protected FineUICore.Panel codePanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 文本面板，包含输入和会话内容。<br/>
       /// 该面板也使用VBox布局，允许子面板垂直排列，提供足够的空间展示内容。
       /// </remarks>
       protected FineUICore.Panel textPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 消息面板，显示会话信息。<br/>
       /// 该面板支持自动滚动并显示边框，方便用户查看历史消息。
       /// </remarks>
       protected FineUICore.Panel messagePanel;
      
       /// <summary>
       /// TextArea
       /// </summary>
       /// <remarks>
       /// 输入区域，允许用户输入需求。<br/>
       /// 该文本区域支持自动增长，最大高度为200像素，提供良好的用户体验。
       /// </remarks>
       protected FineUICore.TextArea txtInput;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 右侧面板，显示预览内容。<br/>
       /// 该面板支持自动滚动，具有适应内容的布局，并显示边框。
       /// </remarks>
       protected FineUICore.Panel rightPanel;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 上一页工具，允许用户导航到上一个视图。<br/>
       /// 该工具使用左箭头图标，点击后触发相应的客户端事件。
       /// </remarks>
       protected FineUICore.Tool btnUp;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 下一页工具，允许用户导航到下一个视图。<br/>
       /// 该工具使用右箭头图标，点击后触发相应的客户端事件。
       /// </remarks>
       protected FineUICore.Tool btnDown;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 返回工具，允许用户返回到特定的视图。<br/>
       /// 该工具使用返回图标，点击后触发相应的客户端事件。
       /// </remarks>
       protected FineUICore.Tool btnBack;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 最大化工具，允许用户将预览面板最大化。<br/>
       /// 该工具使用文件图标，点击后触发最大化事件。
       /// </remarks>
       protected FineUICore.Tool toolMax;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// PNG导出按钮，允许用户将内容导出为PNG格式。<br/>
       /// 该按钮点击后触发相应的客户端事件。
       /// </remarks>
       protected FineUICore.MenuButton btnpng;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// 放大按钮，允许用户将预览内容放大2倍。<br/>
       /// 该按钮点击后触发相应的客户端事件。
       /// </remarks>
       protected FineUICore.MenuButton btnscale;
      
   }
}
