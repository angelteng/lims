using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 解析枚举
    /// </summary>
    public class ParseEnums
    {
        
        #region list type ...

        /// <summary>
        /// 转换列表类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ListType ToListType(int value)
        {
            return ToListType(value.ToString());
        }

        /// <summary>
        /// 转换列表类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static ListType ToListType(string value)
        {
            switch (value.ToLower())
            {
                case "1":
                case "labexamine":
                    return ListType.LabExamine;
                case "2":
                case "TreatResponse":
                    return ListType.TreatResponse;
            }

            return ListType.None;
        }

        #endregion


        #region list type ...

        /// <summary>
        /// 转换自定义字段输入类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CustomField_InputType ToInputType(int value)
        {
            return ToInputType(value.ToString());
        }

        /// <summary>
        /// 转换自定义字段输入类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static CustomField_InputType ToInputType(string value)
        {
            switch (value.ToLower())
            {
                case "1":
                case "textarea":
                    return CustomField_InputType.TextArea;
                case "2":
                case "radio":
                    return CustomField_InputType.Radio;
                case "3":
                case "checkbox":
                    return CustomField_InputType.CheckBox;
                case "4":
                case "dropdownlist":
                    return CustomField_InputType.DropDownList;
                case "5":
                case "file":
                    return CustomField_InputType.File;
            }

            return CustomField_InputType.TextBox;
        }

        #endregion

        #region DbType ...

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbType ToDbType(int value)
        {
            return ToDbType(value.ToString());
        }

        /// <summary>
        /// 转换数据类型
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DbType ToDbType(string value)
        {
            switch (value.ToLower())
            {
                case "1":
                case "binary":
                    return DbType.Binary;
                case "2":
                case "byte":
                    return DbType.Byte;
                case "3":
                case "bool":
                case "boolean":
                    return DbType.Boolean;
                case "4":
                case "currency":
                    return DbType.Currency;
                case "5":
                case "date":
                    return DbType.Date;
                case "6":
                case "datetime":
                    return DbType.DateTime;
                case "7":
                case "decimal":
                    return DbType.Decimal;
                case "8":
                case "double":
                    return DbType.Double;
                case "9":
                case "guid":
                    return DbType.Guid;
                case "10":
                case "short":
                case "int16":
                    return DbType.Int16;
                case "11":
                case "int":
                case "int32":
                    return DbType.Int32;
                case "12":
                case "long":
                case "int64":
                    return DbType.Int64;
                case "13":
                case "object":
                    return DbType.Object;
                case "14":
                case "sbyte":
                    return DbType.SByte;
                case "15":
                case "single":
                    return DbType.Single;
                case "16":
                case "string":
                    return DbType.String;
                case "17":
                case "time":
                    return DbType.Time;
                case "18":
                case "ushort":
                case "uint16":
                    return DbType.UInt16;
                case "19":
                case "uint":
                case "uint32":
                    return DbType.UInt32;
                case "20":
                case "ulong":
                case "uint64":
                    return DbType.UInt64;
                case "21":
                case "varnumeric":
                    return DbType.VarNumeric;
                case "22":
                case "ansistringfixedlength":
                    return DbType.AnsiStringFixedLength;
                case "23":
                case "stringfixedlength":
                    return DbType.StringFixedLength;
                case "25":
                case "xml":
                    return DbType.Xml;
                case "26":
                case "datetime2":
                    return DbType.DateTime2;
                case "27":
                case "datetimeoffset":
                    return DbType.DateTimeOffset;
            }

            return DbType.AnsiString;
        }

        #endregion
    }
}
