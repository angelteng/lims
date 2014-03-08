using System;

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 短信用户状态
    /// </summary>
    public enum SMSUserStatus
    {
        /// <summary>
        /// 当前账号余额不足
        /// </summary>
        当前账号余额不足 = -01,

        /// <summary>
        /// 当前用户ID错误
        /// </summary>
        当前用户ID错误 = -02,

        /// <summary>
        /// 当前密码错误
        /// </summary>
        当前密码错误 = -03,

        /// <summary>
        /// 参数不够或参数内容的类型错误
        /// </summary>
        参数不够或参数内容的类型错误 = -04,

        /// <summary>
        /// 手机号码格式不对！（目前还未实现）
        /// </summary>
        手机号码格式不对 = -05,

        /// <summary>
        /// 短信内容编码不对！（目前还未实现）
        /// </summary>
        短信内容编码不对 = -06,

        /// <summary>
        /// 短信内容含有敏感字符！（目前还未实现）
        /// </summary>
        短信内容含有敏感字符 = -07,

        /// <summary>
        /// 无接收数据
        /// </summary>
        无接收数据 = -08,

        /// <summary>
        /// 系统维护中
        /// </summary>
        系统维护中 = -09,

        /// <summary>
        /// 手机号码数量超长
        /// </summary>
        手机号码数量超长 = -10,

        /// <summary>
        /// 短信内容超长
        /// </summary>
        短信内容超长 = -11,

        /// <summary>
        /// 未知错误
        /// </summary>
        未知错误 = -12,

        /// <summary>
        /// 文件传输错误
        /// </summary>
        文件传输错误 = -13,
    }
}