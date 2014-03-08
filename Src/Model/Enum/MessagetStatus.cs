/******************************************************************
 *
 * 所在模块：Enum(数据枚举)
 * 类 名 称：MessageStatus(MessageStatus)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-05-03
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/

namespace Hope.ITMS.Enums
{
    /// <summary>
    /// 站内短消息状态
    /// </summary>
    public enum MessageStatus
    {
        /// <summary>
        /// 未读
        /// </summary>
        UnRead = 0,
        /// <summary>
        /// 已读
        /// </summary>
        Read = 1,
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 2,
    }

    /// <summary>
    /// 站内短消息状态
    /// </summary>
    public enum MessageStatusCN
    {
        /// <summary>
        /// 未读
        /// </summary>
        未读 = 0,
        /// <summary>
        /// 已读
        /// </summary>
        已读 = 1,
        /// <summary>
        /// 收件人删除
        /// </summary>
        收件人删除 = 2,
    }
}
