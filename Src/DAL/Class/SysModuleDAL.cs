
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysModuleDAL(SysModuleDAL)
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
using System.Collections.Generic;

using Hope.ITMS.Model;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// DataProvider SysModuleDAL
    /// </summary>
    public class SysModuleDAL : BaseDAL<SysModule>
    {
        /// <summary>
        /// SysModuleDAL
        /// </summary>
        public SysModuleDAL()
        {
        }

        /// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysModule GetData(string value)
        {
            return GetDataByProperty("ModuleName", value);
        }

        /// <summary>
        /// 根据ParentID获取全部记录 
        /// </summary>
        /// <param name="ParentID">类型: int, HopeCMS.Model.PermissionModule.ModuleID</param>
        /// <returns>Hope.Model.PermissionModule 实例列表</returns>
        public List<SysModule> GetListByParentID(int value)
        {
            return GetDatasByProperty("ParentID", value);
        }
    }
}


