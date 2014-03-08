
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysModuleController(SysModuleController)
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
    public class ModuleController : ManageController
    {
        /// <summary>
        /// 索引列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return List();
        }

        /// <summary>
        /// 列表查看		
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return new SysModule_List(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new SysModule_Save(this);
        }

        /// <summary>
        /// 生成对应的xml文件
        /// </summary>
        /// <returns></returns>
        public ActionResult Generate()
        {
            return new SysModule_Generate(this);
        }

        /// <summary>
        /// 获取xmlTree
        /// </summary>
        /// <returns></returns>
        public ActionResult GetModuleTreeXMLWithoutFunction()
        {
            return new SysModule_GetModuleTreeXMLWithoutFunction(this);
        }
    }

    #region SysModuleList ...

    /// <summary>
    /// Show SysModule List
    /// </summary>
    public class SysModule_List : ManageActionResult
    {

        private List<SysModule> list = new List<SysModule>();
        private List<SysFunction> list2 = new List<SysFunction>();
        static int _ModuleId = 0;

        SysModuleBLL _SysModuleBLL = BLLFactory.Instance().SysModuleBLL;
        SysFunctionBLL _SysFunctionBLL = BLLFactory.Instance().SysFunctionBLL;

        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("root");

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysModule_List(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            Show();
            LoadPermission();
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        private void Show()
        {
           
        }

        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission(2));
            TemplateHelper.Put("PermissionDelete", CheckPermission(8));
        }

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
                return "Manage\\Module\\Index.html";
            }
        }

        #endregion

    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class SysModule_Save : ManageActionResult
    {
        #region members declaration
        private int _Action = 0;        //1: Add Root, 2: Add Child, 3: Edit, 4: Delete
        SysModule data = null;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SysModule_Save(ManageController controller)
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
            HandlerMessage = BLLFactory.Instance().SysModuleBLL.Add(data);
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
            HandlerMessage = BLLFactory.Instance().SysModuleBLL.Edit(data);
        }

        /// <summary>
        /// 删除模块
        /// </summary>
        private void ProcessDelete()
        {
            if (data.ID <=0)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "请选择需要删除的内容";
                HandlerMessage.Succeed = false;
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Module/";
                return;
            }
            List<int> list = new List<int>();
            list.Add(data.ID);
            HandlerMessage = BLLFactory.Instance().SysModuleBLL.Remove(list);
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private SysModule GetInput()
        {
            SysModule data = new SysModule();
            data.ID = ConvertHelper.ToInt(Request["ModuleID"]);
            data.ParentID = ConvertHelper.ToInt(Request["ParentID"]);
            data.ModuleName = ConvertHelper.ToString(Request["ModuleName"]);
            data.DefaultUrl = ConvertHelper.ToString(Request["DefaultUrl"]);
            data.OrderID = ConvertHelper.ToInt(Request["OrderID"]);
            data.Remark = ConvertHelper.ToString(Request["Remark"]);
            data.IsActive = ConvertHelper.ToBoolean(Request["IsActive"]);
            return data;
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

        #endregion

    }

    #endregion    

    #region process generate ...

    /// <summary>
    /// 生成XML文件
    /// </summary>
    public class SysModule_Generate : ManageActionResult
    {
        #region members declaration
        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("root");
        private List<SysModule> list = new List<SysModule>();
        private List<SysFunction> list2 = new List<SysFunction>();
        static int _ModuleId = 0;
        #endregion

        /// <summary>
        /// 
        /// </summary>
        public SysModule_Generate(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            list = BLLFactory.Instance().SysModuleBLL.GetList();     // 获取模块表所有数据
            list2 = BLLFactory.Instance().SysFunctionBLL.GetList();    // 获取功能表所有数据

            list.Sort(SortByModuleId);

            _ModuleId = 0;
            List<SysModule> rootModuleList = list.FindAll(IsChildModule);

            foreach (SysModule rootModule in rootModuleList)
            {
                XmlNode mItem = _XmlDoc.CreateElement("module");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "ModuleID", rootModule.ID.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "ModuleName", rootModule.ModuleName);
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "DefaultUrl", rootModule.DefaultUrl.Replace("~", "").ToLower());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "IsActive", rootModule.IsActive.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "SubPermissionPage", "");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "SubPermissionName", "");
                _XmlDoc.DocumentElement.AppendChild(mItem);

                _ModuleId = rootModule.ID;

                if (list.Exists(IsChildModule))
                {
                    GenerateModule(mItem);
                }
            }

            // 需要赋予~/App_Data/config/ModuleData.xml的ASPNET写权限
            try
            {
                _XmlDoc.Save(Server.MapPath("~/App_Data/config/AdminModuleData.xml"));
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "文件更新成功";
                HandlerMessage.Succeed = true;
                return;
            }
            catch (Exception ex)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "文件~/App_Data/config/AdminModuleData.xml无写入权限，请确认!";
                HandlerMessage.Succeed = false;
                return;
            }
        }

        #region 生成xml节点
        /// <summary>
        /// module节点
        /// </summary>
        /// <param name="pNode"></param>
        private void GenerateModule(XmlNode pNode)
        {
            List<SysModule> childModuleList = list.FindAll(IsChildModule);

            foreach (SysModule childModule in childModuleList)
            {
                XmlNode mItem = _XmlDoc.CreateElement("module");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "ModuleID", childModule.ID.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "ModuleName", childModule.ModuleName);
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "DefaultUrl", childModule.DefaultUrl.Replace("~", "").ToLower());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "IsActive", childModule.IsActive.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "SubPermissionPage", "");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, mItem, "SubPermissionName", "");
                pNode.AppendChild(mItem);

                _ModuleId = childModule.ID;

                if (list2.Exists(FunctionOfTheModule))
                {
                    GenerateFunction(mItem, childModule);
                }
                if (list.Exists(IsChildModule))
                {
                    GenerateModule(mItem);
                }
            }
        }

        /// <summary>
        /// function节点
        /// </summary>
        /// <param name="pNode"></param>
        /// <param name="pModule"></param>
        private void GenerateFunction(XmlNode pNode, SysModule pModule)
        {
            List<SysFunction> fDataList = list2.FindAll(FunctionOfTheModule);

            foreach (SysFunction fData in fDataList)
            {
                XmlNode item = _XmlDoc.CreateElement("function");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "ModuleID", fData.ModuleID.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "ModuleName", pModule.ModuleName);
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "FunctionName", fData.FunctionName);
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "FunctionKey", fData.FunctionKey);
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "FunctionID", fData.ID.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "FunctionUrl", fData.FunctionUrl.Replace("~", "").ToLower());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "Value", fData.Value.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "AdminPage", fData.AdminPage.ToString());
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "SubPermissionPage", "");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, item, "SubPermissionName", "");
                pNode.AppendChild(item);
            }
        }
        #endregion

        #region 委托排序事件

        /// <summary>
        /// 判断是否存在子级模块
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool IsChildModule(SysModule data)
        {
            if (data.ParentID == _ModuleId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找指定ModuleId的模块
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool FunctionOfTheModule(SysFunction data)
        {
            if (data.ModuleID == _ModuleId)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 按照模块表的ModuleId排序
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        private static int SortByModuleId(SysModule x, SysModule y)
        {
            if (x == null)
            {
                if (y == null)
                {
                    // If x is null and y is null, they're
                    // equal. 
                    return 0;
                }
                else
                {
                    // If x is null and y is not null, y
                    // is greater. 
                    return -1;
                }
            }
            else
            {
                // If x is not null...
                //
                if (y == null)
                // ...and y is null, x is greater.
                {
                    return 1;
                }
                else
                {
                    // ...and y is not null, compare the 
                    // lengths of the two strings.
                    //
                    int retval = x.ID.CompareTo(y.ID);

                    return retval;

                }
            }

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

        #endregion
    }

    #endregion    

    #region process GetModuleTreeXMLWithoutFunction ...

    /// <summary>
    /// 获取XML，不带功能
    /// </summary>
    public class SysModule_GetModuleTreeXMLWithoutFunction : BaseManageActionResult
    {
        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("tree");
        /// <summary>
        /// 删除
        /// </summary>
        public SysModule_GetModuleTreeXMLWithoutFunction(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            XMLHelper.AddAttributeXMLNode(_XmlDoc, _XmlDoc.DocumentElement, "id", "0");

            BuildModuleXML(_XmlDoc.DocumentElement, 0);

            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(_XmlDoc.InnerXml);
            Response.End();
        }

        #region get tree
        private void BuildModuleXML(XmlNode APNode, int ARootID)
        {
            List<SysModule> datas = BLLFactory.Instance().SysModuleBLL.GetListByParentID(ARootID);  //取得子模块列表

            foreach (SysModule pModule in datas)  //循环子模块
            {
                XmlNode xItem = CreateModuleTreeItem(pModule);  //创建模块节点
                APNode.AppendChild(xItem); //叠加子节点 

                BuildModuleXML(xItem, pModule.ID);  //递归模块
            }
        }


        private XmlNode CreateModuleTreeItem(SysModule pModule)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", pModule.ModuleName);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", pModule.ID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", pModule.ModuleName.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", pModule.ParentID.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", pModule.DefaultUrl);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", pModule.Remark);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", pModule.IsActive.ToString());

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

            if (pModule.ParentID == 0)
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "home1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "home1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "home1.gif");
            }
            else
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "user1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "user1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "user1.gif");
            }

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


