/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysFunction(SysFunction)
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
    /// DataEntity SysFunction
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysFunction : BaseData
    {
        /// <summary>
        /// SysFunction
        /// </summary>
        public SysFunction()
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
        public virtual int ModuleID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FunctionKey
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int Value
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FunctionName
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FunctionUrl
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual bool AdminPage
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
            xe = CreateElement(xml, Columns.ModuleID.ToString(),ModuleID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FunctionKey.ToString(),FunctionKey.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Value.ToString(),Value.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FunctionName.ToString(),FunctionName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FunctionUrl.ToString(),FunctionUrl.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.AdminPage.ToString(),AdminPage.ToString());
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
            ModuleID = 0;
            FunctionKey = string.Empty;
            Value = 0;
            FunctionName = string.Empty;
            FunctionUrl = string.Empty;
            AdminPage = false;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysFunction";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            ModuleID,
            FunctionKey,
            Value,
            FunctionName,
            FunctionUrl,
            AdminPage,
            Remark,
        }

        #endregion
        
    }
}


