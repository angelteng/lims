/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：DevUserReservationController(DevUserReservationController)
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
    public class UserReservationController : ManageController
    {
        /// <summary>
        /// 索引列表
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return Add();
        }

        /// <summary>
        /// 列表查看		
        /// </summary>
        /// <returns></returns>
        public ActionResult List()
        {
            return new DevUserReservation_List(this);
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
            return new DevUserReservation_New(this);
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
            return new DevUserReservation_Modify(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new DevUserReservation_Preview(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public ActionResult Save()
        {
            return new DevUserReservation_Save(this);
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
            return new DevUserReservation_Delete(this);
        }
    }
	#endregion
    
    #region DevUserReservationList ...

    /// <summary>
    /// Show DevUserReservation List  
    /// </summary>
    public class DevUserReservation_List : PageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public DevUserReservation_List(ManageController controller)
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
			
            List<DevReservation> datas = BLLFactory.Instance().DevReservationBLL.GetList(pager, orderlist, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }
        
        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", true);
            TemplateHelper.Put("PermissionEdit", true);
            TemplateHelper.Put("PermissionDelete", true);
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
                return "Manage\\UserReservation\\Index.html";
            }
        }        

        #endregion

    }

    #endregion

    #region DevUserReservationNew ...

    /// <summary>
    /// new DevUserReservation
    /// </summary>
    public class DevUserReservation_New : PageActionResult
    {
        /// <summary>
        /// DevUserReservation_New
        /// </summary>
        /// <param name="controller"></param>
        public DevUserReservation_New(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            TemplateHelper.Put("SavePermission", true);
            int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
            DevDevice datas = BLLFactory.Instance().DevDeviceBLL.GetData(id);
            TemplateHelper.Put("DeviceName", datas.CNName);
            //this.TemplateHelper.Put("UserID", CurrentLoginAdmin.RoleID);//检测用户ID
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
                return "Manage\\UserReservation\\Add.html";
            }
        }

        #endregion
    }

    #endregion
	
	#region DevUserReservationEdit ...

    /// <summary>
    /// 
	/// EditDevUserReservation
    /// </summary>
    public class DevUserReservation_Modify : ManageActionResult
    {
        /// <summary>
        /// DevUserReservation_Modify
        /// </summary>
        /// <param name="controller"></param>
        public DevUserReservation_Modify(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
			int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
			DevReservation data = BLLFactory.Instance().DevReservationBLL.GetData(id);
			if (data == null){data = new DevReservation();}
			
            this.TemplateHelper.Put("SavePermission", CheckPermission("ReservationEdit"));
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
                return "Manage\\UserReservation\\Edit.html";
            }
        }

        #endregion
    }

    #endregion

    #region DevUserReservation preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class DevUserReservation_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public DevUserReservation_Preview(ManageController controller)
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
            DevReservation data = BLLFactory.Instance().DevReservationBLL.GetData(id);
            if (data == null)
            {
                data = new DevReservation();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("PermissionEdit", CheckPermission("ReservationEdit"));
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
                return "Manage\\UserReservation\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class DevUserReservation_Save : ManageActionResult
    {

        /// <summary>
        /// DevUserReservation_Save
        /// </summary>
        public DevUserReservation_Save(ManageController controller)
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
            DevReservation data = GetInput();
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
        private void ProcessAdd(DevReservation data)
        {
            if (!CheckPermission("ReservationAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().DevReservationBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(DevReservation data)
        {
            if (!CheckPermission("ReservationEdit"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().DevReservationBLL.Edit(data);
        }

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private DevReservation GetInput()
        {
            DevReservation data = new DevReservation();
			int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id>0)
            {
                DevReservation oData = BLLFactory.Instance().DevReservationBLL.GetData(id);
                if (oData!=null)
                {
                    data = oData;
                }
            }
			
			if(!RequestUtil.IsNullOrEmpty(Request,"UserID"))
			{
                //data.UserID = CurrentLoginAdmin.RoleID;
                data.UserID = 5;
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"UserType"))
			{
				data.UserType = ConvertHelper.ToInt(Request["UserType"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"TestContent"))
			{
				data.TestContent = ConvertHelper.ToString(Request["TestContent"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"BeginTIme"))
			{
				data.BeginTIme = ConvertHelper.ToDateTime(Request["BeginTIme"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"EndTime"))
			{
				data.EndTime = ConvertHelper.ToDateTime(Request["EndTime"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"SampleCount"))
			{
				data.SampleCount = ConvertHelper.ToInt(Request["SampleCount"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"ReservationType"))
			{
				data.ReservationType = ConvertHelper.ToInt(Request["ReservationType"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"DeviceID"))
			{
				data.DeviceID = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
			}
			if(!RequestUtil.IsNullOrEmpty(Request,"Price"))
			{
				data.Price = ConvertHelper.ToDouble(Request["Price"]);
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
                return "/UserReservation/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class DevUserReservation_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public DevUserReservation_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Reservation/";
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
            HandlerMessage = BLLFactory.Instance().DevReservationBLL.Remove(iIds);
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

