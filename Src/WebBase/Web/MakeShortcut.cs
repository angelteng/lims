using System;
using System.Text;
using System.Xml;
using Hope.Util;

namespace Hope.WebBase
{
    /// <summary>
    /// MakeMenu 的摘要说明
    /// </summary>
    public class MakeShortcut : MakeStrategy
    {
        // 需要生成的 HTML 源码
        private StringBuilder innerHTML = new StringBuilder();

        // 需要解析的XML源码，根据此XML生成HTML源码
        private XmlNode node;

        /// <summary>
        /// 构造函数，初始化类实例
        /// </summary>
        public MakeShortcut() { }
        
        /// <summary>
        /// 构造函数，初始化节点
        /// </summary>
        /// <param name="xn"></param>
        public MakeShortcut(XmlNode xn)
        {
            this.node = xn;
        }

        /// <summary>
        /// 生成HTML源码，将其赋值到内部变量innerHTML
        /// </summary>
        public override string GenerateHTML()
        {
            string sUrl = "";
            string suffix1 = "&nbsp;&nbsp;&nbsp;</li>";
            string suffix2 = "&nbsp;&nbsp;&nbsp;|</li>";

            foreach (XmlNode xn in this.node.ChildNodes)
            {
                // 标题
                sUrl = AppConfig.WebMainPathURL.TrimEnd('/') + xn.Attributes["url"].Value.Replace("~","");
                innerHTML.AppendFormat("<li><a href='javascript:GotoLink(\"{0}\")'>{1}</a>", sUrl, xn.Attributes["name"].Value);
                if (xn.Equals(this.node.LastChild))
                {
                    innerHTML.Append(suffix1);
                }
                else
                {
                    innerHTML.Append(suffix2);
                }
            }

            return innerHTML.ToString();
        }
    }
}