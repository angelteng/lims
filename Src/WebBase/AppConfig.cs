using System;
using System.Configuration;
using System.Web;
using System.Web.Hosting;
using System.Xml;
using System.Collections.Generic;

using Hope.Util;

namespace Hope.WebBase
{
    /// <summary>
    /// 应用程序配置
    /// </summary>
    public sealed class AppConfig
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        static AppConfig()
        {
            Initialize();
        }
        #endregion

        #region 版权信息
        /// <summary>
        /// 系统版本
        /// </summary>
        public static string Version
        {
            get { return "V1.0.3"; }
        }

        /// <summary>
        /// 更新时间
        /// </summary>
        public static string UpdateTime
        {
            get { return "2013年11月7日"; }
        }

        /// <summary>
        /// 作者
        /// </summary>
        public static string Author
        {
            get { return "jjf001"; }
        }
        #endregion...

        #region Properties

        #region 应用程序主目录 物理文件路径

        private static string _ApplicationDirectory;

        /// <summary>
        /// 应用程序主目录 物理文件路径
        /// </summary>
        public static string ApplicationDirectory
        {
            get { return _ApplicationDirectory; }
        }

        #endregion


        #region 获取当前WEB应用主目录地址

        private static string _WebMainPathURL;

        /// <summary>
        /// 获取当前WEB应用主目录地址
        /// <!--WebMainPathURL 后不需要反斜杠 / -->
        /// </summary>
        /// <remarks>
        /// 应用于在服务器端使用~/代替
        /// </remarks>
        public static string WebMainPathURL
        {
            get
            {
                _WebMainPathURL = string.Format("{0}{1}",
                                                HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority),
                                                HttpContext.Current.Request.ApplicationPath);

                _WebMainPathURL = _WebMainPathURL.EndsWith("/") ? _WebMainPathURL : _WebMainPathURL + "/";

                return _WebMainPathURL;
            }
        }

        #endregion


        #region 系统配置文件

        private static string _ConfigFile;

        /// <summary>系统配置文件</summary>
        /// <remarks>web.config</remarks>
        public static string ConfigFile
        {
            get { return _ConfigFile; }
        }

        #endregion


        #region 服务配置文件

        private static string _ServerFile;

        /// <summary>服务配置文件</summary>
        /// <remarks>server.xml</remarks>
        public static string ServerFile
        {
            get { return _ServerFile; }
        }

        #endregion


        #region NHibernate
        private static string _MappingDirectory;
        /// <summary>
        /// 映射文件存放主目录
        /// </summary>
        public static string MappingDirectory
        {
            get { return _MappingDirectory; }
        }

        private static Dictionary<string, string> _MappingFiles;
        /// <summary>
        /// 映射文件
        /// </summary>
        public static IDictionary<string, string> MappingFiles
        {
            get { return new Dictionary<string, string>(_MappingFiles); }
        }

        private static string _NHibernateFile;
        /// <summary>
        /// nhibernage文件名称
        /// </summary>
        public static string NHibernateFile
        {
            get { return _NHibernateFile; }
        }
        #endregion

        #region 文件的MINE内容类型

        private static Dictionary<string, string> _ContentTypes;

        /// <summary>
        /// 文件的MINE内容类型
        /// </summary>
        public static IDictionary<string, string> ContentTypes
        {
            get { return new Dictionary<string, string>(_ContentTypes); }
        }

        #endregion


        #region 数据库链接字符串

        private static string _DBConnectionString;

        /// <summary>
        /// 数据库链接字符串
        /// </summary>
        public static string DBConnectionString
        {
            get { return _DBConnectionString; }
        }

        #endregion


        #region 登录的随机码模式

        private static ValidationMode _ValidateMode;

        /// <summary>
        /// 登录的随机码模式
        /// </summary>
        public static ValidationMode ValidateMode
        {
            get { return _ValidateMode; }
        }

        #endregion


        #region 登录的随机码长度

        private static int _ValidateLength;

        /// <summary>
        /// 登录的随机码长度
        /// </summary>
        public static int ValidateLength
        {
            get { return _ValidateLength; }
        }

        #endregion


        #region 后台模板文件主目录

        private static string _ManageTemplateRoot;

        /// <summary>
        /// 后台模板文件主目录
        /// </summary>
        public static string ManageTemplateRoot
        {
            get { return _ManageTemplateRoot; }
        }

        #endregion


        #region 模板文件存放主目录

        private static string _TemplateRoot;

        /// <summary>
        /// 模板文件存放主目录
        /// </summary>
        public static string TemplateRoot
        {
            get { return _TemplateRoot; }
        }

        #endregion


        
        #region 标签文件存放主目录

        private static string _TagTemplateRoot;

        /// <summary>
        /// 标签文件存放主目录
        /// </summary>
        public static string TagTemplateRoot
        {
            get { return _TagTemplateRoot; }
        }

        #endregion

        
        #endregion

        #region 初始化、加载配置文件

        /// <summary>
        /// 初始化
        /// </summary>
        private static void Initialize()
        {
            //_SystemMessages = new Dictionary<string, Dictionary<string, string>>();

            LoadSystemInfo();
            LoadMappingInfo();
            LoadMappingFiles();
            LoadTemplateInfo();
            LoadValidateInfo();
            LoadContenType();
        }

        /// <summary>
        /// 重设
        /// </summary>
        internal static void Reset()
        {
            Initialize();
        }

        /// <summary>
        /// 加载内容类型
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string GetContentType(string type)
        {
            string result = PropertiesHelper.GetString(type, ContentTypes, "");
            if (result != string.Empty)
                return result;

            result = PropertiesHelper.GetString("*", ContentTypes, "");
            return result;
        }

        #region load server config ...

        /// <summary>
        /// 加载系统信息
        /// </summary>
        private static void LoadSystemInfo()
        {
            _ApplicationDirectory = PropertiesHelper.GetString(WebEnvironment.ApplicationDirectory, WebEnvironment.Properties, "");
            _ConfigFile = PropertiesHelper.GetString(WebEnvironment.ConfigFileName, WebEnvironment.Properties, "");
            _ServerFile = PropertiesHelper.GetString(WebEnvironment.ServerFile, WebEnvironment.Properties, "");
            _DBConnectionString = PropertiesHelper.GetString(WebEnvironment.ConnectionString, WebEnvironment.Properties, "");
        }

        /// <summary>
        /// 加载映射信息
        /// </summary>
        private static void LoadMappingInfo()
        {
            bool relative = PropertiesHelper.GetBoolean(WebEnvironment.MappingRelative, WebEnvironment.Properties, true);
            _MappingDirectory = PropertiesHelper.GetString(WebEnvironment.MappingDirectory, WebEnvironment.Properties, "");
            if (relative)
                _MappingDirectory = _MappingDirectory.Replace("~/", ApplicationDirectory).Replace("/", "\\");

            _NHibernateFile = PropertiesHelper.GetString(WebEnvironment.NHibernateFile, WebEnvironment.Properties, "");
        }

        /// <summary>
        /// 加载nhibernate映射文件
        /// </summary>
        private static void LoadMappingFiles()
        {
            _MappingFiles = new Dictionary<string, string>();
            string text = PropertiesHelper.GetString(WebEnvironment.MappingFiles, WebEnvironment.Properties, "");
            text = string.Format("<{0}>{1}</{0}>", WebEnvironment.MappingFiles, text);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);

            foreach (XmlNode node in xml.FirstChild.ChildNodes)
            {
                _MappingFiles[node.InnerText] = node.InnerText;
            }
        }

        /// <summary>
        /// 加载模板信息
        /// </summary>
        private static void LoadTemplateInfo()
        {
            bool relative = PropertiesHelper.GetBoolean(WebEnvironment.TemplateRelate, WebEnvironment.Properties, true);
            _TemplateRoot = PropertiesHelper.GetString(WebEnvironment.TemplateRoot, WebEnvironment.Properties, string.Empty);
            _TagTemplateRoot = PropertiesHelper.GetString(WebEnvironment.TagTemplateRoot, WebEnvironment.Properties, string.Empty);

            if (relative)
            {
                _TemplateRoot = _TemplateRoot.Replace("~/", ApplicationDirectory);
                _TemplateRoot = _TemplateRoot.TrimEnd('/') + "/";
            }

            _ManageTemplateRoot = _TemplateRoot + _ManageTemplateRoot;
            _ManageTemplateRoot = _ManageTemplateRoot.TrimEnd('/') + "/";


            _TagTemplateRoot = _TemplateRoot + _TagTemplateRoot;
            _TagTemplateRoot = _TagTemplateRoot.TrimEnd('/') + "/";

            _TemplateRoot = _TemplateRoot.Replace("/", "\\");
            _ManageTemplateRoot = _ManageTemplateRoot.Replace("/", "\\");
            _TagTemplateRoot = _TagTemplateRoot.Replace("/", "\\");
        }

        /// <summary>
        /// 加载验证码模式
        /// </summary>
        private static void LoadValidateInfo()
        {
            string mode = PropertiesHelper.GetString(WebEnvironment.ValidateMode, WebEnvironment.Properties, "Off");
            _ValidateMode = (ValidationMode)Enum.Parse(typeof(ValidationMode), mode, true);
            _ValidateLength = PropertiesHelper.GetInt32(WebEnvironment.ValidateLength, WebEnvironment.Properties, 0);
        }

        /// <summary>
        /// 加载文件MINE内容类型
        /// </summary>
        private static void LoadContenType()
        {
            _ContentTypes = new Dictionary<string, string>();
            string text = PropertiesHelper.GetString(WebEnvironment.XmlPath_ContentType, WebEnvironment.Properties, "");
            text = string.Format("<{0}>{1}</{0}>", WebEnvironment.XmlPath_ContentType, text);
            XmlDocument xml = new XmlDocument();
            xml.LoadXml(text);

            string key;
            foreach (XmlNode node in xml.FirstChild.ChildNodes)
            {
                if (node.Attributes[WebEnvironment.AttributeName] == null)
                    continue;
                key = node.Attributes[WebEnvironment.AttributeName].Value.Trim();
                _ContentTypes[key] = node.InnerText;
            }
        }
        
        #endregion

        #endregion
    }
}