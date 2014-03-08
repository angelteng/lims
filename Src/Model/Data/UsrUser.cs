/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：UsrUser(UsrUser)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：angel.teng
 * 日    期：2014-03-03
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
    /// DataEntity UsrUser
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class UsrUser : BaseData
    {
        /// <summary>
        /// UsrUser
        /// </summary>
        public UsrUser()
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
        public virtual int GroupID
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Account
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Password
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
        public virtual int Gender
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
        public virtual DateTime Birthday
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime RegDate
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string PhoneNo
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string FaxNo
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string MobileNo
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Email
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string HomeSite
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Postcode
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual string Address
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual int LoginTIme
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime LastLoginTime
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

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime CreateTime
        {
            set;
            get;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual DateTime LastUpdateTime
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
            xe = CreateElement(xml, Columns.GroupID.ToString(),GroupID.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Account.ToString(),Account.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Password.ToString(),Password.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Name.ToString(),Name.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Gender.ToString(),Gender.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Status.ToString(),Status.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Birthday.ToString(),Birthday.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.RegDate.ToString(),RegDate.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.PhoneNo.ToString(),PhoneNo.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FaxNo.ToString(),FaxNo.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.MobileNo.ToString(),MobileNo.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Email.ToString(),Email.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.HomeSite.ToString(),HomeSite.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Postcode.ToString(),Postcode.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Address.ToString(),Address.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LoginTIme.ToString(),LoginTIme.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LastLoginTime.ToString(),LastLoginTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Remark.ToString(),Remark.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.CreateTime.ToString(),CreateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.LastUpdateTime.ToString(),LastUpdateTime.ToString("yyyy-MM-dd HH:mm:ss"));
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ID = 0;
            GroupID = 0;
            Account = string.Empty;
            Password = string.Empty;
            Name = string.Empty;
            Gender = 0;
            Status = 0;
            Birthday = DateTime.Now;
            RegDate = DateTime.Now;
            PhoneNo = string.Empty;
            FaxNo = string.Empty;
            MobileNo = string.Empty;
            Email = string.Empty;
            HomeSite = string.Empty;
            Postcode = string.Empty;
            Address = string.Empty;
            LoginTIme = 0;
            LastLoginTime = DateTime.Now;
            Remark = string.Empty;
            CreateTime = DateTime.Now;
            LastUpdateTime = DateTime.Now;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "UsrUser";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            GroupID,
            Account,
            Password,
            Name,
            Gender,
            Status,
            Birthday,
            RegDate,
            PhoneNo,
            FaxNo,
            MobileNo,
            Email,
            HomeSite,
            Postcode,
            Address,
            LoginTIme,
            LastLoginTime,
            Remark,
            CreateTime,
            LastUpdateTime,
        }

        #endregion
        
    }
}


