using System;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Collections;
using System.Text.RegularExpressions;

using Hope.Util;
using Hope.ITMS.Model;
using Hope.ITMS.BLL;
using Hope.PermissionHandler;
using Hope.WebBase;
using Hope.ITMS.Enums;
using Hope.TemplateUtil;
using System.Collections.Generic;

namespace Hope.ITMS.Web
{
    #region 操作方法基类    
    /// <summary>
    /// 
    /// </summary>
    public class BaseActionResult : ActionResult
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="controller"></param>
        public BaseActionResult(BaseController controller)
        {
            _Controller = controller;
            this.Initialize();
            this.CompleteInitialize();
            this.Executing();
            this.Executed();
            this.Finish();
        }

        #region properties ...

        #region server ...

        private BaseController _Controller;
        /// <summary>
        /// 
        /// </summary>
        protected BaseController Controller
        {
            get { return _Controller; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected HttpResponse Response
        {
            get { return HttpContext.Current.Response; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected HttpRequest Request
        {
            get { return HttpContext.Current.Request; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected HttpServerUtility Server
        {
            get { return HttpContext.Current.Server; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected System.Web.SessionState.HttpSessionState Session
        {
            get { return HttpContext.Current.Session; }
        }

        #endregion

        #region Application       
        private string _ApplicationUrl;
        /// <summary>
        /// 当前使用地址
        /// </summary>
        protected string ApplicationUrl
        {
            get { return _ApplicationUrl; }
        }

        private SystemMessage _HandlerMessage = new SystemMessage();
        /// <summary>
        /// 处理消息
        /// </summary>
        protected SystemMessage HandlerMessage
        {
            get { return _HandlerMessage; }
            set { _HandlerMessage = value; }
        }

        private static Hashtable _ModulePermissionTable;
        /// <summary>
        /// 权限表
        /// </summary>
        public static Hashtable ModulePermissionTable
        {
            set
            {
                _ModulePermissionTable = value;
                HttpContext.Current.Session["_ModulePermissionTable"] = _ModulePermissionTable;
            }
            get
            {
                if (_ModulePermissionTable == null || _ModulePermissionTable.Count == 0)
                {
                    _ModulePermissionTable = (Hashtable)HttpContext.Current.Session["_ModulePermissionTable"];
                }
                return _ModulePermissionTable;
            }
        }
        #endregion

        #region template ...

        /// <summary>
        /// 模板文件名称
        /// </summary>
        protected virtual string TemplatePath
        {
            get { return ""; }
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual PageMode CurrentPageMode
        {
            get { return PageMode.Html; }
        }

        private VelocityHelper _TemplateHelper;
        /// <summary>
        /// 模板应用助手
        /// </summary>
        protected VelocityHelper TemplateHelper
        {
            get { return _TemplateHelper; }
        }

        #endregion

        #endregion

        #region protected methods ...
        /// <summary>
        /// 初始化
        /// </summary>
        private void Initialize()
        {
            _TemplateHelper = VelocityHelper.Instance();
            _ApplicationUrl = AppConfig.WebMainPathURL.TrimEnd('/') + "/";
        }

        /// <summary>
        /// 初始化完成
        /// </summary>
        protected virtual void CompleteInitialize()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ExecuteResult(ControllerContext context)
        {

        }

        /// <summary>
        /// 执行
        /// </summary>
        protected virtual void Executing()
        {
           
        }

        /// <summary>
        /// 执行后
        /// </summary>
        protected virtual void Executed()
        {

        }

        /// <summary>
        /// 完成
        /// </summary>
        private void Finish()
        {
            if (this.CurrentPageMode == PageMode.Image)
                return;
            else if (this.CurrentPageMode == PageMode.Text && this.TemplatePath == "")
            {
                OutputText();
                return;
            }
            else if (this.CurrentPageMode == PageMode.Xml && this.TemplatePath == "")
            {
                OutputXml();
                return;
            }
            this.TemplateHelper.Put("Message", this.HandlerMessage);
            this.TemplateHelper.GetResponsePageHtml(TemplatePath);
        }

        #region output message ...

        /// <summary>
        /// 输出JSON格式数据
        /// </summary>
        private void OutputText()
        {
            Response.ContentEncoding = Encoding.UTF8;
            Response.ContentType = "text/plain";
            Response.Write(HandlerMessage.ToJSON());
            Response.End();
        }

        /// <summary>
        /// 输出XML格式数据
        /// </summary>
        private void OutputXml()
        {
            XmlDocument xml = XMLHelper.CreateXmlDoc();
            xml.DocumentElement.AppendChild(HandlerMessage.ToXmlNode(xml));
            OutputXml(xml);
        }

        /// <summary>
        /// 输出XML格式数据
        /// </summary>
        /// <param name="xml"></param>
        private void OutputXml(XmlDocument xml)
        {
            Response.ContentType = "text/xml";
            Response.ContentEncoding = Encoding.UTF8;
            //Response.Write("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            Response.Write(xml.InnerXml);
            Response.End();
        }

        #endregion

        #region get pager ...

        /// <summary>
        /// 获取分页信息
        /// </summary>
        /// <returns></returns>
        protected PagerData GetPager()
        {
            PagerData pager = new PagerData();
            pager.CurrentPage = ConvertHelper.ToInt(Request["Page"]);
            pager.PageSize = ConvertHelper.ToInt(Request["PageSize"], 20);

            return pager;
        }
        
        /// <summary>
        /// 获取分页网址
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        protected string GetPageUrl(PagerData pager)
        {
            string strQuery = Request.Url.Query;
            string strPrefix = Request.FilePath; //Context.Request.Url.ToString();
            if (strQuery != string.Empty)
            {
                //strPrefix = strPrefix.Replace(strQuery, "");
                strQuery = strQuery.Replace("?", "&");
            }
            if (HttpContext.Current.Request["Page"] != null)
            {
                strQuery = strQuery.Replace(string.Format("&{0}={1}", "Page", Request["Page"]), "");
            }
            if (HttpContext.Current.Request["PageSize"] != null)
            {
                strQuery = strQuery.Replace(string.Format("&{0}={1}", "PageSize",  Request["PageSize"]), "");
            }

            return string.Format("{0}/?{1}{2}{3}", strPrefix, "Page=***", "&PageSize=***", strQuery);
        }

        #endregion

        #endregion
    }
    #endregion

    #region ManageActionResult 检查管理员权限
    /// <summary>
    /// 
    /// </summary>
    public class ManageActionResult : BaseActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public ManageActionResult(ManageController controller) : base(controller)
        {

        }

        /// <summary>
        /// 初始化完成
        /// </summary>
        protected override void CompleteInitialize()
        {
            base.CompleteInitialize();
            CheckLogin();
            InitPageInformation();
            CheckPagePermission();
        }

        /// <summary>
        /// 判断权限
        /// </summary>
        protected void CheckPagePermission()
        {
            if (!CheckPermission(_ModuleID, _FunctionValue))
            {
                //写入日志
                BLLFactory.Instance().SysLogBLL.AddLog("越权操作", "越权操作", "", "", (int)LogPriority.Low, (int)LogType.AccessFailed);

                Response.Redirect("~/Error/E403");     /// 无权限访问
                Response.End();
            }
        }

        #region check login ...

        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        private bool CheckLogin()
        {
            if (Session["_AdminInfo"] == null)
            {
                string targetUrl = "~/Login/";
                Response.Redirect(targetUrl);
                return false;
            }
            if (_CurrentLoginAdmin == null)
            {
                _CurrentLoginAdmin = (SysAdmin)HttpContext.Current.Session["_AdminInfo"];
                _bSuperAdmin = (bool)Session["_SuperRole"];
            }            
            return true;
        }

        #endregion

        #region check permission ...

        /// <summary>
        /// 初始化页面信息，包括所属模块、功能，以及功能值
        /// </summary>
        private void InitPageInformation()
        {
            string sCurrentPUrl = "";

            //string sUrl = Request.Url.ToString();                        
            string sQuery = Request.Url.Query.ToString();

            string sFunctionUrl = Request.CurrentExecutionFilePath;
            //如果采用虚拟目录，则替换虚拟目录为空
            if (!Request.ApplicationPath.Equals("/"))
            {
                sFunctionUrl = Regex.Replace(sFunctionUrl, Request.ApplicationPath, "", RegexOptions.IgnoreCase);
            }
            
            //判断是否已/开头
            //（约定"sFunctionUrl"或"DefaultUrl"必须以/开头，但是网站配置可以不用/结尾）
            if (!sFunctionUrl.StartsWith("/"))
            {
                sFunctionUrl = "/" + sFunctionUrl;
            }

            // 如果Url参数不为空，则将参数置为空
            if (!string.IsNullOrEmpty(sQuery))
            {
                sFunctionUrl = sFunctionUrl.Replace(sQuery, "");
            }

            //去除Route参数
            if (Controller.RouteData.Values["ID"].ToString().Length > 0)
            {
                sFunctionUrl = sFunctionUrl.Replace(Controller.RouteData.Values["ID"].ToString(), "");   
            }

            //获取映射页面地址
            if (string.IsNullOrEmpty(MappingURL))
            {
                sCurrentPUrl = sFunctionUrl;
            }
            else
            {
                sCurrentPUrl = MappingURL;
            }

            //全部统一为小写
            sCurrentPUrl = sCurrentPUrl.ToLower();
            //统一以 / 结束
            if (!sCurrentPUrl.EndsWith("/"))
            {
                sCurrentPUrl += "/";
            }

            ModuleFunctionHandler mfHandler = new ModuleFunctionHandler(Server.MapPath("~/App_Data/config/AdminModuleData.xml"));

            ModuleFunctionData mfData = mfHandler.GetMFDataByURL(sCurrentPUrl);

            if (mfData != null)
            {
                _ModuleID = mfData.ModuleID;
                _FunctionID = mfData.FunctionID;
                _FunctionValue = mfData.Value;

                //SysFunction temFunction = BLLFactory.Instance().SysFunctionBLL.GetData(mfData.FunctionID);
                //if (temFunction != null)
                //{
                   //Page.Title = temFunction.FunctionName;
                //}
            }
            else
            {
                //写入日志
               BLLFactory.Instance().SysLogBLL.AddLog("越权操作", "越权操作", "", "", (int)LogPriority.Low, (int)LogType.AccessFailed);

                Response.Redirect("~/Error/E403");                   /// 无权限访问
            }
        }

        /// <summary>
        /// 验证模块权限
        /// </summary>
        /// <returns>True | False</returns>
        public bool CheckPermission(int moduleID)
        {
            //如果是超级管理员的话不检查页面权限
            //bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin)
            {
                return true;
            }

            bool bResult = false;
            //从Hashtable中取得模块的权限值
            try
            {
                int iFunctionValues = 0;

                if (ModulePermissionTable != null && ModulePermissionTable.ContainsKey(moduleID))
                {
                    iFunctionValues = (int)ModulePermissionTable[moduleID];
                }

                bResult = iFunctionValues > 0; //判断时候有权限和有功能，一般最后一级模块才会有功能

            }
            catch (Exception ex)
            {
                LogUtil.error("无此模块权限！" + ex.Message);
            }

            return bResult;
        }

        /// <summary>
        /// 验证操作权限
        /// </summary>
        /// <param name="module">功能模块</param>
        /// <param name="purview">权限值</param>
        /// <returns></returns>
        protected bool CheckPermission(int moduleID, object function)
        {
            bool bResult = true;

            //如果是超级管理员的话不检查页面权限
            //bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin)
            {
                return true;
            }

            if (moduleID != 0)
            {
                int ifunction = (int)function;
                int iFunctionValues = 0;

                if (ModulePermissionTable != null && ModulePermissionTable.ContainsKey(moduleID))
                {
                    iFunctionValues = (int)ModulePermissionTable[moduleID];
                }

                bResult = (ifunction & iFunctionValues) > 0;
            }

            return bResult;
        }

        /// <summary>
        /// 验证操作权限
        /// </summary>
        /// <param name="functionKey">功能名称</param>
        /// <returns></returns>
        protected bool CheckPermission(string functionKey)
        {
            //如果是超级管理员的话不检查页面权限
            bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin){ return true;}

            ModuleFunctionHandler mfHandler = new ModuleFunctionHandler(Server.MapPath("~/App_Data/config/AdminModuleData.xml"));
            ModuleFunctionData mfData = mfHandler.GetMFDataByKey(functionKey);

            if (mfData != null)
            {
                return CheckPermission(mfData.ModuleID, mfData.Value);                
            }
            else
            {
                return false;
            }
        }

        #endregion

        #region properties ...

        private int _ModuleID = -1;
        private int _FunctionID = -1;
        private int _FunctionValue = 0;

        /// <summary>
        /// 在两个页面需要是同一个权限值的时候，但是URL的地址不一样，我们目前的情况必须是每个页面做一个权限值，
        /// 当需要处理这种情况的话就改写MappingURL属性，改为对应有权限的URL地址。
        /// </summary>
        protected virtual string MappingURL
        {
            get
            {
                return "";
            }
        }

        private SysAdmin _CurrentLoginAdmin;
        /// <summary>
        /// 登录用户
        /// </summary>
        public SysAdmin CurrentLoginAdmin
        {
            get { return _CurrentLoginAdmin; }
        }

        private bool _bSuperAdmin = false;
        /// <summary>
        /// 当前管理员是否超级管理员
        /// </summary>
        public bool bSuperAdmin
        {
            get { return _bSuperAdmin; }
        }


        #endregion

    }
    #endregion

    #region ManageActionResult 不检查管理员权限，只验证登录
    /// <summary>
    /// 
    /// </summary>
    public class BaseManageActionResult : BaseActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public BaseManageActionResult(ManageController controller)
            : base(controller)
        {

        }
        
        protected override void CompleteInitialize()
        {
            base.CompleteInitialize();
            CheckLogin();
        }

        #region check login ...
        /// <summary>
        /// 验证登录
        /// </summary>
        /// <returns></returns>
        private bool CheckLogin()
        {
            if (Session["_AdminInfo"] == null)
            {
                string targetUrl = "~/Login/";
                Response.Redirect(targetUrl);
                return false;
            }
            if (_CurrentLoginAdmin == null)
            {
                _CurrentLoginAdmin = (SysAdmin)HttpContext.Current.Session["_AdminInfo"];
                _bSuperAdmin = (bool)Session["_SuperRole"];
            }
            return true;
        }
        #endregion 

        #region 检查权限的方法
        public bool CheckPermission(int moduleID)
        {
            //如果是超级管理员的话不检查页面权限
            //bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin)
            {
                return true;
            }

            bool bResult = false;
            //从Hashtable中取得模块的权限值
            try
            {
                int iFunctionValues = 0;

                if (ModulePermissionTable != null && ModulePermissionTable.ContainsKey(moduleID))
                {
                    iFunctionValues = (int)ModulePermissionTable[moduleID];
                }

                bResult = iFunctionValues > 0; //判断时候有权限和有功能，一般最后一级模块才会有功能
            }
            catch (Exception ex)
            {
                LogUtil.error("无此模块权限！" + ex.Message);
            }

            return bResult;
        }

        /// <summary>
        /// 验证操作权限
        /// </summary>
        /// <param name="functionKey">功能名称</param>
        /// <returns></returns>
        protected bool CheckPermission(string functionKey)
        {
            //如果是超级管理员的话不检查页面权限
            //bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin) { return true; }

            ModuleFunctionHandler mfHandler = new ModuleFunctionHandler(Server.MapPath("~/App_Data/config/AdminModuleData.xml"));
            ModuleFunctionData mfData = mfHandler.GetMFDataByKey(functionKey);

            if (mfData != null)
            {
                return CheckPermission(mfData.ModuleID, mfData.Value);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 验证操作权限
        /// </summary>
        /// <param name="module">功能模块</param>
        /// <param name="purview">权限值</param>
        /// <returns></returns>
        protected bool CheckPermission(int moduleID, object function)
        {
            bool bResult = true;

            //如果是超级管理员的话不检查页面权限
            //bool bSuperAdmin = BLLFactory.Instance().SysRoleBLL.IsSuperRole(this.CurrentLoginAdmin.RoleID);

            if (_bSuperAdmin)
            {
                return true;
            }

            if (moduleID != 0)
            {
                int ifunction = (int)function;
                int iFunctionValues = 0;

                if (ModulePermissionTable != null && ModulePermissionTable.ContainsKey(moduleID))
                {
                    iFunctionValues = (int)ModulePermissionTable[moduleID];
                }

                bResult = (ifunction & iFunctionValues) > 0;
            }

            return bResult;
        }
        #endregion

        #region properties ...
        private SysAdmin _CurrentLoginAdmin;
        /// <summary>
        /// 登录用户
        /// </summary>
        public SysAdmin CurrentLoginAdmin
        {
            get { return _CurrentLoginAdmin; }
        }

        private bool _bSuperAdmin = false;
        /// <summary>
        /// 当前管理员是否超级管理员
        /// </summary>
        public bool bSuperAdmin
        {
            get { return _bSuperAdmin; }
        }


        #endregion
    }
    #endregion    

    #region PageActionResult 不检查管理员权限，不验证登录
    /// <summary>
    /// 
    /// </summary>
    public class PageActionResult : BaseActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public PageActionResult(BaseController controller)
            : base(controller)
        {

        }

        protected override void CompleteInitialize()
        {
            base.CompleteInitialize();
        }
    }
    #endregion    
}
