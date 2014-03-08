using System;
using System.Text;
using System.Xml;

namespace Hope.WebBase
{
    /// <summary>
    /// MakeMenu 的摘要说明
    /// </summary>
    public class MakeUserMenu : MakeStrategy
    {
        // 需要生成的 HTML 源码
        private StringBuilder innerHTML = new StringBuilder();

        // 需要解析的XML源码，根据此XML生成HTML源码
        private XmlNode node;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MakeUserMenu() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xn"></param>
        public MakeUserMenu(XmlNode xn)
        {
            this.node = xn;
        }
        /// <summary>
        /// 生成HTML源码，将其赋值到内部变量innerHTML
        /// </summary>
        public override string GenerateHTML()
        {
            foreach (XmlNode xn in this.node.ChildNodes)
            {
                // 标题
                innerHTML.AppendFormat("<h1 class=\"type\"><a href=\"{0}\">{1}</a></h1>", xn.Attributes["url"].Value, xn.Attributes["name"].Value);

                // 子标题
                innerHTML.Append("<div class=\"content\">");
                innerHTML.Append("<table width=\"100%\" border=\"0\" cellspacing=\"0\" cellpadding=\"0\">");
                innerHTML.Append("  <tr>");
                innerHTML.Append("    <td><img src=\"../../Images/Admin/Default/menu_topline.gif\" width=\"182\" height=\"5\" /></td>");
                innerHTML.Append("  </tr>");
                innerHTML.Append("</table>");

                if (xn.HasChildNodes)
                {
                    // 生成栏目
                    foreach (XmlNode n in xn)
                    {
                        string sUrl = n.Attributes["url"].Value;
                        //sUrl = sUrl;

                        if (n.Equals(xn.FirstChild))
                        {
                            //第一个
                            innerHTML.Append("<ul class=\"MM\">");
                        }
                        if (n.Equals(xn.LastChild))
                        {
                            //最后一个
                            innerHTML.AppendFormat("<li onclick=\"OpenMainFrame('{0}')\" class=\"{1}\"><a href=\"javascript:void(0);\">{2}</a></li>", sUrl, "b", n.Attributes["name"].Value);
                            innerHTML.Append("</ul>");
                            break;
                        }

                        innerHTML.AppendFormat("<li onclick=\"OpenMainFrame('{0}')\"><a href=\"javascript:void(0);\">{1}</a></li>", sUrl, n.Attributes["name"].Value);

                    }
                }

                innerHTML.Append("</div>");
            }

            return innerHTML.ToString();
        }
    }
}