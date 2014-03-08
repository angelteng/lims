/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：DevReservation(DevReservation)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：angel.teng
 * 日    期：2014-02-15
 * ddyss100@163.com
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
    /// DataEntity DevReservation
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class DevReservation : BaseData
    {
        /// <summary>
        /// DevReservation
        /// </summary>
        public DevReservation()
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
        /// 
        /// </summary>
        public virtual int UserID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int UserType
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string TestContent
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime BeginTIme
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime EndTime
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int SampleCount
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ReservationType
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int DeviceID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual double Price
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
            xe = CreateElement(xml, Columns.UserID.ToString(),UserID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UserType.ToString(),UserType.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.TestContent.ToString(),TestContent.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.BeginTIme.ToString(),BeginTIme.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EndTime.ToString(),EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SampleCount.ToString(),SampleCount.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ReservationType.ToString(),ReservationType.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.DeviceID.ToString(),DeviceID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Price.ToString(),Price.ToString());
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
            UserID = 0;
            UserType = 0;
            TestContent = string.Empty;
            BeginTIme = DateTime.Now;
            EndTime = DateTime.Now;
            SampleCount = 0;
            ReservationType = 0;
            DeviceID = 0;
            Price = 0;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DevReservation";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            UserID,
            UserType,
            TestContent,
            BeginTIme,
            EndTime,
            SampleCount,
            ReservationType,
            DeviceID,
            Price,
            Remark,
        }

        #endregion
        
    }
}


