using System;
using System.Collections.Generic;
using System.Text;

using Hope.ITMS.DAL;
using Hope.ITMS.Model;
using Hope.Util;

namespace Hope.ITMS.BLL
{
    /// <summary>
    /// 业务逻辑基类
    /// </summary>
    public abstract class BaseBLL
    {
        /// <summary>
        /// bll 基类
        /// </summary>
        public BaseBLL()
        {

        }

        #region HandlerMessage
        private SystemMessage _HandlerMessage;
        /// <summary>
        /// 系统信息，用于输出执行时的结果信息
        /// </summary>
        public SystemMessage HandlerMessage
        {
            set { _HandlerMessage = value; }
            get
            {
                if (_HandlerMessage == null)
                {
                    _HandlerMessage = new SystemMessage();
                }
                return _HandlerMessage;
            }
        }
        #endregion
    }
}
