
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysFunctionDAL(SysFunctionDAL)
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
using System.Collections.Generic;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// DataProvider SysFunctionDAL
    /// </summary>
    public class SysFunctionDAL : BaseDAL<SysFunction>
    {
        /// <summary>
        /// SysFunctionDAL
        /// </summary>
        public SysFunctionDAL()
        {
        }

        /// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysFunction GetData(string value)
        {
            return GetDataByProperty("FunctionName", value);
        }

        /// <summary>
        /// 获取ModuleID全部记录
        /// </summary>
        /// <param name="ModuleID">类型: int, HopeCMS.Model.PermissionFunction.ModuleID</param>
        /// <returns>HopeCMS.Model.PermissionFunction 实例列表</returns>
        public List<SysFunction> GetListByModuleID(int value)
        {
            return GetDatasByProperty("ModuleID", value);
        }
    }
}


