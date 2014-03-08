using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using Hope.Util;
using NHibernate.UserTypes;

namespace Hope.ITMS.DAL
{
    public class CustomXmlProxy : IUserType
    {
        #region interface ...

        /// <summary>
        /// 本类型实例是否可变
        /// </summary>
        public bool IsMutable
        {
            get { return false; }
        }

        /// <summary>
        /// 返回数据类型
        /// </summary>
        public Type ReturnedType
        {
            get { return typeof(string); }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { return new NHibernate.SqlTypes.SqlType[] { new NHibernate.SqlTypes.SqlType(DbType.String) }; }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cached"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object Assemble(object cached, object owner)
        {
            return DeepCopy(cached);
        }

        /// <summary>
        /// 提供自定义的完全复制方法
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object DeepCopy(object value)
        {
            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public object Disassemble(object value)
        {
            return DeepCopy(value);
        }

        /// <summary>
        /// 对象比较
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public new bool Equals(object x, object y)
        {
            if (x == null || y == null)
            {
                return false;
            }
            return x == y;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        int IUserType.GetHashCode(object x)
        {
            return x.GetHashCode();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rs"></param>
        /// <param name="names"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object NullSafeGet(System.Data.IDataReader rs, string[] names, object owner)
        {
            object r = rs[names[0]];
            if (r == DBNull.Value)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(r.ToString());
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlNodeList items = root.ChildNodes;
            foreach (XmlNode item in items)
            {
                string key = item.Attributes["key"].Value;
                string value = item.InnerText;
                sb.AppendFormat("{0}={1}\n", key, value);
            }

            return sb.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            string str = value == null ? string.Empty : value.ToString();
           
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            string[] items = str.Split('\n');
            foreach (string item in items)
            {
                try
                {
                    string[] kvs = item.Split('=');
                    if (kvs.Length < 2)
                    {
                        break;
                    }

                    XmlElement node = xml.CreateElement("item");
                    node.SetAttribute("key", kvs[0]);
                    node.InnerText = kvs[1];
                    xml.DocumentElement.AppendChild(node);
                }
                catch (Exception exception)
                {
                    LogUtil.error(exception.ToString());
                }
            }

            IDataParameter parameter = (IDataParameter)cmd.Parameters[index];
            parameter.Value = xml.DocumentElement.OuterXml;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="original"></param>
        /// <param name="target"></param>
        /// <param name="owner"></param>
        /// <returns></returns>
        public object Replace(object original, object target, object owner)
        {
            return original;
        }

        #endregion
    }
}
