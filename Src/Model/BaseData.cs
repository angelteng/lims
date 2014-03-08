
using System;
using System.Collections.Generic;
using System.Xml;

namespace Hope.ITMS.Model
{

    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class BaseData
    {

        /// <summary>
        /// 
        /// </summary>
        public BaseData()
        {
            Values = new Dictionary<string, string>();
            Kyes = new Dictionary<string, string>();
        }

        protected readonly string elementTag = "item";
        protected readonly string elementName = "name";

        /// <summary>
        /// 创建xml元素
        /// </summary>
        /// <param name="xml">xml文档</param>
        /// <param name="name">元素名称</param>
        /// <param name="value">元素值</param>
        /// <returns>返回xml元素</returns>
        protected XmlElement CreateElement(XmlDocument xml, string name, string value)
        {
            XmlElement node = xml.CreateElement(elementTag);
            node.InnerText = value;

            XmlAttribute attr = xml.CreateAttribute(elementName);
            attr.Value = name;

            node.Attributes.Append(attr);

            return node;
        }

        /// <summary>
        /// 所有定义值
        /// </summary>
        public virtual Dictionary<string, string> Values
        {
            get;
            set;
        }

        /// <summary>
        /// 自定义键
        /// </summary>
        public virtual Dictionary<string, string> Kyes
        {
            get;
            set;
        }
    }
}
