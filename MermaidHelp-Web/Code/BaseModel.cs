using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MermaidHelp
{
    public class BaseModel : PageModel
    {
        #region IsPostBack

        /// <summary>
        /// 是否页面回发
        /// </summary>
        public bool IsPostBack
        {
            get
            {
                return FineUICore.PageContext.IsFineUIAjaxPostBack();
            }
        }

        #endregion IsPostBack

        #region RegisterStartupScript

        /// <summary>
        /// 注册客户端脚本
        /// </summary>
        /// <param name="scripts"></param>
        public void RegisterStartupScript(string scripts)
        {
            FineUICore.PageContext.RegisterStartupScript(scripts);
        }

        #endregion RegisterStartupScript

        #region ViewBag

        private DynamicViewData _viewBag;

        /// <summary>
        /// Add ViewBag to PageModel
        /// https://forums.asp.net/t/2128012.aspx?Razor+Pages+ViewBag+has+gone+
        /// https://github.com/aspnet/Mvc/issues/6754
        /// </summary>
        public dynamic ViewBag
        {
            get
            {
                if (_viewBag == null)
                {
                    _viewBag = new DynamicViewData(ViewData);
                }
                return _viewBag;
            }
        }

        #endregion ViewBag

        #region ShowNotify

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        public virtual void ShowNotify(string message)
        {
            ShowNotify(message, MessageBoxIcon.Information);
        }

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon)
        {
            ShowNotify(message, messageIcon, Target.Top);
        }

        /// <summary>
        /// 显示通知对话框
        /// </summary>
        /// <param name="message"></param>
        /// <param name="messageIcon"></param>
        /// <param name="target"></param>
        public virtual void ShowNotify(string message, MessageBoxIcon messageIcon, Target target)
        {
            Notify n = new Notify();
            n.Target = target;
            n.Message = message;
            n.MessageBoxIcon = messageIcon;
            n.PositionX = Position.Center;
            n.PositionY = Position.Top;
            n.DisplayMilliseconds = 3000;
            n.ShowHeader = false;

            n.Show();
        }

        #endregion ShowNotify

        #region Session

        /// <summary>
        /// 得到Session
        /// </summary>
        /// <param name="key">键</param>
        public T GetSession<T>(string key)
        {
            var value = FineUICore.PageContext.Current.Session.GetString(key);
            if (string.IsNullOrEmpty(value))
            {
                value = string.Empty;
                return default(T);
            }
            JObject _value = JObject.Parse(value);
            T ms = (T)_value.ToObject(typeof(T));
            return ms;
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        protected void SetSession(string key, string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = "";
            }
            //IHttpContextAccessor PageContext = PageContext.
            var httpContextAccessor = FineUICore.PageContext.GetService<IHttpContextAccessor>();
            FineUICore.PageContext.Current.Session.SetString(key, value);
            //HttpContext.Session.SetString(key, value);
        }

        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        protected void SetSession(string key, object value)
        {
            string _value = JObject.FromObject(value).ToString(Newtonsoft.Json.Formatting.None);
            SetSession(key, _value);
        }

        #endregion Session
    }
}