/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysLog(SysLog)
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
    /// DataEntity SysLog
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysLog : BaseData
    {
        /// <summary>
        /// SysLog
        /// </summary>
        public SysLog()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// ID
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 分类
        /// </summary>
        public virtual int Type
        {
            set;
            get;
        }

        /// <summary>
        /// 优先级别
        /// </summary>
        public virtual int Priority
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
        public virtual string Message
        {
            set;
            get;
        }

        /// <summary>
        /// 记录日期
        /// </summary>
        public virtual DateTime Timestamp
        {
            set;
            get;
        }

        /// <summary>
        /// 用户名
        /// </summary>
        public virtual string UserName
        {
            set;
            get;
        }

        /// <summary>
        /// 用户IP
        /// </summary>
        public virtual string UserIP
        {
            set;
            get;
        }

        /// <summary>
        /// StackTrace，TargetSite：异常源，堆栈跟踪，等异常信息
        /// </summary>
        public virtual string Source
        {
            set;
            get;
        }

        /// <summary>
        /// 页面
        /// </summary>
        public virtual string ScriptName
        {
            set;
            get;
        }

        /// <summary>
        /// 提交信息
        /// </summary>
        public virtual string PostString
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
            xe = CreateElement(xml, Columns.Type.ToString(),Type.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Priority.ToString(),Priority.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Title.ToString(),Title.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Message.ToString(),Message.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Timestamp.ToString(),Timestamp.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UserName.ToString(),UserName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UserIP.ToString(),UserIP.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Source.ToString(),Source.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ScriptName.ToString(),ScriptName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.PostString.ToString(),PostString.ToString());
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ID = 0;
            Type = 0;
            Priority = 0;
            Title = string.Empty;
            Message = string.Empty;
            Timestamp = DateTime.Now;
            UserName = string.Empty;
            UserIP = string.Empty;
            Source = string.Empty;
            ScriptName = string.Empty;
            PostString = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysLog";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            Type,
            Priority,
            Title,
            Message,
            Timestamp,
            UserName,
            UserIP,
            Source,
            ScriptName,
            PostString,
        }

        #endregion
        
    }
}


