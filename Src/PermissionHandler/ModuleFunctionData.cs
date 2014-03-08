using System;
using System.Collections.Generic;
using System.Text;

namespace Hope.PermissionHandler
{
    /// <summary>
    /// 模块功能数据
    /// </summary>
    public class ModuleFunctionData
    {
        #region Declaration
        private int _ModuleID = -1;
        private string _ModuleName = string.Empty;
        private string _FunctionName = string.Empty;
        private string _FunctionKey = string.Empty;
        private int _FunctionID = -1;
        private string _FunctionUrl = string.Empty;
        private int _Value = -1;
        private bool _AdminPage = false;
        #endregion

        #region Properties
        /// <summary>
        /// 模块ID
        /// </summary>
        public virtual int ModuleID
        {
            set { _ModuleID = value; }
            get { return _ModuleID; }
        }

        /// <summary>
        /// 模块名
        /// </summary>
        public virtual string ModuleName
        {
            set { _ModuleName = value; }
            get { return _ModuleName; }
        }

        /// <summary>
        /// 功能名
        /// </summary>
        public virtual string FunctionName
        {
            set { _FunctionName = value; }
            get { return _FunctionName; }
        }

        /// <summary>
        /// 功能键
        /// </summary>
        public virtual string FunctionKey
        {
            set { _FunctionKey = value; }
            get { return _FunctionKey; }
        }

        /// <summary>
        /// 功能ID
        /// </summary>
        public virtual int FunctionID
        {
            set { _FunctionID = value; }
            get { return _FunctionID; }
        }

        /// <summary>
        /// 功能URL
        /// </summary>
        public virtual string FunctionUrl
        {
            set { _FunctionUrl = value; }
            get { return _FunctionUrl; }
        }

        /// <summary>
        /// 功能值
        /// </summary>
        public virtual int Value
        {
            set { _Value = value; }
            get { return _Value; }
        }

        /// <summary>
        /// 是否管理页面
        /// </summary>
        public virtual bool AdminPage
        {
            set { _AdminPage = value; }
            get { return _AdminPage; }
        }

        #endregion
    }
}
