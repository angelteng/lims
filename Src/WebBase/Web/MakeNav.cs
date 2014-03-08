using System;
using System.Text;
using System.Xml;

namespace Hope.WebBase
{
    /// <summary>
    /// MakeMenu 的摘要说明
    /// </summary>
    public class MakeNav : MakeStrategy
    {
        // 需要生成的 HTML 源码
        private StringBuilder innerHTML = new StringBuilder();

        // 需要解析的XML源码，根据此XML生成HTML源码
        private XmlNode node;

        /// <summary>
        /// 构造函数，初始化类实例
        /// </summary>
        public MakeNav() { }

        /// <summary>
        /// 构造函数，初始化节点
        /// </summary>
        /// <param name="xn"></param>
        public MakeNav(XmlNode xn)
        {
            this.node = xn;
        }

        /// <summary>
        /// 生成HTML源码，将其赋值到内部变量innerHTML
        /// </summary>
        public override string GenerateHTML()
        {
            for (int i = 0; i < node.ChildNodes.Count; i++)
            {
                if (i == 0)
                {
                    innerHTML.AppendFormat(" <li class=\"clicked\" onclick=\"UpdateMenu(this,'{0}');\" onmouseover=\"MouseOverMenu(this,'hover');\" onmouseout=\"MouseOutMenu(this,'hover');\">{1}</li>", node.ChildNodes[i].Attributes["id"].Value, node.ChildNodes[i].Attributes["name"].Value);
                }
                else
                {
                    innerHTML.AppendFormat(" <li onclick=\"UpdateMenu(this,'{0}');\" onmouseover=\"MouseOverMenu(this,'hover');\" onmouseout=\"MouseOutMenu(this,'hover');\">{1}</li>", node.ChildNodes[i].Attributes["id"].Value, node.ChildNodes[i].Attributes["name"].Value);
                }                
            }

            return innerHTML.ToString();
        }
    }
}