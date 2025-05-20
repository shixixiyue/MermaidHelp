
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
       /// 该面板允许展示左侧的输入部分和右侧的预览部分，整体采用水平布局，提供良好的用户体验。
       /// </remarks>
       protected FineUICore.Panel mainPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 左侧面板，主要用于展示代码和输入区域。<br/>
       /// 该面板包含两个子面板，一个用于显示返回的代码，另一个用于输入需求，采用垂直布局。
       /// </remarks>
       protected FineUICore.Panel leftPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 内容面板，用于展示返回的代码。<br/>
       /// 该面板支持自动滚动和折叠功能，初始状态为展开，允许用户查看长代码内容。
       /// </remarks>
       protected FineUICore.Panel codePanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 文本面板，包含输入区域和会话记录。<br/>
       /// 该面板支持扩展，且包含多个工具，如新建会话按钮和输入框。
       /// </remarks>
       protected FineUICore.Panel textPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 消息面板，用于显示会话记录。<br/>
       /// 该面板支持自动滚动，便于查看历史消息。
       /// </remarks>
       protected FineUICore.Panel messagePanel;
      
       /// <summary>
       /// TextArea
       /// </summary>
       /// <remarks>
       /// 文本区，用于用户输入需求。<br/>
       /// 支持自动增长，最大高度为200像素，提升用户输入体验。
       /// </remarks>
       protected FineUICore.TextArea txtInput;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 右侧内容面板，主要用于展示预览内容。<br/>
       /// 此面板支持自动滚动，且包含多个工具按钮，支持页面导航和图像导出功能。
       /// </remarks>
       protected FineUICore.Panel rightPanel;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 工具按钮，允许用户返回上一页。<br/>
       /// 点击后触发页面向上导航事件。
       /// </remarks>
       protected FineUICore.Tool btnUp;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 工具按钮，允许用户返回下一页。<br/>
       /// 点击后触发页面向下导航事件。
       /// </remarks>
       protected FineUICore.Tool btnDown;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 工具按钮，允许用户退回到指定页面。<br/>
       /// 点击后触发退回事件，便于用户快速返回。
       /// </remarks>
       protected FineUICore.Tool btnBack;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 工具按钮，用于最大化预览内容。<br/>
       /// 点击后会将预览区域最大化，提供更好的查看体验。
       /// </remarks>
       protected FineUICore.Tool toolMax;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// 菜单按钮，用于导出PNG格式的图片。<br/>
       /// 点击后会触发导出图片的事件，允许用户保存预览内容。
       /// </remarks>
       protected FineUICore.MenuButton btnpng;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// 菜单按钮，用于放大预览内容。<br/>
       /// 点击后会触发放大事件，便于用户查看细节。
       /// </remarks>
       protected FineUICore.MenuButton btnscale;
      
   }
}
