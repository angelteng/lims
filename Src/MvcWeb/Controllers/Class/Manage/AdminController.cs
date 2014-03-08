
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysAdminController(SysAdminController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-06-26
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
using NHibernate.Criterion;

namespace Hope.ITMS.Web
{
    #region 方法定义    
    /// <summary>
    /// 
    /// </summary>
    public class AdminController : ManageController
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
            return new SysAdmin_List(this);
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult New()
        {

            return new SysAdmin_New(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new SysAdmin_Preview(this);
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Modify()
        {
            return new SysAdmin_Modify(this);
        }

        /// <summary>
        /// 编辑个人信息
        /// </summary>
        /// <returns></returns>
        public ActionResult SelfEdit()
        {
            return new SysAdmin_PersonalInfoEdit(this);
        }

        /// <summary>
        /// 角色分配
        /// </summary>
        /// <returns></returns>
        public ActionResult Role()
        {
            return new SysAdmin_RoleEdit(this);
        }

        /// <summary>
        /// 保存数据
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new SysAdmin_Save(this);
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
            return new SysAdmin_Delete(this);
        }
    }
    #endregion

    #region SysAdminList ...

    /// <summary>
    /// Show SysAdmin List
    /// </summary>
    public class SysAdmin_List : ManageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_List(ManageController controller)
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
            PagerData pager = GetPager();

            #region 非超级管理员无法查看
            List<SimpleExpression> exps = new List<SimpleExpression>();
            if (this.CurrentLoginAdmin.RoleID != 1)
            {
                exps.Add(NHibernate.Criterion.Expression.Gt("RoleID", 1));  //先把超级管理员排除
            }
            #endregion

            #region 获取排序
            OrderItemCollection orderlist = new OrderItemCollection();
            string orderColumn = RequestUtil.RequestString(Request, "OrderColumn", "ID");
            OrderType orderType = (OrderType)Enum.Parse(typeof(OrderType), RequestUtil.RequestString(Request, "OrderType", "ASC"));
            OrderItem orderItem = new OrderItem(orderColumn,orderType);
            orderlist.Add(orderItem);
            #endregion
            
            #region 处理搜索
            if (!RequestUtil.IsNullOrEmpty(Request, "v"))
            { 
                string keyword = CommonHelper.ReplaceBadChar(RequestUtil.RequestString(Request,"v",""));
                bool isBlur = RequestUtil.RequestBoolean(Request, "IsBlur", false);
                string field = RequestUtil.RequestString(Request, "k", "Name");
                if (field == "ID")
                {
                    exps.Add(NHibernate.Criterion.Restrictions.Eq(field, ConvertHelper.ToInt(keyword)));
                }
                else
                {
                    if (isBlur)
                    {
                        exps.Add(NHibernate.Criterion.Restrictions.Like(field, keyword, MatchMode.Anywhere));
                    }
                    else
                    {
                        exps.Add(NHibernate.Criterion.Restrictions.Eq(field, keyword));
                    }
                }
            }
            #endregion

            List<SysAdmin> datas = BLLFactory.Instance().SysAdminBLL.GetList(pager, orderlist, exps);

            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }

        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission("AdminAdd"));
            TemplateHelper.Put("PermissionEdit", CheckPermission("AdminEdit"));
            TemplateHelper.Put("PermissionDelete", CheckPermission("AdminDelete"));
            TemplateHelper.Put("PermissionView", CheckPermission("AdminView"));
            TemplateHelper.Put("PermissionRoleEdit", CheckPermission("AdminRoleEdit"));            
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
                return "Manage\\Admin\\Index.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysAdminNew ...

    /// <summary>
    /// new SysAdmin
    /// </summary>
    public class SysAdmin_New : ManageActionResult
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_New(ManageController controller) : base(controller)
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
        /// 显示数据
        /// </summary>
        private void Show()
        {
            SysAdmin data = new SysAdmin();
            this.TemplateHelper.Put("SavePermission", CheckPermission("AdminAdd"));                
           
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
                return "Manage\\Admin\\add.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysAdminEdit ...

    /// <summary>
    /// Modify SysAdmin
    /// </summary>
    public class SysAdmin_Modify : ManageActionResult
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_Modify(ManageController controller): base(controller)
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
        /// 显示数据
        /// </summary>
        private void Show()
        {
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(id);

            this.TemplateHelper.Put("SavePermission", CheckPermission("AdminEdit"));
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
                return "Manage\\Admin\\Edit.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysAdmin_PersonalInfoEdit ...

    /// <summary>
    /// Modify Admin PersonalInfoEdit
    /// </summary>
    public class SysAdmin_PersonalInfoEdit : BaseManageActionResult
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_PersonalInfoEdit(ManageController controller)
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
        /// 显示数据
        /// </summary>
        private void Show()
        {
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(CurrentLoginAdmin.ID);

            //this.TemplateHelper.Put("SavePermission", CheckPermission("AdminPersonalInfoEdit"));
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
                return "Manage\\Admin\\SelfEdit.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysAdmimRoleEdit ...

    /// <summary>
    /// SysAdmin RoleEdit
    /// </summary>
    public class SysAdmin_RoleEdit : ManageActionResult
    {
        /// <summary>
        /// constructor
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_RoleEdit(ManageController controller)
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
        /// 显示数据
        /// </summary>
        private void Show()
        {
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(id);

            this.TemplateHelper.Put("SavePermission", CheckPermission("AdminEdit"));
            this.TemplateHelper.Put("Entity", data);

            List<SysRole> rolelist = BLLFactory.Instance().SysRoleBLL.GetList(this.CurrentLoginAdmin.RoleID);
            this.TemplateHelper.Put("rolelist", rolelist);
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
                return "Manage\\Admin\\roleEdit.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysAdmin preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class SysAdmin_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysAdmin_Preview(ManageController controller)
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
            SysAdmin data = BLLFactory.Instance().SysAdminBLL.GetData(id);
            if (data == null)
            {
                data = new SysAdmin();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("SavePermission",CheckPermission("AdminEdit"));
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
                return "Manage\\Admin\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class SysAdmin_Save : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        public SysAdmin_Save(ManageController controller)
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
            SysAdmin data = GetInput();
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
        private void ProcessAdd(SysAdmin data)
        {
            if (!CheckPermission("AdminAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().SysAdminBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(SysAdmin data)
        {
            string action = RequestUtil.RequestString(Request, "Action", "");
            string funName = "AdminEdit";
            if (action == "Self")
            {
                funName = "AdminSelfEdit";
            }

            if (!CheckPermission(funName))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                return;
            }            
            HandlerMessage = BLLFactory.Instance().SysAdminBLL.Edit(data);
            if (action == "Self")
            {
                HandlerMessage.TargetUrl = ApplicationUrl + "Home/Main/";
            }
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private SysAdmin GetInput()
        {
            SysAdmin data = new SysAdmin();
            int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id>0)
            {
                SysAdmin oData = BLLFactory.Instance().SysAdminBLL.GetData(id);
                if (oData!=null)
                {
                    data = oData;
                }
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "RoleID"))
            {
                data.RoleID = ConvertHelper.ToInt(Request["RoleID"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Name"))
            {
                data.Name = ConvertHelper.ToString(Request["Name"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Password"))
            {
                data.Password = EncryptionUtil.MD5Hash(ConvertHelper.ToString(Request["Password"]));
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "RealName"))
            {
                data.RealName = ConvertHelper.ToString(Request["RealName"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Status"))
            {
                data.Status = ConvertHelper.ToInt(Request["Status"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "PhoneNo"))
            {
                data.PhoneNo = ConvertHelper.ToString(Request["PhoneNo"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Email"))
            {
                data.Email = ConvertHelper.ToString(Request["Email"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Remark"))
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
                return "/Admin/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class SysAdmin_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public SysAdmin_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Admin/";
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
            #region admin 账号与当前管理员无法删除            
            if (iIds.Contains(1))
            {
                iIds.Remove(1);
            }
            if (iIds.Contains(this.CurrentLoginAdmin.ID))
            {
                 iIds.Remove(this.CurrentLoginAdmin.ID);
            }
            #endregion
            HandlerMessage = BLLFactory.Instance().SysAdminBLL.Remove(iIds);
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


