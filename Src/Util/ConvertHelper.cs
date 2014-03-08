using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Collections.Generic;
using System.Xml;

namespace Hope.Util
{
    /// <summary>
    /// 数据类型转换类
    /// </summary>
    public class ConvertHelper
    {
        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public ConvertHelper()
        {

        }

        #endregion

        #region Private Variables

        /// <summary>
        /// 1900-01-01 00:00:00
        /// </summary>
        private static readonly string sMinDateTime = "1900-01-01 00:00:00";

        /// <summary>
        /// 9999-12-31 23:59:59
        /// </summary>
        private static readonly string sMaxDateTime = "9999-12-31 23:59:59";

        #endregion

        #region Public Variables

        /// <summary>
        /// 1900-01-01 00:00:00
        /// </summary>
        public static readonly DateTime MinDateTime = ToDateTime(sMinDateTime);

        /// <summary>
        /// 9999-12-31 23:59:59
        /// </summary>
        public static readonly DateTime MaxDateTime = ToDateTime(sMaxDateTime);

        #endregion

        #region Byte

        /// <summary>
        /// 转化为Byte，数值范围：0～255，转化失败则默认为0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns>defautl value = -1</returns>
        public static byte ToByte(object objValue)
        {
            byte result = 0;
            try
            {
                result = Convert.ToByte(objValue);
            }
            catch
            { }
            return result;
        }

        /// <summary>
        /// 转化为Byte，数值范围：0～255，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static byte ToByte(object objValue, byte defaultValue)
        {
            byte result = defaultValue;
            try
            {
                result = Convert.ToByte(objValue);
            }
            catch
            { }
            return result;
        }

        #endregion

        #region Short

        /// <summary>
        /// 转化为Short(16位)，数值范围：-32768～32767，转化失败则默认为0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns>defautl value = -1</returns>
        public static short ToShort(object objValue)
        {
            short result = 0;
            try
            {
                result = Convert.ToInt16(objValue);
            }
            catch
            { }
            return result;
        }

        /// <summary>
        /// 转化为Short(16位)，数值范围：-32768～32767，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static short ToShort(object objValue, short defaultValue)
        {
            short result = defaultValue;
            try
            {
                result = Convert.ToInt16(objValue);
            }
            catch
            { }
            return result;
        }

        #endregion

        #region Int

        /// <summary>
        /// 转化为Int(32位)，数值范围：-2147483648～2147483647，转化失败则默认为-1
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns>defautl value = -1</returns>
        public static int ToInt(object objValue)
        {
            int iResult = -1;
            if (objValue == null || objValue.ToString() == string.Empty)
            {
                return iResult;
            }
            try
            {
                iResult = Convert.ToInt32(objValue);
            }
            catch
            { }
            return iResult;
        }


        /// <summary>
        /// 转化为Int(32位)，数值范围：-2147483648～2147483647，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int ToInt(object objValue, int defaultValue)
        {
            int iResult = defaultValue;
            if (objValue == null || objValue.ToString() == string.Empty)
            {
                return iResult;
            }
            try
            {
                iResult = Convert.ToInt32(objValue);
            }
            catch
            { }
            return iResult;
        }

        #endregion

        #region Long

        /// <summary>
        /// 转化为Long(64位)，数值范围：-9223372036854775808～9223372036854775807，转化失败则默认为0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static long ToLong(object objValue)
        {
            long lResult = 0;
            try
            {
                lResult = Convert.ToInt64(objValue);
            }
            catch
            { }
            return lResult;
        }

        /// <summary>
        /// 转化为Long(64位)，数值范围：-9223372036854775808～9223372036854775807，转化失败则默认为0
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="fromBase">源数据进制 2-2进制 8-8进制 10-10进制 16-16进制</param>
        /// <returns></returns>
        public static long ToLong(object objValue, int fromBase)
        {
            long lResult = 0;
            try
            {
                lResult = Convert.ToInt64(objValue.ToString(), fromBase);
            }
            catch
            { }
            return lResult;
        }

        /// <summary>
        /// 转化为Long(64位)，数值范围：-9223372036854775808～9223372036854775807，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static long ToLong(object objValue, long defaultValue)
        {
            long lResult = defaultValue;
            try
            {
                lResult = Convert.ToInt64(objValue);
            }
            catch
            { }
            return lResult;
        }

        #endregion

        #region Float

        /// <summary>
        /// 转化为Float，转化失败则默认为0.0F
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static float ToFloat(object objValue)
        {
            float result = 0.0F;
            try
            {
                result = Convert.ToSingle(objValue);
            }
            catch
            { }
            return result;
        }

