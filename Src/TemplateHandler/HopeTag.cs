using System;
using System.Web;

using Hope.Util;
using Hope.ITMS.Model;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 标签
    /// </summary>
    public partial class HopeTag
    {
        #region 构造函数

        /// <summary>
        /// 构造函数
        /// </summary>
        public HopeTag()
        {

        }

        #endregion

        #region 变量定义

        /// <summary>
        /// VelocityHelper实例
        /// </summary>
        public VelocityHelper TemplateHandlerInstance = null;

        //private IQuery<ContentArticle, int> query;
        //private IQuery<ContentNode, int> query_node;

        #endregion

        #region 工具类

        /// <summary>
        /// 设置默认风格
        /// </summary>
        /// <param name="cssInput"></param>
        /// <returns></returns>
        private static string SetDefaultClassName(string cssInput)
        {
            string css = "paginator";

            if (!string.IsNullOrEmpty(cssInput))
            {
                css = cssInput;
            }

            return css;
        }

        ///// <summary>
        ///// 将html 转化成纯文本
        ///// </summary>
        ///// <param name="source"></param>
        ///// <returns></returns>
        ///// <example>
        ///// 调用：${HopeTag.Html2Txt("testedtext")}
        ///// </example>
        //public string Html2Txt(string source)
        //{
        //    return CommonClass.ConvertHtml2Txt(source);
        //}

        /// <summary>
        /// 格式化标题
        /// </summary>
        /// <param name="title">标题内容</param>
        /// <param name="maxChar">最大字符数</param>
        /// <returns>格式化后的标题</returns>
        /// <example>
        /// 调用：${HopeTag.FormatTitle("$article.Title",25)}
        /// 输出HTML格式化后的列表页的标题，最大显示字数为25，超出以…代替
        /// </example>
        public string FormatTitle(string title, int maxChar)
        {
            return FormatTitle(title, maxChar, "…");
        }

        /// <summary>
        /// 格式化标题
        /// </summary>
        /// <param name="title">标题内容</param>
        /// <param name="maxChar">最大字符数</param>
        /// <param name="alternativeCharacters">代替字符串</param>
        /// <returns>格式化后的标题</returns>
        /// <example>
        /// 调用：${HopeTag.FormatTitle("$article.Title",25,"")}
        /// 输出HTML格式化后的列表页的标题，最大显示字数为25，超出以alternativeCharacters代替
        /// </example>
        public string FormatTitle(string title, int maxChar, string alternativeCharacters)
        {
            string output = title;

            if (string.IsNullOrEmpty(alternativeCharacters))
            {
                alternativeCharacters = string.Empty;
            }

            if (maxChar > 1 && maxChar < output.Length)
            {
                output = output.Substring(0, maxChar) + alternativeCharacters;
            }

            return output;
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sbcLength"></param>
        /// <returns></returns>
        public string FormatString(string str, int sbcLength)
        {
            return FormatString(str, sbcLength, "…");
        }

        /// <summary>
        /// 格式化字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="sbcLength"></param>
        /// <param name="alternativeCharacters"></param>
        /// <returns></returns>
        public string FormatString(string str, int sbcLength, string alternativeCharacters)
        {
            string outputStr = str;

            if (sbcLength < outputStr.Length)
            {
                if (string.IsNullOrEmpty(alternativeCharacters))
                {
                    alternativeCharacters = string.Empty;
                }

                outputStr = string.Format("{0}{1}", StringHelper.FormatStr(outputStr, sbcLength), alternativeCharacters);
            }

            return outputStr;
        }

        /// <summary>
        /// 格式化日期,格式默认为："yyyy-MM-dd"
        /// </summary>
        /// <param name="dt">日期</param>
        /// <returns>格式化后的日期</returns>
        /// 调用：${HopeTag.FormatDateTime($article.UpdateTime)}
        /// 输出：2009-12-24
        public string FormatDateTime(DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 格式化日期
        /// </summary>
        /// <param name="dt">日期</param>
        /// <param name="format">格式，默认为："yyyy-MM-dd"</param>
        /// <returns>格式化后的日期</returns>
        /// 调用：${HopeTag.FormatDateTime($article.UpdateTime,"yyyy-MM-dd HH:mm:ss")}
        /// 输出：200-12-24 20:22:01
        /// 
        /// 调用：${HopeTag.FormatDateTime($article.UpdateTime,"MM-dd")}
        /// 输出：12-24
        /// 
        /// 调用：${HopeTag.FormatDateTime($article.UpdateTime,"")}
        /// 输出：2009-12-24
        public string FormatDateTime(DateTime dt, string format)
        {
            string formats = string.IsNullOrEmpty(format) ? "yyyy-MM-dd" : format;
            return dt.ToString(formats);
        }

        /// <summary>
        /// 对 HTML 编码的字符串进行解码，并返回已解码的字符串。
        /// </summary>
        /// <param name="s">要解码的 HTML 字符串。</param>
        /// <returns>已解码的文本。</returns>
        /// 调用：${HopeTag.HtmlDecode("string to decode")}
        public string HtmlDecode(string s)
        {
            return System.Web.HttpContext.Current.Server.HtmlDecode(s);
        }

        /// <summary>
        /// 对字符串进行 HTML 编码并返回已编码的字符串。
        /// </summary>
        /// <param name="s">要编码的文本字符串。</param>
        /// <returns>HTML 已编码的文本。</returns>
        /// 调用：${HopeTag.HtmlEncode("string to encode")}
        public string HtmlEncode(string s)
        {
            return System.Web.HttpContext.Current.Server.HtmlEncode(s);
        }

        /// <summary>
        /// 输出当前访问的IP
        /// </summary>
        /// <returns></returns>
        public string CurrentClientIP()
        {
            return CommonHelper.IPAddress;
        }

        /// <summary>
        /// 获取客户端浏览器类型及版本
        /// </summary>
        /// <returns>客户端浏览器类型及版本</returns>
        public string GetClientBrowserType()
        {
            string userAgent = HttpContext.Current.Request.UserAgent;
            if (userAgent != null)
            {
                string browserName = string.Empty;
                if (userAgent.Contains("MSIE 9.0"))
                {
                    browserName = "IE 9";
                }
                else if (userAgent.Contains("MSIE 8.0"))
                {
                    browserName = "IE 8";
                }
                else if (userAgent.Contains("MSIE 7.0"))
                {
                    browserName = "IE 7";
                }
                else if (userAgent.Contains("MSIE 6.0"))
                {
                    browserName = "IE 6";
                }
                else if (userAgent.Contains("Chrome"))
                {
                    browserName = "Chrome";
                }
                else if (userAgent.Contains("Firefox"))
                {
                    browserName = "Firefox";
                }
                else if (userAgent.Contains("Opera"))
                {
                    browserName = "Opera";
                }
                else if (userAgent.Contains("Safari"))
                {
                    browserName = "Safari";
                }
                else
                {
                    browserName = "other";
                }
                return browserName;
            }
            else
            {
                return "";
            }
        }

        /// <summary>
        /// 返回客户端浏览器版本
        /// </summary>
        public string ClientBrowserVersion
        {
            get
            {
                return CommonHelper.GetClientBrowserVersion();
            }
        }

        #endregion
    }

    /// <summary>
    /// PagerEventArgs
    /// </summary>
    public partial class PagerEventArgs : EventArgs
    {
        /// <summary>
        /// NodeID
        /// </summary>
        public int NodeID;
        /// <summary>
        /// PageCount
        /// </summary>
        public int PageCount;
    }
}