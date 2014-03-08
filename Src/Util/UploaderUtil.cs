using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Web.UI.WebControls;

namespace Hope.Util
{
    public class UploaderUtil : System.Web.UI.Page
    {
        #region 变量定义

        private bool _EnbleFileUpload = false;   //是否启用上传
        private decimal _FileUploadMaxSize = 2048;   //最大上传大小，单位KB
        private string _FileUploadDir = "UploadFiles";    //默认上传目录，相对于网站根目录
        private string[] _FileUploadType = { "rar", "doc", "ppt", "xls", "bmp", "jpg", "png", "rtf", "pptx", "xlsx", "mp3", "zip", "docx", "flw" };   //允许上传的文件类型
        private string _UploadFilePath = "${FileType}/${Year}/${Month}";                 //上传文件目录规则
        private string _UploadFileName = "${Year}${Month}${Day}${Hour}${Minute}${Second}${Random}";   //上传文件名规则

        private string uploadPath = "";    //自定义上传路径

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        public UploaderUtil()
        {
            // 读取默认的上传配置信息
            LoadConfig();
        }

        #endregion

        #region 文件上传接口

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /// <param name="filePath">文件路径</param>
        /// <returns></returns>
        public string UploadFile(FileUpload uploadFile)
        {
            // 如果上传被禁用则直接返回，报错
            if (!_EnbleFileUpload)
            {
                //return new UploadFile(uploadFile.FileName, UploadErrorNumber.Disabled);
                return null;
            }

            // 上传路径为空则返回
            if (uploadFile.FileName == null)    
            {
                //return new UploadFile(uploadFile.FileName, UploadErrorNumber.InvalidFile);
                return null;
            }
            // 如果上传文件类型不允许则直接返回，报错
            string typeName = Path.GetExtension(uploadFile.FileName);
            if (!CheckIsTypeAllowed(typeName.Trim('.')))
            {
                //return new UploadFile(uploadFile.FileName, UploadErrorNumber.FileNotAllowed);
                return null;
            }

            // 如果上传文件超过最大上传文件大小则直接返回，报错
            if (uploadFile.PostedFile.ContentLength > _FileUploadMaxSize * 1024)
            {
                //return new UploadFile(uploadFile.FileName, UploadErrorNumber.FileTooLarge);
                return null;
            }

            //如果目录不存在，则创建之
            string virualFileDir = uploadPath != "" ? uploadPath : GetFilePath(uploadFile);       //如果不是自定义的上传路径，则使用系统定义的路径保存
            if (!virualFileDir.EndsWith("/")) virualFileDir += "/";
            string fileDir = System.Web.HttpContext.Current.Server.MapPath(virualFileDir);
            if (!Directory.Exists(fileDir))
            {
                Directory.CreateDirectory(fileDir);
            }

            //组织文件名
            string newFileName = GetFileName(uploadFile);
            string filePath = fileDir + newFileName + typeName;

            int i = 0;
            while (System.IO.File.Exists(filePath)) //如果文件重名则重新命名
            {
                filePath = fileDir + newFileName + string.Format(" ({0})" + typeName, ++i);
            }

            // 保存文件
            try
            {
                uploadFile.SaveAs(filePath);
            }
            catch (Exception)
            {
                return null;
            }

            return filePath;
        }

        ///// <summary>
        ///// 上传单个文件
        ///// </summary>
        ///// <param name="uploadedFile">需要上传的文件</param>
        ///// <returns>返回已上传的文件对象</returns>
        //public UploadFile Save(HttpPostedFile uploadedFile)
        //{
        //    // 如果上传被禁用则直接返回，报错
        //    if (!_EnbleFileUpload)
        //    {
        //        return new UploadFile(uploadedFile.FileName, UploadErrorNumber.Disabled);
        //    }

        //    //上传文件无效
        //    if (uploadedFile == null)
        //    {
        //        return new UploadFile(uploadedFile.FileName, UploadErrorNumber.InvalidFile);
        //    }

        //    // 如果上传文件类型不允许则直接返回，报错
        //    string typeName = Path.GetExtension(uploadedFile.FileName);
        //    if (!CheckIsTypeAllowed(typeName.Trim('.')))
        //    {
        //        return new UploadFile(uploadedFile.FileName, UploadErrorNumber.FileNotAllowed);
        //    }

