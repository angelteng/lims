/******************************************************************
 *
 * 所在模块：Business Logic Layer(业务逻辑层)
 * 类 名 称：BLLFactory
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-11-06
 * xingzhewujiang1999@gmail.com
 * QQ:970355214
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hope.ITMS.BLL
{
    public class BLLFactory
    {
        #region 构造函数
        private static BLLFactory theInstance = null;
        private BLLFactory() { }
        /// <summary>
        /// 获得BLLFactory的单例
        /// </summary>
        /// <returns>BLLFactory实例</returns>
        public static BLLFactory Instance()
        {
            if (theInstance == null)
            {
                theInstance = new BLLFactory();
            }
            return theInstance;
        }
        #endregion

        #region Properties Delcaration

        private CommonFieldBLL _CommonFieldBLL = new CommonFieldBLL();
        private CommonModelBLL _CommonModelBLL = new CommonModelBLL();
        private OrgGroupBLL _OrgGroupBLL = new OrgGroupBLL();
        private OrgUserBLL _OrgUserBLL = new OrgUserBLL();
        private SysAdminBLL _SysAdminBLL = new SysAdminBLL();
        private SysFunctionBLL _SysFunctionBLL = new SysFunctionBLL();
        private SysLogBLL _SysLogBLL = new SysLogBLL();
        private SysModuleBLL _SysModuleBLL = new SysModuleBLL();
        private SysPermissionBLL _SysPermissionBLL = new SysPermissionBLL();
        private SysRoleBLL _SysRoleBLL = new SysRoleBLL();
        private SysTagBLL _SysTagBLL = new SysTagBLL();
        private TaskTaskBLL _TaskTaskBLL = new TaskTaskBLL();
        private TaskTaskHandlerBLL _TaskTaskHandlerBLL = new TaskTaskHandlerBLL();
        private TaskTaskTypeBLL _TaskTaskTypeBLL = new TaskTaskTypeBLL();
        private DevDeviceBLL _DevDeviceBLL = new DevDeviceBLL();
        private DevReservationBLL _DevReservationBLL = new DevReservationBLL();
        private UsrUserBLL _UsrUserBLL = new UsrUserBLL();

        #region virtual
        #endregion...
        #endregion

        #region Properties Implemetation


        /// <summary>
        /// 返回CommonFieldBLL实例
        /// </summary>
        public CommonFieldBLL CommonFieldBLL
        {
            get
            {
                return _CommonFieldBLL;
            }
        }


        /// <summary>
        /// 返回CommonModelBLL实例
        /// </summary>
        public CommonModelBLL CommonModelBLL
        {
            get
            {
                return _CommonModelBLL;
            }
        }


        /// <summary>
        /// 返回OrgGroupBLL实例
        /// </summary>
        public OrgGroupBLL OrgGroupBLL
        {
            get
            {
                return _OrgGroupBLL;
            }
        }


        /// <summary>
        /// 返回OrgUserBLL实例
        /// </summary>
        public OrgUserBLL OrgUserBLL
        {
            get
            {
                return _OrgUserBLL;
            }
        }


        /// <summary>
        /// 返回SysAdminBLL实例
        /// </summary>
        public SysAdminBLL SysAdminBLL
        {
            get
            {
                return _SysAdminBLL;
            }
        }


        /// <summary>
        /// 返回SysFunctionBLL实例
        /// </summary>
        public SysFunctionBLL SysFunctionBLL
        {
            get
            {
                return _SysFunctionBLL;
            }
        }


        /// <summary>
        /// 返回SysLogBLL实例
        /// </summary>
        public SysLogBLL SysLogBLL
        {
            get
            {
                return _SysLogBLL;
            }
        }


        /// <summary>
        /// 返回SysModuleBLL实例
        /// </summary>
        public SysModuleBLL SysModuleBLL
        {
            get
            {
                return _SysModuleBLL;
            }
        }


        /// <summary>
        /// 返回SysPermissionBLL实例
        /// </summary>
        public SysPermissionBLL SysPermissionBLL
        {
            get
            {
                return _SysPermissionBLL;
            }
        }


        /// <summary>
        /// 返回SysRoleBLL实例
        /// </summary>
        public SysRoleBLL SysRoleBLL
        {
            get
            {
                return _SysRoleBLL;
            }
        }


        /// <summary>
        /// 返回SysTagBLL实例
        /// </summary>
        public SysTagBLL SysTagBLL
        {
            get
            {
                return _SysTagBLL;
            }
        }


        /// <summary>
        /// 返回TaskTaskBLL实例
        /// </summary>
        public TaskTaskBLL TaskTaskBLL
        {
            get
            {
                return _TaskTaskBLL;
            }
        }


        /// <summary>
        /// 返回TaskTaskHandlerBLL实例
        /// </summary>
        public TaskTaskHandlerBLL TaskTaskHandlerBLL
        {
            get
            {
                return _TaskTaskHandlerBLL;
            }
        }


        /// <summary>
        /// 返回TaskTaskTypeBLL实例
        /// </summary>
        public TaskTaskTypeBLL TaskTaskTypeBLL
        {
            get
            {
                return _TaskTaskTypeBLL;
            }
        }

        /// <summary>
        /// 返回DevDevice实例
        /// </summary>
        public DevDeviceBLL DevDeviceBLL
        {
            get
            {
                return _DevDeviceBLL;
            }
        }

        /// <summary>
        /// 返回DevReservation实例
        /// </summary>
        public DevReservationBLL DevReservationBLL
        {
            get
            {
                return _DevReservationBLL;
            }
        }

        #region virtual
        #endregion...

        #endregion

        /// <summary>
        /// 返回UsrUser实例
        /// </summary>
        public UsrUserBLL UsrUserBLL
        {
            get
            {
                return _UsrUserBLL;
            }
        }

        #region virtual
        #endregion...

       
    }
}



