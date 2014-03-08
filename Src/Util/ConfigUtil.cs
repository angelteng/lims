using System;
using System.Configuration;
using System.Web;

namespace Hope.Util
{
    /// <summary>
    /// Config操作类
    /// </summary>
    public class ConfigUtil
    {
        #region Private Variables

        //private static readonly string ExeConfigName = "App.config";
        //private static readonly string WebConfigName = "Web.config";

        #endregion

        #region Constructors

        /// <summary>
        /// Constructor
        /// </summary>
        public ConfigUtil()
        {
            ConfigType = ConfigType.ExeConfig;

            Initialize();
        }

        ///// <summary>
        ///// Constructor
        ///// </summary>
        //public ConfigUtil(ConfigType configType)
        //{
        //    ConfigType = configType;

        //    if (ConfigType == ConfigType.ExeConfig)
        //    {
        //        ConfigPath = AppDomain.CurrentDomain.BaseDirectory + ExeConfigName;
        //    }
        //    else
        //    {
        //        ConfigPath = HttpContext.Current.Request.ApplicationPath + WebConfigName;
        //    }

        //    Initialize();
        //}

        ///// <summary>
        ///// Constructor
        ///// </summary>
        ///// <param name="path">Config文件的位置</param>
        ///// <param name="type">Config文件的类型</param>
        //public ConfigUtil(ConfigType configType, string configPath)
        //{
        //    ConfigPath = configPath;
        //    ConfigType = configType;

        //    Initialize();
        //}

        /// <summary>
        /// 实例化Configuration
        /// </summary>
        /// <remarks>根据配置文件类型的不同，分别采取不同的实例化方法</remarks>
        private void Initialize()
        {
            if (ConfigType == ConfigType.ExeConfig) // WinForm应用程序的配置文件
            {
                Configuration = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            }
            else    // WebForm的配置文件
            {
                Configuration = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(ConfigPath);
            }
        }

        #endregion

        #region Properties

        private string _ConfigPath;
        /// <summary>
        /// Config文件位置
        /// </summary>
        public string ConfigPath
        {
            get
            {
                return _ConfigPath;
            }
            set
            {
                _ConfigPath = value;
            }
        }

        private ConfigType _ConfigType;
        /// <summary>
        /// Config文件类型
        /// </summary>
        public ConfigType ConfigType
        {
            get
            {
                return _ConfigType;
            }
            set
            {
                _ConfigType = value;
            }
        }

