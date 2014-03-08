
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysPermissionDAL(SysPermissionDAL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-05-23
 * wensaint@126.com
 * QQ:286661274
 * ------------编辑修改信息--------------
 * 作    者：
 * 日    期：
 * 内    容：
******************************************************************/
using System;

using Hope.ITMS.Model;
using NHibernate;
using Hope.Util;
using System.Collections.Generic;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// DataProvider SysPermissionDAL
    /// </summary>
    public class SysPermissionDAL : BaseDAL<SysPermission>
    {
        /// <summary>
        /// SysPermissionDAL
        /// </summary>
        public SysPermissionDAL()
        {
        }

        /// <summary>
        /// 更新数据列表
        /// </summary>
        /// <param name="list">类型: Model.SysPermission 实例列表</param>
        /// <returns>成功|失败</returns>
        public bool BatchUpdateRolePValues(List<SysPermission> list)
        {
            bool bResult = false;

            ISession session = NHibernateHelper.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();

            try
            {
                foreach (SysPermission data in list)
                {
                    session.Save(data);
                }
                trans.Commit();
                bResult = true;
            }
            catch (Exception e)
            {
                LogUtil.Write(typeof(SysPermission), e);
                trans.Rollback();
                throw;
            }
            return bResult;
        }        
    }
}