        //    // 如果上传文件超过最大上传文件大小则直接返回，报错
        //    if (uploadedFile.ContentLength > _FileUploadMaxSize*1024)
        //    {
        //        return new UploadFile(uploadedFile.FileName, UploadErrorNumber.FileTooLarge);
        //    }

        //    //如果目录不存在，则创建之
        //    string virualFileDir = uploadPath != "" ? uploadPath : GetFilePath(uploadedFile);       //如果不是自定义的上传路径，则使用系统定义的路径保存
        //    if (!virualFileDir.EndsWith("/")) virualFileDir += "/";
        //    string fileDir = System.Web.HttpContext.Current.Server.MapPath(virualFileDir);
        //    if (!Directory.Exists(fileDir))
        //    {
        //        Directory.CreateDirectory(fileDir);
        //    }

        //    //组织文件名
        //    string newFileName = GetFileName(uploadedFile);
        //    string filePath = fileDir + newFileName + typeName;

        //    int i = 0;
        //    while (System.IO.File.Exists(filePath)) //如果文件重名则重新命名
        //    {
        //        filePath = fileDir + newFileName + string.Format(" ({0})" + typeName, ++i);
        //    }

        //    //保存文件
        //    uploadedFile.SaveAs(filePath);
        //    UploadFile fileUploaded = new UploadFile();

        //    // 生成缩略图
        //    if (uploadedFile.ContentType.ToString().Contains("image"))
        //    {
        //        string fileThumImgPath_64 = fileDir + newFileName + "_s64" + typeName;
        //        string fileThumImgPath_128 = fileDir + newFileName + "_s128" + typeName;
        //        string fileThumImgPath_256 = fileDir + newFileName + "_s256" + typeName;

        //        if (i > 0)
        //        {
        //            fileThumImgPath_64 = fileDir + newFileName + string.Format(" ({0})" + "_s64" + typeName, i);
        //            fileThumImgPath_128 = fileDir + newFileName + string.Format(" ({0})" + "_s128" + typeName, i);
        //            fileThumImgPath_256 = fileDir + newFileName + string.Format(" ({0})" + "_s256" + typeName, i);
        //        }

        //        SaveThumnail(filePath, fileThumImgPath_64, THUMNAIL_WIDTH_64, THUMNAIL_HEIGHT_64, true);
        //        SaveThumnail(filePath, fileThumImgPath_128, THUMNAIL_WIDTH_128, THUMNAIL_HEIGHT_128, true);
        //        SaveThumnail(filePath, fileThumImgPath_256, THUMNAIL_WIDTH_256, THUMNAIL_HEIGHT_256, true);

        //        using (Image img = Image.FromFile(filePath))
        //        {
        //            if (img != null)
        //            {
        //                fileUploaded.Height = img.Height;
        //                fileUploaded.Width = img.Width;
        //            }
        //        }
        //    }

        //    fileUploaded.ErrorNumber = UploadErrorNumber.NoError;
        //    fileUploaded.IsFile = true;
        //    fileUploaded.Name = newFileName + typeName;
        //    fileUploaded.Size = uploadedFile.ContentLength / 1024;
        //    fileUploaded.Url = virualFileDir.EndsWith("/") ? virualFileDir : virualFileDir + "/"; ;
        //    fileUploaded.Extension = typeName.Trim('.');

        //    return fileUploaded;
        //}

        ///// <summary>
        ///// 上传多个文件
        ///// </summary>
        ///// <param name="uploadedFiles">需要上传的文件</param>
        ///// <returns>返回已上传的文件对象列表</returns>
        //public List<UploadFile> Save(HttpFileCollection uploadedFiles)
        //{
        //    List<UploadFile> output = new List<UploadFile>();

        //    for (int i = 0; i < uploadedFiles.Count; i++)
        //    {
        //        // 上传文件无效直接退出
        //        if (uploadedFiles[i].FileName == "")
        //        {
        //            continue;
        //        }

        //        UploadFile uploaded = Save(uploadedFiles[i]);
        //        if (uploaded != null)
        //        {
        //            output.Add(uploaded);

