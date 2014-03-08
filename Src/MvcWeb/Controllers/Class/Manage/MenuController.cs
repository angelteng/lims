
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysMenuController(SysMenuController)
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
    public class MenuController : ManageController
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
            return new SysMenu_List(this);
        }

        /// <summary>
        /// 生成对应的xml文件
        /// </summary>
        /// <returns></returns>
        [ValidateInput(false)]
        public ActionResult Generate()
        {
            return new SysMenu_Generate(this);
        }

        /// <summary>
        /// 从数据库获取xml tree
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuTreeXML()
        {
            return new SysMenu_GetMenuTreeXML(this);
        }

        /// <summary>
        /// 从已经生成的文件中获取xml tree
        /// </summary>
        /// <returns></returns>
        public ActionResult GetMenuTreeXML2()
        {
            return new SysMenu_GetMenuTreeXML2(this);
        }
    }

    #region SysMenu_List ...

    /// <summary>
    /// Show SysMenu List
    /// </summary>
    public class SysMenu_List : ManageActionResult
    {       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysMenu_List(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            Show();
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        private void Show()
        {
           
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
                return "Manage\\Menu\\Index.html";
            }
        }

        #endregion

    }

    #endregion  

    #region SysMenu_GetMenuTreeXML ...

    /// <summary>
    /// Show SysMenu List
    /// </summary>
    public class SysMenu_GetMenuTreeXML : BaseManageActionResult
    {
        XmlDocument _XmlDoc = XMLHelper.CreateXmlDoc("tree");
        static int _ModuleId;

        List<SysModule> list = new List<SysModule>();
        List<SysFunction> list2 = new List<SysFunction>();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysMenu_GetMenuTreeXML(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            BuildModuleXML();
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
                CreateNode(data, APNode);
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
                CreateNode(d, node);
            }

            _ModuleId = data.ID;
            List<SysFunction> functions = list2.FindAll(FindFunction);

            foreach (SysFunction func in functions)
            {
                CreateFunctionNodeItem(func, node);
            }
        }

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

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");

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

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "set3.gif");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "set3.gif");

            APNode.AppendChild(xItem);
        }
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

    #region SysMenu_GetMenuTreeXML2 ...

    /// <summary>
    /// Show SysMenu List
    /// </summary>
    public class SysMenu_GetMenuTreeXML2 : BaseManageActionResult
    {
        XmlDocument _XmlDoc = new XmlDocument();
        XmlDocument _XmlDocLoaded = new XmlDocument();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysMenu_GetMenuTreeXML2(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            BuildXML();
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
        public void BuildXML()
        {
            _XmlDoc = XMLHelper.CreateXmlDoc("tree");
            XMLHelper.AddAttributeXMLNode(_XmlDoc, _XmlDoc.DocumentElement, "id", "0");

            _XmlDocLoaded.Load(Server.MapPath("~/App_Data/Config/Menu.xml"));

            foreach (XmlNode node in _XmlDocLoaded.DocumentElement.ChildNodes)
            {
                XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");
                _XmlDoc.DocumentElement.AppendChild(xItem);

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", node.Attributes["id"].Value);

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", node.Attributes["name"].Value);

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "2");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", node.Attributes["url"].Value);

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", "");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "home1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "home1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "home1.gif");

                foreach (XmlNode childNode in node.ChildNodes)
                {
                    GenerateTreeNodes(childNode, xItem);
                }
            }
        }

        /// <summary>
        /// 将加载的Xml写入到新建的Xml中
        /// </summary>
        /// <param name="node">加载的Xml片段</param>
        /// <param name="pNode">新建的Xml片段父节点</param>
        private void GenerateTreeNodes(XmlNode node, XmlNode pNode)
        {
            XmlNode xItem = _XmlDoc.CreateNode("element", "item", "");
            pNode.AppendChild(xItem);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "id", node.Attributes["id"].Value);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "text", node.Attributes["name"].Value);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "open", "1");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "nodetype", "2");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data1", node.Attributes["url"].Value);

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data2", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data3", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data4", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data5", "");

            XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "data6", "");

            if (node.Attributes["id"].Value.Contains("M"))
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "user1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "user1.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "user1.gif");
            }
            else if (node.Attributes["id"].Value.Contains("F"))
            {
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im0", "set3.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im1", "set3.gif");
                XMLHelper.AddAttributeXMLNode(_XmlDoc, xItem, "im2", "set3.gif");
            }

            foreach (XmlNode childNode in node.ChildNodes)
            {
                GenerateTreeNodes(childNode, xItem);
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
    
    #region process generate ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class SysMenu_Generate : ManageActionResult
    { 
        /// <summary>
        /// 
        /// </summary>
        public SysMenu_Generate(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            string xmlText = RequestUtil.RequestString(Request,"xmlStr","");
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlText);
            string fileName = string.Format("~/App_Data/config/Menu.xml",
                DateTime.Now.ToString("yyyyMMdd"),
                DateTime.Now.ToString("hhmmss"));

            // 需要赋予~/App_Data/config/MenuData.xml的ASPNET写权限
            try
            {
                doc.Save(Server.MapPath(fileName));
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "文件更新成功";
                HandlerMessage.Succeed = true;
                return;
            }
            catch (Exception ex)
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "文件~/App_Data/config/Menu.xml无写入权限，请确认!";
                HandlerMessage.Succeed = false;
                return;
            }
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


