/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：OrgGroup(OrgGroup)
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
    /// DataEntity OrgGroup
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class OrgGroup : BaseData
    {
        /// <summary>
        /// OrgGroup
        /// </summary>
        public OrgGroup()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// ID、主键
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 用户组名称
        /// </summary>
        public virtual string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 模型ID
        /// </summary>
        public virtual int ModelID
        {
            set;
            get;
        }

        /// <summary>
        /// 排序号，越小越靠前
        /// </summary>
        public virtual int OrderID
        {
            set;
            get;
        }

        /// <summary>
        /// 备注说明
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
            xe = CreateElement(xml, Columns.ModelID.ToString(),ModelID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.OrderID.ToString(),OrderID.ToString());
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
            ModelID = 0;
            OrderID = 0;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "OrgGroup";
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
            ModelID,
            OrderID,
            Remark,
        }

        #endregion
        
    }
}


