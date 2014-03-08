using System;
using System.IO;
using System.Text;

namespace Hope.Util
{
    /// <summary>
    /// 文件操作类
    /// </summary>
    public class FileHelper
    {
        #region 检测指定目录是否存在
        /// <summary> 
        /// 检测指定目录是否存在 
        /// </summary> 
        /// <param name="directoryPath">目录的绝对路径</param>         
        public static bool IsExistDirectory(string directoryPath)
        {
            return Directory.Exists(directoryPath);
        }
        #endregion

        #region 检测指定文件是否存在
        /// <summary> 
        /// 检测指定文件是否存在,如果存在则返回true。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static bool IsExistFile(string filePath)
        {
            return File.Exists(filePath);
        }
        #endregion

        #region 检测指定目录是否为空
        /// <summary> 
        /// 检测指定目录是否为空 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static bool IsEmptyDirectory(string directoryPath)
        {
            try
            {
                //判断是否存在文件 
                string[] fileNames = GetFileNames(directoryPath);
                if (fileNames.Length > 0)
                {
                    return false;
                }
                //判断是否存在文件夹 
                string[] directoryNames = GetDirectories(directoryPath);
                if (directoryNames.Length > 0)
                {
                    return false;
                }
                return true;
            }
            catch (Exception)
            {
                return true;
            }
        }
        #endregion

