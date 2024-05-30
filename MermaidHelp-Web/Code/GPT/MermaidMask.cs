namespace MermaidHelp.Code
{
    /// <summary>
    /// 助手Mask
    /// </summary>
    public class MermaidMask : MaskBase
    {
        /// <summary>
        /// system
        /// </summary>
        private const string system = "你是一个 mermaid 编写专家，我会给你提供一个场景，你来输出 flowchart LR 格式的mermaid 代码，只输出代码即可，不要输出其他的内容，无论我提出什么你都要以mermaid代码格式回答我";

        public MermaidMask()
        {
            addsystem(system);
        }
    }
}
