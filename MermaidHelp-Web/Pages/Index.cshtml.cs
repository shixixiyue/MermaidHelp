using MermaidHelp.Code;
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

        protected async Task btnPage_Click(object sender, EventArgs e)
        {
            var page = Request.Form["page"];
            MermaidMask mask = GetSession<MermaidMask>(sessionkey);
            RegisterStartupScript($"F.ui.codePanel.setText('{mask.assistantsaid[Convert.ToInt32(page)]}');");
        }

        protected async Task btnBack_Click(object sender, EventArgs e)
        {
            var page = Request.Form["page"];
            var mask = new MermaidMask();
            MermaidMask oldmask = GetSession<MermaidMask>(sessionkey);
            for (int i = 0; i < Convert.ToInt32(page) + 1; i++)
            {
                mask.adduser(oldmask.usersaid[i]);
                mask.addassistant(oldmask.assistantsaid[i]);
            }

            SetSession(sessionkey, mask);
        }

        private const string sessionkey = "Mermaid";

        private string GetMessage(string input)
        {
            /*
             * 拿缓存的对话，如果没有就new MermaidMask()
             * 提交 拿结果 再放回缓存
             */
            MermaidMask mask = GetSession<MermaidMask>(sessionkey);
            if (mask == null)
            {
                mask = new MermaidMask();
            }

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