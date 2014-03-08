
/******************************************************************
 *
 * 所在模块：
 * 类 名 称：SystemMessage(系统处理消息)
 * 功能描述：系统处理过程中响应的消息
 * 
 * ------------创建信息------------------
 * 作    者：Xiaogug
 * 日    期：2008-07-20
 * xiaogug@163.com
 * MSN:xiaogug@hotmail.com
 * QQ:31805204
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.Xml;

namespace Hope.Util
{

    /// <summary>
    /// 系统响应信息
    /// </summary>
    public class SystemMessage
    {
        /// <summary>
        /// 系统响应信息
        /// </summary>
        public SystemMessage()
        {
            Initialize();
        }

        #region propertys ...

        private string _Code;
        /// <summary>
        /// 消息代码
        /// </summary>
        public string Code
        {
            set { _Code = value; }
            get { return _Code; }
        }

        private string _Text;
        /// <summary>
        /// 消息内容
        /// </summary>
        public string Text
        {
            set { _Text = value; }
            get { return _Text; }
        }

        private bool _Succeed;
        /// <summary>
        /// 处理是否成功
        /// </summary>
        public bool Succeed
        {
            set { _Succeed = value; }
            get { return _Succeed; }
        }

        private string _TargetUrl;
        /// <summary>
        /// 目标网址
        /// </summary>
        public string TargetUrl
        {
            set { _TargetUrl = value; }
            get { return _TargetUrl; }
        }

        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }

        #endregion

        #region to xml node ...

        /// <summary>
        /// 转换为XML节点
        /// </summary>
        /// <param name="parentDoc">父XML文档</param>
        /// <returns>返回XML文档节点</returns>
        public XmlNode ToXmlNode(XmlDocument parentDoc)
        {
            return ToXmlNode(parentDoc, "Item");
        }

        /// <summary>
        /// 转换为XML节点
        /// </summary>
        /// <param name="parentDoc">父XML文档</param>
        /// <param name="nodeName">节点名</param>
        /// <returns>返回XML文档节点</returns>
        public XmlNode ToXmlNode(XmlDocument parentDoc, string nodeName)
        {
            XmlDocument xmlDoc = parentDoc;
            if (xmlDoc == null)
            {
                xmlDoc = XMLHelper.CreateXmlDoc();
            }
            XmlNode xn = parentDoc.CreateNode("element", nodeName, "");
            XmlElement xe;
            xe = xmlDoc.CreateElement("Code");
            xe.InnerText = _Code;
            xn.AppendChild(xe);
            xe = xmlDoc.CreateElement("Text");
            xe.InnerText = Text;
            xn.AppendChild(xe);
            xe = xmlDoc.CreateElement("Succeed");
            xe.InnerText = Succeed.ToString().ToLower();
            xn.AppendChild(xe);
            xe = xmlDoc.CreateElement("TargetUrl");
            xe.InnerText = TargetUrl;
            xn.AppendChild(xe);
            xe = xmlDoc.CreateElement("Remark");
            xe.InnerText = Remark;
            xn.AppendChild(xe);

            return xn;
        }

        #endregion

        #region to JSON ...

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public string ToJSON()
        {
            return string.Format("{0}\"Code\": \"{2}\",\"Text\":\"{3}\",\"Succeed\":{4},\"Remark\":\"{5}\",\"TargetUrl\":\"{6}\"{1}",
                "{",
                "}",
                this.Code,
                this.Text,
                this.Succeed.ToString().ToLower(),
                this.Remark,
                this.TargetUrl
                );
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="elementName"></param>
        /// <returns></returns>
        public string ToJSON(string elementName)
        {
            return string.Format("{0}\"{1}\":[{0}\"Code\": \"{2}\",\"Text\":\"{3}\",\"Succeed\":{4},\"Remark\":\"{5}\",\"TargetUrl\":\"{6}\"{7}] {7}",
                "{",
                elementName,
                this.Code,
                this.Text,
                this.Succeed.ToString().ToLower(),
                this.Remark,
                this.TargetUrl,
                "}"
                );
        }

        #endregion

        /// <summary>
        /// 初始化默认值
        /// </summary>
        private void Initialize()
        {
            _Code = "00";
            _Text = string.Empty;
            _TargetUrl = "javascript:window.history.back()";
            _Remark = string.Empty;
            _Succeed = false;

        }
    }
}
