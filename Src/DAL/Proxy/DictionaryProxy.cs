using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using Hope.Util;
using NHibernate.SqlTypes;
using NHibernate.UserTypes;

namespace Hope.ITMS.DAL
{
    public class DictionaryProxy : IUserType
    {
        #region interface ...

        #region 

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
            get { return typeof (Dictionary<int, string>); }
        }

        /// <summary>
        /// 数据库类型
        /// </summary>
        public SqlType[] SqlTypes
        {
            get { return new SqlType[] {new SqlType(DbType.String)}; }
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

        #endregion


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
            Dictionary<string, string> dic = new Dictionary<string, string>();

            if (r == DBNull.Value)
            {
                return dic;
            }

            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.LoadXml(r.ToString());
            XmlNode root = xmlDoc.SelectSingleNode("root");
            XmlNodeList items = root.ChildNodes;
            foreach (XmlNode item in items)
            {
                string key = item.Attributes["key"].Value;
                string value = item.InnerText;
                dic.Add(key, value);
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            Dictionary<string, string> dic = (Dictionary<string, string>)value;
           
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            XmlElement root = xml.DocumentElement;
            foreach (KeyValuePair<string, string> keyValuePair in dic)
            {
                XmlElement node = xml.CreateElement("item");
                node.SetAttribute("key", keyValuePair.Key);
                node.InnerText = keyValuePair.Value;
                root.AppendChild(node);
            }

            IDataParameter parameter = (IDataParameter)cmd.Parameters[index];
            parameter.Value = root.OuterXml;
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
