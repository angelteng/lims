using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using Hope.Util;

namespace Hope.WebBase
{
    /// <summary>
    /// 系统多国语言
    /// </summary>
    public sealed class Message
    {
        static Message()
        {
            _CN = new Dictionary<string, string>();
            _EN = new Dictionary<string, string>();
        }

        #region Property

        private static string _MessageFileName;
        /// <summary>
        /// 语言文件名
        /// </summary>
        public static string MessageFileName
        {
            get{ return _MessageFileName; }
            set { _MessageFileName = value; }
        }

        #region 中文

        private static Dictionary<string, string> _CN;
        /// <summary>
        /// 中文
        /// </summary>
        public static Dictionary<string, string> CN
        {
            get { return _CN; }
        }

        #endregion


        #region 英文

        private static Dictionary<string, string> _EN;
        /// <summary>
        /// 英文
        /// </summary>
        public static Dictionary<string, string> EN
        {
            get { return _EN; }
        }

        #endregion

        /// <summary>
        /// 节点属性
        /// </summary>
        private const string AttributeName = "code";

        /// <summary>
        /// xml节点前缀
        /// </summary>
        private const string XmlPath_RootPrefix = "lang";

        private const string XmlPath_CN = "cn";

        private const string XmlPath_EN = "en";

        #endregion

        #region Methord
        /// <summary>
        /// 解析xml文件
        /// </summary>
        public static void ReadXml()
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                string path = PropertiesHelper.GetString(WebEnvironment.ApplicationDirectory, WebEnvironment.Properties, "") + _MessageFileName;
                xml.Load(path);
            }
            catch
            {
                return;
            }

            XmlNode root = xml.SelectSingleNode(XmlPath_RootPrefix);

            //解析中文节点
            XmlNode node = root.SelectSingleNode(XmlPath_CN);
            if (node != null)
            {
                SetProperties(node.ChildNodes, CN);
            }

            //解析英文节点
            node = root.SelectSingleNode(XmlPath_EN);
            if (node != null)
            {
                SetProperties(node.ChildNodes, EN);
            }
        }

        private static void SetProperties(XmlNodeList nodes, Dictionary<string, string> dictionary)
        {
            //GlobalProperties.Clear();
            string key;
            foreach (XmlNode node in nodes)
            {
                if (node.Attributes[AttributeName] == null)
                    continue;
                key = node.Attributes[AttributeName].Value.Trim();
                dictionary[key] = node.InnerText;
            }
        }

        #endregion


    }
}
