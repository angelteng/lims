/****************************
 * 
 * 设置结节 xPath
 * 
 * **************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Hope.WebBase
{
    /// <summary>
    /// 系统环境
    /// </summary>
    public sealed class WebEnvironment
    {
        /// <summary>
        /// 
        /// </summary>
        static WebEnvironment()
        {
            GlobalProperties = new Dictionary<string, string>();
        }

        private static string _ServerFileName;
        /// <summary>
        /// 服务配置文件名
        /// </summary>
        internal static string ServerFileName
        {
            get { return _ServerFileName; }
            set { _ServerFileName = value; }
        }

        //系统
        public const string ApplicationDirectory = "application_directory";
        public const string ConfigFileName = "config_file_name";
        public const string ServerFile = "server_file";
        public const string ConnectionString = "connection_string";
        //映射
        public const string MappingRelative = "mapping_relative";
        public const string MappingDirectory = "mapping_directory";
        public const string MappingFiles = "nhibernate-mapping";
        public const string NHibernateFile = "nhibernate_file";
        //模板
        public const string TemplateRelate = "template_relative";
        public const string TemplateRoot = "template_root";
        public const string TagTemplateRoot = "tag_template_root";
        //验证
        public const string ValidateMode = "validate_mode";
        public const string ValidateLength = "validate_length";
        //后台
        public const string AdminDirectory = "admin_directory";

        /// <summary>
        /// xml节点前缀
        /// </summary>
        public const string XmlPath_RootPrefix = "configuration";
        /// <summary>
        /// 服务配置节点
        /// </summary>
        public const string XmlPath_Server = "server";
        /// <summary>
        /// 文件MINE类型
        /// </summary>
        public const string XmlPath_ContentType = "content-type";

        private static Dictionary<string, string> GlobalProperties;
        /// <summary>
        /// 属性
        /// </summary>
        public static IDictionary<string, string> Properties
        {
            get { return new Dictionary<string, string>(GlobalProperties); }
        }

        /// <summary>
        /// 加载全局属性
        /// </summary>
        internal static void LoadGlobalProperties()
        {
            
        }

        public const string AttributeName = "name";
        /// <summary>
        /// 设置属性
        /// </summary>
        internal static void SetProperties(XmlNodeList nodes)
        {
            //GlobalProperties.Clear();
            string key;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes[AttributeName] == null)
                    continue;
                key = node.Attributes[AttributeName].Value.Trim();
                GlobalProperties[key] = node.InnerText;
            }
        }

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        internal static void SetPropertie(string key,string value)
        {
            GlobalProperties[key] = value;
        }

    }
}
