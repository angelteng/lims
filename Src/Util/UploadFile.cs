using System;

namespace Hope.Util
{
    /// <summary>
    /// �ļ��ϴ���
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
        /// ���캯��
        /// </summary>
        public UploadFile() { }

        /// <summary>
        /// ���캯��
        /// </summary>
        public UploadFile(UploadErrorNumber errorNumber)
        {
            _ErrorNumber = errorNumber;
        }

        /// <summary>
        /// ���캯��
        /// </summary>
        public UploadFile(string name, UploadErrorNumber errorNumber)
        {
            _Name = name;
            _ErrorNumber = errorNumber;
        }

        #endregion

        #region Properties

        /// <summary>
        /// �ļ�/����
        /// </summary>
        public virtual string Name
        {
            set { _Name = value; }
            get { return _Name; }
        }

        /// <summary>
        /// �ļ�·��
        /// </summary>
        public virtual string Url
        {
            set { _Url = value; }
            get { return _Url; }
        }

        /// <summary>
        /// �ļ���С
        /// </summary>
        public virtual Decimal Size
        {
            set { _Size = value; }
            get { return _Size; }
        }

        /// <summary>
        /// �ļ���׺
        /// </summary>
        public virtual string Extension
        {
            set { _Extension = value; }
            get { return _Extension; }
        }

        /// <summary>
        /// ���ļ������ļ���
        /// </summary>
        public virtual bool IsFile
        {
            set { _IsFile = value; }
            get { return _IsFile; }
        }

        /// <summary>
        /// �������
        /// </summary>
        public virtual UploadErrorNumber ErrorNumber
        {
            set { _ErrorNumber = value; }
            get { return _ErrorNumber; }
        }

        /// <summary>
        /// �ļ��߶�
        /// </summary>
        public virtual int Height
        {
            set { _Height = value; }
            get { return _Height; }
        }

        /// <summary>
        /// �ļ���ȶ�
        /// </summary>
        public virtual int Width
        {
            set { _Width = value; }
            get { return _Width; }
        }

        /// <summary>
        /// �ļ�����
        /// </summary>
        public virtual string Description
        {
            set { _Description = value; }
            get { return _Description; }
        }

        #endregion

    }

    /// <summary>
    /// �ϴ��������
    /// </summary>
    public enum UploadErrorNumber
    {
        /// <summary>
        /// �޴���
        /// </summary>
        NoError = 0,

        /// <summary>
        /// �ϴ�������
        /// </summary>
        Disabled = 1,

        /// <summary>
        /// �ϴ��ļ���Ч
        /// </summary>
        InvalidFile = 10,

        /// <summary>
        /// �ļ�����Ч
        /// </summary>
        InvalidFolder = 11,

        /// <summary>
        /// �����͵��ļ��������ϴ�
        /// </summary>
        FileNotAllowed = 12,

        /// <summary>
        /// �ϴ��ļ�̫�󣬳����趨�����ֵ
        /// </summary>
        FileTooLarge = 13,

        /// <summary>
        /// �����ļ�����Ȩ��
        /// </summary>
        NoPermission = 20,

        /// <summary>
        /// δ֪����
        /// </summary>
        UnknownError = 99,
    }

    /// <summary>
    /// �ϴ�������Ϣ
    /// </summary>
    public enum UploadErrorMessage
    {
        /// <summary>
        /// �޴���
        /// </summary>
        �޴��� = 0,

        /// <summary>
        /// �ϴ�������
        /// </summary>
        �ϴ������� = 1,

        /// <summary>
        /// �ϴ��ļ���Ч
        /// </summary>
        �ϴ��ļ���Ч = 10,

        /// <summary>
        /// �ļ�����Ч
        /// </summary>
        �ļ�����Ч = 11,

        /// <summary>
        /// �����͵��ļ��������ϴ�
        /// </summary>
        �����͵��ļ��������ϴ� = 12,

        /// <summary>
        /// �ϴ��ļ�̫�󣬳����趨�����ֵ
        /// </summary>
        �ϴ��ļ�̫�� = 13,

        /// <summary>
        /// �����ļ�����Ȩ��
        /// </summary>
        �����ļ�����Ȩ�� = 20,

        /// <summary>
        /// δ֪����
        /// </summary>
        δ֪���� = 99,
    }
}
