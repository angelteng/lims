/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysAdmin(SysAdmin)
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
    /// DataEntity SysAdmin
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysAdmin : BaseData
    {
        /// <summary>
        /// SysAdmin
        /// </summary>
        public SysAdmin()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 自增长ID,主键
        /// </summary>
        public virtual int ID
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
        /// 登录名称
        /// </summary>
        public virtual string Name
        {
            set;
            get;
        }

        /// <summary>
        /// 登录密码，加密
        /// </summary>
        public virtual string Password
        {
            set;
            get;
        }

        /// <summary>
        /// 真实姓名
        /// </summary>
        public virtual string RealName
        {
            set;
            get;
        }

        /// <summary>
        /// 状态 1-正常 -1-禁用
        /// </summary>
        public virtual int Status
        {
            set;
            get;
        }

        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual string PhoneNo
        {
            set;
            get;
        }

        /// <summary>
        /// 电子邮箱
        /// </summary>
        public virtual string Email
        {
            set;
            get;
        }

        /// <summary>
        /// 登录次数
        /// </summary>
        public virtual int LoginTimes
        {
            set;
            get;
        }

        /// <summary>
        /// 上次登录时间
        /// </summary>
        public virtual DateTime LastLoginTime
        {
            set;
            get;
        }

        /// <summary>
        /// 登录失败次数
        /// </summary>
        public virtual int ErrorTimes
        {
            set;
            get;
        }

        /// <summary>
        /// 上次登录失败时间
        /// </summary>
        public virtual DateTime LastErrorTime
        {
            set;
            get;
        }

        /// <summary>
        /// 备注
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
            xe = CreateElement(xml, Columns.RoleID.ToString(),RoleID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Password.ToString(),Password.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.RealName.ToString(),RealName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Status.ToString(),Status.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.PhoneNo.ToString(),PhoneNo.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Email.ToString(),Email.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LoginTimes.ToString(),LoginTimes.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LastLoginTime.ToString(),LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.ErrorTimes.ToString(),ErrorTimes.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LastErrorTime.ToString(),LastErrorTime.ToString("yyyy-MM-dd HH:mm:ss"));
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
            RoleID = 0;
            Name = string.Empty;
            Password = string.Empty;
            RealName = string.Empty;
            Status = 0;
            PhoneNo = string.Empty;
            Email = string.Empty;
            LoginTimes = 0;
            LastLoginTime = DateTime.Now;
            ErrorTimes = 0;
            LastErrorTime = DateTime.Now;
            Remark = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysAdmin";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            RoleID,
            Name,
            Password,
            RealName,
            Status,
            PhoneNo,
            Email,
            LoginTimes,
            LastLoginTime,
            ErrorTimes,
            LastErrorTime,
            Remark,
        }

        #endregion
        
    }
}


