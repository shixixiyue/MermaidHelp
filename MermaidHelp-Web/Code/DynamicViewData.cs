using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;

namespace MermaidHelp
{
    /// <summary>
    /// 为了和之前代码兼容，手工创建 DynamicViewData 类，实际项目中，推荐使用 ViewData 属性！!
    /// .Net Core 3.1 删除了 Microsoft.AspNetCore.Mvc.ViewFeatures.Internal 命名空间，所以 DynamicViewData 也就不存在了
    /// https://docs.microsoft.com/en-us/dotnet/core/compatibility/2.2-3.1
    /// https://www.cnblogs.com/soneltd/p/4756695.html
    /// https://forums.asp.net/t/2128012.aspx?Razor+Pages+ViewBag+has+gone+
    /// https://github.com/aspnet/Mvc/issues/6754
    /// </summary>
    public class DynamicViewData : DynamicObject
    {
        /// <summary>
        /// 数据字段
        /// </summary>
        private ViewDataDictionary ViewData;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="ViewData"></param>
        public DynamicViewData(ViewDataDictionary viewData)
        {
            ViewData = viewData;
        }

        /// <summary>
        /// 调用 ViewBag["key"]; 时执行
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="indexes"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        {
            if (indexes == null || indexes.Length != 1)
            {
                throw new ArgumentException("indexes");
            }

            string key = indexes[0] as string;
            if (key != null)
            {
                result = ViewData[key];
                return true;
            }
            else
            {
                throw new ArgumentException("indexes");
            }
        }

        /// <summary>
        /// 调用 ViewBag["key"] = "value"; 时执行
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="indexes"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)
        {
            if (indexes == null || indexes.Length != 1)
            {
                throw new ArgumentException("indexes");
            }

            string key = indexes[0] as string;
            if (key != null)
            {
                ViewData[key] = value;
                return true;
            }
            else
            {
                throw new ArgumentException("indexes");
            }
        }

        /// <summary>
        /// 获取所有key
        /// </summary>
        /// <returns></returns>
        public override IEnumerable<string> GetDynamicMemberNames()
        {
            return ViewData.Keys;
        }

        /// <summary>
        /// 调用 ViewBag.key; 时执行
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public override bool TryGetMember(GetMemberBinder binder, out object result)
        {
            result = ViewData[binder.Name];
            return true;
        }

        /// <summary>
        /// 调用 ViewBag.key = "value"; 时执行
        /// </summary>
        /// <param name="binder"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool TrySetMember(SetMemberBinder binder, object value)
        {
            ViewData[binder.Name] = value;
            return true;
        }
    }
}
