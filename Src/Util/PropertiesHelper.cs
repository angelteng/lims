using System;
using System.Collections;
using System.Collections.Generic;

namespace Hope.Util
{
    /// <summary>
    /// 属性助手
    /// </summary>
    public static class PropertiesHelper
    {

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回布尔值</returns>
        public static bool GetBoolean(string property, IDictionary<string, string> properties, bool defaultValue)
        {
            string toParse;
            properties.TryGetValue(property, out toParse);

            return toParse == null ? defaultValue : bool.Parse(toParse);
        }

        /// <summary>
        /// 获取布尔值
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <returns>返回布尔值</returns>
        public static bool GetBoolean(string property, IDictionary<string, string> properties)
        {
            string toParse;
            properties.TryGetValue(property, out toParse);
            return toParse == null ? false : bool.Parse(properties[property]);
        }

        /// <summary>
        /// 获取int数值
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回int数值</returns>
        public static int GetInt32(string property, IDictionary<string, string> properties, int defaultValue)
        {
            string toParse;
            properties.TryGetValue(property, out toParse);
            return ConvertHelper.ToInt(toParse,defaultValue);
        }

        /// <summary>
        /// 获取long数值
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回long数值</returns>
        public static long GetInt64(string property, IDictionary<string, string> properties, long defaultValue)
        {
            string toParse;
            properties.TryGetValue(property, out toParse);
            return ConvertHelper.ToLong(toParse, defaultValue);
        }

        /// <summary>
        /// 获取decimal数值
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回decimal数值</returns>
        public static decimal GetDecimal(string property, IDictionary<string, string> properties, decimal defaultValue)
        {
            string toParse;
            properties.TryGetValue(property, out toParse);
            return ConvertHelper.ToDecimal(toParse, defaultValue);
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <returns>返回字符串</returns>
        public static string GetString(string property, IDictionary<string, string> properties)
        {
            return GetString(property, properties,"");
        }

        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="property">属性名</param>
        /// <param name="properties">属性集合</param>
        /// <param name="defaultValue">默认值</param>
        /// <returns>返回字符串</returns>
        public static string GetString(string property, IDictionary<string, string> properties, string defaultValue)
        {
            string value;
            properties.TryGetValue(property, out value);
            return value ?? defaultValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="property"></param>
        /// <param name="properties"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static Dictionary<string, string> GetDictionary(string property, Dictionary<string, Dictionary<string, string>> properties, Dictionary<string, string> defaultValue)
        {
            Dictionary<string, string> value;
            properties.TryGetValue(property, out value);
            return value ?? defaultValue;
        }

        /*
        public static IDictionary<string, string> ToDictionary(string property, string delim, IDictionary<string, string> properties)
        {
            IDictionary<string, string> map = new Dictionary<string, string>();

            if (properties.ContainsKey(property))
            {
                string propValue = properties[property];
                StringTokenizer tokens = new StringTokenizer(propValue, delim, false);
                IEnumerator<string> en = tokens.GetEnumerator();
                while (en.MoveNext())
                {
                    string key = en.Current;

                    string value = en.MoveNext() ? en.Current : String.Empty;
                    map[key] = value;
                }
            }
            return map;
        }
        

        public static string[] ToStringArray(string property, string delim, IDictionary properties)
        {
            return ToStringArray((string)properties[property], delim);
        }

        
        public static string[] ToStringArray(string propValue, string delim)
        {
            if (propValue != null)
            {
                return StringHelper.Split(delim, propValue);
            }
            else
            {
                return new string[0];
            }
        }
        */
    }
}