        //            if (uploaded.ErrorNumber != UploadErrorNumber.NoError)
        //            {
        //                RollBack(output);
        //                break;
        //            }
        //        }
        //    }

        //    return output;
        //}

        ///// <summary>
        ///// 当出现上传错误时，回滚，删除已上传的文件
        ///// </summary>
        ///// <param name="output"></param>
        //private void RollBack(List<UploadFile> output)
        //{
        //    string fileDir = "";
        //    string fileName = "";
        //    string file_orginal = "";
        //    string thumb_64 = "";
        //    string thumb_128 = "";
        //    string thumb_256 = "";
        //    foreach (UploadFile file in output)
        //    {
        //        fileDir = System.Web.HttpContext.Current.Server.MapPath(file.Url);
        //        fileName = file.Name.Substring(0, file.Name.LastIndexOf('.'));
        //        file_orginal = fileDir + fileName + "." + file.Extension;
        //        thumb_64 = fileDir + fileName + "_s64." + file.Extension;
        //        thumb_128 = fileDir + fileName + "_s128." + file.Extension;
        //        thumb_256 = fileDir + fileName + "_s256." + file.Extension;

        //        try
        //        {
        //            File.Delete(file_orginal);
        //            File.Delete(thumb_64);
        //            File.Delete(thumb_128);
        //            File.Delete(thumb_256);
        //        }
        //        catch (Exception ex)
        //        {
        //            LogUtil.error(ex.Message);
        //        }
        //    }
        //}

        ///// <summary>
        ///// 获取文件列表
        ///// </summary>
        ///// <param name="path">目录</param>
        ///// <returns>返回文件对象列表</returns>
        //public List<UploadFile> GetFiles(string path)
        //{
        //    path = path.EndsWith("/") ? path : path + "/";
        //    List<UploadFile> files = new List<UploadFile>();
        //    string fileDir = System.Web.HttpContext.Current.Server.MapPath(path);

        //    //找不到文件夹，直接退出
        //    if (!Directory.Exists(fileDir))
        //    {
        //        return files;
        //    }

        //    System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo(fileDir);
        //    System.IO.FileInfo[] aFiles = oDir.GetFiles();
        //    Bitmap img = null;
        //    for (int i = 0; i < aFiles.Length; i++)
        //    {
        //        Decimal iFileSize = Math.Round((Decimal)aFiles[i].Length / 1024);
        //        if (iFileSize < 1 && aFiles[i].Length != 0) iFileSize = 1;

        //        UploadFile file = new UploadFile();
        //        file.ErrorNumber = UploadErrorNumber.NoError;
        //        file.IsFile = true;
        //        file.Name = aFiles[i].Name;
        //        file.Size = iFileSize;
        //        file.Url = path;
        //        file.Extension = aFiles[i].Extension;

        //        try
        //        {
        //            img = new Bitmap(fileDir + aFiles[i].Name);
        //            file.Height = img.Height;
        //            file.Width = img.Width;
        //        }
        //        catch (Exception ex)
        //        {
        //            LogUtil.error(ex.Message);
        //        }

        //        files.Add(file);
        //    }

        //    return files;
        //}

        ///// <summary>
        ///// 获取目录列表
        ///// </summary>
        ///// <param name="path">目录</param>
        ///// <returns>返回文件对象列表</returns>
        //public List<UploadFile> GetFolders(string path)
        //{
        //    List<UploadFile> dirs = new List<UploadFile>();
        //    string fileDir = System.Web.HttpContext.Current.Server.MapPath(path);

        //    //找不到文件夹，直接退出
        //    if (!Directory.Exists(fileDir))
        //    {
        //        return dirs;
        //    }

        //    System.IO.DirectoryInfo oDir = new System.IO.DirectoryInfo(fileDir);
        //    System.IO.DirectoryInfo[] aDirs = oDir.GetDirectories();

        //    for (int i = 0; i < aDirs.Length; i++)
        //    {
        //        UploadFile dir = new UploadFile();
        //        dir.ErrorNumber = UploadErrorNumber.NoError;
        //        dir.IsFile = false;
        //        dir.Name = aDirs[i].Name;
        //        dir.Size = 0;
        //        dir.Url = path.EndsWith("/") ? path : path + "/";
        //        dir.Extension = "";

