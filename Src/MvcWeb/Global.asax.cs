using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Hope.ITMS.Model;
using Hope.ITMS.TaskHandler;
using Monitor = Hope.ITMS.TaskHandler.Monitor;

namespace MvcWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            #region 管理员路由

            routes.MapRoute(
                "Manage", // 路由名称
                "Manage/{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Home", action = "Index", id = "" }, // 参数默认值
                new string[] { "Hope.ITMS.Web" }
            );

            #endregion

            
            #region 默认路由

            routes.MapRoute(
                "Default", // 路由名称
                "{controller}/{action}/{id}", // 带有参数的 URL
                new { controller = "Index", action = "Index", id = "" }, // 参数默认值
                new string[] { "Hope.ITMS.Web" }
            );

            #endregion
        }

        protected void Application_Start()
        {
            string applicationDirectory = HttpContext.Current.Server.MapPath("~/");
            Hope.WebBase.Configuration cfg = new Hope.WebBase.Configuration(applicationDirectory, "web.config");


            Monitor monitor = new Monitor();
            Thread thread = new Thread(new ThreadStart(monitor.Start));
            thread.Start();

            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);
        }
    }
}