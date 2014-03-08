using System;
using System.Xml;

namespace Hope.TemplateUtil
{
    public partial class HopeTag
    {
        /// <summary>
        /// ÇøÓòÎÄµµ
        /// </summary>
        public XmlDocument regionalXmlDoc = new XmlDocument();

        public XmlDocument RegionalXmlDoc
        {
            get
            {
                if (regionalXmlDoc.DocumentElement != null)
                {
                    return regionalXmlDoc;
                }

                try
                {
                    regionalXmlDoc.Load(System.Web.HttpContext.Current.Server.MapPath("~/App_Data/config/RegionalData.xml"));
                }
                catch (Exception)
                {

                }

                return regionalXmlDoc;
            }
        }

        //public string GetRegionalItem(string name)
        //{
        //    string lang = Hope.Util.SiteConfigUtil.Instance().IndexPageLanguage;

        //    if (CurrentCategoryID > 0)
        //    {
        //        lang = Category.UICulture;
        //    }

        //    return GetRegionalItem(name, lang);
        //}

        //public string GetRegionalItem(string name, string lang)
        //{
        //    string xpath = string.Format("/*/language[@name='{0}']/{1}", lang, name);
        //    XmlNode node = RegionalXmlDoc.DocumentElement.SelectSingleNode(xpath);
        //    if (node != null)
        //    {
        //        return node.InnerXml;
        //    }

        //    return "";
        //}
    }
}