        #region 检测指定目录中是否存在指定的文件
        /// <summary> 
        /// 检测指定目录中是否存在指定的文件,若要搜索子目录请使用重载方法. 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>         
        public static bool Contains(string directoryPath, string searchPattern)
        {
            try
            {
                //获取指定的文件列表 
                string[] fileNames = GetFileNames(directoryPath, searchPattern, false);
                //判断指定文件是否存在 
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary> 
        /// 检测指定目录中是否存在指定的文件 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param>  
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static bool Contains(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                //获取指定的文件列表 
                string[] fileNames = GetFileNames(directoryPath, searchPattern, true);
                //判断指定文件是否存在 
                if (fileNames.Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        #endregion

        #region 创建一个目录
        /// <summary> 
        /// 创建一个目录 
        /// </summary> 
        /// <param name="directoryPath">目录的绝对路径</param> 
        public static void CreateDirectory(string directoryPath)
        {
            //如果目录不存在则创建该目录 
            if (!IsExistDirectory(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }
        }
        #endregion

        #region 创建一个文件
        /// <summary> 
        /// 创建一个文件。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void CreateFile(string filePath)
        {
            try
            {
                //如果文件不存在则创建该文件 
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象 
                    FileInfo file = new FileInfo(filePath);
                    //创建文件 
                    FileStream fs = file.Create();
                    //关闭文件流 
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog( TraceLogLevel.Error, ex.Message ); 
                throw ex;
            }
        }
        /// <summary> 
        /// 创建一个文件,并将字节流写入文件。 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="buffer">二进制流数据</param> 
        public static void CreateFile(string filePath, byte[] buffer)
        {
            try
            {
                //如果文件不存在则创建该文件 
                if (!IsExistFile(filePath))
                {
                    //创建一个FileInfo对象 
                    FileInfo file = new FileInfo(filePath);
                    //创建文件 
                    FileStream fs = file.Create();
                    //写入二进制流 
                    fs.Write(buffer, 0, buffer.Length);
                    //关闭文件流 
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog( TraceLogLevel.Error, ex.Message ); 
                throw ex;
            }
        }
        #endregion

        #region 获取文本文件的行数
        /// <summary> 
        /// 获取文本文件的行数 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static int GetLineCount(string filePath)
        {
            //将文本文件的各行读到一个字符串数组中 
            string[] rows = File.ReadAllLines(filePath);
            //返回行数 
            return rows.Length;
        }
        #endregion

        #region 获取一个文件的长度
        /// <summary> 
        /// 获取一个文件的长度,单位为Byte 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static int GetFileSize(string filePath)
        {
            //创建一个文件对象 
            FileInfo fi = new FileInfo(filePath);
            //获取文件的大小 
            return (int)fi.Length;
        }
        /// <summary> 
        /// 获取一个文件的长度,单位为KB 
        /// </summary> 
        /// <param name="filePath">文件的路径</param>         
        //public static double GetFileSizeByKB( string filePath ) 
        //{ 
        //    //创建一个文件对象 
        //    FileInfo fi = new FileInfo( filePath );            
        //    //获取文件的大小 
        //    return ConvertHelper.ToDouble( ConvertHelper.ToDouble( fi.Length ) / 1024 , 1 ); 
        //}
        /// <summary> 
        /// 获取一个文件的长度,单位为MB 
        /// </summary> 
        /// <param name="filePath">文件的路径</param>         
        //public static double GetFileSizeByMB( string filePath ) 
        //{ 
        //    //创建一个文件对象 
        //    FileInfo fi = new FileInfo( filePath );
        //    //获取文件的大小 
        //    return ConvertHelper.ToDouble( ConvertHelper.ToDouble( fi.Length ) / 1024 / 1024 , 1 ); 
        //} 
        #endregion

        #region 获取指定目录中的文件列表
        /// <summary> 
        /// 获取指定目录中所有文件列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static string[] GetFileNames(string directoryPath)
        {
            //如果目录不存在，则抛出异常 
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            //获取文件列表 
            return Directory.GetFiles(directoryPath);
        }
        /// <summary> 
        /// 获取指定目录及子目录中所有文件列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetFileNames(string directoryPath, string searchPattern, bool isSearchChild)
        {
            //如果目录不存在，则抛出异常 
            if (!IsExistDirectory(directoryPath))
            {
                throw new FileNotFoundException();
            }
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetFiles(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取指定目录中的子目录列表
        /// <summary> 
        /// 获取指定目录中所有子目录列表,若要搜索嵌套的子目录列表,请使用重载方法. 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param>         
        public static string[] GetDirectories(string directoryPath)
        {
            try
            {
                return Directory.GetDirectories(directoryPath);
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        /// <summary> 
        /// 获取指定目录及子目录中所有子目录列表 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        /// <param name="searchPattern">模式字符串，"*"代表0或N个字符，"?"代表1个字符。 
        /// 范例："Log*.xml"表示搜索所有以Log开头的Xml文件。</param> 
        /// <param name="isSearchChild">是否搜索子目录</param> 
        public static string[] GetDirectories(string directoryPath, string searchPattern, bool isSearchChild)
        {
            try
            {
                if (isSearchChild)
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.AllDirectories);
                }
                else
                {
                    return Directory.GetDirectories(directoryPath, searchPattern, SearchOption.TopDirectoryOnly);
                }
            }
            catch (IOException ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 向文本文件写入内容
        /// <summary> 
        /// 向文本文件中写入内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param>         
        public static void WriteText(string filePath, string content)
        {
            //向文件写入内容 
            File.WriteAllText(filePath, content);
        }
        #endregion

        #region 向文本文件的尾部追加内容
        /// <summary> 
        /// 向文本文件的尾部追加内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="content">写入的内容</param> 
        public static void AppendText(string filePath, string content)
        {
            File.AppendAllText(filePath, content);
        }
        #endregion

        #region 将现有文件的内容复制到新文件中
        /// <summary> 
        /// 将源文件的内容复制到目标文件中 
        /// </summary> 
        /// <param name="sourceFilePath">源文件的绝对路径</param> 
        /// <param name="destFilePath">目标文件的绝对路径</param> 
        public static void Copy(string sourceFilePath, string destFilePath)
        {
            File.Copy(sourceFilePath, destFilePath, true);
        }
        #endregion

        #region 将文件移动到指定目录
        /// <summary> 
        /// 将文件移动到指定目录 
        /// </summary> 
        /// <param name="sourceFilePath">需要移动的源文件的绝对路径</param> 
        /// <param name="descDirectoryPath">移动到的目录的绝对路径</param> 
        public static void Move(string sourceFilePath, string descDirectoryPath)
        {
            //获取源文件的名称 
            string sourceFileName = GetFileName(sourceFilePath);
            if (IsExistDirectory(descDirectoryPath))
            {
                //如果目标中存在同名文件,则删除 
                if (IsExistFile(descDirectoryPath + "\\" + sourceFileName))
                {
                    DeleteFile(descDirectoryPath + "\\" + sourceFileName);
                }
                //将文件移动到指定目录 
                File.Move(sourceFilePath, descDirectoryPath + "\\" + sourceFileName);
            }
        }
        #endregion

        #region 将流读取到缓冲区中
        /// <summary> 
        /// 将流读取到缓冲区中 
        /// </summary> 
        /// <param name="stream">原始流</param> 
        //public static byte[] StreamToBytes( Stream stream ) 
        //{ 
        //    try 
        //    { 
        //        //创建缓冲区 
        //        byte[] buffer = new byte[stream.Length];
        //        //读取流 
        //        stream.Read( buffer, 0, ConvertHelper.ToInt32( stream.Length ) );
        //        //返回流 
        //        return buffer; 
        //    } 
        //    catch ( Exception ex ) 
        //    { 
        //        LogHelper.WriteTraceLog( TraceLogLevel.Error, ex.Message ); 
        //        throw ex; 
        //    } 
        //    finally 
        //    { 
        //        //关闭流 
        //        stream.Close(); 
        //    } 
        //} 
        #endregion

        #region 将文件读取到缓冲区中
        /// <summary> 
        /// 将文件读取到缓冲区中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static byte[] FileToBytes(string filePath)
        {
            //获取文件的大小  
            int fileSize = GetFileSize(filePath);
            //创建一个临时缓冲区 
            byte[] buffer = new byte[fileSize];
            //创建一个文件流 
            FileInfo fi = new FileInfo(filePath);
            FileStream fs = fi.Open(FileMode.Open);
            try
            {
                //将文件流读入缓冲区 
                fs.Read(buffer, 0, fileSize);
                return buffer;
            }
            catch (IOException ex)
            {
                //LogHelper.WriteTraceLog( TraceLogLevel.Error, ex.Message ); 
                throw ex;
            }
            finally
            {
                //关闭文件流 
                fs.Close();
            }
        }
        #endregion

        #region 将文件读取到字符串中
        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        //public static string FileToString( string filePath ) 
        //{ 
        //    return FileToString( filePath, BaseInfo.DefaultEncoding ); 
        //} 
        /// <summary> 
        /// 将文件读取到字符串中 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        /// <param name="encoding">字符编码</param> 
        public static string FileToString(string filePath, Encoding encoding)
        {
            //创建流读取器 
            StreamReader reader = new StreamReader(filePath, encoding);
            try
            {
                //读取流 
                return reader.ReadToEnd();
            }
            catch (Exception ex)
            {
                //LogHelper.WriteTraceLog( TraceLogLevel.Error, ex.Message ); 
                throw ex;
            }
            finally
            {
                //关闭流读取器 
                reader.Close();
            }
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 包含扩展名 )
        /// <summary> 
        /// 从文件的绝对路径中获取文件名( 包含扩展名 ) 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetFileName(string filePath)
        {
            //获取文件的名称 
            FileInfo fi = new FileInfo(filePath);
            return fi.Name;
        }
        #endregion

        #region 从文件的绝对路径中获取文件名( 不包含扩展名 )
        /// <summary> 
        /// 从文件的绝对路径中获取文件名( 不包含扩展名 ) 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetFileNameNoExtension(string filePath)
        {
            //获取文件的名称 
            FileInfo fi = new FileInfo(filePath);
            return fi.Name.Split('.')[0];
        }
        #endregion

        #region 从文件的绝对路径中获取扩展名
        /// <summary> 
        /// 从文件的绝对路径中获取扩展名 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param>         
        public static string GetExtension(string filePath)
        {
            //获取文件的名称 
            FileInfo fi = new FileInfo(filePath);
            return fi.Extension;
        }
        #endregion

        #region 从文件的绝对路径中获取扩展名( 小写，不带"." )
        /// <summary>
        /// 从文件的绝对路径中获取扩展名( 小写，不带"." )
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string GetExtensionName(string filePath)
        {
            int idx = filePath.LastIndexOf('.') + 1;

            return idx > 0 ? filePath.Substring(idx).ToLower() : string.Empty;
        }
        #endregion

        #region 清空指定目录
        /// <summary> 
        /// 清空指定目录下所有文件及子目录,但该目录依然保存. 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static void ClearDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                //删除目录中所有的文件 
                string[] fileNames = GetFileNames(directoryPath);
                for (int i = 0; i < fileNames.Length; i++)
                {
                    DeleteFile(fileNames[i]);
                }
                //删除目录中所有的子目录 
                string[] directoryNames = GetDirectories(directoryPath);
                for (int i = 0; i < directoryNames.Length; i++)
                {
                    DeleteDirectory(directoryNames[i]);
                }
            }
        }
        #endregion

        #region 清空文件内容
        /// <summary> 
        /// 清空文件内容 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void ClearFile(string filePath)
        {
            //删除文件 
            File.Delete(filePath);
            //重新创建该文件 
            CreateFile(filePath);
        }
        #endregion

        #region 删除指定文件
        /// <summary> 
        /// 删除指定文件 
        /// </summary> 
        /// <param name="filePath">文件的绝对路径</param> 
        public static void DeleteFile(string filePath)
        {
            if (IsExistFile(filePath))
            {
                File.Delete(filePath);
            }
        }
        #endregion

        #region 删除指定目录
        /// <summary> 
        /// 删除指定目录及其所有子目录 
        /// </summary> 
        /// <param name="directoryPath">指定目录的绝对路径</param> 
        public static void DeleteDirectory(string directoryPath)
        {
            if (IsExistDirectory(directoryPath))
            {
                Directory.Delete(directoryPath, true);
            }
        }
        #endregion

        /***********************************************************************************
         * 
         * File Helper 2
         * 
         * *********************************************************************************/

        private bool _alreadyDispose = false;

        #region 构造函数
        public FileHelper()
        {
            //
            // TODO: 在此处添加构造函数逻辑
            //
        }

        ~FileHelper()
        {
            Dispose(); ;
        }

        protected virtual void Dispose(bool isDisposing)
        {
            if (_alreadyDispose) return;
            //if (isDisposing)
            //{
            //     if (xml != null)
            //     {
            //         xml = null;
            //     }
            //}
            _alreadyDispose = true;
        }
        #endregion

        #region IDisposable 成员

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

        #region 取得文件后缀名
        /****************************************
          * 函数名称：GetPostfixStr
          * 功能说明：取得文件后缀名
          * 参     数：filename:文件名称
          * 调用示列：
          *            string filename = "aaa.aspx";        
          *            string s = EC.FileObj.GetPostfixStr(filename);         
         *****************************************/
        /// <summary>
        /// 取后缀名
        /// </summary>
        /// <param name="filename">文件名</param>
        /// <returns>.gif|.html格式</returns>
        public static string GetPostfixStr(string filename)
        {
            int start = filename.LastIndexOf(".");
            int length = filename.Length;
            string postfix = filename.Substring(start, length - start);
            return postfix;
        }
        #endregion

        #region 写文件
        /****************************************
          * 函数名称：WriteFile
          * 功能说明：写文件,会覆盖掉以前的内容
          * 参     数：Path:文件路径,Strings:文本内容
          * 调用示列：
          *            string Path = Server.MapPath("Default2.aspx");       
          *            string Strings = "这是我写的内容啊";
          *            EC.FileObj.WriteFile(Path,Strings);
         *****************************************/
        /// <summary>
        /// 写文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="Strings">文件内容</param>
        public static void WriteFile(string Path, string Strings)
        {
            if (!System.IO.File.Exists(Path))
            {
                System.IO.FileStream f = System.IO.File.Create(Path);
                f.Close();
            }
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(Path, false, System.Text.Encoding.GetEncoding("gb2312"));
            f2.Write(Strings);
            f2.Close();
            f2.Dispose();
        }
        #endregion
        
        #region 写文件
        /****************************************
          * 函数名称：WriteFile
          * 功能说明：写文件,会覆盖掉以前的内容
          * 参     数：dPath:文件路径,FileName:文件名称,Strings:文本内容
          * 调用示列：
          *            string dPath = Server.MapPath;       
          *            string FileName = "Default2.aspx";
          *            string strText = "这是我写的内容啊";
         *             string Encoding = "UTF-8";
          *            WriteFile(dPath,FileName,strText,Encoding);
         *****************************************/
        /// <summary>
        /// 传入文件夹路径、文件名称和文件内容、文件编码写文件
        /// </summary>
        /// <param name="dPath">文件路径</param>
        /// <param name="dPath">文件名称</param>
        /// <param name="strText">文件内容</param>
        /// <param name="Encoding">文件编码</param>
        public static void WriteFile(string dPath,string FileName,string strText,string Encoding)
        {
            CreateDirectory(dPath);  //创建文件夹
            string fileFullPath = dPath + FileName;  //加入/以防路径最后不带/
            //判断并创建文件
            if (!System.IO.File.Exists(fileFullPath))
            {
                System.IO.FileStream f = System.IO.File.Create(fileFullPath);
                f.Close();
            }
            //为编码设定默认为GB2312
            if (Encoding == "" || string.IsNullOrEmpty(Encoding))
            {
                Encoding = "GB2312";
            }
            //写入内容
            System.IO.StreamWriter f2 = new System.IO.StreamWriter(fileFullPath, false, System.Text.Encoding.GetEncoding(Encoding));
            f2.Write(strText);
            f2.Close();
            f2.Dispose();
        }
        #endregion

        #region 读文件
        /****************************************
          * 函数名称：ReadFile
          * 功能说明：读取文本内容
          * 参     数：Path:文件路径
          * 调用示列：
          *            string Path = Server.MapPath("Default2.aspx");       
          *            string s = EC.FileObj.ReadFile(Path);
         *****************************************/
        /// <summary>
        /// 读文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <returns></returns>
        public static string ReadFile(string Path)
        {
            string s = "";
            if (!System.IO.File.Exists(Path))
                s = "不存在相应的目录";
            else
            {
                StreamReader f2 = new StreamReader(Path, System.Text.Encoding.GetEncoding("gb2312"));
                s = f2.ReadToEnd();
                f2.Close();
                f2.Dispose();
            }

            return s;
        }
        #endregion

        #region 追加文件
        /****************************************
          * 函数名称：FileAdd
          * 功能说明：追加文件内容
          * 参     数：Path:文件路径,strings:内容
          * 调用示列：
          *            string Path = Server.MapPath("Default2.aspx");     
          *            string Strings = "新追加内容";
          *            EC.FileObj.FileAdd(Path, Strings);
         *****************************************/
        /// <summary>
        /// 追加文件
        /// </summary>
        /// <param name="Path">文件路径</param>
        /// <param name="strings">内容</param>
        public static void FileAdd(string Path, string strings)
        {
            StreamWriter sw = File.AppendText(Path);
            sw.Write(strings);
            sw.Flush();
            sw.Close();
        }
        #endregion

        #region 拷贝文件
        /****************************************
          * 函数名称：FileCoppy
          * 功能说明：拷贝文件
          * 参     数：OrignFile:原始文件,NewFile:新文件路径
          * 调用示列：
          *            string orignFile = Server.MapPath("Default2.aspx");     
          *            string NewFile = Server.MapPath("Default3.aspx");
          *            EC.FileObj.FileCoppy(OrignFile, NewFile);
         *****************************************/
        /// <summary>
        /// 拷贝文件
        /// </summary>
        /// <param name="OrignFile">原始文件</param>
        /// <param name="NewFile">新文件路径</param>
        public static void FileCoppy(string orignFile, string NewFile)
        {
            File.Copy(orignFile, NewFile, true);
        }

        #endregion

        #region 删除文件
        /****************************************
          * 函数名称：FileDel
          * 功能说明：删除文件
          * 参     数：Path:文件路径
          * 调用示列：
          *            string Path = Server.MapPath("Default3.aspx");    
          *            EC.FileObj.FileDel(Path);
         *****************************************/
        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="Path">路径</param>
        public static void FileDel(string Path)
        {
            File.Delete(Path);
        }
        #endregion

        #region 移动文件
        /****************************************
          * 函数名称：FileMove
          * 功能说明：移动文件
          * 参     数：OrignFile:原始路径,NewFile:新文件路径
          * 调用示列：
          *             string orignFile = Server.MapPath("../说明.txt");    
          *             string NewFile = Server.MapPath("../../说明.txt");
          *             EC.FileObj.FileMove(OrignFile, NewFile);
         *****************************************/
        /// <summary>
        /// 移动文件
        /// </summary>
        /// <param name="OrignFile">原始路径</param>
        /// <param name="NewFile">新路径</param>
        public static void FileMove(string orignFile, string NewFile)
        {
            File.Move(orignFile, NewFile);
        }
        #endregion

        #region 在当前目录下创建目录
        /****************************************
          * 函数名称：FolderCreate
          * 功能说明：在当前目录下创建目录
          * 参     数：OrignFolder:当前目录,NewFloder:新目录
          * 调用示列：
          *            string orignFolder = Server.MapPath("test/");    
          *            string NewFloder = "new";
          *            EC.FileObj.FolderCreate(OrignFolder, NewFloder);
         *****************************************/
        /// <summary>
        /// 在当前目录下创建目录
        /// </summary>
        /// <param name="OrignFolder">当前目录</param>
        /// <param name="NewFloder">新目录</param>
        public static void FolderCreate(string orignFolder, string NewFloder)
        {
            Directory.SetCurrentDirectory(orignFolder);
            Directory.CreateDirectory(NewFloder);
        }
        #endregion

        #region 递归删除文件夹目录及文件
        /****************************************
          * 函数名称：DeleteFolder
          * 功能说明：递归删除文件夹目录及文件
          * 参     数：dir:文件夹路径
          * 调用示列：
          *            string dir = Server.MapPath("test/");  
          *            EC.FileObj.DeleteFolder(dir);       
         *****************************************/
        /// <summary>
        /// 递归删除文件夹目录及文件
        /// </summary>
        /// <param name="dir"></param>
        /// <returns></returns>
        public static void DeleteFolder(string dir)
        {
            if (Directory.Exists(dir)) //如果存在这个文件夹删除之
            {
                foreach (string d in Directory.GetFileSystemEntries(dir))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件
                    else
                        DeleteFolder(d); //递归删除子文件夹
                }
                Directory.Delete(dir); //删除已空文件夹
            }

        }

        #endregion

        #region 将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
        /****************************************
          * 函数名称：CopyDir
          * 功能说明：将指定文件夹下面的所有内容copy到目标文件夹下面 果目标文件夹为只读属性就会报错。
          * 参     数：srcPath:原始路径,aimPath:目标文件夹
          * 调用示列：
          *            string srcPath = Server.MapPath("test/");  
          *            string aimPath = Server.MapPath("test1/");
          *            EC.FileObj.CopyDir(srcPath,aimPath);   
         *****************************************/
        /// <summary>
        /// 指定文件夹下面的所有内容copy到目标文件夹下面
        /// </summary>
        /// <param name="srcPath">原始路径</param>
        /// <param name="aimPath">目标文件夹</param>
        public static void CopyDir(string srcPath, string aimPath)
        {
            try
            {
                // 检查目标目录是否以目录分割字符结束如果不是则添加之
                if (aimPath[aimPath.Length - 1] != Path.DirectorySeparatorChar)
                    aimPath += Path.DirectorySeparatorChar;
                // 判断目标目录是否存在如果不存在则新建之
                if (!Directory.Exists(aimPath))
                    Directory.CreateDirectory(aimPath);
                // 得到源目录的文件列表，该里面是包含文件以及目录路径的一个数组
                //如果你指向copy目标文件下面的文件而不包含目录请使用下面的方法
                //string[] fileList = Directory.GetFiles(srcPath);
                string[] fileList = Directory.GetFileSystemEntries(srcPath);
                //遍历所有的文件和目录
                foreach (string file in fileList)
                {
                    //先当作目录处理如果存在这个目录就递归Copy该目录下面的文件

                    if (Directory.Exists(file))
                        CopyDir(file, aimPath + Path.GetFileName(file));
                    //否则直接Copy文件
                    else
                        File.Copy(file, aimPath + Path.GetFileName(file), true);
                }

            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
        }


        #endregion

    }
}