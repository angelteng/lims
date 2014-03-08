using System;

namespace Hope.Util
{
    /// <summary>
    /// 文件上传类
    /// </summary>
    public partial class UploadFile
    {

        #region Declaration

        private string _Name = String.Empty;
        private string _Url = String.Empty;
        private Decimal _Size = 0;
        private string _Extension = "";
        private bool _IsFile = true;
        private UploadErrorNumber _ErrorNumber = UploadErrorNumber.NoError;
        private int _Height = 0;
        private int _Width = 0;
        private string _Description = string.Empty;

        #endregion

        #region Constructor

        /// <summary>
        /// 构造函数
        /// </summary>
        public UploadFile() { }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UploadFile(UploadErrorNumber errorNumber)
        {
            _ErrorNumber = errorNumber;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        public UploadFile(string name, UploadErrorNumber errorNumber)
        {
            _Name = name;
            _ErrorNumber = errorNumber;
        }

        #endregion

        #region Properties

        /// <summary>
        /// 文件/夹名
        /// </summary>
        public virtual string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }

        /// <summary>
        /// 文件路径
        /// </summary>
        public virtual string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }

        /// <summary>
        /// 文件大小
        /// </summary>
        public virtual Decimal Size
        {
            set { _Size = value; }
            get { return _Size; }
        }

        /// <summary>
        /// 文件后缀
        /// </summary>
        public virtual string Extension
        {
            set { _Extension = value; }
            get { return _Extension; }
        }

        /// <summary>
        /// 是文件或者文件夹
        /// </summary>
        public virtual bool IsFile
        {
            set { _IsFile = value; }
            get { return _IsFile; }
        }

        /// <summary>
        /// 错误代码
        /// </summary>
        public virtual UploadErrorNumber ErrorNumber
        {
            set { _ErrorNumber = value; }
            get { return _ErrorNumber; }
        }

        /// <summary>
        /// 文件高度
        /// </summary>
        public virtual int Height
        {
            set { _Height = value; }
            get { return _Height; }
        }

        /// <summary>
        /// 文件宽度度
        /// </summary>
        public virtual int Width
        {
            set { _Width = value; }
            get { return _Width; }
        }

        /// <summary>
        /// 文件描述
        /// </summary>
        public virtual string Description
        {
            set { _Description = value; }
            get { return _Description; }
        }

        #endregion

    }

    /// <summary>
    /// 上传错误代码
    /// </summary>
    public enum UploadErrorNumber
    {
        /// <summary>
        /// 无错误
        /// </summary>
        NoError = 0,

        /// <summary>
        /// 上传被禁用
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// 上传文件无效
        /// </summary>
        InvalidFile = 10,

        /// <summary>
        /// 文件夹无效
        /// </summary>
        InvalidFolder = 11,

        /// <summary>
        /// 该类型的文件不允许上传
        /// </summary>
        FileNotAllowed = 12,

        /// <summary>
        /// 上传文件太大，超过设定的最大值
        /// </summary>
        FileTooLarge = 13,

        /// <summary>
        /// 访问文件夹无权限
        /// </summary>
        NoPermission = 20,

        /// <summary>
        /// 未知错误
        /// </summary>
        UnknownError = 99,
    }

    /// <summary>
    /// 上传错误信息
    /// </summary>
    public enum UploadErrorMessage
    {
        /// <summary>
        /// 无错误
        /// </summary>
        无错误 = 0,

        /// <summary>
        /// 上传被禁用
        /// </summary>
        上传被禁用 = 1,

        /// <summary>
        /// 上传文件无效
        /// </summary>
        上传文件无效 = 10,

        /// <summary>
        /// 文件夹无效
        /// </summary>
        文件夹无效 = 11,

        /// <summary>
        /// 该类型的文件不允许上传
        /// </summary>
        该类型的文件不允许上传 = 12,

        /// <summary>
        /// 上传文件太大，超过设定的最大值
        /// </summary>
        上传文件太大 = 13,

        /// <summary>
        /// 访问文件夹无权限
        /// </summary>
        访问文件夹无权限 = 20,

        /// <summary>
        /// 未知错误
        /// </summary>
        未知错误 = 99,
    }
}
