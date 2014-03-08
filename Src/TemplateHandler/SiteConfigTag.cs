using Hope.Util;
using Hope.WebBase;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 网站设置类标签
    /// </summary>
    public partial class HopeTag
    {
        /// <summary>
        /// 系统名称
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteName}
        /// 输出：中山大学
        /// </example>
        public string SiteName
        {
            get
            {
                return SiteConfigUtil.Instance().SiteName;
            }
        }

        /// <summary>
        /// 系统Logo
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteLogo}
        /// 输出：Images/Default/logo.jpg
        /// </example>
        public string SiteLogo
        {
            get
            {
                return SiteConfigUtil.Instance().SiteLogo;
            }
        }


        /// <summary>
        /// 系统ICP备案号
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteICPNum}
        /// </example>
        public string SiteICPNum
        {
            get
            {
                return SiteConfigUtil.Instance().SiteICPNum;
            }
        }

        /// <summary>
        /// 系统版权信息
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteCopyright}
        /// </example>
        public string SiteCopyright
        {
            get
            {
                return SiteConfigUtil.Instance().SiteCopyright;
            }
        }

        /// <summary>
        /// 系统作者（知识产权拥有者）
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SiteAuthor}
        /// </example>
        public string SiteAuthor
        {
            get
            {
                return AppConfig.Author;
            }
        }

        /// <summary>
        /// 系统版本
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.SysVersion}
        /// </example>
        public string SysVersion
        {
            get
            {
                return AppConfig.Version+"("+AppConfig.UpdateTime+")";
            }
        }
    }
}