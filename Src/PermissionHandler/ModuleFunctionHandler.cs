using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using Hope.Util;

namespace Hope.PermissionHandler
{
    /// <summary>
    /// 模块功能处理
    /// </summary>
    public sealed class ModuleFunctionHandler
    {
        private string _LoadFileName = "";

        private XmlDocument _XmlDoc = new XmlDocument();

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="fileName"></param>
        public ModuleFunctionHandler(string fileName)
        {
            //在这里如何去的App_Data的物理路径？
            _LoadFileName = fileName;
            this.ReloadXMLData();
        }

        //public static ModuleFunctionHandler Instance()
        //{
        //    return theInstance;
        //}

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public bool ReBuildXMLData(string fileName)
        {
            bool bResult = true;

            XmlDocument xmlDoc = XMLHelper.CreateXmlDoc("root");

            List<SysModule> moduleDatas = BLLFactory.Instance().SysModuleBLL.GetListByParentID(0);

            foreach (SysModule pModule in moduleDatas)
            {
                List<SysModule> moduleGroups = BLLFactory.Instance().SysModuleBLL.GetListByParentID(pModule.ID);

                foreach (SysModule moduleGroup in moduleGroups)
                {
                    List<SysModule> modules = BLLFactory.Instance().SysModuleBLL.GetListByParentID(moduleGroup.ID);

                    foreach (SysModule module in modules)
                    {
                        List<SysFunction> functionDatas = BLLFactory.Instance().SysFunctionBLL.GetListByModuleID(module.ID);

                        foreach (SysFunction pFunction in functionDatas)
                        {
                            XmlNode xFunction = xmlDoc.CreateNode("element", "function", "");

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "ModuleID", module.ID.ToString());

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "ModuleName", module.ModuleName);

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "FunctionName", pFunction.FunctionName);

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "FunctionKey", pFunction.FunctionKey);

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "FunctionID", pFunction.ID.ToString());

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "FunctionUrl", pFunction.FunctionUrl);

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "Value", pFunction.Value.ToString());

                            XMLHelper.AddAttributeXMLNode(xmlDoc, xFunction, "AdminPage", pFunction.AdminPage.ToString());

                            xmlDoc.DocumentElement.AppendChild(xFunction);
                        }
                    }
                }
            }

            xmlDoc.Save(fileName);

            return bResult;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool ReloadXMLData()
        {
            _XmlDoc.Load(_LoadFileName);

            return true;
        }

        #region xPath Query
        
        /// <summary>
        /// 根据url查询
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ModuleFunctionData GetMFDataByURL(string url)
        {

            XmlNode funNode = _XmlDoc.DocumentElement.SelectSingleNode("//function[@FunctionUrl='" + url + "']");

            if (funNode != null)
            {
                ModuleFunctionData mfData = new ModuleFunctionData();

                foreach (XmlAttribute attrNode in funNode.Attributes)
                {
                    if (attrNode.Name.Equals("ModuleID"))
                    {
                        mfData.ModuleID = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("FunctionID"))
                    {
                        mfData.FunctionID = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("Value"))
                    {
                        mfData.Value = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("FunctionUrl"))
                    {
                        mfData.FunctionUrl = attrNode.InnerText;
                    }

                    if (attrNode.Name.Equals("AdminPage"))
                    {
                        mfData.AdminPage = attrNode.InnerText.Equals("True");
                    }
                }
                return mfData;
            }
            return null;
        }

        /// <summary>
        /// 根据Key查询
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public ModuleFunctionData GetMFDataByKey(string functionKey)
        {

            XmlNode funNode = _XmlDoc.DocumentElement.SelectSingleNode("//function[@FunctionKey='" + functionKey + "']");

            if (funNode != null)
            {
                ModuleFunctionData mfData = new ModuleFunctionData();

                foreach (XmlAttribute attrNode in funNode.Attributes)
                {
                    if (attrNode.Name.Equals("ModuleID"))
                    {
                        mfData.ModuleID = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("FunctionID"))
                    {
                        mfData.FunctionID = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("Value"))
                    {
                        mfData.Value = ConvertHelper.ToInt(attrNode.InnerText);
                    }

                    if (attrNode.Name.Equals("FunctionUrl"))
                    {
                        mfData.FunctionUrl = attrNode.InnerText;
                    }

                    if (attrNode.Name.Equals("AdminPage"))
                    {
                        mfData.AdminPage = attrNode.InnerText.Equals("True");
                    }
                }
                return mfData;
            }
            return null;
        }
        #endregion

    }
}
