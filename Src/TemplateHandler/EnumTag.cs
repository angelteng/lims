using System;
using System.Collections.Generic;
using Hope.ITMS.BLL;
using Hope.ITMS.Enums;
using System.Collections;
using Hope.ITMS.Model;
using Hope.ITMS.Model.Enum;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 枚举类标签
    /// </summary>
    public partial class HopeTag
    {
        #region 获取日志类别

        /// <summary>
        /// 获取日志类别
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetLogType(1)}
        /// </returns>
        public string GetLogType(object enumValue)
        {
            return GetType(enumValue, typeof (LogType));
        }

        /// <summary>
        /// 获取日志类别
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetLogType(1)}
        /// </returns>
        public string GetLogTypeCN(object enumValue)
        {
            return GetType(enumValue, typeof(LogTypeCN));
        }

        #endregion
        
        #region 获取日志优先级

        /// <summary>
        /// 获取日志优先级
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetLogPriority(1)}
        /// </returns>
        public string GetLogPriority(object enumValue)
        {
            return GetType(enumValue, typeof (LogPriority));
        }

        /// <summary>
        /// 获取日志优先级
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetLogPriority(1)}
        /// </returns>
        public string GetLogPriorityCN(object enumValue)
        {
            return GetType(enumValue, typeof(LogPriorityCN));
        }

        #endregion
        
        #region 获取状态 1:正常 -1:禁用
        /// <summary>
        /// 获取状态 1:正常 -1:禁用 
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetStatus(1)}
        /// </returns>
        public string GetStatus(object enumValue)
        {
            return GetType(enumValue, typeof (ObjectStatus));
        }

        /// <summary>
        /// 获取状态 1:正常 -1:禁用 
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetStatus(1)}
        /// </returns>
        public string GetStatusCN(object enumValue)
        {
            return GetType(enumValue, typeof(ObjectStatusCN));
        }

        #endregion

        #region 获取状态集合 1:正常 -1:禁用

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetStatusList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (ObjectStatus));

            foreach (int key in keys)
            {
                dic.Add(key, GetStatus(key));
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetStatusListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (ObjectStatusCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetStatusCN(key));
            }

            return dic;
        }

        #endregion
        
        #region 获取性别：1：男 0：女 -1：未知
        /// <summary>
        /// 获取性别：1：男 0：女 -1：未知
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.GetGender(1)}
        /// </example>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetGender(object enumValue)
        {
            return GetType(enumValue, typeof (Gender));
        }

        /// <summary>
        /// 获取性别：1：男 0：女 -1：未知
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="isEn"></param>
        /// <returns></returns>
        public string GetGenderCN(object enumValue)
        {
            return GetType(enumValue, typeof(GenderCN));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetGenderListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof(GenderCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetGenderCN(key));
            }

            return dic;
        }

        #endregion

        #region 获取短消息状态 0-未读 1-已读 2-收件人删除
        /// <summary>
        /// 获取短消息状态 0-未读 1-已读 2-收件人删除
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetMessageStatus(1)}
        /// </returns>
        public string GetMessageStatus(object enumValue)
        {
            return GetType(enumValue, typeof (MessageStatus));
        }

        /// <summary>
        /// 获取短消息状态 0-未读 1-已读 2-收件人删除
        /// </summary>
        /// <param name="enumValue">枚举值</param>
        /// <returns>
        /// 调用实例：${HopeTag.GetMessageStatus(1)}
        /// </returns>
        public string GetMessageStatusCN(object enumValue)
        {
            return GetType(enumValue, typeof(MessageStatusCN));
        }
        #endregion

        #region 获取数据库字段类型

        /// <summary>
        /// 获取数据库字段类型
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetDataType(object enumValue)
        {
            return GetType(enumValue, typeof (CustomField_DataType));
        }

        /// <summary>
        /// 获取数据库字段类型
        /// </summary>
        /// <param name="enumValue">类型值</param>
        /// <returns></returns>
        public string GetDataTypeCN(object enumValue)
        {
            return GetType(enumValue, typeof(CustomField_DataTypeCN));
        }

        #endregion

        #region 获取数据库字段类型集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetDataTypeList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (CustomField_DataType));

            foreach (int key in keys)
            {
                dic.Add(key, GetDataType(key));
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetDataTypeListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (CustomField_DataTypeCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetDataTypeCN(key));
            }

            return dic;
        }

        #endregion
        
        #region 获取表单字段类型

        /// <summary>
        /// 获取表单字段类型
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetInputType(object enumValue)
        {
            return GetType(enumValue, typeof (CustomField_InputType));
        }

        /// <summary>
        /// 获取表单字段类型
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetInputTypeCN(object enumValue)
        {
            return GetType(enumValue, typeof(CustomField_InputTypeCN));
        }

        
        #endregion

        #region 获取表单字段类型集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetInputTypeList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof(CustomField_InputType));

            foreach (int key in keys)
            {
                dic.Add(key, GetInputType(key));
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetInputTypeListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof(CustomField_InputTypeCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetInputTypeCN(key));
            }

            return dic;
        }

        #endregion

        #region 获取字段类型 0：系统，1：自定义

        /// <summary>
        /// 获取字段类型 0：系统，1：自定义
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetFieldType(object enumValue)
        {
            return GetType(enumValue, typeof (FieldType));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetFieldTypeCN(object enumValue)
        {
            return GetType(enumValue, typeof(FieldTypeCN));
        }
        
        
        #endregion

        #region 获取字段类型集合

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetFieldTypeList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (FieldType));

            foreach (int key in keys)
            {
                dic.Add(key, GetFieldType(key));
            }

            return dic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetFieldTypeListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (FieldTypeCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetFieldTypeCN(key));
            }

            return dic;
        }

        #endregion


        #region 获取任务优先级

        /// <summary>
        /// GetTaskPriority
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetTaskPriority(object enumValue)
        {
            return GetType(enumValue, typeof (TaskPriority));
        }

        /// <summary>
        /// GetTaskPriority
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetTaskPriorityCN(object enumValue)
        {
            return GetType(enumValue, typeof (TaskPriorityCN));
        }



        public Dictionary<int, string> GetTaskPriorityList()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (TaskPriority));

            foreach (int key in keys)
            {
                dic.Add(key, GetTaskPriority(key));
            }

            return dic;
        }

        public Dictionary<int, string> GetTaskPriorityListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (TaskPriorityCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetTaskPriorityCN(key));
            }

            return dic;
        }

        #endregion


        #region 获取任务进度

        /// <summary>
        /// GetProgressType
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetProgressType(object enumValue)
        {
            return GetType(enumValue, typeof (ProgressType));
        }

        /// <summary>
        /// GetProgressTypeCN
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetProgressTypeCN(object enumValue)
        {
            return GetType(enumValue, typeof (ProgressTypeCN));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetProgressTypeListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (ProgressTypeCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetProgressTypeCN(key));
            }

            return dic;
        }

        #endregion

        #region 获取频率

        /// <summary>
        /// GetFrequency
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetFrequency(object enumValue)
        {
            return GetType(enumValue, typeof (Frequency));
        }

        /// <summary>
        /// GetFrequency
        /// </summary>
        /// <param name="enumValue"></param>
        /// <returns></returns>
        public string GetFrequencyCN(object enumValue)
        {
            return GetType(enumValue, typeof (FrequencyCN));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Dictionary<int, string> GetFrequencyListCN()
        {
            Dictionary<int, string> dic = new Dictionary<int, string>();
            Array keys = Enum.GetValues(typeof (FrequencyCN));

            foreach (int key in keys)
            {
                dic.Add(key, GetFrequencyCN(key));
            }

            return dic;
        }

        #endregion

        
        #region GetType

        /// <summary>
        /// GetType
        /// </summary>
        /// <param name="enumValue"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        private string GetType(object enumValue, Type type)
        {
            if (Enum.IsDefined(type, enumValue))
            {
                return Enum.GetName(type, enumValue);
            }
            return string.Empty;
        }

        #endregion

    }
}