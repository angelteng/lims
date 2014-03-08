using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using System.Web;

namespace Hope.Util
{
    /// <summary>
    /// 网站配置
    /// </summary>
    public sealed class SiteConfigUtil
    {
        private static readonly SiteConfigUtil theInstance = new SiteConfigUtil();

        private XmlDocument _XmlDocument = null;

        private string _SiteConfigDataFileName = "~/App_Data/config/SiteConfigData.xml";

        private SiteConfigUtil()
        {
            _XmlDocument = new XmlDocument();
            _XmlDocument.Load(HttpContext.Current.Server.MapPath(_SiteConfigDataFileName));
            RefreshConfigData();
        }

        /// <summary>
        /// 获取网站配置的唯一静态变量
        /// </summary>
        /// <returns></returns>
        public static SiteConfigUtil Instance()
        {
            return theInstance;
        }

        #region 刷新网站配置

        /// <summary>
        /// 刷新网站配置
        /// </summary>
        public void RefreshConfigData()
        {
            string sValue = "";
            XmlNode itemNode = null;

            //网站信息配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteName");
            _SiteName = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteUrl");
            _SiteUrl = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteLogo");
            _SiteLogo = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteICPNum");
            _SiteICPNum = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteCopyright");
            _SiteCopyright = HttpContext.Current.Server.HtmlDecode(itemNode.InnerXml);

            //文件上传
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//EnbleFileUpload");
            sValue = itemNode.InnerXml;
            _EnbleFileUpload = ConvertHelper.ToBoolean(sValue, true);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadMaxSize");
            _FileUploadMaxSize = ConvertHelper.ToLong((itemNode.InnerXml));

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadDir");
            _FileUploadDir = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadType");
            _FileUploadType = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//UploadFilePath");
            _UploadFilePath = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//UploadFileName");
            _UploadFileName = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileDeleteDelay");
            _FileDeleteDelay = ConvertHelper.ToInt(itemNode.InnerXml);

            //短信配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//EnableSMS");
            sValue = itemNode.InnerXml;
            _EnableSMS = ConvertHelper.ToBoolean(sValue, true);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSUserName");
            _SMSUserName = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSUserPwd");
            _SMSUserPwd = itemNode.InnerXml;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSDelay");
            _SMSDelay = ConvertHelper.ToInt(itemNode.InnerXml);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSContent");
            _SMSContent = itemNode.InnerXml;

            //邮件配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//EnableMail");
            sValue = itemNode.InnerXml;
            _EnableMail = ConvertHelper.ToBoolean(sValue, false);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//MailAccount");
            sValue = itemNode.InnerXml;
            _MailAccount = ConvertHelper.ToString(sValue, string.Empty);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//MailPasswd");
            sValue = itemNode.InnerXml;
            _MailPasswd = ConvertHelper.ToString(sValue, string.Empty);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//DisplayName");
            sValue = itemNode.InnerXml;
            _DisplayName = ConvertHelper.ToString(sValue, string.Empty);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//SmtpHost");
            sValue = itemNode.InnerXml;
            _SmtpHost = ConvertHelper.ToString(sValue, string.Empty);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//Port");
            sValue = itemNode.InnerXml;
            _Port = ConvertHelper.ToInt(sValue, 25);

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//RegisterMsg");
            sValue = itemNode.InnerXml;
            _RegisterMsg = ConvertHelper.ToString(sValue, string.Empty);
        }

        #endregion

        #region 保存网站配置

