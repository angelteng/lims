
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysRoleController(SysRoleController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-07-03
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System.Collections.Generic;
using System.Web.Mvc;

using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using Hope.WebBase;
using Hope.Util;
using System.Xml;
using System.Collections;
using System;
using NHibernate.Criterion;

namespace Hope.ITMS.Web
{
    #region 方法申明
    /// <summary>
    /// 
    /// </summary>
    public class RoleController : ManageController
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
            return new SysRole_List(this);
        }

        /// <summary>
        /// 显示添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            return New();
        }

        /// <summary>
        /// 显示添加
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {
            return new SysRole_New(this);
        }

        /// <summary>
        /// 显示添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit()
        {
            return Modify();
        }

        /// <summary>
        /// 显示修改页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify()
        {
            return new SysRole_Modify(this);
        }

        /// <summary>
        /// 权限分配
        /// </summary>
        /// <returns></returns>
        public ActionResult Permission()
        {
            return new SysRole_Permission(this);
        }

        /// <summary>
        /// 从数据库获取xml tree
        /// </summary>
        /// <returns></returns>
        public ActionResult GetPermissionTreeXML()
        {
            return new SysRole_GetPermissionTreeXML(this);
        }

        /// <summary>
        /// 权限保存
        /// </summary>
        /// <returns></returns>
        public ActionResult PermissionSave()
        {
            return new SysRole_PermissionSave(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new SysRole_Preview(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new SysRole_Save(this);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Remove()
        {
            return Delete();
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <returns></returns>
        public ActionResult Delete()
        {
            return new SysRole_Delete(this);
        }
    }
    #endregion

    #region SysRoleList ...

    /// <summary>
    /// Show SysRole List
    /// </summary>
    public class SysRole_List : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_List(ManageController controller)
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

            var list = BLLFactory.Instance().OrgGroupBLL.GetList();
            TemplateHelper.Put("Entitys", BLLFactory.Instance().OrgGroupBLL.GetList());
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        private void Show()
        {
            PagerData pager = GetPager();

            #region 非超级管理员无法查看
            List<SimpleExpression> exps = new List<SimpleExpression>();
            if (this.CurrentLoginAdmin.RoleID != 1)
            {
                exps.Add(NHibernate.Criterion.Expression.Gt("ID", 1));  //先把超级管理员排除
            }
            #endregion

            #region 获取排序
            OrderItemCollection orderlist = new OrderItemCollection();
            string orderColumn = RequestUtil.RequestString(Request, "OrderColumn", "ID");
            OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType), RequestUtil.RequestString(Request, "OrderType", "ASC"));
            OrderItem orderItem = new OrderItem(orderColumn, orderType);
            orderlist.Add(orderItem);
            #endregion

            #region 处理搜索
            if (!RequestUtil.IsNullOrEmpty(Request, "v"))
            {
                string keyword = CommonHelper.ReplaceBadChar(RequestUtil.RequestString(Request, "v", ""));
                bool isBlur = RequestUtil.RequestBoolean(Request, "IsBlur", false);
                string field = RequestUtil.RequestString(Request, "k", "Name");
                if (field == "ID")
                {
                    exps.Add(NHibernate.Criterion.Expression.Eq(field, ConvertHelper.ToInt(keyword)));
                }
                else
                {
                    if (isBlur)
                    {
                        exps.Add(NHibernate.Criterion.Expression.Like(field, keyword, MatchMode.Anywhere));
                    }
                    else
                    {
                        exps.Add(NHibernate.Criterion.Expression.Eq(field, keyword));
                    }
                }
            }
            #endregion

            List<SysRole> datas = BLLFactory.Instance().SysRoleBLL.GetList(pager, orderlist, exps);

            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }

        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission("RoleAdd"));
            TemplateHelper.Put("PermissionEdit", CheckPermission("RoleEdit"));
            TemplateHelper.Put("PermissionDelete", CheckPermission("RoleDelete"));
            TemplateHelper.Put("PermissionView", CheckPermission("RoleView"));
            TemplateHelper.Put("PermissionPermission", CheckPermission("RolePermission"));
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
                return "Manage\\Role\\Index.html";
            }
        }

        #endregion
    }
    #endregion

    #region SysRoleNew ...

    /// <summary>
    /// new SysRole
    /// </summary>
    public class SysRole_New : ManageActionResult
    {
        /// <summary>
        /// SysRole_New
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_New(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            TemplateHelper.Put("SavePermission", CheckPermission("RoleAdd"));
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
                return "Manage\\Role\\Add.html";
            }
        }

        #endregion
    }

    #endregion

    #region SysRoleEdit ...

    /// <summary>
    /// 
    /// EditSysRole
    /// </summary>
    public class SysRole_Modify : ManageActionResult
    {
        /// <summary>
        /// SysRole_Modify
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_Modify(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(id);
            if (data == null) { data = new SysRole(); }

            this.TemplateHelper.Put("SavePermission", CheckPermission("RoleEdit"));
            this.TemplateHelper.Put("Entity", data);
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
                return "Manage\\Role\\Edit.html";
            }
        }

        #endregion
    }

    #endregion

    #region SysRole_GetPermissionTreeXML ...

    /// <summary>
    /// Show SysPermission List
    /// </summary>
    public class SysRole_GetPermissionTreeXML : BaseManageActionResult
    {
        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("tree");
        static int _ModuleId;
        private int _roleID = -1;
        List<SysModule> list = new List<SysModule>();
        List<SysFunction> list2 = new List<SysFunction>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_GetPermissionTreeXML(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            _roleID = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(_roleID);
            if (data!=null)
            {
                BuildModuleXML();
            }            
            Response.ContentType = "text/xml";
            Response.ContentEncoding = System.Text.Encoding.UTF8;
            Response.Write(_XmlDoc.InnerXml);
            Response.End();
        }

        #region 生成xml
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public XmlDocument BuildModuleXML()
        {
            XMLHelper.AddAttributeXMLNode(_XmlDoc, _XmlDoc.DocumentElement, "id", "0");
            BuildModuleXML(_XmlDoc.DocumentElement);
            return _XmlDoc;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="APNode"></param>
        private void BuildModuleXML(XmlNode APNode)
        {
            list = BLLFactory.Instance().SysModuleBLL.GetList();
            list2 = BLLFactory.Instance().SysFunctionBLL.GetList();
            List<SysModule> root = list.FindAll(FindRootModule);

            foreach (SysModule data in root)
            {
                if (CheckPermission(data.ID))
                {
                    CreateNode(data, APNode);
                }                
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="APNode"></param>
        private void CreateNode(SysModule data, XmlNode APNode)
        {
            XmlNode node = CreateNodeItem(data);
            APNode.AppendChild(node);

            _ModuleId = data.ID;
            List<SysModule> child = list.FindAll(FindChildModule);

            foreach (SysModule d in child)
            {
                if (CheckPermission(d.ID))
                {
                    CreateNode(d, node);
                }
            }

            _ModuleId = data.ID;
            List<SysFunction> functions = list2.FindAll(FindFunction);

            foreach (SysFunction func in functions)
            {
                if (func != null && CheckPermission(func.ModuleID, func.Value))
                {
                    CreateFunctionNodeItem(func, node);
                }
            }
        }

        #region 创建节点        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pModule"></param>
        /// <returns></returns>
        private XmlNode CreateNodeItem(SysModule pModule)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", "M" + pModule.ID.ToString());
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", pModule.ModuleName);
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "1");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", pModule.DefaultUrl);
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", "1");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

            //判断是否默认选中
            if (IsNodeChecked(pModule.ID, 0))
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "checked", "True");
            }

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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data"></param>
        /// <param name="APNode"></param>
        private void CreateFunctionNodeItem(SysFunction data, XmlNode APNode)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", "F" + data.ID.ToString());
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", data.FunctionName);
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "2");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", data.FunctionUrl);
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", data.Value.ToString());
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

            //判断是否默认选中
            if (IsNodeChecked(data.ModuleID, data.Value))
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "checked", "True");
            }

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "set3.gif");

            APNode.AppendChild(xItem);
        }

        /// <summary>
        /// 判断修改的角色是否具有当前节点权限 如有则显示为选中
        /// </summary>
        /// <param name="moduleID">模块ID</param>
        /// <param name="funValue">功能值</param>
        /// <returns>True || False</returns>
        private bool IsNodeChecked(int moduleID,int funValue)
        {
            bool result = false;
            SysPermission data = BLLFactory.Instance().SysPermissionBLL.GetData(moduleID, _roleID);

            //funValue 表示当前节点为根模块
            if (funValue <= 0)
            {                
                if (data != null) { result = true; }
            }
            else
            {
                if (data != null && (funValue & data.FunctionValues)>0) { result = true; }
            }

            return result;
        }
        #endregion

        #endregion

        #region 委托事件
        /// <summary>
        /// 查找根模块
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool FindRootModule(SysModule data)
        {
            if (data.ParentID == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 查找子模块
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool FindChildModule(SysModule data)
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
        /// 查找模块下的功能
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private static bool FindFunction(SysFunction data)
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

    #region SysRolePermissionEdit ...

    /// <summary>
    /// SysRole_Permission
    /// </summary>
    public class SysRole_Permission : ManageActionResult
    {
        /// <summary>
        /// SysRole_Permission
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_Permission(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(id);
            if (data == null) { return; }

            this.TemplateHelper.Put("SavePermission", CheckPermission("PermissionPermission"));
            this.TemplateHelper.Put("Entity", data);
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
                return "Manage\\Role\\Permission.html";
            }
        }

        #endregion
    }

    #endregion

    #region process permission save ...

    /// <summary>
    /// 保存权限 修改
    /// </summary>
    public class SysRole_PermissionSave : ManageActionResult
    {
        string pValueStr = string.Empty;
        int _roleID = 0;
        List<int> _functionIDList = new List<int>();
        List<SysPermission> pList = new List<SysPermission>();
        Hashtable permissionTable = new Hashtable();     //待保存的权限值
        /// <summary>
        /// SysRole_Save
        /// </summary>
        public SysRole_PermissionSave(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            _roleID = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(_roleID);
            if (_roleID == 1)
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "超级管理员无需权限配置!";
                HandlerMessage.Succeed = true;
                return;
            }
            else if (data == null)
            {
                HandlerMessage.Code = "00";
                HandlerMessage.Text = "没有找到ID为 [" + _roleID.ToString() + "] 的角色!";
                HandlerMessage.Succeed = false;
                return;
            }

            //获取功能值
            pValueStr = RequestUtil.RequestString(Request, "pValues", string.Empty);
            string[] ids = pValueStr.Split(',');
            foreach (string idStr in ids)
            {
                if (idStr.Contains("F"))
                {
                    int fId = ConvertHelper.ToInt(idStr.Substring(1), -1);
                    if (fId>0 && !_functionIDList.Contains(fId))
                    {
                        _functionIDList.Add(fId);
                    }
                }
            }
            SavePermissions();
        }

        #region 保存权限设置      

        private void SavePermissions()
        {
            foreach (int functionID in _functionIDList)
            {
                if (functionID > 0)
                {
                    SysFunction functionData = BLLFactory.Instance().SysFunctionBLL.GetData(functionID);

                    addToPermissionList(functionData.ModuleID,functionData.Value);                    

                    SetParentModule(functionData.ModuleID);
                }
            }


            foreach (DictionaryEntry de in permissionTable)
            {
                int iValues = ConvertHelper.ToInt(de.Value, 0);
                int iModuleID = ConvertHelper.ToInt(de.Key, 0);

                if (iModuleID > 0)
                {
                    SysPermission data = new SysPermission();
                    data.ModuleID = iModuleID;
                    data.RoleID = _roleID;
                    data.FunctionValues = iValues;

                    if (!pList.Contains(data))
                    {
                        pList.Add(data);
                    }
                }
            }//foreach

            //先删除再添加
            BLLFactory.Instance().SysPermissionBLL.RemoveRole(_roleID);
            HandlerMessage = BLLFactory.Instance().SysPermissionBLL.BatchUpdateRolePValues(pList);
        }

        /// <summary>
        /// 将父级模块功能值设为1
        /// </summary>
        /// <param name="pModuleID"></param>
        private void SetParentModule(int pModuleID)
        {
            SysModule moduleData = BLLFactory.Instance().SysModuleBLL.GetData(pModuleID);
            int pID = moduleData.ParentID;
            if (pID == 0)
            {
                return;
            }

            moduleData = BLLFactory.Instance().SysModuleBLL.GetData(pID);
            permissionTable[moduleData.ID]=1;
            SetParentModule(moduleData.ID);
        }

        /// <summary>
        /// 将单条权限值加入到hashtable中，将mudule相同的值相加
        /// </summary>
        /// <param name="moduleID"></param>
        /// <param name="functionValues"></param>
        private void addToPermissionList(int moduleID, int functionValues)
        {
            Object temObj = permissionTable[moduleID];
            if (temObj != null)
            {
                int ifunValues = ConvertHelper.ToInt(temObj, 0);
                ifunValues += functionValues;
                permissionTable[moduleID] = ifunValues;
            }
            else
            {
                permissionTable[moduleID] = functionValues;
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

        /// <summary>
        /// 权限映射
        /// </summary>
        protected override string MappingURL
        {
            get
            {
                return "/Role/Permission/";
            }
        }
        #endregion
    }

    #endregion

    #region SysRole preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class SysRole_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysRole_Preview(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 页面执行
        /// </summary>
        protected override void Executing()
        {
            Show();
        }

        /// <summary>
        /// 显示
        /// </summary>
        private void Show()
        {
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(id);
            if (data == null)
            {
                data = new SysRole();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("PermissionEdit", CheckPermission("RoleEdit"));
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
                return "Manage\\Role\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class SysRole_Save : ManageActionResult
    {

        /// <summary>
        /// SysRole_Save
        /// </summary>
        public SysRole_Save(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            if (!CheckInput())
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，数据输入不正确";
                return;
            }
            ProcessSave();
        }

        /// <summary>
        /// 处理保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessSave()
        {
            SysRole data = GetInput();
            if (data.ID <= 0)
            {
                ProcessAdd(data);
            }
            else
            {
                ProcessEdit(data);
            }
        }

        /// <summary>
        /// 处理添加保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessAdd(SysRole data)
        {
            if (!CheckPermission("RoleAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().SysRoleBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(SysRole data)
        {
            if (!CheckPermission("RoleEdit"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().SysRoleBLL.Edit(data);
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private SysRole GetInput()
        {
            SysRole data = new SysRole();
            int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id > 0)
            {
                SysRole oData = BLLFactory.Instance().SysRoleBLL.GetData(id);
                if (oData != null)
                {
                    data = oData;
                }
            }
            if (Request["ID"] != null)
            {
                data.ID = ConvertHelper.ToInt(Request["ID"]);
            }
            if (Request["Name"] != null)
            {
                data.Name = ConvertHelper.ToString(Request["Name"]);
            }
            if (Request["CNName"] != null)
            {
                data.CNName = ConvertHelper.ToString(Request["CNName"]);
            }
            if (Request["Status"] != null)
            {
                data.Status = ConvertHelper.ToInt(Request["Status"]);
            }
            if (Request["Remark"] != null)
            {
                data.Remark = ConvertHelper.ToString(Request["Remark"]);
            }
            return data;
        }

        /// <summary>
        /// 验证输入数据
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            return true;
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
                return "/Role/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class SysRole_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public SysRole_Delete(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            if (!CheckDelete())
            {
                return;
            }
            ProcessDelete();
        }

        /// <summary>
        /// 检查删除
        /// </summary>
        /// <returns></returns>
        private bool CheckDelete()
        {
            if (string.IsNullOrEmpty(Request["ID"]))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "请选择需要删除的内容";
                HandlerMessage.Succeed = false;
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Role/";
                return false;
            }
            return true;
        }

        /// <summary>
        /// 删除处理
        /// </summary>
        private void ProcessDelete()
        {
            string sId = RequestUtil.RequestString(Request, "ID", "");
            List<int> iIds = CommonHelper.SplitString(sId);
            #region 超级管理员与系统管理员无法删除
            if (iIds.Contains(1))
            {
                iIds.Remove(1);
            }
            if (iIds.Contains(2))
            {
                iIds.Remove(2);
            }
            #endregion
            HandlerMessage = BLLFactory.Instance().SysRoleBLL.Remove(iIds);
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
}