/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：CommonModel(CommonModel)
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
    /// DataEntity CommonModel
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class CommonModel : BaseData
    {
        /// <summary>
        /// CommonModel
        /// </summary>
        public CommonModel()
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
        public virtual int Type
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
        public virtual string Icon
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Item
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Unit
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int OrderID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool EnableValidateCode
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
        public virtual string Description
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
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Icon.ToString(),Icon.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Item.ToString(),Item.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Unit.ToString(),Unit.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.OrderID.ToString(),OrderID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EnableValidateCode.ToString(),EnableValidateCode.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Status.ToString(),Status.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Description.ToString(),Description.ToString());
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
            Name = string.Empty;
            Icon = string.Empty;
            Item = string.Empty;
            Unit = string.Empty;
            OrderID = 0;
            EnableValidateCode = false;
            Status = 0;
            Description = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CommonModel";
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
            Name,
            Icon,
            Item,
            Unit,
            OrderID,
            EnableValidateCode,
            Status,
            Description,
        }

        #endregion
        
    }
}