        /// <summary>
        /// 转化为Float，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static float ToFloat(object objValue, float defaultValue)
        {
            float dResult = defaultValue;
            try
            {
                dResult = Convert.ToSingle(objValue);
            }
            catch
            { }
            return dResult;
        }

        #endregion

        #region Double

        /// <summary>
        /// 转化为Double，转化失败则默认为0.0
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static double ToDouble(object objValue)
        {
            double dResult = 0.0;
            try
            {
                dResult = Convert.ToDouble(objValue);
            }
            catch
            { }
            return dResult;
        }

        /// <summary>
        /// 转化为Double，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static double ToDouble(object objValue, double defaultValue)
        {
            double dResult = defaultValue;
            try
            {
                dResult = Convert.ToDouble(objValue);
            }
            catch
            { }
            return dResult;
        }

        #endregion

        #region Decimal

        /// <summary>
        /// 转化为Decimal，转化失败则默认为0m
        /// </summary>
        /// <param name="objValue"></param>        
        /// <returns>default Value</returns>
        public static decimal ToDecimal(object objValue)
        {
            decimal defaultValue = 0m;
            decimal result;
            try
            {
                result = Convert.ToDecimal(objValue);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转化为Decimal，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static decimal ToDecimal(object objValue, decimal defaultValue)
        {
            decimal result;
            try
            {
                result = Convert.ToDecimal(objValue);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        #endregion

        #region DateTime

        /// <summary>
        /// 转化为DateTime，转化失败则默认为"1900-01-01 00:00:00"
        /// </summary>
        /// <param name="objValue"></param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objValue)
        {
            DateTime dt = MinDateTime;
            try
            {
                dt = Convert.ToDateTime(objValue);
            }
            catch
            { }
            return dt;
        }

        /// <summary>
        /// 转化为预期格式的DateTime，转化失败则默认为"1900-01-01 00:00:00"
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="format">需要转换object的预期格式</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objValue, string format)
        {
            DateTime dt = MinDateTime;
            try
            {
                string s = Convert.ToString(objValue);
                dt = DateTime.ParseExact(s, format, null);
            }
            catch
            { }
            return dt;
        }

        /// <summary>
        /// 转化为DateTime，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objValue, DateTime defaultValue)
        {
            DateTime dt = defaultValue;
            try
            {
                dt = Convert.ToDateTime(objValue);
            }
            catch
            { }
            return dt;
        }

        /// <summary>
        /// 转化为预期格式的DateTime，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="format">需要转换object的预期格式</param>
        /// <param name="defaultValue">default value</param>
        /// <returns></returns>
        public static DateTime ToDateTime(object objValue, string format, DateTime defaultValue)
        {
            DateTime dt = defaultValue;
            try
            {
                string s = Convert.ToString(objValue);
                dt = DateTime.ParseExact(s, format, null);
            }
            catch
            { }
            return dt;
        }

        #endregion

        #region Bool

        /// <summary>
        /// 转化为Bool，转化失败则默认为false
        /// </summary>
        /// <param name="objValue"></param>        
        /// <returns>default Value = false</returns>
        public static bool ToBoolean(object objValue)
        {
            bool defaultValue = false;
            bool result;
            try
            {
                result = Convert.ToBoolean(objValue);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        /// <summary>
        /// 转化为Bool，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static bool ToBoolean(object objValue, bool defaultValue)
        {
            bool result;
            try
            {
                result = Convert.ToBoolean(objValue);
            }
            catch
            {
                result = defaultValue;
            }
            return result;
        }

        #endregion

        #region Float

        /// <summary>
        /// 转化为Float，转化失败则默认为string.Empty
        /// </summary>
        /// <param name="objValue"></param>       
        /// <returns>default value = ""</returns>
        public static string ToString(object objValue)
        {
            string defaultValue = string.Empty;

            if (objValue == null || objValue.ToString() == string.Empty)
            {
                return defaultValue;
            }
            string str = Convert.ToString(objValue);
            return str.Trim();
        }

        /// <summary>
        /// 转化为Float，需提供默认值
        /// </summary>
        /// <param name="objValue"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static string ToString(object objValue, string defaultValue)
        {
            if (objValue == null || objValue.ToString() == string.Empty)
            {
                return defaultValue;
            }
            string str = Convert.ToString(objValue);
            return str.Trim();
        }

        #endregion

        #region IList to datatable 
        /// <summary>
         /// 将集合类转换成DataTable
         /// </summary>
         /// <param name="list">集合</param>
         /// <returns></returns>
         public static DataTable ToDataTable(IList list)
         {
             DataTable result = new DataTable();
             if (list.Count > 0)
             {
                 PropertyInfo[] propertys = list[0].GetType().GetProperties();
                 foreach (PropertyInfo pi in propertys)
                 {
                     result.Columns.Add(pi.Name, pi.PropertyType);
                 }
 
                for (int i = 0; i < list.Count; i++)
                 {
                     ArrayList tempList = new ArrayList();
                     foreach (PropertyInfo pi in propertys)
                     {
                         object obj = pi.GetValue(list[i], null);
                         tempList.Add(obj);
                     }
                     object[] array = tempList.ToArray();
                     result.LoadDataRow(array, true);
                 }
             }
             return result;
         }
 
        /// <summary>
         /// 将泛型集合类转换成DataTable
         /// </summary>
         /// <typeparam name="T">集合项类型</typeparam>
         /// <param name="list">集合</param>
         /// <returns>数据集(表)</returns>
         public static DataTable ToDataTable<T>(IList<T> list)
         {
             return ToDataTable<T>(list, null);
         }
 
        /// <summary>
         /// 将泛型集合类转换成DataTable
         /// </summary>
         /// <typeparam name="T">集合项类型</typeparam>
         /// <param name="list">集合</param>
         /// <param name="propertyName">需要返回的列的列名</param>
         /// <returns>数据集(表)</returns>
         public static DataTable ToDataTable<T>(IList<T> list, params string[] propertyName)
         {
             List<string> propertyNameList = new List<string>();
             if (propertyName != null)
                 propertyNameList.AddRange(propertyName);
 
            DataTable result = new DataTable();
             if (list.Count > 0)
             {
                 PropertyInfo[] propertys = list[0].GetType().GetProperties();
                 foreach (PropertyInfo pi in propertys)
                 {
                     if (propertyNameList.Count == 0)
                     {
                         result.Columns.Add(pi.Name, pi.PropertyType);
                     }
                     else 
                     {
                         if (propertyNameList.Contains(pi.Name))
                             result.Columns.Add(pi.Name, pi.PropertyType);
                     }
                 }
 
                for (int i = 0; i < list.Count; i++)
                 {
                     ArrayList tempList = new ArrayList();
                     foreach (PropertyInfo pi in propertys)
                     {
                         if (propertyNameList.Count == 0)
                         {
                             object obj = pi.GetValue(list[i], null);
                             tempList.Add(obj);
                         }
                         else
                         {
                             if (propertyNameList.Contains(pi.Name))
                             {
                                 object obj = pi.GetValue(list[i], null);
                                 tempList.Add(obj);
                             }
                         }
                     }
                     object[] array = tempList.ToArray();
                     result.LoadDataRow(array, true);
                 }
             }
             return result;
         }
 
        #endregion     
 
        #region IList to List
         /// <summary>
        /// IList To List
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gbList"></param>
        /// <returns></returns>
         public static List<T> ToList<T>(IList<T> gbList) where T : class
         {
             if (gbList != null && gbList.Count >= 1)
             {
                 List<T> list = new List<T>();
                 for (int i = 0; i < gbList.Count; i++)
                 {
                     T temp = gbList[i] as T;
                     if (temp != null)
                         list.Add(temp);
                 }
                 return list;
             }
             return null;
         }
        #endregion         

         #region transfer between custom fields and xmldocument...

         /// <summary>
         /// xml transfer to collection
         /// </summary>
         /// <param name="xml"></param>
         /// <returns></returns>
         public static Dictionary<string, string> XMLToCollection(XmlDocument xml)
         {
             Dictionary<string, string> collection = new Dictionary<string, string>();
             XmlNodeList nodes = xml.FirstChild.FirstChild.ChildNodes;
             foreach (XmlNode node in nodes)
             {
                 collection.Add(node.Name, node.InnerText);
             }
             return collection;
         }

         /// <summary>
         /// collection transfer to xml ...
         /// </summary>
         /// <param name="collection"></param>
         /// <returns></returns>
         public static XmlDocument CollectionToXML(Dictionary<string, string> collection)
         {
             XmlDocument xml = new XmlDocument();
             xml.LoadXml("<root/>");

             XmlNode xn = xml.CreateNode("element", "item", "");
             XmlElement xe;
             foreach (KeyValuePair<string, string> item in collection)
             {
                 xe = xml.CreateElement(item.Key);
                 xe.InnerText = item.Value;
                 xn.AppendChild(xe);
             }
             xml.DocumentElement.AppendChild(xn);
             return xml;
         }

         #endregion
    }
}