        /// <summary>
        /// 保存网站配置
        /// </summary>
        public bool SaveConfigData()
        {
            bool retult = false;

            XmlNode itemNode = null;

            //网站信息配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteName");
            itemNode.InnerXml = _SiteName;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteUrl");
            itemNode.InnerXml = _SiteUrl;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteLogo");
            itemNode.InnerXml = _SiteLogo;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteICPNum");
            itemNode.InnerXml = _SiteICPNum;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("BaseConfig//SiteCopyright");
            itemNode.InnerXml = HttpContext.Current.Server.HtmlEncode(_SiteCopyright);

            //文件上传
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//EnbleFileUpload");
            itemNode.InnerXml = _EnbleFileUpload.ToString().ToLower();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadMaxSize");
            itemNode.InnerXml = _FileUploadMaxSize.ToString();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadDir");
            itemNode.InnerXml = _FileUploadDir;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileUploadType");
            itemNode.InnerXml = _FileUploadType;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//UploadFilePath");
            itemNode.InnerXml = _UploadFilePath;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//UploadFileName");
            itemNode.InnerXml = _UploadFileName;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("FileUpload//FileDeleteDelay");
            itemNode.InnerXml = _FileDeleteDelay.ToString();

            //短信配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//EnableSMS");
            itemNode.InnerXml = _EnableSMS.ToString().ToLower();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSUserName");
            itemNode.InnerXml = _SMSUserName;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSUserPwd");
            itemNode.InnerXml = _SMSUserPwd;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSDelay");
            itemNode.InnerXml = _SMSDelay.ToString();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("SMSConfig//SMSContent");
            itemNode.InnerXml = _SMSContent;

            //邮件配置
            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//EnableMail");
            itemNode.InnerXml = _EnableMail.ToString().ToLower();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//MailAccount");
            itemNode.InnerXml = _MailAccount;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//MailPasswd");
            itemNode.InnerXml = _MailPasswd;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//DisplayName");
            itemNode.InnerXml = _DisplayName;

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//SmtpHost");
            itemNode.InnerXml = _SmtpHost.ToLower();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//Port");
            itemNode.InnerXml = _Port.ToString();

            itemNode = _XmlDocument.DocumentElement.SelectSingleNode("MailConfig//RegisterMsg");
            itemNode.InnerXml = _RegisterMsg;

            //分隔

            try
            {
                _XmlDocument.Save(HttpContext.Current.Server.MapPath(_SiteConfigDataFileName));

                RefreshConfigData();

                retult = true;
            }
            catch (Exception ex)
            {
                LogUtil.error(ex.Message);
            }

            return retult;

        }

        #endregion        

        #region 基本信息
        private string _SiteName;
        private string _SiteUrl;
        private string _SiteLogo;
        private string _SiteICPNum;
        private string _SiteCopyright;

        /// <summary>
        /// SiteName
        /// </summary>
        public string SiteName
        {
            get
            {
                return _SiteName;
            }
            set
            {
                _SiteName = value;
            }
        }

        /// <summary>
        /// SiteUrl
        /// </summary>
        public string SiteUrl
        {
            get
            {
                return _SiteUrl;
            }
            set
            {
                _SiteUrl = value;
            }
        }

        /// <summary>
        /// SiteLogo
        /// </summary>
        public string SiteLogo
        {
            get
            {
                return _SiteLogo;
            }
            set
            {
                _SiteLogo = value;
            }
        }
        
        /// <summary>
        /// SiteICPNum
        /// </summary>
        public string SiteICPNum
        {
            get
            {
                return _SiteICPNum;
            }
            set
            {
                _SiteICPNum = value;
            }
        }

        /// <summary>
        /// SiteCopyright
        /// </summary>
        public string SiteCopyright
        {
            get
            {
                return _SiteCopyright;
            }
            set
            {
                _SiteCopyright = value;
            }
        }
        #endregion...        

        #region 文件上传配置
        private bool _EnbleFileUpload;
        private long _FileUploadMaxSize;
        private string _FileUploadDir;
        private string _FileUploadType;
        private string _UploadFilePath;
        private string _UploadFileName;
        private int _FileDeleteDelay;

        /// <summary>
        /// EnbleFileUpload
        /// </summary>
        public bool EnbleFileUpload
        {
            get
            {
                return _EnbleFileUpload;
            }
            set
            {
                _EnbleFileUpload = value;
            }
        }

