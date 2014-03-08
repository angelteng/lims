/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysMessage(SysMessage)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-11-05
 * xingzhewujiang1999@gmail.com
 * QQ:970355214
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.Collections;
using System.Data;
using System.Xml;

using Hope.Util;
using Hope.ITMS.Enums;

namespace Hope.ITMS.Model
{
    /// <summary>
    /// DataEntity SysMessage
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysMessage : BaseData
    {
        /// <summary>
        /// SysMessage
        /// </summary>
        public SysMessage()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 主键
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 收件人
        /// </summary>
        public virtual int Receiver
        {
            set;
            get;
        }

        /// <summary>
        /// 标题
        /// </summary>
        public virtual string Title
        {
            set;
            get;
        }

        /// <summary>
        /// 内容
        /// </summary>
        public virtual string Details
        {
            set;
            get;
        }

        /// <summary>
        /// 是否已读
        /// </summary>
        public virtual int Status
        {
            set;
            get;
        }

        /// <summary>
        /// 查阅时间
        /// </summary>
        public virtual DateTime ReadTime
        {
            set;
            get;
        }

        /// <summary>
        /// 发件人
        /// </summary>
        public virtual int Creator
        {
            set;
            get;
        }

        /// <summary>
        /// 发件时间
        /// </summary>
        public virtual DateTime CreatTime
        {
            set;
            get;
        }

        #endregion
        
        #region Base Methods ...
        
        /// <summary>
        /// 转换为XML节点
        /// </summary>
        /// <param name="parentDoc">父XML文档</param>
        /// <returns>返回XML文档节点</returns>
        public virtual XmlNode ToXmlNode(XmlDocument parentDoc)
        {
            return ToXmlNode(parentDoc, "item");
        }
        
        /// <summary>
        /// 转换为XML节点
        /// </summary>
        /// <param name="parentDoc">父XML文档</param>
        /// <param name="nodeName">节点名</param>
        /// <returns>返回XML文档节点</returns>
        public virtual XmlNode ToXmlNode(XmlDocument parentDoc, string nodeName)
        {
            XmlDocument xml = parentDoc;
            if (xml == null)
            {
                xml = XMLHelper.CreateXmlDoc();
            }
            XmlNode xn = parentDoc.CreateNode("element", nodeName, "");
            
            XmlElement xe;
            xe = CreateElement(xml, Columns.ID.ToString(),ID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Receiver.ToString(),Receiver.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Title.ToString(),Title.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Details.ToString(),Details.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Status.ToString(),Status.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ReadTime.ToString(),ReadTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Creator.ToString(),Creator.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.CreatTime.ToString(),CreatTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ID = 0;
            Receiver = 0;
            Title = string.Empty;
            Details = string.Empty;
            Status = 0;
            ReadTime = DateTime.Now;
            Creator = 0;
            CreatTime = DateTime.Now;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysMessage";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            Receiver,
            Title,
            Details,
            Status,
            ReadTime,
            Creator,
            CreatTime,
        }

        #endregion
        
    }
}


