/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：DevDevice(DevDevice)
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
    /// DataEntity DevDevice
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class DevDevice : BaseData
    {
        /// <summary>
        /// DevDevice
        /// </summary>
        public DevDevice()
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
        public virtual string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string CNName
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
        public virtual string Pic
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Video
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DeviceModel
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Supplier
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SupplierURL
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Number
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Price
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Status
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int OrderNum
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PersonInCharge
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Contact
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Information
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime BeginTime
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
        public virtual int MaxApplyTIme
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int MaxPreApplyTIme
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Location
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
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.CNName.ToString(),CNName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.TestContent.ToString(),TestContent.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Pic.ToString(),Pic.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Video.ToString(),Video.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.DeviceModel.ToString(),DeviceModel.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Supplier.ToString(),Supplier.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SupplierURL.ToString(),SupplierURL.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Number.ToString(),Number.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Price.ToString(),Price.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Status.ToString(),Status.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.OrderNum.ToString(),OrderNum.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.PersonInCharge.ToString(),PersonInCharge.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Contact.ToString(),Contact.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Information.ToString(),Information.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.BeginTime.ToString(),BeginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EndTime.ToString(),EndTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.MaxApplyTIme.ToString(),MaxApplyTIme.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.MaxPreApplyTIme.ToString(),MaxPreApplyTIme.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Location.ToString(),Location.ToString());
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
            Name = string.Empty;
            CNName = string.Empty;
            TestContent = string.Empty;
            Pic = string.Empty;
            Video = string.Empty;
            DeviceModel = string.Empty;
            Supplier = string.Empty;
            SupplierURL = string.Empty;
            Number = 0;
            Price = 0;
            Status = 0;
            OrderNum = 0;
            PersonInCharge = string.Empty;
            Contact = string.Empty;
            Information = string.Empty;
            BeginTime = DateTime.Now;
            EndTime = DateTime.Now;
            MaxApplyTIme = 0;
            MaxPreApplyTIme = 0;
            Location = string.Empty;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "DevDevice";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            Name,
            CNName,
            TestContent,
            Pic,
            Video,
            DeviceModel,
            Supplier,
            SupplierURL,
            Number,
            Price,
            Status,
            OrderNum,
            PersonInCharge,
            Contact,
            Information,
            BeginTime,
            EndTime,
            MaxApplyTIme,
            MaxPreApplyTIme,
            Location,
            Remark,
        }

        #endregion
        
    }
}