        /// <summary>
        /// FileUploadMaxSize
        /// </summary>
        public long FileUploadMaxSize
        {
            get
            {
                return _FileUploadMaxSize;
            }
            set
            {
                _FileUploadMaxSize = value;
            }
        }

        /// <summary>
        /// FileUploadDir
        /// </summary>
        public string FileUploadDir
        {
            get
            {
                return _FileUploadDir;
            }
            set
            {
                _FileUploadDir = value;
            }
        }

        /// <summary>
        /// FileUploadType
        /// </summary>
        public string FileUploadType
        {
            get
            {
                return _FileUploadType;
            }
            set
            {
                _FileUploadType = value;
            }
        }

        /// <summary>
        /// UploadFilePath
        /// </summary>
        public string UploadFilePath
        {
            get
            {
                return _UploadFilePath;
            }
            set
            {
                _UploadFilePath = value;
            }
        }

        /// <summary>
        /// UploadFileName
        /// </summary>
        public string UploadFileName
        {
            get
            {
                return _UploadFileName;
            }
            set
            {
                _UploadFileName = value;
            }
        }

        /// <summary>
        /// FileDeleteDelay
        /// </summary>
        public int FileDeleteDelay
        {
            get
            {
                return _FileDeleteDelay;
            }
            set
            {
                _FileDeleteDelay = value;
            }
        }
        
        #endregion...               

        #region 短信配置
        private bool _EnableSMS;
        private string _SMSUserName;
        private string _SMSUserPwd;
        private int _SMSDelay;
        private string _SMSContent;

        /// <summary>
        /// EnableSMS
        /// </summary>
        public bool EnableSMS
        {
            get
            {
                return _EnableSMS;
            }
            set
            {
                _EnableSMS = value;
            }
        }

        /// <summary>
        /// SMSUserName
        /// </summary>
        public string SMSUserName
        {
            get
            {
                return _SMSUserName;
            }
            set
            {
                _SMSUserName = value;
            }
        }

        /// <summary>
        /// SMSUserPwd
        /// </summary>
        public string SMSUserPwd
        {
            get
            {
                return _SMSUserPwd;
            }
            set
            {
                _SMSUserPwd = value;
            }
        }

        /// <summary>
        /// SMSDelay
        /// </summary>
        public int SMSDelay
        {
            get
            {
                return _SMSDelay;
            }
            set
            {
                _SMSDelay = value;
            }
        }

        /// <summary>
        /// SMSContent
        /// </summary>
        public string SMSContent
        {
            get
            {
                return _SMSContent;
            }
            set
            {
                _SMSContent = value;
            }
        }
        #endregion...               

        #region 邮件配置

        private bool _EnableMail;
        private string _MailAccount;
        private string _MailPasswd;
        private string _DisplayName;
        private string _SmtpHost;
        private int _Port;
        private string _RegisterMsg;

        /// <summary>
        /// 邮件开关
        /// </summary>
        public bool EnableMail
        {
            get { return _EnableMail; }
            set { _EnableMail = value; }
        }

        /// <summary>
        /// MailAccount
        /// </summary>
        public string MailAccount
        {
            get { return _MailAccount; }
            set { _MailAccount = value; }
        }

        /// <summary>
        /// MailPasswd
        /// </summary>
        public string MailPasswd
        {
            get { return _MailPasswd; }
            set { _MailPasswd = value; }
        }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName
        {
            get { return _DisplayName; }
            set { _DisplayName = value; }
        }

        /// <summary>
        /// SmtpHost
        /// </summary>
        public string SmtpHost
        {
            get { return _SmtpHost; }
            set { _SmtpHost = value; }
        }

        /// <summary>
        /// Port
        /// </summary>
        public int Port
        {
            get { return _Port; }
            set { _Port = value; }
        }

        /// <summary>
        /// RegisterMsg
        /// </summary>
        public string RegisterMsg
        {
            get { return _RegisterMsg; }
            set { _RegisterMsg = value; }
        }

        #endregion
    }
}
