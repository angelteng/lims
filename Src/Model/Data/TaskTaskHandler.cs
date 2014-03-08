/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：TaskTaskHandler(TaskTaskHandler)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-11-06
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
    /// DataEntity TaskTaskHandler
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class TaskTaskHandler : BaseData
    {
        /// <summary>
        /// TaskTaskHandler
        /// </summary>
        public TaskTaskHandler()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 任务
        /// </summary>
        public virtual int TaskID
        {
            set;
            get;
        }

        /// <summary>
        /// 跟进人
        /// </summary>
        public virtual int HandlerID
        {
            set;
            get;
        }

        /// <summary>
        /// 工时
        /// </summary>
        public virtual XmlDocument WorkTime
        {
            set;
            get;
        }

        /// <summary>
        /// 总计工时
        /// </summary>
        public virtual double TotalHours
        {
            set;
            get;
        }

        /// <summary>
        /// 小结
        /// </summary>
        public virtual string Summary
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Remark
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
            xe = CreateElement(xml, Columns.TaskID.ToString(),TaskID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.HandlerID.ToString(),HandlerID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.WorkTime.ToString(),WorkTime.InnerXml.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.TotalHours.ToString(),TotalHours.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Summary.ToString(),Summary.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Remark.ToString(),Remark.ToString());
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ID = 0;
            TaskID = 0;
            HandlerID = 0;
            WorkTime = XMLHelper.CreateXmlDoc();
            TotalHours = 0;
            Summary = string.Empty;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "TaskTaskHandler";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            TaskID,
            HandlerID,
            WorkTime,
            TotalHours,
            Summary,
            Remark,
        }

        #endregion
        
    }
}


