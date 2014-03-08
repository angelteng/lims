using System;
using System.Data;
using System.Xml;

using NHibernate;
using NHibernate.UserTypes;
using Hope.Util;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// 
    /// </summary>
    public class XmlDocumentProxy : IUserType
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
            get { return typeof(XmlDocument); }
        }

        /// <summary>
        /// 
        /// </summary>
        public NHibernate.SqlTypes.SqlType[] SqlTypes
        {
            get { return new NHibernate.SqlTypes.SqlType[] { new NHibernate.SqlTypes.SqlType(DbType.Xml) }; }
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
            return ((XmlDocument)x).InnerXml == ((XmlDocument)y).InnerXml;

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
                return null;
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(r.ToString());

            return xmlDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cmd"></param>
        /// <param name="value"></param>
        /// <param name="index"></param>
        public void NullSafeSet(System.Data.IDbCommand cmd, object value, int index)
        {
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            if (value != null)
            {
                xml = (XmlDocument)value;
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
