using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace Hope.Util
{

    /// <summary>
    /// Xml属性
    /// </summary>
    public class XmlProperty<T>
    {
        /// <summary>
        /// 当前数据类型
        /// </summary>
        private Type CurrentType = typeof(T);

        /// <summary>
        /// 标签名
        /// </summary>
        private readonly string tabName = "property";

        /// <summary>
        /// Xml属性
        /// </summary>
        public XmlProperty()
        {
        }

        #region load data ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        public T LoadData(XmlDocument xml)
        {
            return LoadData(xml.FirstChild.ChildNodes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeList"></param>
        /// <returns></returns>
        public T LoadData(XmlNodeList nodeList)
        {
            return LoadData(nodeList, "name");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="xml"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public T LoadData(XmlDocument xml, string attributeName)
        {
            return LoadData(xml.FirstChild.ChildNodes,attributeName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="nodeList"></param>
        /// <param name="attributeName"></param>
        /// <returns></returns>
        public T LoadData(XmlNodeList nodeList, string attributeName)
        {
            T entity = default(T);
            PropertyInfo pi;
            foreach (XmlNode node in nodeList)
            {
                if (node.HasChildNodes)
                    continue;
                pi = CurrentType.GetProperty(node.Attributes[attributeName].Value);
                pi.SetValue(entity, node.InnerText, null);
            }
            return entity;
        }

        #endregion

        #region load xml ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public XmlDocument LoadXml(T entity)
        {
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            XmlNode xn;
            XmlAttribute xa;
            foreach (PropertyInfo pi in CurrentType.GetProperties())
            {
                xn = xml.CreateNode("element", tabName, "");
                xa = xml.CreateAttribute("name");
                xa.Value = pi.Name;
                xn.InnerText = pi.GetValue(entity, null).ToString();
                xn.Attributes.Append(xa);
                xml.DocumentElement.AppendChild(xn);
            }
            return xml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity">数据实体</param>
        /// <param name="propertyNames">需要转换为xml的实体属性名</param>
        /// <param name="attributeName">xml属性名</param>
        /// <returns></returns>
        public XmlDocument LoadXml(T entity,string[] propertyNames,string attributeName)
        {
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            XmlNode xn;
            XmlAttribute xa;
            PropertyInfo pi;
            foreach (string propertyName in propertyNames)
            {
                pi = CurrentType.GetProperty(propertyName);

                xn = xml.CreateNode("element", tabName, "");
                xa = xml.CreateAttribute(attributeName);
                xa.Value = propertyName;
                xn.InnerText = pi.GetValue(entity, null).ToString();
                xn.Attributes.Append(xa);
                xml.DocumentElement.AppendChild(xn);
            }

            return xml;
        }

        #endregion        

    }
}
