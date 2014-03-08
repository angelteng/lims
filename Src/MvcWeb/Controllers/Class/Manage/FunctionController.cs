
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysFunctionController(SysFunctionController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-06-01
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.WebBase;
using Hope.Util;
using System.Xml;

namespace Hope.ITMS.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class FunctionController : ManageController
    {
        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new SysFunction_Save(this);
        }

        /// <summary>
        /// 获取XML不带模块值
        /// </summary>
        /// <returns></returns>
        public ActionResult GetFunctionTreeXMLWithoutModule()
        {
            return new SysFunction_GetFunctionTreeXMLWithoutModule(this);
        }
    }

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class SysFunction_Save : ManageActionResult
    {
        #region members declaration
        private int _Action = 0;        //1: Add Root, 2: Add Child, 3: Edit, 4: Delete
        SysFunction data = null;
        #endregion
        /// <summary>
        /// 
        /// </summary>
        public SysFunction_Save(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            _Action = RequestUtil.RequestInt(Request, "Action", 0);
            ProcessSave();
        }

        /// <summary>
        /// 处理保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessSave()
        {
            data = GetInput();
            switch (_Action)
            {
                case 1: ProcessAdd(); break;
                case 2: ProcessAdd(); break;
                case 3: ProcessEdit(); break;
                case 4: ProcessDelete(); break;
                default: break;
            }
            HandlerMessage.Remark = _Action.ToString();  //标识操作类型
        }

        /// <summary>
        /// 处理添加保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessAdd()
        {
            if (!CheckPermission(2))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().SysFunctionBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit()
        {
            if (!CheckPermission(4))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().SysFunctionBLL.Edit(data);
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private SysFunction GetInput()
        {
            SysFunction data = new SysFunction();
            data.ID = ConvertHelper.ToInt(Request["FunctionId"]);
            data.ModuleID = ConvertHelper.ToInt(Request["ModuleID"]);
            data.FunctionKey = ConvertHelper.ToString(Request["FunctionKey"]);
            data.Value = ConvertHelper.ToInt(Request["Value"]);
            data.FunctionName = ConvertHelper.ToString(Request["FunctionName"]);
            data.FunctionUrl = ConvertHelper.ToString(Request["DefaultUrl"]);
            data.AdminPage = ConvertHelper.ToBoolean(Request["AdminPage"]);
            data.Remark = ConvertHelper.ToString(Request["Remark"]);
            return data;
        }

        /// <summary>
        /// 删除功能
        /// </summary>
        private void ProcessDelete()
        {
            if (data.ID <= 0)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "请选择需要删除的内容";
                HandlerMessage.Succeed = false;
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Module/";
                return;
            }
            List<int> list = new List<int>();
            list.Add(data.ID);
            HandlerMessage = BLLFactory.Instance().SysFunctionBLL.Remove(list);
        }

        #region override ...

        /// <summary>
        /// 当前页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Text;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "";
            }
        }

        /// <summary>
        /// 权限映射
        /// </summary>
        protected override string MappingURL
        {
            get
            {
                return "/Module/Save/";
            }
        }
        #endregion
    }

    #endregion    

    #region process GetFunctionTreeXMLWithoutModule ...

    /// <summary>
    /// 获取XML
    /// </summary>
    public class SysFunction_GetFunctionTreeXMLWithoutModule : BaseManageActionResult
    {
        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("tree");
        int moduleId;

        /// <summary>
        /// 获取XML
        /// </summary>
        public SysFunction_GetFunctionTreeXMLWithoutModule(ManageController controller)
            : base(controller)
        {            
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            moduleId = RequestUtil.RequestInt(Request, "ModuleId", 0);
            if (moduleId != 0)
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, _XmlDoc.DocumentElement, "id", "0");

                BuildModuleXML(_XmlDoc.DocumentElement, moduleId);

                Response.ContentType = "text/xml";
                Response.ContentEncoding = System.Text.Encoding.UTF8;
                Response.Write(_XmlDoc.InnerXml);
                Response.End();
            }
        }

        #region build xml
        private void BuildModuleXML(XmlNode APNode, int ARootID)
        {
            //根据模块取得次模块的功能列表
            List<SysFunction> funDatas = BLLFactory.Instance().SysFunctionBLL.GetListByModuleID(ARootID);

            if (funDatas.Count > 0) //有功能信息
            {
                foreach (SysFunction pFunction in funDatas)  //循环子功能
                {
                    XmlNode xfItem = CreateFunctionTreeItem(pFunction);

                    APNode.AppendChild(xfItem);
                }
            }
        }

        private XmlNode CreateModuleTreeItem(SysModule pModule)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", pModule.ModuleName);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", pModule.ID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", pModule.ID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", pModule.ParentID.ToString());

            return xItem;
        }

        private XmlNode CreateFunctionTreeItem(SysFunction pFunction)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", pFunction.FunctionName);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", pFunction.ID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "2");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", pFunction.FunctionKey.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", pFunction.ModuleID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", pFunction.FunctionUrl);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", pFunction.Remark);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", pFunction.Value.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", pFunction.AdminPage.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "set3.gif");

            return xItem;
        }
        #endregion        

        #region override ...

        /// <summary>
        /// 当前页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板文件路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "";
            }
        }

        #endregion
    }

    #endregion

}


