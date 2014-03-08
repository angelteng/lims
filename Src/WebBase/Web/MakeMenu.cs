using System;
using System.Text;
using System.Xml;

namespace Hope.WebBase
{
    /// <summary>
    /// MakeMenu 的摘要说明
    /// </summary>
    public class MakeMenu : MakeStrategy
    {
        // 需要生成的 HTML 源码
        private StringBuilder innerHTML = new StringBuilder();

        // 需要解析的XML源码，根据此XML生成HTML源码
        private XmlNode node;

        /// <summary>
        /// 构造函数
        /// </summary>
        public MakeMenu() { }
        
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="xn"></param>
        public MakeMenu(XmlNode xn)
        {
            this.node = xn;
        }

        /// <summary>
        /// 生成HTML源码，将其赋值到内部变量innerHTML
        /// </summary>
        public override string GenerateHTML()
        {
            string sUrl = "";
            for (int i = 0; i < this.node.ChildNodes.Count; i++)
            {
                XmlNode xn = this.node.ChildNodes[i];

                innerHTML.Append("<ul>");
                innerHTML.AppendFormat("<li onmouseover=\"MouseOverMenuL2(this,'hover');\" onmouseout=\"MouseOutMenuL2(this,'hover');\"><a href=\"{0}\">{1}</a><ul>", "javascript:void(0)", xn.Attributes["name"].Value);                    

                // 三级菜单
                foreach (XmlNode x in xn.ChildNodes)
                {
                    sUrl = x.Attributes["url"].Value;
                    innerHTML.AppendFormat("<li><a href=\"javascript:OpenMainFrame('{0}')\" title=\"{1}\">{1}</a></li>", sUrl, x.Attributes["name"].Value);
                }

                innerHTML.Append("</ul></li>");
                innerHTML.Append("</ul>");
            }

            return innerHTML.ToString();
        }
    }
}