/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：GroupTaskController(GroupTaskController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-11-06
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
    public class GroupTaskController : ManageController
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
            return new GroupTask_List(this);
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
            return new GroupTask_New(this);
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
            return new GroupTask_Modify(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new GroupTask_Preview(this);
        }

        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        [ValidateInput( false)]
        public ActionResult Save()
        {
            return new GroupTask_Save(this);
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
            return new GroupTask_Delete(this);
        }
    }
	#endregion
    
    #region GroupTaskList ...

    /// <summary>
    /// Show GroupTask List  
    /// </summary>
    public class GroupTask_List : ManageActionResult
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public GroupTask_List(ManageController controller)
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

            #region 各组任务

            exps.Add(Expression.Gt("TypeID", 1));

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
			
            List<TaskTask> datas = BLLFactory.Instance().TaskTaskBLL.GetList(pager, orderlist, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }
        
        /// <summary>
        /// 加载权限 2-add 4-edit 8-delete
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionAdd", CheckPermission("GroupTaskAdd"));
            TemplateHelper.Put("PermissionEdit", CheckPermission("GroupTaskEdit"));
            TemplateHelper.Put("PermissionDelete", CheckPermission("GroupTaskDelete"));
            TemplateHelper.Put("PermissionView", CheckPermission("GroupTaskView"));
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
                return "Manage\\GroupTask\\Index.html";
            }
        }        

        #endregion

    }

    #endregion

    #region GroupTaskNew ...

    /// <summary>
    /// new GroupTask
    /// </summary>
    public class GroupTask_New : ManageActionResult
    {
        /// <summary>
        /// GroupTask_New
        /// </summary>
        /// <param name="controller"></param>
        public GroupTask_New(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            TemplateHelper.Put("SavePermission", CheckPermission("GroupTaskAdd"));
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
                return "Manage\\GroupTask\\Add.html";
            }
        }

        #endregion
    }

    #endregion
	
	#region GroupTaskEdit ...

    /// <summary>
    /// 
	/// EditGroupTask
    /// </summary>
    public class GroupTask_Modify : ManageActionResult
    {
        /// <summary>
        /// GroupTask_Modify
        /// </summary>
        /// <param name="controller"></param>
        public GroupTask_Modify(ManageController controller)
            : base(controller)
        {
        }
        
		/// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
			int id = ConvertHelper.ToInt(Controller.RouteData.Values["ID"]);
			TaskTask data = BLLFactory.Instance().TaskTaskBLL.GetData(id);
			if (data == null){data = new TaskTask();}
		    List<TaskTaskHandler> handlers = BLLFactory.Instance().TaskTaskHandlerBLL.GetListByTaskID(data.ID);

            this.TemplateHelper.Put("SavePermission", CheckPermission("GroupTaskEdit"));
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("Handlers", handlers);
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
                return "Manage\\GroupTask\\Edit.html";
            }
        }

        #endregion
    }

    #endregion

    #region GroupTask preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class GroupTask_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public GroupTask_Preview(ManageController controller)
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
            TaskTask data = BLLFactory.Instance().TaskTaskBLL.GetData(id);
            if (data == null)
            {
                data = new TaskTask();
            }
            this.TemplateHelper.Put("Entity", data);
            this.TemplateHelper.Put("PermissionEdit", CheckPermission("GroupTaskEdit"));
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
                return "Manage\\GroupTask\\view.html";
            }
        }
        #endregion
    }

    #endregion

    #region process save ...

    /// <summary>
    /// 保存添加/修改
    /// </summary>
    public class GroupTask_Save : ManageActionResult
    {

        /// <summary>
        /// GroupTask_Save
        /// </summary>
        public GroupTask_Save(ManageController controller)
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
        private void ProcessSave()
        {
            TaskTask data = GetInput();
            if (data.ID <= 0)
            {
                ProcessAdd(data);
            }
            else
            {
                ProcessEdit(data);
            }
            if (HandlerMessage.Succeed)
            {
                ProcessHandler(data);
            }
        }

        /// <summary>
        /// 处理添加保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessAdd(TaskTask data)
        {
            if (!CheckPermission("GroupTaskAdd"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有添加数据的权限";
                return;
            }
            HandlerMessage = BLLFactory.Instance().TaskTaskBLL.Add(data);
        }

        /// <summary>
        /// 处理修改保存
        /// </summary>
        /// <param name="data"></param>
        private void ProcessEdit(TaskTask data)
        {
            if (!CheckPermission("GroupTaskEdit"))
            {
                HandlerMessage.Code = "01";
                HandlerMessage.Text = "对不起，你没有修改数据的权限";
                HandlerMessage.TargetUrl = "javascript:window.history.back();";
                return;
            }
            HandlerMessage = BLLFactory.Instance().TaskTaskBLL.Edit(data);
        }

        #region 处理任务跟进

        /// <summary>
        /// 处理任务跟进
        /// </summary>
        /// <param name="data"></param>
        private void ProcessHandler(TaskTask data)
        {
            if (RequestUtil.IsNull(Request, "Task_Handler"))
            {
                return;
            }

            List<OrgUser> newUsers = new List<OrgUser>();
            string strs = RequestUtil.RequestString(Request, "Task_Handler", string.Empty);
            string[] users = strs.Split(',');
            foreach (string s in users)
            {
                OrgUser user = BLLFactory.Instance().OrgUserBLL.GetData(s);
                if (user == null)
                {
                    continue;
                }
                newUsers.Add(user);
            }

            List<TaskTaskHandler> oldDatas = BLLFactory.Instance().TaskTaskHandlerBLL.GetListByTaskID(data.ID);

            //remove old data which is not in new data
            IEnumerable<int> oldIds = from a in oldDatas
                                      where !(from b in newUsers select b.ID).Contains(a.HandlerID)
                                      select a.ID;

            BLLFactory.Instance().TaskTaskHandlerBLL.Remove(oldIds.ToList());

            //add new data which is not in old data
            IEnumerable<OrgUser> toAdds = from a in newUsers
                                          where !(from b in oldDatas select b.HandlerID).Contains(a.ID)
                                          select a;
            foreach (OrgUser user in toAdds)
            {
                TaskTaskHandler handler = new TaskTaskHandler();
                handler.TaskID = data.ID;
                handler.HandlerID = user.ID;
                BLLFactory.Instance().TaskTaskHandlerBLL.Add(handler);
            }
        }

        #endregion
        
        #region 获取输入

        /// <summary>
        /// 获取输入
        /// </summary>
        /// <returns></returns>
        private TaskTask GetInput()
        {
            TaskTask data = new TaskTask();
            int id = RequestUtil.RequestInt(Request, "ID", -1);
            if (id > 0)
            {
                TaskTask oData = BLLFactory.Instance().TaskTaskBLL.GetData(id);
                if (oData != null)
                {
                    data = oData;
                }
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "ID"))
            {
                data.ID = ConvertHelper.ToInt(Request["ID"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Name"))
            {
                data.Name = ConvertHelper.ToString(Request["Name"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "TypeID"))
            {
                data.TypeID = ConvertHelper.ToInt(Request["TypeID"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Source"))
            {
                data.Source = ConvertHelper.ToString(Request["Source"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Progress"))
            {
                data.Progress = ConvertHelper.ToInt(Request["Progress"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Detail"))
            {
                data.Detail = ConvertHelper.ToString(Request["Detail"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Submitor"))
            {
                data.Submitor = ConvertHelper.ToInt(Request["Submitor"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "SubmitTime"))
            {
                data.SubmitTime = ConvertHelper.ToDateTime(Request["SubmitTime"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Priority"))
            {
                data.Priority = ConvertHelper.ToInt(Request["Priority"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Deadline"))
            {
                data.Deadline = ConvertHelper.ToDateTime(Request["Deadline"]);
            }
            if (!RequestUtil.IsNullOrEmpty(Request, "Remark"))
            {
                data.Remark = ConvertHelper.ToString(Request["Remark"]);
            }
            return data;
        }

        #endregion


        /// <summary>
        /// 验证输入数据
        /// </summary>
        /// <returns></returns>
        private bool CheckInput()
        {
            return !RequestUtil.HasNullOrEmpty(Request, "Name");
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
                return "/GroupTask/New/";
            }
        }
        #endregion
    }

    #endregion

    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class GroupTask_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public GroupTask_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Task/";
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
            HandlerMessage = BLLFactory.Instance().TaskTaskBLL.Remove(iIds);
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

