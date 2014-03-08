
/******************************************************************
 * 
 * 	所在模块：Xiaogug.Taobaobao.DataProvider（SQL Server数据库访问）
 * 	类 名 称：NHibernateHelper（NHibernate助手）
 * 	功能描述：NHibernate助手
 * 
 * 	------------创建信息------------------
 * 	作    者：Xiaogug
 * 	日    期：2009-10-11
 *  xiaogug@163.com
 *  MSN:xiaogug@hotmail.com
 *  QQ:31805204
 * 	------------编辑修改信息--------------
 * 	作    者：
 * 	日    期：
 * 	内    容：
******************************************************************/
using System;
using System.Xml;

using NHibernate;
using Hope.Util;
using Hope.WebBase;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// NHibernate助手
    /// </summary>
    public class NHibernateHelper
    {
        public static NHibernate.Cfg.Configuration Configuration;
        public static ISessionFactory SessionFactory;
        public static bool Succeed;

        /// <summary>
        /// NHibernate助手
        /// </summary>
        static NHibernateHelper()
        {
            try
            {
                Configuration = new NHibernate.Cfg.Configuration();
                LoadServerFile();
                Configuration.AddAssembly("Hope.ITMS.Model");
                Configuration.SetProperty("connection.connection_string", AppConfig.DBConnectionString);
                SessionFactory = Configuration.BuildSessionFactory();
            }
            catch (Exception e)
            {
                Succeed = false;
                LogUtil.Write(e.Message);
                return;
            }
            Succeed = true;
        }

        #region members...

        #endregion

        #region methods ...

        /// <summary>
        /// 初始化
        /// </summary>
        public static void Initialize()
        {
            LogUtil.Write("启动NHibernate...");           
        }

        /// <summary>
        /// 加载Server文件
        /// </summary>
        private static void LoadServerFile()
        {
            XmlDocument xml = new XmlDocument();
            XmlNode xn;
            xml.Load(AppConfig.ApplicationDirectory + AppConfig.ServerFile);
            XmlNode root = xml.LastChild;
            xn = root.SelectSingleNode(WebEnvironment.MappingFiles);

            Configuration.Configure(AppConfig.MappingDirectory + xn.Attributes["name"].Value);
            //加载映射文件名
            foreach (XmlNode n in xn.ChildNodes)
            {
                Configuration.AddFile(AppConfig.MappingDirectory + n.InnerText);
            }
        }

        #endregion
    }
}
