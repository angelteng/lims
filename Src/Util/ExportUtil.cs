using System;
using System.IO;
using System.Configuration;
using System.Web;

namespace Hope.Util
{
    public class ExportUtil : System.Web.UI.Page
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        public ExportUtil()
        {

        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <param name="fileName">文件名</param>
        /// <returns></returns>
        public string ExportFile(string fileName)
        {
            // 读取web.config获取导出文件路径，没有则创建
            string path = ConfigurationManager.AppSettings["ExportPath"];
            string physicalPath = HttpContext.Current.Server.MapPath(path);
            if (!Directory.Exists(physicalPath))
            {
                Directory.CreateDirectory(physicalPath);
            }

            // 命名导出文件
            string filePath = string.Format("{0}{1}_{2}.xls", path, fileName, DateTime.Now.ToString("yyyyMMddHHmmssff"));
            //string exportPath = Server.MapPath(filePath);

            return filePath;
        }
    }
}
