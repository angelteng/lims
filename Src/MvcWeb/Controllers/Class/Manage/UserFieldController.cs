/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：UserFieldController(UserFieldController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-11-05
 * xingzhewujiang1999@gmail.com
 * QQ:970355214
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
using Hope.ITMS.Model.Enum;
using Hope.WebBase;
using Hope.Util;
using NHibernate.Criterion;

namespace Hope.ITMS.Web
{
	#region 方法申明
    /// <summary>
    /// 
    /// </summary>
    public class UserFieldController : ManageController
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
            return new UserField_List(this);
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
            return new UserField_New(this);
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
            return new UserField_Modify(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new UserField_Preview(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new UserField_Save(this);
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
            return new UserField_Delete(this);
        }
    }
	#endregion
    
    #region UserFieldList ...

    /// <summary>
    /// Show UserField List  
    /// </summary>
    public class UserField_List : ManageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public UserField_List(ManageController controller)
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
			List<SimpleExpression> exps = new List<SimpleExpression>();

            #region 模型ID

            int modelId = RequestUtil.RequestInt(Request, "ModelID", 0);
            exps.Add(Expression.Eq("ModelID", modelId));

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
			
            List<CommonField> datas = BLLFactory.Instance().CommonFieldBLL.GetList(pager, orderlist, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
            this.TemplateHelper.Put("ModelID", modelId);
        }
        
        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission("UserFielddAdd"));
            TemplateHelper.Put("PermissionEdit", CheckPermission("UserFieldEdit"));
            TemplateHelper.Put("PermissionDelete", CheckPermission("UserFieldDelete"));
            TemplateHelper.Put("PermissionView", CheckPermission("UserFieldView"));
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
                return "Manage\\UserField\\Index.html";
            }
        }        

        #endregion

    }

    #endregion

    #region UserFieldNew ...

    /// <summary>
    /// new UserField
    /// </summary>
    public class UserField_New : ManageActionResult
    {
        /// <summary>
        /// UserField_New
        /// </summary>
        /// <param name="controller"></param>
        public UserField_New(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            TemplateHelper.Put("ModelID", RequestUtil.RequestInt(Request, "ModelID", 0));
            TemplateHelper.Put("SavePermission", CheckPermission("FieldAdd"));
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
                return "Manage\\UserField\\Add.html";
            }
        }

        #endregion
    }

    #endregion
	
	#region UserFieldEdit ...

    /// <summary>
    /// 
	/// EditUserField
    /// </summary>
    public class UserField_Modify : ManageActionResult
    {
        /// <summary>
        /// UserField_Modify
        /// </summary>
        /// <param name="controller"></param>
        public UserField_Modify(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
			int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
			CommonField data = BLLFactory.Instance().CommonFieldBLL.GetData(id);
			if (data == null){data = new CommonField();}

            this.TemplateHelper.Put("SavePermission", CheckPermission("UserFieldEdit"));
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
                return "Manage\\UserField\\Edit.html";
            }
        }

        #endregion
    }

    #endregion

    #region UserField preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class UserField_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public UserField_Preview(ManageController controller)
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
            CommonField data = BLLFactory.Instance().CommonFieldBLL.GetData(id);
            if (data == null)
            {
                data = new CommonField();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("PermissionEdit", CheckPermission("UserFieldEdit"));
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
                return "Manage\\UserField\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class UserField_Save : ManageActionResult
    {

        /// <summary>
        /// UserField_Save
        /// </summary>
        public UserField_Save(ManageController controller)
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
            CommonField data = GetInput();
            data.Type = (int) ModelType.UserModel;

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
        private void ProcessAdd(CommonField data)
        {
            if (!CheckPermission("FieldAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().CommonFieldBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(CommonField data)
        {
            if (!CheckPermission("UserFieldEdit"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().CommonFieldBLL.Edit(data);
        }

        #region 获取输入

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private CommonField GetInput()
        {
            CommonField data = new CommonField();
            int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id > 0)
            {
                CommonField oData = BLLFactory.Instance().CommonFieldBLL.GetData(id);
                if (oData != null)
                {
                    data = oData;
                }
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "ID"))
            {
                data.ID = ConvertHelper.ToInt(Request["ID"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "ModelID"))
            {
                data.ModelID = ConvertHelper.ToInt(Request["ModelID"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Type"))
            {
                data.Type = ConvertHelper.ToInt(Request["Type"]);
            }
            //if (!RequestUtil.IsNullOrEmpty(Request, "OrderID"))
            //{
            //    data.OrderID = ConvertHelper.ToInt(Request["OrderID"]);
            //}
            if (!RequestUtil.IsNullOrEmpty(Request, "Code"))
            {
                data.Code = ConvertHelper.ToString(Request["Code"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Name"))
            {
                data.Name = ConvertHelper.ToString(Request["Name"]);
            }
            if (!RequestUtil.IsNull(Request, "PrefixText"))
            {
                data.PrefixText = ConvertHelper.ToString(Request["PrefixText"]);
            }
            if (!RequestUtil.IsNull(Request, "SuffixText"))
            {
                data.SuffixText = ConvertHelper.ToString(Request["SuffixText"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "ComponentType"))
            {
                data.ComponentType = ConvertHelper.ToInt(Request["ComponentType"]);
            }
            if (!RequestUtil.IsNull(Request, "ComponentWidth"))
            {
                data.ComponentWidth = ConvertHelper.ToInt(Request["ComponentWidth"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Required"))
            {
                data.Required = ConvertHelper.ToBoolean(Request["Required"]);
            }
            if (!RequestUtil.IsNull(Request, "DefaultValue"))
            {
                data.DefaultValue = ConvertHelper.ToString(Request["DefaultValue"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "ListItem"))
            {
                data.ListItem = GetListItem();
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "IsShow"))
            {
                data.IsShow = ConvertHelper.ToBoolean(Request["IsShow"]);
            }
            if (!RequestUtil.IsNull(Request, "ComponentID"))
            {
                data.ComponentID = ConvertHelper.ToString(Request["ComponentID"]);
            }
            if (!RequestUtil.IsNull(Request, "ClassName"))
            {
                data.ClassName = ConvertHelper.ToString(Request["ClassName"]);
            }
            if (!RequestUtil.IsNull(Request, "Regex"))
            {
                data.Regex = ConvertHelper.ToString(Request["Regex"]);
            }
            if (!RequestUtil.IsNull(Request, "Remark"))
            {
                data.Remark = ConvertHelper.ToString(Request["Remark"]);
            }
            return data;
        }

        #endregion

        /// <summary>
        /// GetListItem
        /// </summary>
        /// <returns></returns>
        private Dictionary<string, string> GetListItem()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string str = RequestUtil.RequestString(Request, "ListItem", string.Empty);
            string[] items = str.Split('\n');
            foreach (string item in items)
            {
                string[] kvs = item.Split('=');
                if (kvs.Length < 2) continue;
                dic.Add(kvs[0], kvs[1]);
            }
            return dic;
        }


        /// <summary>
        /// 验证输入数据
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            return !RequestUtil.HasNullOrEmpty(Request, "Code", "Name", "ModelID");
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
                return "/UserField/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class UserField_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public UserField_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/UserField/";
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
            HandlerMessage = BLLFactory.Instance().CommonFieldBLL.Remove(iIds);
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

