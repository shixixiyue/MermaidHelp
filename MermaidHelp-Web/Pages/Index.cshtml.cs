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
             * �û���ĶԻ������û�о�new MermaidMask()
             * �ύ �ý�� �ٷŻػ���
             */
            MermaidMask mask = GetSession<MermaidMask>(sessionkey);
            if (mask == null) { mask = new MermaidMask(); }
            mask.adduser(input);

            var msg = mask.GetResult().Result;

            mask.addassistant(msg);

            SetSession(sessionkey, mask);
            // �� Windows ���Ļ��з� \r\n �滻Ϊ \n
            string normalizedInput = msg.Replace("\r\n", "\n");

            // �����з� \n �滻Ϊ <br> ��ǩ
            string output = normalizedInput.Replace("\n", "\\n");

            return output;
        }
    }
}
