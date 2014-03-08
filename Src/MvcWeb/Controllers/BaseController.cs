using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Xml;

namespace Hope.ITMS.Web
{
    #region 页面基类 ...

    /// <summary>
    /// 基本界面基类
    /// </summary>
    public class BaseController : Controller
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        protected override void OnException(ExceptionContext filterContext)
        {
            //LogHelper.Write(filterContext.Exception.Message);
            base.OnException(filterContext);
        }
    }

    #endregion

    #region 会员界面基类 ...

    /// <summary>
    /// 会员界面的基类
    /// </summary>
    public class MemberBaseController : BaseController
    {
    }

    #endregion

    #region 管理界面基类 ...

    /// <summary>
    /// 管理界面的基类
    /// </summary>
    public class ManageController : BaseController
    {
    }

    #endregion

    #region 内容基类 ...

    /// <summary>
    /// 内容基类
    /// </summary>
    public class ContentController : BaseController
    {
    }

    #endregion
}
