using System.Xml;
using System.Web.Mvc;
using NHibernate.Criterion;
using System.Collections.Generic;

using Hope.WebBase;
using Hope.Util;
using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using System;

namespace Hope.ITMS.Web
{
    /// <summary>
    /// 系统首页
    /// </summary>
    public class IndexController : ManageController
    {
        /// <summary>
        /// 首页－框架页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return new Index_Index(this);
        }

        /// <summary>
        /// 首页－主体页面
        /// </summary>
        /// <returns></returns>
        //public ActionResult Main()
        //{
        //    return new Index_Main(this);
        //}
    }

    #region Index index ...

    /// <summary>
    /// 管理界面首页
    /// </summary>
    public class Index_Index : PageActionResult
    {
        private XmlDocument _XmlDoc = null;
        private XmlDocument _MenuXmlDoc = null;
        private int mid = 0;
        private int fid = 0;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Index_Index(ManageController controller): base(controller)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            //this.TemplateHelper.Put("LoginInfo", string.Format("您的用户名：{0}&nbsp;&nbsp;您的身份：{1}", CurrentLoginAdmin.Name, GetRoleName(CurrentLoginAdmin.RoleID)));
            //this.TemplateHelper.Put("PermissionSelfEdit", CheckPermission("AdminPersonalInfoEdit")); 

            #region load menu
            _XmlDoc = XMLHelper.CreateXmlDoc();
            _MenuXmlDoc = new XmlDocument();
            _MenuXmlDoc.Load(CommonHelper.GetWebFullPath() + "/App_Data/Config/Menu.xml");
            BuildNode(_MenuXmlDoc.DocumentElement, _XmlDoc.DocumentElement);

            XmlNode rootNode = _XmlDoc.DocumentElement;

            // 选择当前选中的模块，根据不同模块而初始化不同的菜单，默认为第一个模块
            MakeHtml makeHtml = new MakeHtml(new MakeMenu(rootNode));

            this.TemplateHelper.Put("AdminNav", makeHtml.GetHtml());
            #endregion...
        }

        #region menu
        /// <summary>
        /// 构建菜单
        /// </summary>
        /// <param name="menuXmlPNode"></param>
        /// <param name="xmlPNode"></param>
        private void BuildNode(XmlNode menuXmlPNode, XmlNode xmlPNode)
        {
            foreach (XmlNode menuXmlNode in menuXmlPNode.ChildNodes)
            {
                string sid = menuXmlNode.Attributes["id"].Value;
                XmlNode xItem = null;

                if (sid.Contains("M"))
                {
                    mid = ConvertHelper.ToInt(sid.Replace("M", ""));
                    //if (CheckPermission(mid))
                    //{
                        xItem = CreateItem(menuXmlNode);
                        xmlPNode.AppendChild(xItem);
                    //}
                }
                else if (sid.Contains("F"))
                {
                    fid = ConvertHelper.ToInt(sid.Replace("F", ""));

                    SysFunction func = BLLFactory.Instance().SysFunctionBLL.GetData(fid);
                    //if (func != null && CheckPermission(func.ModuleID, func.Value))
                    //{
                        xItem = CreateItem(menuXmlNode);
                        xmlPNode.AppendChild(xItem);
                    //}
                }

                if (menuXmlNode.HasChildNodes && xItem != null)
                {
                    BuildNode(menuXmlNode, xItem);
                }
            }
        }

        /// <summary>
        /// 创建菜单节点
        /// </summary>
        /// <param name="menuXmlPNode"></param>
        /// <returns></returns>
        private XmlNode CreateItem(XmlNode menuXmlPNode)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "node", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", menuXmlPNode.Attributes["id"].Value);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "name", menuXmlPNode.Attributes["name"].Value);

            string url = "";
            if (string.IsNullOrEmpty(menuXmlPNode.Attributes["url"].Value))
            {
                url = "javascript:void(0);";
            }
            else
            {
                url = AppConfig.WebMainPathURL + menuXmlPNode.Attributes["url"].Value.Replace("~/", "");
            }

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "url", url);

            return xItem;
        }
        #endregion

        /// <summary>
        /// 获得当前登录管理员的角色名
        /// </summary>
        /// <param name="roleID">管理员角色ID</param>
        /// <returns>当前登录管理员角色名</returns>
        private string GetRoleName(int roleID)
        {
            SysRole data = BLLFactory.Instance().SysRoleBLL.GetData(roleID);
            if (data != null)
            {
                return data.CNName;
            }
            else
            {
                return "未分配";
            }
        }

        #region override ...

        /// <summary>
        /// 页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Index\\index.html";
            }
        }

        #endregion

    }

    #endregion    

    #region Index main ...

    /// <summary>
    /// 管理界面首页
    /// </summary>
    public class Index_Main : BaseManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Index_Main(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        protected override void Executing()
        {
            //LoadMsg();
        }

        /// <summary>
        /// 加载未读短消息
        /// </summary>
        //private void LoadMsg()
        //{
        //    List<SimpleExpression> exps = new List<SimpleExpression>();
        //    exps.Add(NHibernate.Criterion.Expression.Eq("Receiver", CurrentLoginAdmin.ID));
        //    exps.Add(new SimpleExpression("Status", (int)MessageStatus.UnRead, "="));            

        //    List<SysMessage> datas = BLLFactory.Instance().SysMessageBLL.GetList(exps);

        //    this.TemplateHelper.Put("MessageEntityList", datas);
        //}

        #region override ...

        /// <summary>
        /// 页面模式
        /// </summary>
        protected override PageMode CurrentPageMode
        {
            get
            {
                return PageMode.Html;
            }
        }

        /// <summary>
        /// 模板路径
        /// </summary>
        protected override string TemplatePath
        {
            get
            {
                return "Manage\\Index\\main.html";
            }
        }

        #endregion

    }

    #endregion    
}
