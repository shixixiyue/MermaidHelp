
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
       /// 该面板使用了水平布局，并且支持视口模式，能够自适应不同的屏幕尺寸。
       /// </remarks>
       protected FineUICore.Panel mainPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 左侧面板，使用垂直布局，承载其他控件。<br/>
       /// 该面板的宽度比例为2，并且支持边距和内边距设置。
       /// </remarks>
       protected FineUICore.Panel leftPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 代码展示面板，提供代码返回的视图。<br/>
       /// 此面板支持自动滚动和折叠功能，并且默认展开。
       /// </remarks>
       protected FineUICore.Panel codePanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 文本面板，包含会话和输入区域。<br/>
       /// 此面板为垂直布局，能够容纳多个子项，并带有内边距和边距设置。
       /// </remarks>
       protected FineUICore.Panel textPanel;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 会话面板，显示用户的消息。<br/>
       /// 此面板支持自动滚动，并且显示边框，提供更好的视觉效果。
       /// </remarks>
       protected FineUICore.Panel messagePanel;
      
       /// <summary>
       /// TextArea
       /// </summary>
       /// <remarks>
       /// 文本输入区域，允许用户输入需求信息。<br/>
       /// 支持自动增长高度，最大高度为200像素，可提升用户体验。
       /// </remarks>
       protected FineUICore.TextArea txtInput;
      
       /// <summary>
       /// Panel
       /// </summary>
       /// <remarks>
       /// 右侧面板，显示预览内容。<br/>
       /// 此面板使用了适应性布局，能够根据内容大小自动调整，并支持滚动。
       /// </remarks>
       protected FineUICore.Panel rightPanel;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 上一页工具，允许用户查看前一页内容。<br/>
       /// 该工具使用图标表示，点击后会触发页面变更事件。
       /// </remarks>
       protected FineUICore.Tool btnUp;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 下一页工具，允许用户查看下一页内容。<br/>
       /// 该工具使用图标表示，点击后会触发页面变更事件。
       /// </remarks>
       protected FineUICore.Tool btnDown;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 退回到此工具，允许用户返回至特定页面。<br/>
       /// 该工具使用图标表示，增强用户的导航体验。
       /// </remarks>
       protected FineUICore.Tool btnBack;
      
       /// <summary>
       /// Tool
       /// </summary>
       /// <remarks>
       /// 最大化工具，允许用户将预览区域最大化。<br/>
       /// 该工具使用图标表示，点击后会触发最大化事件。
       /// </remarks>
       protected FineUICore.Tool toolMax;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// PNG格式导出按钮，允许用户将当前视图导出为PNG图片。<br/>
       /// 该按钮支持点击事件，触发相应的导出操作。
       /// </remarks>
       protected FineUICore.MenuButton btnpng;
      
       /// <summary>
       /// MenuButton
       /// </summary>
       /// <remarks>
       /// 放大按钮，允许用户将当前视图放大2倍。<br/>
       /// 该按钮支持点击事件，触发放大操作，提升用户的查看体验。
       /// </remarks>
       protected FineUICore.MenuButton btnscale;
      
   }
}
