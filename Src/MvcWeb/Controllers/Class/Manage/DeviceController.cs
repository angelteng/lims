/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：DevDeviceController(DevDeviceController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2014-02-15
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
using Hope.WebBase;
using Hope.Util;
using NHibernate.Criterion;

namespace Hope.ITMS.Web
{
	#region 方法申明
    /// <summary>
    /// 
    /// </summary>
    public class DeviceController : ManageController
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
            return new DevDevice_List(this);
        }

        /// <summary>
        /// 用户列表查看		
        /// </summary>
        /// <returns></returns>
        public ActionResult UserList()
        {
            return new DevUserDevice_List(this);
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
            return new DevDevice_New(this);
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
            return new DevDevice_Modify(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new DevDevice_Preview(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new DevDevice_Save(this);
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
            return new DevDevice_Delete(this);
        }
    }
	#endregion
    
    #region DevDeviceList ...

    /// <summary>
    /// Show DevDevice List  
    /// </summary>
    public class DevDevice_List : ManageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public DevDevice_List(ManageController controller)
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
			
            List<DevDevice> datas = BLLFactory.Instance().DevDeviceBLL.GetList(pager, orderlist, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }
        
        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission("DeviceAdd"));
            TemplateHelper.Put("PermissionEdit", CheckPermission("DeviceEdit"));
            TemplateHelper.Put("PermissionDelete", CheckPermission("DeviceDelete"));
            TemplateHelper.Put("PermissionView", CheckPermission("DeviceView"));
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
                return "Manage\\Device\\Index.html";
            }
        }        

        #endregion

    }

    #endregion

    #region DevUserDeviceList ...

    /// <summary>
    /// Show DevDevice List  
    /// </summary>
    public class DevUserDevice_List : PageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public DevUserDevice_List(ManageController controller)
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

            List<DevDevice> datas = BLLFactory.Instance().DevDeviceBLL.GetList(pager, orderlist, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }

        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            
            TemplateHelper.Put("PermissionView", true);
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
                return "Manage\\Device\\Index_User.html";
            }
        }

        #endregion

    }

    #endregion


    #region DevDeviceNew ...

    /// <summary>
    /// new DevDevice
    /// </summary>
    public class DevDevice_New : ManageActionResult
    {
        /// <summary>
        /// DevDevice_New
        /// </summary>
        /// <param name="controller"></param>
        public DevDevice_New(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            TemplateHelper.Put("SavePermission", CheckPermission("DeviceAdd"));
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
                return "Manage\\Device\\Add.html";
            }
        }

        #endregion
    }

    #endregion
	
	#region DevDeviceEdit ...

    /// <summary>
    /// 
	/// EditDevDevice
    /// </summary>
    public class DevDevice_Modify : ManageActionResult
    {
        /// <summary>
        /// DevDevice_Modify
        /// </summary>
        /// <param name="controller"></param>
        public DevDevice_Modify(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
			int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
			DevDevice data = BLLFactory.Instance().DevDeviceBLL.GetData(id);
			if (data == null){data = new DevDevice();}
			
            this.TemplateHelper.Put("SavePermission", CheckPermission("DeviceEdit"));
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
                return "Manage\\Device\\Edit.html";
            }
        }

        #endregion
    }

    #endregion

    #region DevDevice preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class DevDevice_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public DevDevice_Preview(ManageController controller)
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
            DevDevice data = BLLFactory.Instance().DevDeviceBLL.GetData(id);
            if (data == null)
            {
                data = new DevDevice();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("PermissionEdit", CheckPermission("DeviceEdit"));
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
                return "Manage\\Device\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class DevDevice_Save : ManageActionResult
    {

        /// <summary>
        /// DevDevice_Save
        /// </summary>
        public DevDevice_Save(ManageController controller)
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
            DevDevice data = GetInput();
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
        private void ProcessAdd(DevDevice data)
        {
            if (!CheckPermission("DeviceAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().DevDeviceBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(DevDevice data)
        {
            if (!CheckPermission("DeviceEdit"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().DevDeviceBLL.Edit(data);
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private DevDevice GetInput()
        {
            DevDevice data = new DevDevice();
			int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id>0)
            {
                DevDevice oData = BLLFactory.Instance().DevDeviceBLL.GetData(id);
                if (oData!=null)
                {
                    data = oData;
                }
            }
			if(!RequestUtil.IsNullOrEmpty(Request,"ID"))
			{
				data.ID = ConvertHelper.ToInt(Request["ID"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Name"))
			{
				data.Name = ConvertHelper.ToString(Request["Name"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"CNName"))
			{
				data.CNName = ConvertHelper.ToString(Request["CNName"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"TestContent"))
			{
				data.TestContent = ConvertHelper.ToString(Request["TestContent"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Pic"))
			{
				data.Pic = ConvertHelper.ToString(Request["Pic"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Video"))
			{
				data.Video = ConvertHelper.ToString(Request["Video"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"DeviceModel"))
			{
				data.DeviceModel = ConvertHelper.ToString(Request["DeviceModel"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Supplier"))
			{
				data.Supplier = ConvertHelper.ToString(Request["Supplier"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"SupplierURL"))
			{
				data.SupplierURL = ConvertHelper.ToString(Request["SupplierURL"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Number"))
			{
				data.Number = ConvertHelper.ToInt(Request["Number"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Price"))
			{
				data.Price = ConvertHelper.ToInt(Request["Price"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Status"))
			{
				data.Status = ConvertHelper.ToInt(Request["Status"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"OrderNum"))
			{
				data.OrderNum = ConvertHelper.ToInt(Request["OrderNum"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"PersonInCharge"))
			{
				data.PersonInCharge = ConvertHelper.ToString(Request["PersonInCharge"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Contact"))
			{
				data.Contact = ConvertHelper.ToString(Request["Contact"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Information"))
			{
				data.Information = ConvertHelper.ToString(Request["Information"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"BeginTime"))
			{
				data.BeginTime = ConvertHelper.ToDateTime(Request["BeginTime"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"EndTime"))
			{
				data.EndTime = ConvertHelper.ToDateTime(Request["EndTime"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"MaxApplyTIme"))
			{
				data.MaxApplyTIme = ConvertHelper.ToInt(Request["MaxApplyTIme"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"MaxPreApplyTIme"))
			{
				data.MaxPreApplyTIme = ConvertHelper.ToInt(Request["MaxPreApplyTIme"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Location"))
			{
				data.Location = ConvertHelper.ToString(Request["Location"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Remark"))
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
                return "/Device/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class DevDevice_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public DevDevice_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Device/";
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
            HandlerMessage = BLLFactory.Instance().DevDeviceBLL.Remove(iIds);
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

