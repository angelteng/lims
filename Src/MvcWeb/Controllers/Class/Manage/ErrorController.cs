
/******************************************************************
 *
 * 所在模块：Web(页面操作)
 * 类 名 称：SysRoleController(SysRoleController)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-05-23
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

namespace Hope.ITMS.Web
{
    /// <summary>
    /// 
    /// </summary>
    public class ErrorController : BaseController
    {
        /// <summary>
        /// 1002
        /// </summary>
        /// <returns></returns>
        public ActionResult E1002()
        {
            return new Error_1002(this);
        }

        /// <summary>
        /// 403		
        /// </summary>
        /// <returns></returns>
        public ActionResult E403()
        {
            return new Error_403(this);
        }

        /// <summary>
        /// 404
        /// </summary>
        /// <returns></returns>
        public ActionResult E404()
        {
            return new Error_404(this);
        }

        /// <summary>
        /// 500
        /// </summary>
        /// <returns></returns>
        public ActionResult E500()
        {
            return new Error_500(this);
        }
    }

    #region 1002 ...

    /// <summary>
    /// 1002
    /// </summary>
    public class Error_1002 : PageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Error_1002(BaseController controller)
            : base(controller)
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
                return "Error\\1002.html";
            }
        }

        #endregion

    }

    #endregion

    #region 403 ...

    /// <summary>
    /// 403
    /// </summary>
    public class Error_403 : PageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Error_403(BaseController controller)
            : base(controller)
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
                return "Error\\403.html";
            }
        }

        #endregion
    }

    #endregion

    #region 404 ...

    /// <summary>
    /// 404
    /// </summary>
    public class Error_404 : PageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Error_404(BaseController controller)
            : base(controller)
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
                return "Error\\404.html";
            }
        }

        #endregion

    }

    #endregion

    #region 500 ...

    /// <summary>
    /// 500
    /// </summary>
    public class Error_500 : PageActionResult
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="controller"></param>
        public Error_500(BaseController controller)
            : base(controller)
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
                return "Error\\500.html";
            }
        }

        #endregion

    }

    #endregion
}


