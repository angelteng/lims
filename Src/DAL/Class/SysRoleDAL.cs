
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysRoleDAL(SysRoleDAL)
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

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// DataProvider SysRoleDAL
    /// </summary>
    public class SysRoleDAL : BaseDAL<SysRole>
    {
        /// <summary>
        /// SysRoleDAL
        /// </summary>
        public SysRoleDAL()
        {
        }

        /// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysRole GetData(string value)
        {
            return GetDataByProperty("Name", value);
        }
    }
}