        //        dirs.Add(dir);
        //    }

        //    return dirs;
        //}

        ///// <summary>
        ///// 获取目录/文件列表
        ///// </summary>
        ///// <param name="path">目录</param>
        ///// <returns>返回文件对象列表</returns>
        //public List<UploadFile> GetFilesAndFolders(string path)
        //{
        //    List<UploadFile> list = GetFolders(path);
        //    list.AddRange(GetFiles(path));

        //    return list;
        //}
        #endregion

        #region 生成缩略图

        ///// <summary>
        ///// 生成缩略图质量差，体积大，不推荐使用。不推荐使用。
        ///// </summary>
        ///// <param name="sourceFilePath"></param>
        ///// <param name="outputFilePath"></param>
        ///// <param name="width"></param>
        ///// <param name="height"></param>
        //private void SaveThumbnailImage(string sourceFilePath, string outputFilePath, int width, int height)
        //{
        //    Bitmap source = new Bitmap(sourceFilePath);
        //    System.Drawing.Image thumb = source.GetThumbnailImage(width, height, null, IntPtr.Zero);
        //    thumb.Save(outputFilePath);
        //}

        ///// <summary>
        ///// 保存缩略图，图像质量较好，体积较大，无压缩。内部调用。
        ///// </summary>
        ///// <param name="sourceFilePath"></param>
        ///// <param name="thumbWi"></param>
        ///// <param name="thumbHi"></param>
        ///// <param name="maintainAspect"></param>
        ///// <returns></returns>
        //private System.Drawing.Image CreateThumbnail(string sourceFilePath, int thumbWi, int thumbHi, bool maintainAspect)
        //{
        //    using (Bitmap source = new Bitmap(sourceFilePath))
        //    {
        //        System.Drawing.Bitmap ret = null;

        //        try
        //        {
        //            // return the source image if it's smaller than the designated thumbnail
        //            if (source.Width < thumbWi && source.Height < thumbHi)
        //            {
        //                ret = new Bitmap(source.Width, source.Height);
        //            }
        //            else
        //            {

        //                int wi, hi;

        //                wi = thumbWi;
        //                hi = thumbHi;

        //                if (maintainAspect)
        //                {
        //                    // maintain the aspect ratio despite the thumbnail size parameters
        //                    if (source.Width > source.Height)
        //                    {
        //                        wi = thumbWi;
        //                        hi = (int)(source.Height * ((decimal)thumbWi / source.Width));
        //                    }
        //                    else
        //                    {
        //                        hi = thumbHi;
        //                        wi = (int)(source.Width * ((decimal)thumbHi / source.Height));
        //                    }
        //                }

        //                // original code that creates lousy thumbnails
        //                // System.Drawing.Image ret = source.GetThumbnailImage(wi,hi,null,IntPtr.Zero);
        //                ret = new Bitmap(wi, hi);
        //            }

        //            //! [slow]
        //            using (Graphics g = Graphics.FromImage(ret))
        //            {
        //                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        //                g.FillRectangle(Brushes.White, 0, 0, ret.Width, ret.Height);
        //                g.DrawImage(source, 0, 0, ret.Width, ret.Height);
        //            }
        //        }
        //        catch
        //        {
        //            ret = null;
        //        }

        //        return ret;
        //    }
        //}

        ///// <summary>
        ///// 保存缩略图，图像质量较好，体积小，JPEG压缩，精度75
        ///// </summary>
        ///// <param name="sourceFilePath"></param>
        ///// <param name="outputFilePath"></param>
        ///// <param name="thumbWi"></param>
        ///// <param name="thumbHi"></param>
        ///// <param name="maintainAspect"></param>
        //public void SaveThumnail(string sourceFilePath, string outputFilePath, int thumbWi, int thumbHi, bool maintainAspect)
        //{
        //    using (System.Drawing.Image myThumbnail = CreateThumbnail(sourceFilePath, thumbWi, thumbHi, maintainAspect))
        //    {

        //        //Configure JPEG Compression Engine
        //        System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
        //        long[] quality = new long[1];
        //        quality[0] = 75;
        //        System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
        //        encoderParams.Param[0] = encoderParam;

