using FineUICore;
using MermaidHelp.Code;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR.Protocol;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace MermaidHelp.Pages
{
    public partial class IndexModel : BaseModel
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected async Task txtInput_Enter(object sender, EventArgs e)
        {
            var inpput = txtInput.Text;
            var output = GetMessage(inpput);
            //RegisterStartupScript($"F.ui.messagePanel.setText('{inpput}');");
            RegisterStartupScript($"F.ui.codePanel.setText('{output}');");
        }

        protected async Task btnRefresh_Click(object sender, EventArgs e)
        {
            SetSession(sessionkey, new MermaidMask());
            RegisterStartupScript($"F.ui.messagePanel.clear();");
        }

        private const string sessionkey = "Mermaid";

        private string GetMessage(string input)
        {
            /*
             * 拿缓存的对话，如果没有就new MermaidMask()
             * 提交 拿结果 再放回缓存
             */
            MermaidMask mask = GetSession<MermaidMask>(sessionkey);
            if (mask == null) { mask = new MermaidMask(); }
            mask.adduser(input);

            var msg = mask.GetResult().Result;

            mask.addassistant(msg);

            SetSession(sessionkey, mask);
            // 将 Windows 风格的换行符 \r\n 替换为 \n
            string normalizedInput = msg.Replace("\r\n", "\n");

            // 将换行符 \n 替换为 <br> 标签
            string output = normalizedInput.Replace("\n", "\\n");

            return output;
        }
    }
}
