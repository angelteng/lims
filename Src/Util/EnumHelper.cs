using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;

namespace Hope.Util
{
    /// <summary>
    /// 枚举操作类
    /// </summary>
    public class EnumHelper
    {
        /// <summary>
        /// 将枚举存入Dictionary
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <returns></returns>
        public static Dictionary<string, int> EnumDictionary(Type enumType)
        {
            Dictionary<string, int> dir = new Dictionary<string, int>();

            string[] enumNames = Enum.GetNames(enumType);
            foreach (string enumName in enumNames)
            {
                if (!dir.ContainsKey(enumName)) // 防止添加已经存在的键，Add方法抛出异常
                {
                    dir.Add(enumName, (int)Enum.Parse(enumType, enumName));
                }
            }

            return dir;
        }

        /// <summary>
        ///  获取枚举名称
        /// </summary>
        /// <param name="enumType">枚举类型</param>
        /// <param name="enumValue">枚举数值</param>
        /// <returns></returns>
        public static string GetEnumName(Type enumType, int enumValue)
        {
            return Enum.IsDefined(enumType, enumValue) ? Enum.GetName(enumType, enumValue) : string.Empty;
        }

    }

    /// <summary>
    /// EnumHelper 2
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public static class EnumHelper<T>
    {
        public static string GetDescription(Enum en)
        {
            Type type = en.GetType();

            MemberInfo[] memInfo = type.GetMember(en.ToString());

            if (memInfo != null && memInfo.Length > 0)
            {
                object[] attrs = memInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }

            return en.ToString();
        }

        public static T Parse(string value)
        {
            return (T)Enum.Parse(typeof(T), value);
        }

        public static IList<T> GetValues()
        {
            IList<T> list = new List<T>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                list.Add((T)value);
            }

            return list;
        }

        public static Dictionary<T, string> GetValueDescriptionDictionary()
        {
            Dictionary<T, string> dictionary = new Dictionary<T, string>();
            foreach (object value in Enum.GetValues(typeof(T)))
            {
                dictionary.Add((T)value, GetDescription((Enum)value));
            }
            return dictionary;
        }
    }

}