        private Configuration _Configuration;
        /// <summary>
        /// 配置文件对象
        /// </summary>
        public Configuration Configuration
        {
            get
            {
                return _Configuration;
            }
            set
            {
                _Configuration = value;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 获取ConfigurationSection
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private ConfigurationSection GetConfigurationSection(string name)
        {
            return Configuration.GetSection(name);
        }

        /// <summary>
        /// 获取AppSettingsSection
        /// </summary>
        /// <returns></returns>
        private AppSettingsSection GetAppSettingsSection()
        {
            return (AppSettingsSection)GetConfigurationSection("appSettings");
        }

        /// <summary>
        /// 获取ConnectionStringsSection
        /// </summary>
        /// <returns></returns>
        private ConnectionStringsSection GetConnectionStringsSection()
        {
            return (ConnectionStringsSection)GetConfigurationSection("connectionStrings");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 读取AppSetting值
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetAppSettingValue(string key)
        {
            // 读取运行时配置文件
            return GetAppSettingsSection().Settings[key].Value;
            // 读取当前配置文件
            //return ConfigurationManager.AppSettings[key];
        }

        /// <summary>
        /// 读取ConnectionString值
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public string GetConnectionString(string name)
        {
            // 读取运行时配置文件
            return GetConnectionStringsSection().ConnectionStrings[name].ConnectionString;
            // 读取当前配置文件
            //return ConfigurationManager.ConnectionStrings[name].ConnectionString;
        }


        ///// <summary>
        ///// 添加应用程序配置节点，如果已经存在此节点，则会修改该节点的值
        ///// </summary>
        ///// <param name="key">节点名称</param>
        ///// <param name="value">节点值</param>
        //public void AddAppSetting(string key, string value)
        //{
        //    AppSettingsSection appSetting = (AppSettingsSection)Configuration.GetSection("appSettings");
        //    if (appSetting.Settings[key] == null)   // 如果不存在此节点，则添加
        //    {
        //        appSetting.Settings.Add(key, value);
        //    }
        //    else    // 如果存在此节点，则修改
        //    {
        //        ModifyAppSetting(key, value);
        //    }
        //}

        ///// <summary>
        ///// 添加数据库连接字符串节点，如果已经存在此节点，则会修改该节点的值
        ///// </summary>
        ///// <param name="key">节点名称</param>
        ///// <param name="value">节点值</param>
        //public void AddConnectionString(string key, string connectionString)
        //{
        //    ConnectionStringsSection connectionSetting = (ConnectionStringsSection)Configuration.GetSection("connectionStrings");
        //    if (connectionSetting.ConnectionStrings[key] == null)   // 如果不存在此节点，则添加
        //    {
        //        ConnectionStringSettings connectionStringSettings = new ConnectionStringSettings(key, connectionString);
        //        connectionSetting.ConnectionStrings.Add(connectionStringSettings);
        //    }
        //    else    // 如果存在此节点，则修改
        //    {
        //        ModifyConnectionString(key, connectionString);
        //    }
        //}

        ///// <summary>
        ///// 修改应用程序配置节点，如果不存在此节点，则会添加此节点及对应的值
        ///// </summary>
        ///// <param name="key">节点名称</param>
        ///// <param name="value">节点值</param>
        //public void ModifyAppSetting(string key, string newValue)
        //{
        //    AppSettingsSection appSetting = (AppSettingsSection)Configuration.GetSection("appSettings");
        //    if (appSetting.Settings[key] != null)   // 如果存在此节点，则修改
        //    {
        //        appSetting.Settings[key].Value = newValue;
        //    }
        //    else    // 如果不存在此节点，则添加
        //    {
        //        AddAppSetting(key, newValue);
        //    }
        //}

        ///// <summary>
        ///// 修改数据库连接字符串节点，如果不存在此节点，则会添加此节点及对应的值
        ///// </summary>
        ///// <param name="key">节点名称</param>
        ///// <param name="value">节点值</param>
        //public void ModifyConnectionString(string key, string connectionString)
        //{
        //    ConnectionStringsSection connectionSetting = (ConnectionStringsSection)Configuration.GetSection("connectionStrings");
        //    if (connectionSetting.ConnectionStrings[key] != null)   // 如果存在此节点，则修改
        //    {
        //        connectionSetting.ConnectionStrings[key].ConnectionString = connectionString;
        //    }
        //    else    // 如果不存在此节点，则添加
        //    {
        //        AddConnectionString(key, connectionString);
        //    }
        //}

        ///// <summary>
        ///// 删除应用程序配置节点
        ///// </summary>
        ///// <param name="key"></param>
        //public void RemoveAppSetting(string key)
        //{
        //    AppSettingsSection appSetting = (AppSettingsSection)Configuration.GetSection("appSettings");
        //    appSetting.Settings.Remove(key);
        //}

        ///// <summary>
        ///// 删除数据库连接字符串节点
        ///// </summary>
        ///// <param name="key"></param>
        //public void RemoveConnectionString(string key)
        //{
        //    ConnectionStringsSection connectionSetting = (ConnectionStringsSection)Configuration.GetSection("connectionStrings");
        //    connectionSetting.ConnectionStrings.Remove(key);
        //}

        /// <summary>
        /// 修改数据库连接字符串
        /// </summary>
        /// <param name="name"></param>
        /// <param name="connectionString"></param>
        public void UpdateConnectionString(string name, string connectionString)
        {
            ConnectionStringsSection connectionStringsSection = (ConnectionStringsSection)Configuration.GetSection("connectionStrings");
            connectionStringsSection.ConnectionStrings[name].ConnectionString = connectionString;
        }

        /// <summary>
        /// 保存修改
        /// </summary>
        public void Save()
        {
            Configuration.Save();
        }

        /// <summary>
        /// 保存修改到指定文件
        /// </summary>
        /// <param name="path"></param>
        public void Save(string path)
        {
            Configuration.SaveAs(path);
        }

        #endregion

        /************************************************************************************
         * 
            用法实例：
            （一）WebForm中的用法

            ConfigurationOperator co = new ConfigurationOperator(ConfigType.WebConfig);
                    string key = txtConnectionStringKey.Text;
                    string value = txtConnectionStringValue.Text;
                    co.AddConnectionString(key, value);
                    co.Save();
            （二）WinForm中的用法

            ConfigurationOperator co = new ConfigurationOperator(ConfigType.ExeConfig);
                        co.AddAppSetting("Font-Size", "9");
                        co.AddAppSetting("WebSite", /html/net/);
                        co.AddConnectionString("Connection", "test");
                        co.Save();//保存写入结果
         * 
         * ***********************************************************************************/
    }

    /// <summary>
    /// Config文件类型枚举
    /// </summary>
    public enum ConfigType
    {
        /// <summary>
        /// Web.Config文件
        /// </summary>
        WebConfig = 1,

        /// <summary>
        /// App.Config文件
        /// </summary>
        ExeConfig = 2,
    }
}