/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：CommonField(CommonField)
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
using System.Collections.Generic;
using System.Data;
using System.Xml;

using Hope.Util;
using Hope.ITMS.Enums;

namespace Hope.ITMS.Model
{
    /// <summary>
    /// DataEntity CommonField
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class CommonField : BaseData
    {
        /// <summary>
        /// CommonField
        /// </summary>
        public CommonField()
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
        public virtual int ModelID
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
        public virtual int OrderID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Code
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
        public virtual string PrefixText
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string SuffixText
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ComponentType
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int ComponentWidth
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool Required
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string DefaultValue
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual Dictionary<string, string> ListItem
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool IsShow
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ComponentID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string ClassName
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Regex
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
            xe = CreateElement(xml, Columns.ModelID.ToString(),ModelID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Type.ToString(),Type.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.OrderID.ToString(),OrderID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Code.ToString(),Code.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.PrefixText.ToString(),PrefixText.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SuffixText.ToString(),SuffixText.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ComponentType.ToString(),ComponentType.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ComponentWidth.ToString(),ComponentWidth.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Required.ToString(),Required.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.DefaultValue.ToString(),DefaultValue.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ListItem.ToString(),ListItem.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.IsShow.ToString(),IsShow.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ComponentID.ToString(),ComponentID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ClassName.ToString(),ClassName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Regex.ToString(),Regex.ToString());
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
            ModelID = 1;
            Type = 1;
            OrderID = 1;
            Code = string.Empty;
            Name = string.Empty;
            PrefixText = string.Empty;
            SuffixText = string.Empty;
            ComponentType = 0;
            ComponentWidth = 0;
            Required = false;
            DefaultValue = string.Empty;
            ListItem = new Dictionary<string, string>();
            IsShow = false;
            ComponentID = string.Empty;
            ClassName = string.Empty;
            Regex = string.Empty;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "CommonField";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            ModelID,
            Type,
            OrderID,
            Code,
            Name,
            PrefixText,
            SuffixText,
            ComponentType,
            ComponentWidth,
            Required,
            DefaultValue,
            ListItem,
            IsShow,
            ComponentID,
            ClassName,
            Regex,
            Remark,
        }

        #endregion
        
    }
}


