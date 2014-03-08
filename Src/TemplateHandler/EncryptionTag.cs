using System.Web;
using System.Text;
using System;
using System.Collections.Generic;

using Hope.Util;
using Hope.ITMS.Enums;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 加密解密类标签
    /// </summary>
    public partial class HopeTag
    {
        /// <summary>
        /// 加密，得新字符串
        /// </summary>
        /// <param name="SourceStr">源文</param>
        /// <returns>
        /// 调用实例：${HopeTag.EncryptStr(stg)}
        /// </returns>
        public string EncryptStr(object SourceStr)
        {
            return EncryptionUtil.EncryptPassword(SourceStr.ToString());
        }

        /// <summary>
        /// 解密，得新字符串
        /// </summary>
        /// <param name="SourceStr">源文</param>
        /// <returns>
        /// 调用实例：${HopeTag.DecrypeStr(stg)}
        /// </returns>
        public string DecrypeStr(object SourceStr)
        {
            return EncryptionUtil.DecrypePassword(SourceStr.ToString());
        }
    }
}