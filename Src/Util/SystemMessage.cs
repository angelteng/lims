
/******************************************************************
 *
 * ����ģ�飺
 * �� �� �ƣ�SystemMessage(ϵͳ������Ϣ)
 * ����������ϵͳ�����������Ӧ����Ϣ
 * 
 * ------------������Ϣ------------------
 * ��    �ߣ�Xiaogug
 * ��    �ڣ�2008-07-20
 * xiaogug@163.com
 * MSN:xiaogug@hotmail.com
 * QQ:31805204
 * ------------�༭�޸���Ϣ--------------
 * ��    �ߣ�
 * ��    �ڣ�
 * ��    �ݣ�
******************************************************************/
using System;
using System.Xml;

namespace Hope.Util
{

    /// <summary>
    /// ϵͳ��Ӧ��Ϣ
    /// </summary>
    public class SystemMessage
    {
        /// <summary>
        /// ϵͳ��Ӧ��Ϣ
        /// </summary>
        public SystemMessage()
        {
            Initialize();
        }

        #region propertys ...

        private string _Code;
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public string Code
        {
            set { _Code = value; }
            get { return _Code; }
        }

        private string _Text;
        /// <summary>
        /// ��Ϣ����
        /// </summary>
        public string Text
        {
            set { _Text = value; }
            get { return _Text; }
        }

        private bool _Succeed;
        /// <summary>
        /// �����Ƿ�ɹ�
        /// </summary>
        public bool Succeed
        {
            set { _Succeed = value; }
            get { return _Succeed; }
        }

        private string _TargetUrl;
        /// <summary>
        /// Ŀ����ַ
        /// </summary>
        public string TargetUrl
        {
            set { _TargetUrl = value; }
            get { return _TargetUrl; }
        }

        private string _Remark;
        /// <summary>
        /// ��ע
        /// </summary>
        public string Remark
        {
            set { _Remark = value; }
            get { return _Remark; }
        }

        #endregion

        #region to xml node ...

        /// <summary>
        /// ת��ΪXML�ڵ�
        /// </summary>
        /// <param name="parentDoc">��XML�ĵ�</param>
        /// <returns>����XML�ĵ��ڵ�</returns>
        public XmlNode ToXmlNode(XmlDocument parentDoc)
        {
            return ToXmlNode(parentDoc, "Item");
        }

        /// <summary>
        /// ת��ΪXML�ڵ�
        /// </summary>
        /// <param name="parentDoc">��XML�ĵ�</param>
        /// <param name="nodeName">�ڵ���</param>
        /// <returns>����XML�ĵ��ڵ�</returns>
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
        /// ��ʼ��Ĭ��ֵ
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
