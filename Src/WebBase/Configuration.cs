using System.Configuration;
using System.IO;
using System.Xml;
using Hope.Util;

namespace Hope.WebBase
{
    /// <summary>
    /// 配置
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// 配置入口
        /// </summary>
        /// <param name="applicationDirectory">应用程序主目录</param>
        /// <param name="configFileName">web.config文件</param>
        public Configuration(string applicationDirectory,string configFileName)
        {
            SetAppConfig(applicationDirectory, configFileName);
            ReadXml();

            ConfigLog(applicationDirectory, configFileName);
        }

        /// <summary>
        /// 配置日志
        /// </summary>
        /// <param name="applicationDirectory"></param>
        /// <param name="configFileName"></param>
        private void ConfigLog(string applicationDirectory, string configFileName)
        {
            log4net.Config.XmlConfigurator.Configure(new FileInfo(applicationDirectory + configFileName));
        }


        /// <summary>
        /// 重设
        /// </summary>
        public void Reset()
        {
            AppConfig.Reset();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="applicationDirectory">应用程序主目录</param>
        /// <param name="configFileName">web.config文件</param>
        private static void SetAppConfig(string applicationDirectory, string configFileName)
        {
            //application_directory：/Work/DotNet/Project/CMS/Src/MvcWeb
            WebEnvironment.SetPropertie(WebEnvironment.ApplicationDirectory, applicationDirectory);
            //config_file_name : web.config
            WebEnvironment.SetPropertie(WebEnvironment.ConfigFileName, configFileName);

            ExeConfigurationFileMap map = new ExeConfigurationFileMap();
            map.ExeConfigFilename = applicationDirectory + configFileName;

            System.Configuration.Configuration config = ConfigurationManager.OpenMappedExeConfiguration(map, ConfigurationUserLevel.None);
            string serverFile = config.AppSettings.Settings["ServerFile"] == null ? "App_Data\\Config\\Server.xml" : config.AppSettings.Settings["ServerFile"].Value;
            //server_file : /App_Data/config/server.xml
            WebEnvironment.SetPropertie(WebEnvironment.ServerFile, serverFile);
            
            string connectionString = config.ConnectionStrings.ConnectionStrings["DBConnection"] == null ? "" : config.ConnectionStrings.ConnectionStrings["DBConnection"].ConnectionString;
            //connection_string : …………
            WebEnvironment.SetPropertie(WebEnvironment.ConnectionString, connectionString);

            //语言文件
            Message.MessageFileName = config.AppSettings.Settings["MessageFile"] == null ? "App_Data\\Config\\Message.xml" : config.AppSettings.Settings["MessageFile"].Value;
            Message.ReadXml();
        }

        /// <summary>
        /// 
        /// </summary>
        private void ReadXml()
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                string path = PropertiesHelper.GetString(WebEnvironment.ApplicationDirectory, WebEnvironment.Properties, "") +
                    PropertiesHelper.GetString(WebEnvironment.ServerFile, WebEnvironment.Properties, "");
                xml.Load(path);
            }
            catch
            {
                return;
            }
            XmlNode root = xml.SelectSingleNode(WebEnvironment.XmlPath_RootPrefix);
            XmlNode node = root.SelectSingleNode(WebEnvironment.XmlPath_Server);
            //解析server节点
            Parse(node);
           
            node = root.SelectSingleNode(WebEnvironment.XmlPath_ContentType);
            //解析content-type节点
            ParseContentType(node);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="configNode"></param>
        private void Parse(XmlNode configNode)
        {
            if (configNode == null)
            {
                return;
            }
            WebEnvironment.SetProperties(configNode.ChildNodes);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        private void ParseContentType(XmlNode node)
        {
            if (node == null)
            {
                return;
            }
            WebEnvironment.SetPropertie(WebEnvironment.XmlPath_ContentType, node.InnerXml);
        }
    }
}
