
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysLogController(SysLogController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-06-20
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
using Hope.ITMS.Enums;
using Hope.WebBase;
using Hope.Util;
using NHibernate.Criterion;
using System;

namespace Hope.ITMS.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class LogController : ManageController
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
            return new SysLog_List(this);
        }

        /// <summary>
        /// 显示查看页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Preview()
        {
            return new SysLog_Preview(this);
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
            return new SysLog_Delete(this);
        }
    }

    #region SysLogList ...

    /// <summary>
    /// Show SysLog List
    /// </summary>
    public class SysLog_List : ManageActionResult
    {
        //日志类型
        private int type = -1;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysLog_List(ManageController controller)
            : base(controller)
        {
        }

        /// <summary>
        /// 执行
        /// </summary>
        protected override void Executing()
        {
            type = RequestUtil.RequestInt(Request, "Type", -1);            

            Show();
            LoadPermission();
        }

        /// <summary>
        /// 显示列表
        /// </summary>
        private void Show()
        {
            string loghref = "<a href=?Type=" + (int)LogType.Exception+">系统异常</a>";
            loghref += " | <a href=?Type=" + (int)LogType.LoginFailed + ">登录失败</a>";
            loghref += " | <a href=?Type=" + (int)LogType.AccessFailed + ">越权操作</a>";
            loghref += " | <a href=?Type=" + (int)LogType.LoginSucceed + ">登录成功</a>";
            loghref += " | <a href=?Type=" + (int)LogType.MailFailed + ">邮件发送失败</a>";
            loghref += " | <a href=?Type=" + (int)LogType.MailSucceed + ">邮件发送成功</a>";
            this.TemplateHelper.Put("LogTypeHref", loghref);

            PagerData pager = GetPager();
            List<SimpleExpression> exps = new List<SimpleExpression>();
            if (type!=-1)
            {
                exps.Add(NHibernate.Criterion.Expression.Eq("Type", type));
            }           
            List<SysLog> datas = BLLFactory.Instance().SysLogBLL.GetList(pager, exps);
            this.TemplateHelper.Put("EntityList", datas);
            this.TemplateHelper.Put("Pager", pager);
            this.TemplateHelper.Put("PagerUrl", GetPageUrl(pager));
        }

        /// <summary>
        /// 加载权限判断
        /// </summary>
        private void LoadPermission()
        {
            TemplateHelper.Put("PermissionDelete", CheckPermission("LogDelete"));
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
                return "Manage\\Log\\Index.html";
            }
        }

        #endregion

    }

    #endregion

    #region SysLog preview ...

    /// <summary>
    /// preview
    /// </summary>
    public class SysLog_Preview : ManageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public SysLog_Preview(ManageController controller)
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
            SysLog data = BLLFactory.Instance().SysLogBLL.GetData(id);
            if (data == null)
            {
                data = new SysLog();
            }
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
                return "Manage\\Log\\view.html";
            }
        }
        #endregion
    }

    #endregion
    
    #region process delete ...

    /// <summary>
    /// 删除
    /// </summary>
    public class SysLog_Delete : ManageActionResult
    {
        /// <summary>
        /// 删除
        /// </summary>
        public SysLog_Delete(ManageController controller)
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
                HandlerMessage.TargetUrl = this.ApplicationUrl + "/Log/";
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
            //处理底部批量删除功能
            List<SimpleExpression> expres = new List<SimpleExpression>();
            if (ConvertHelper.ToInt(sId) <= 0 && iIds.Count<=1)
            {
                switch (sId)
                {
                    case "0":
                        break;

                    //一周前的
                    case "-7":
                        double days = ConvertHelper.ToDouble(sId);
                        expres.Add(NHibernate.Criterion.Expression.Le("Timestamp",DateTime.Now.AddDays(days)));
                        DeleteItems(expres);
                        break;

                    //一月前
                    case "-30":
                        expres.Add(NHibernate.Criterion.Expression.Le("Timestamp", DateTime.Now.AddMonths(-1)));
                        DeleteItems(expres);
                        break;

                    //一年前
                    case "-365":
                        expres.Add(NHibernate.Criterion.Expression.Le("Timestamp",DateTime.Now.AddYears(-1)));
                        DeleteItems(expres);
                        break;

                    //所有
                    case "-1":
                        expres.Add(NHibernate.Criterion.Expression.Gt("ID", 0));
                        DeleteItems(expres);
                        break;
                }
            }
            else
            {                
                HandlerMessage = BLLFactory.Instance().SysLogBLL.Remove(iIds);
            }
        }

        #region DeleteItems
        /// <summary>
        /// 根据查询条件批量删除
        /// </summary>
        /// <param name="expres">查询条件</param>
        private void DeleteItems(List<SimpleExpression> expres)
        {
            List<int> ids = new List<int>();
            List<SysLog> list = BLLFactory.Instance().SysLogBLL.GetList(expres);

            foreach (SysLog data in list)
            {
                ids.Add(data.ID);
            }
            HandlerMessage = BLLFactory.Instance().SysLogBLL.Remove(ids);
        }
        #endregion...

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