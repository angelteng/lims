
/******************************************************************
 *
 * 所在模块：Enums(系统枚举模块)
 * 类 名 称：TemplateType(模板类型)
 * 功能描述：系统模块的类型
 * 
 * ------------创建信息------------------
 * 作    者：Nick
 * 日    期：2009-08-06
 * ajj82@163.com
 * MSN:ajj82@163.com
 * QQ:46810878
 * ------------编辑修改信息--------------
 * 作    者：jjf001
 * 日    期：2013年10月28日
 * 内    容：LogType添加 邮件发送成功/失败类型
******************************************************************/
using System;

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 系统日志类型
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// 越权操作
        /// </summary>
        AccessFailed = 1,

        /// <summary>
        /// 登录成功
        /// </summary>
        LoginSucceed = 2,

        /// <summary>
        /// 异常记录
        /// </summary>
        Exception = 3,

        /// <summary>
        /// 登录失败
        /// </summary>
        LoginFailed = 4,

        /// <summary>
        /// 发送邮件成功
        /// </summary>
        MailSucceed = 5,

        /// <summary>
        /// 发送邮件失败
        /// </summary>
        MailFailed = 6,
    }

    /// <summary>
    /// 系统日志类型
    /// </summary>
    public enum LogTypeCN
    {
        /// <summary>
        /// 越权操作
        /// </summary>
        越权操作 = 1,

        /// <summary>
        /// 登录成功
        /// </summary>
        登录成功 = 2,

        /// <summary>
        /// 异常记录
        /// </summary>
        异常记录 = 3,

        /// <summary>
        /// 登录失败
        /// </summary>
        登录失败 = 4,

        /// <summary>
        /// 邮件发送成功
        /// </summary>
        邮件发送成功 = 5,

        /// <summary>
        /// 邮件发送失败
        /// </summary>
        邮件发送失败 = 6,
    }

    /// <summary>
    /// 系统日志优先级别
    /// </summary>
    public enum LogPriority
    {
        /// <summary>
        /// 低
        /// </summary>
        Low = 1,

        /// <summary>
        /// 普通
        /// </summary>
        Normal = 2,

        /// <summary>
        /// 高
        /// </summary>
        High = 3,

        /// <summary>
        /// 最高
        /// </summary>
        Top = 4,
    }

    /// <summary>
    /// 系统日志优先级别
    /// </summary>
    public enum LogPriorityCN
    {
        /// <summary>
        /// 低
        /// </summary>
        低 = 1,

        /// <summary>
        /// 普通
        /// </summary>
        普通 = 2,

        /// <summary>
        /// 高
        /// </summary>
        高 = 3,

        /// <summary>
        /// 最高
        /// </summary>
        最高 = 4,
    }
}
