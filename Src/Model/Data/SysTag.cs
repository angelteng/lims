/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysTag(SysTag)
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
    /// DataEntity SysTag
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysTag : BaseData
    {
        /// <summary>
        /// SysTag
        /// </summary>
        public SysTag()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 主键，自增长
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 标签名称
        /// </summary>
        public virtual string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 类别
        /// </summary>
        public virtual string TagCategory
        {
            set;
            get;
        }

        /// <summary>
        /// 描述
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
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.TagCategory.ToString(),TagCategory.ToString());
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
            Name = string.Empty;
            TagCategory = string.Empty;
            Description = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysTag";
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
            TagCategory,
            Description,
        }

        #endregion
        
    }
}


