using System.Web;
using System.Text;
using System;
using System.Collections.Generic;

using Hope.Util;
using Hope.ITMS.BLL;
using Hope.ITMS.Model;
using Hope.ITMS.Enums;
using Hope.WebBase;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 通用类标签
    /// </summary>
    public partial class HopeTag
    {
        #region 变量定义

        private string _Skin_CSS;
        private string _InstallDir;
        private string _InstallVirtualDir;
        private string _AdminDir;

        #endregion
        
        #region 网站安装目录，包括虚拟目录标签

        /// <summary>
        /// 网站安装目录，包括虚拟目录标签
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.InstallDir}
        /// 输出：http://192.168.1.183/HopeCMS/
        /// </example>
        public string InstallDir
        {
            get
            {
                _InstallDir = AppConfig.WebMainPathURL.TrimEnd('/') + "/";
                return _InstallDir;
            }
        }

        #endregion

        #region 站点虚拟目录名称

        /// <summary>
        /// 站点虚拟目录名称
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.InstallVDir}
        /// 输出：返回HopeCMS，如果网站安装目录为：http://192.168.1.183/HopeCMS/
        /// </example>
        public string InstallVDir
        {
            get
            {
                _InstallVirtualDir = HttpContext.Current.Request.ApplicationPath;
                return _InstallVirtualDir;
            }
        }

        #endregion

        #region 网站URL地址

        /// <summary>
        /// 网站URL地址
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteUrl}
        /// </example>
        public string SiteUrl
        {
            get
            {
                return InstallDir;
            }
        }

        #endregion
        
        #region 用户登录地址

        /// <summary>
        /// 返回用户登录地址
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.UserLoginUrl}
        /// 输出：返回用户登录地址 http://192.168.1.183/HopeCMS/User/Login.aspx
        /// </example>
        public string UserLoginUrl
        {
            get
            {
                return string.Format("{0}User/Login", SiteUrl); ;
            }
        }

        #endregion
    }
}