        //        System.Drawing.Imaging.ImageCodecInfo[] arrayICI = System.Drawing.Imaging.ImageCodecInfo.GetImageEncoders();
        //        System.Drawing.Imaging.ImageCodecInfo jpegICI = null;
        //        for (int x = 0; x < arrayICI.Length; x++)
        //        {
        //            if (arrayICI[x].FormatDescription.Equals("JPEG"))
        //            {
        //                jpegICI = arrayICI[x];
        //                break;
        //            }
        //        }

        //        myThumbnail.Save(outputFilePath, jpegICI, encoderParams);
        //    }
        //}
        #endregion

        #region 工具/配置

        /// <summary>
        /// 读取默认的上传配置信息
        /// </summary>
        private void LoadConfig()
        {
            _EnbleFileUpload = SiteConfigUtil.Instance().EnbleFileUpload;

            if (SiteConfigUtil.Instance().FileUploadMaxSize>0)
            {
                _FileUploadMaxSize = SiteConfigUtil.Instance().FileUploadMaxSize;
            }
            if (!string.IsNullOrEmpty(SiteConfigUtil.Instance().FileUploadDir))
            {
                _FileUploadDir = SiteConfigUtil.Instance().FileUploadDir;
            }
            if (!string.IsNullOrEmpty(SiteConfigUtil.Instance().FileUploadType))
            {
                _FileUploadType = SiteConfigUtil.Instance().FileUploadType.Split('|');
            }
            if (!string.IsNullOrEmpty(SiteConfigUtil.Instance().UploadFilePath))
            {
                _UploadFilePath = SiteConfigUtil.Instance().UploadFilePath;
            }
            if (!string.IsNullOrEmpty(SiteConfigUtil.Instance().UploadFileName))
            {
                _UploadFileName = SiteConfigUtil.Instance().UploadFileName;
            }
        }

        /// <summary>
        /// 检查是否是允许上传的文件类型
        /// </summary>
        /// <param name="typeName">上传文件的类型</param>
        /// <returns>该文件是否是允许上传</returns>
        private bool CheckIsTypeAllowed(string typeName)
        {
            return (System.Array.IndexOf(this._FileUploadType, typeName) >= 0);
        }

        /// <summary>
        /// 返回上传路径
        /// </summary>
        /// <param name="uploadedFile"></param>
        /// <returns></returns>
        private string GetFilePath(FileUpload uploadedFile)
        {
            string filePath = "~/" + _FileUploadDir + "/";

            string treePath = _UploadFilePath;
            treePath = treePath.Replace("${FileType}", Path.GetExtension(uploadedFile.FileName).Trim('.'));
            treePath = treePath.Replace("${Year}", DateTime.Now.Year.ToString());
            treePath = treePath.Replace("${Month}", DateTime.Now.Month.ToString().PadLeft(2, '0'));
            treePath = treePath.Replace("${Day}", DateTime.Now.Day.ToString().PadLeft(2, '0'));

            return filePath + treePath.Trim('/') + "/";
        }

        /// <summary>
        /// 获取上传后的文件名
        /// </summary>
        /// <param name="uploadedFile">上传的文件</param>
        /// <returns>上传后的文件名</returns>
        private string GetFileName(FileUpload uploadedFile)
        {
            string fName = _UploadFileName;
            fName = fName.Replace("${Origin}", Path.GetFileName(uploadedFile.FileName).Replace(Path.GetExtension(uploadedFile.FileName), ""));
            fName = fName.Replace("${Random}", Guid.NewGuid().ToString("N"));
            fName = fName.Replace("${Year}", DateTime.Now.Year.ToString());
            fName = fName.Replace("${Month}", DateTime.Now.Month.ToString().PadLeft(2, '0'));
            fName = fName.Replace("${Day}", DateTime.Now.Day.ToString().PadLeft(2, '0'));
            fName = fName.Replace("${Hour}", DateTime.Now.Hour.ToString().PadLeft(2, '0'));
            fName = fName.Replace("${Minute}", DateTime.Now.Hour.ToString().PadLeft(2, '0'));
            fName = fName.Replace("${Second}", DateTime.Now.Hour.ToString().PadLeft(2, '0'));

            return fName;
        }

        #endregion
    }
}
