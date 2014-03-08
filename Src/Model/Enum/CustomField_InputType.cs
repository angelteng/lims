using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 自定义字段输入类型
    /// </summary>
    public enum CustomField_InputType
    {
        /// <summary>
        /// 附件
        /// </summary>
        File=1,
        /// <summary>
        /// 下拉框
        /// </summary>
        DropDownList=2,
        /// <summary>
        /// 复选框
        /// </summary>
        CheckBox=3,
        /// <summary>
        /// 单选框
        /// </summary>
        Radio=4,
        /// <summary>
        /// 文本域
        /// </summary>
        TextArea=5,
        /// <summary>
        /// 文本框
        /// </summary>
        TextBox=6,
        /// <summary>
        /// 日期选择框
        /// </summary>
        DateTimePicker = 7,
        /// <summary>
        /// 富文本编辑器
        /// </summary>
        HtmlEditor = 8,
        /// <summary>
        /// 密码框
        /// </summary>
        Password = 9,
    }

    /// <summary>
    /// 自定义字段输入类型
    /// </summary>
    public enum CustomField_InputTypeCN
    {
        /// <summary>
        /// 附件
        /// </summary>
        附件 = 1,
        /// <summary>
        /// 下拉框
        /// </summary>
        下拉框 = 2,
        /// <summary>
        /// 复选框
        /// </summary>
        复选框 = 3,
        /// <summary>
        /// 单选框
        /// </summary>
        单选框 = 4,
        /// <summary>
        /// 文本域
        /// </summary>
        多行文本 = 5,
        /// <summary>
        /// 文本框
        /// </summary>
        单行文本 = 6,
        /// <summary>
        /// 日期选择框
        /// </summary>
        日期选择框 = 7,
        /// <summary>
        /// 富文本编辑器
        /// </summary>
        富文本编辑器 = 8,
        /// <summary>
        /// 密码框
        /// </summary>
        密码框 = 9,
    }
}
