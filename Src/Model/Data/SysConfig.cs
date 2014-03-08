/******************************************************************
 *
 * 所在模块：Model(数据实体)
 * 类 名 称：SysConfig(SysConfig)
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
    /// DataEntity SysConfig
    /// </summary>
    /// <remarks>
    /// 
    /// </remarks>
    [Serializable]
    public class SysConfig : BaseData
    {
        /// <summary>
        /// SysConfig
        /// </summary>
        public SysConfig()
        {
            Initialize();
        }
        
        #region Base Members ...

        /// <summary>
        /// 主键、自增长
        /// </summary>
        public virtual int ID
        {
            set;
            get;
        }

        /// <summary>
        /// 网站名称
        /// </summary>
        public virtual string SiteName
        {
            set;
            get;
        }

        /// <summary>
        /// 网站Url
        /// </summary>
        public virtual string SiteUrl
        {
            set;
            get;
        }

        /// <summary>
        /// 网站Logo
        /// </summary>
        public virtual string SiteLogo
        {
            set;
            get;
        }

        /// <summary>
        /// ICP备案号
        /// </summary>
        public virtual string SiteICPNum
        {
            set;
            get;
        }

        /// <summary>
        /// 版权信息
        /// </summary>
        public virtual string SiteCopyright
        {
            set;
            get;
        }

        /// <summary>
        /// 是否允许上传文件
        /// </summary>
        public virtual bool EnbleFileUpload
        {
            set;
            get;
        }

        /// <summary>
        /// 允许最大上传大小
        /// </summary>
        public virtual int FileUploadMaxSize
        {
            set;
            get;
        }

        /// <summary>
        /// 上传目录
        /// </summary>
        public virtual string FileUploadDir
        {
            set;
            get;
        }

        /// <summary>
        /// 允许上传文件类型
        /// </summary>
        public virtual string FileUploadType
        {
            set;
            get;
        }

        /// <summary>
        /// 上传文件路径
        /// </summary>
        public virtual string UploadFilePath
        {
            set;
            get;
        }

        /// <summary>
        /// 上传文件名
        /// </summary>
        public virtual string UploadFileName
        {
            set;
            get;
        }

        /// <summary>
        /// 允许用户上传文件类型
        /// </summary>
        public virtual string UserFileUploadType
        {
            set;
            get;
        }

        /// <summary>
        /// 用户文件上传目录
        /// </summary>
        public virtual string UserFileUploadDir
        {
            set;
            get;
        }

        /// <summary>
        /// 上传文件缓存天数
        /// </summary>
        public virtual int FileDeleteDelay
        {
            set;
            get;
        }

        /// <summary>
        /// 是否启用短信
        /// </summary>
        public virtual bool EnableSMS
        {
            set;
            get;
        }

        /// <summary>
        /// 短息用户名
        /// </summary>
        public virtual string SMSUserName
        {
            set;
            get;
        }

        /// <summary>
        /// 短息用户名密码
        /// </summary>
        public virtual string SMSUserPwd
        {
            set;
            get;
        }

        /// <summary>
        /// 短息延时
        /// </summary>
        public virtual int SMSDelay
        {
            set;
            get;
        }

        /// <summary>
        /// 短信内容
        /// </summary>
        public virtual string SMSContent
        {
            set;
            get;
        }

        /// <summary>
        /// 是否启用邮件
        /// </summary>
        public virtual bool EnableMail
        {
            set;
            get;
        }

        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public virtual string MailAccount
        {
            set;
            get;
        }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public virtual string MailPasswd
        {
            set;
            get;
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public virtual string DisplayName
        {
            set;
            get;
        }

        /// <summary>
        /// SMTP地址
        /// </summary>
        public virtual string SmtpHost
        {
            set;
            get;
        }

        /// <summary>
        /// SMTP端口
        /// </summary>
        public virtual int Port
        {
            set;
            get;
        }

        /// <summary>
        /// 注册欢迎信息
        /// </summary>
        public virtual string RegisterMsg
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
            xe = CreateElement(xml, Columns.SiteName.ToString(),SiteName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SiteUrl.ToString(),SiteUrl.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SiteLogo.ToString(),SiteLogo.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SiteICPNum.ToString(),SiteICPNum.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SiteCopyright.ToString(),SiteCopyright.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EnbleFileUpload.ToString(),EnbleFileUpload.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FileUploadMaxSize.ToString(),FileUploadMaxSize.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FileUploadDir.ToString(),FileUploadDir.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FileUploadType.ToString(),FileUploadType.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UploadFilePath.ToString(),UploadFilePath.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UploadFileName.ToString(),UploadFileName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UserFileUploadType.ToString(),UserFileUploadType.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.UserFileUploadDir.ToString(),UserFileUploadDir.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.FileDeleteDelay.ToString(),FileDeleteDelay.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EnableSMS.ToString(),EnableSMS.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SMSUserName.ToString(),SMSUserName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SMSUserPwd.ToString(),SMSUserPwd.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SMSDelay.ToString(),SMSDelay.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SMSContent.ToString(),SMSContent.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.EnableMail.ToString(),EnableMail.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.MailAccount.ToString(),MailAccount.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.MailPasswd.ToString(),MailPasswd.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.DisplayName.ToString(),DisplayName.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.SmtpHost.ToString(),SmtpHost.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.Port.ToString(),Port.ToString());
            xn.AppendChild(xe);
            xe = CreateElement(xml, Columns.RegisterMsg.ToString(),RegisterMsg.ToString());
            xn.AppendChild(xe);
            
            return xn;
        }
        
        /// <summary>
        /// 设置初始化默认值
        /// </summary>
        private void Initialize()
        {
            ID = 0;
            SiteName = string.Empty;
            SiteUrl = string.Empty;
            SiteLogo = string.Empty;
            SiteICPNum = string.Empty;
            SiteCopyright = string.Empty;
            EnbleFileUpload = false;
            FileUploadMaxSize = 0;
            FileUploadDir = string.Empty;
            FileUploadType = string.Empty;
            UploadFilePath = string.Empty;
            UploadFileName = string.Empty;
            UserFileUploadType = string.Empty;
            UserFileUploadDir = string.Empty;
            FileDeleteDelay = 0;
            EnableSMS = false;
            SMSUserName = string.Empty;
            SMSUserPwd = string.Empty;
            SMSDelay = 0;
            SMSContent = string.Empty;
            EnableMail = false;
            MailAccount = string.Empty;
            MailPasswd = string.Empty;
            DisplayName = string.Empty;
            SmtpHost = string.Empty;
            Port = 0;
            RegisterMsg = string.Empty;
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return "SysConfig";
        }
        #endregion
        
        #region Order Columns ...

        /// <summary>
        /// 
        /// </summary>
        public enum Columns
        {
            ID,
            SiteName,
            SiteUrl,
            SiteLogo,
            SiteICPNum,
            SiteCopyright,
            EnbleFileUpload,
            FileUploadMaxSize,
            FileUploadDir,
            FileUploadType,
            UploadFilePath,
            UploadFileName,
            UserFileUploadType,
            UserFileUploadDir,
            FileDeleteDelay,
            EnableSMS,
            SMSUserName,
            SMSUserPwd,
            SMSDelay,
            SMSContent,
            EnableMail,
            MailAccount,
            MailPasswd,
            DisplayName,
            SmtpHost,
            Port,
            RegisterMsg,
        }

        #endregion
        
    }
}


