

namespace MermaidHelp
{
    /// <summary>
    /// gptapi提交
    /// </summary>
    public static class GPTHelper
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public static async Task<Rootobject> POST(MaskBase mask, GPTpost content = null)
        {
            if (content == null)
            {
                content = new GPTpost()
                {
                    messages = mask.messages,
                    model = HelpStep.Projects.model != null ? HelpStep.Projects.model : HelpStep.Projects.model
                };
            }
            content.messages = mask.messages;
            HttpClient client = new HttpClient();
            client.Timeout = TimeSpan.FromHours(1);
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            client.DefaultRequestHeaders.Add("Authorization", $"Bearer {HelpStep.Projects.key}");

            StringContent strcontent = new StringContent(content.ToString(), Encoding.UTF8, "application/json");

            var result = await client.PostAsync(HelpStep.Projects.url, strcontent);
            var msg = (await result?.Content?.ReadAsStringAsync()) ?? "";
            var res = JObject.Parse(msg).ToObject<Rootobject>();
            return res;
        }

        /// <summary>
        /// 得到结果
        /// </summary>
        /// <param name="mask"></param>
        /// <returns></returns>
        public static async Task<string> GetResult(this MaskBase mask, GPTpost content = null)
        {
            var recontent = await POST(mask, content);
            return recontent.GetMessage();
        }
    }

    #region 基类

    public class Rootobject
    {
        public string id { get; set; }
        public string _object { get; set; }
        public int created { get; set; }
        public string model { get; set; }
        public Choice[] choices { get; set; }
        public Usage usage { get; set; }
        public object system_fingerprint { get; set; }

        public string GetMessage()
        {
            return choices?[0]?.message?.content ?? "";
        }
    }

    public class Usage
    {
        public int prompt_tokens { get; set; }
        public int completion_tokens { get; set; }
        public int total_tokens { get; set; }
    }

    public class Choice
    {
        public int index { get; set; }
        public Message message { get; set; }
        public object logprobs { get; set; }
        public string finish_reason { get; set; }
    }

    public class Message
    {
        public string role { get; set; }
        public string content { get; set; }
    }

    /// <summary>
    /// 提交基类
    /// </summary>
    public class GPTpost
    {
        /// <summary>
        ///
        /// </summary>
        public JArray messages { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string model { get; set; } = "gpt-3.5-turbo-16k";//gpt-3.5-turbo|gpt-3.5-turbo-16k-0613

        /// <summary>
        ///
        /// </summary>
        public int max_tokens { get; set; } = 4000;

        /// <summary>
        ///
        /// </summary>
        public bool stream { get; set; } = false;

        /// <summary>
        /// 回答同样一个问题的相似度
        /// </summary>
        public double temperature { get; set; } = 0.6;

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.ToJson();
        }
    }

    /// <summary>
    /// 面具基类
    /// </summary>
    public class MaskBase
    {
        /// <summary>
        /// 消息主体
        /// </summary>
        public JArray messages { get; set; } = new JArray();

        /// <summary>
        /// 用户说
        /// </summary>
        public List<string> usersaid { get; private set; } = new List<string>();

        /// <summary>
        /// 机器人说
        /// </summary>
        public List<string> assistantsaid { get; private set; } = new List<string>();

        /// <summary>
        /// 新增system角色
        /// </summary>
        /// <param name="str"></param>
        public void addsystem(string str)
        {
            var role = "system";
            var content = str;
            messages.Add(JObject.FromObject(new { role, content }));
        }

        /// <summary>
        /// 用户说
        /// </summary>
        /// <param name="str"></param>
        public void adduser(string str)
        {
            var role = "user";
            var content = str;
            usersaid.Add(str);
            messages.Add(JObject.FromObject(new { role, content }));
        }

        /// <summary>
        /// 机器人恢复说
        /// </summary>
        /// <param name="str"></param>
        public void addassistant(string str)
        {
            var role = "assistant";
            var content = str;
            assistantsaid.Add(str);
            messages.Add(JObject.FromObject(new { role, content }));
        }
    }

    #endregion 基类
}
