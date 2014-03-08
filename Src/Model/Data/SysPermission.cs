/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysPermission(SysPermission)
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
    /// DataEntity SysPermission
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysPermission : BaseData
    {
        /// <summary>
        /// SysPermission
        /// </summary>
        public SysPermission()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual int ModuleID
        {
            set;
            get;
        }

        /// <summary>
        /// 角色ID
        /// </summary>
        public virtual int RoleID
        {
            set;
            get;
        }

        /// <summary>
        /// 权限值
        /// </summary>
        public virtual int FunctionValues
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
            xe = CreateElement(xml, Columns.ModuleID.ToString(),ModuleID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.RoleID.ToString(),RoleID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FunctionValues.ToString(),FunctionValues.ToString());
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ModuleID = 0;
            RoleID = 0;
            FunctionValues = 0;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysPermission";
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is SysPermission)
            {
                SysPermission entity = obj as SysPermission;
                if(this.ModuleID == entity.ModuleID && this.RoleID == entity.RoleID)
                {
                    return true;
                }
            }
            return false; 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return 0;
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ModuleID,
            RoleID,
            FunctionValues,
        }

        #endregion
        
    }
}


