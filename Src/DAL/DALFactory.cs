/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
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

namespace Hope.ITMS.DAL
{
    public class DALFactory
    {
        #region 构造函数
        private static DALFactory theInstance = null;
        private DALFactory() { }
        /// <summary>
        /// 获得DALFactory的单例
        /// </summary>
        /// <returns>DALFactory</returns>
        public static DALFactory Instance()
        {
            if (theInstance == null)
            {
                theInstance = new DALFactory();
            }
            return theInstance;
        }
        #endregion

        #region Properties Delcaration

        private CommonFieldDAL _CommonFieldDAL = new CommonFieldDAL();
        private CommonModelDAL _CommonModelDAL = new CommonModelDAL();
        private OrgGroupDAL _OrgGroupDAL = new OrgGroupDAL();
        private OrgUserDAL _OrgUserDAL = new OrgUserDAL();
        private SysAdminDAL _SysAdminDAL = new SysAdminDAL();
        private SysFunctionDAL _SysFunctionDAL = new SysFunctionDAL();
        private SysLogDAL _SysLogDAL = new SysLogDAL();
        private SysModuleDAL _SysModuleDAL = new SysModuleDAL();
        private SysPermissionDAL _SysPermissionDAL = new SysPermissionDAL();
        private SysRoleDAL _SysRoleDAL = new SysRoleDAL();
        private SysTagDAL _SysTagDAL = new SysTagDAL();
        private TaskTaskDAL _TaskTaskDAL = new TaskTaskDAL();
        private TaskTaskHandlerDAL _TaskTaskHandlerDAL = new TaskTaskHandlerDAL();
        private TaskTaskTypeDAL _TaskTaskTypeDAL = new TaskTaskTypeDAL();
        private DevDeviceDAL _DevDeviceDAL = new DevDeviceDAL();
        private DevReservationDAL _DevReservationDAL = new DevReservationDAL();
        private UsrUserDAL _UsrUserDAL = new UsrUserDAL();

        #region virtual
        #endregion...
        #endregion

        #region Properties Implemetation


        /// <summary>
        /// 返回CommonFieldDAL实例
        /// </summary>
        public CommonFieldDAL CommonFieldDAL
        {
            get
            {
                return _CommonFieldDAL;
            }
        }


        /// <summary>
        /// 返回CommonModelDAL实例
        /// </summary>
        public CommonModelDAL CommonModelDAL
        {
            get
            {
                return _CommonModelDAL;
            }
        }


        /// <summary>
        /// 返回OrgGroupDAL实例
        /// </summary>
        public OrgGroupDAL OrgGroupDAL
        {
            get
            {
                return _OrgGroupDAL;
            }
        }


        /// <summary>
        /// 返回OrgUserDAL实例
        /// </summary>
        public OrgUserDAL OrgUserDAL
        {
            get
            {
                return _OrgUserDAL;
            }
        }


        /// <summary>
        /// 返回SysAdminDAL实例
        /// </summary>
        public SysAdminDAL SysAdminDAL
        {
            get
            {
                return _SysAdminDAL;
            }
        }


        /// <summary>
        /// 返回SysFunctionDAL实例
        /// </summary>
        public SysFunctionDAL SysFunctionDAL
        {
            get
            {
                return _SysFunctionDAL;
            }
        }


        /// <summary>
        /// 返回SysLogDAL实例
        /// </summary>
        public SysLogDAL SysLogDAL
        {
            get
            {
                return _SysLogDAL;
            }
        }


        /// <summary>
        /// 返回SysModuleDAL实例
        /// </summary>
        public SysModuleDAL SysModuleDAL
        {
            get
            {
                return _SysModuleDAL;
            }
        }


        /// <summary>
        /// 返回SysPermissionDAL实例
        /// </summary>
        public SysPermissionDAL SysPermissionDAL
        {
            get
            {
                return _SysPermissionDAL;
            }
        }


        /// <summary>
        /// 返回SysRoleDAL实例
        /// </summary>
        public SysRoleDAL SysRoleDAL
        {
            get
            {
                return _SysRoleDAL;
            }
        }


        /// <summary>
        /// 返回SysTagDAL实例
        /// </summary>
        public SysTagDAL SysTagDAL
        {
            get
            {
                return _SysTagDAL;
            }
        }


        /// <summary>
        /// 返回TaskTaskDAL实例
        /// </summary>
        public TaskTaskDAL TaskTaskDAL
        {
            get
            {
                return _TaskTaskDAL;
            }
        }


        /// <summary>
        /// 返回TaskTaskHandlerDAL实例
        /// </summary>
        public TaskTaskHandlerDAL TaskTaskHandlerDAL
        {
            get
            {
                return _TaskTaskHandlerDAL;
            }
        }


        /// <summary>
        /// 返回TaskTaskTypeDAL实例
        /// </summary>
        public TaskTaskTypeDAL TaskTaskTypeDAL
        {
            get
            {
                return _TaskTaskTypeDAL;
            }
        }


        /// <summary>
        /// 返回DevDevice实例
        /// </summary>
        public DevDeviceDAL DevDeviceDAL
        {
            get
            {
                return _DevDeviceDAL;
            }
        }

        /// <summary>
        /// 返回DevReservation实例
        /// </summary>
        public DevReservationDAL DevReservationDAL
        {
            get
            {
                return _DevReservationDAL;
            }
        }

        #region virtual
        #endregion...

        #endregion

        /// <summary>
        /// 返回UsrUser实例
        /// </summary>
        public UsrUserDAL UsrUserDAL
        {
            get
            {
                return _UsrUserDAL;
            }
        }

        #region virtual
        #endregion...

       
    }
}



