
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysAdminDAL(SysAdminDAL)
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
    /// DataProvider SysAdminDAL
    /// </summary>
    public class SysAdminDAL : BaseDAL<SysAdmin>
    {
        /// <summary>
        /// SysAdminDAL
        /// </summary>
        public SysAdminDAL()
        {
        }

        /// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysAdmin GetData(string value)
        {
            return GetDataByProperty("Name", value);
        }

        /// <summary>
        /// 根据数据name 或 PhoneNo属性值获取相关记录
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public SysAdmin GetLoginData(string value)
        {
            if (GetDataByProperty("Name", value) != null)
            {
                return GetDataByProperty("Name", value);
            }
            else
            {
                return GetDataByProperty("PhoneNo", value);
            }
        }
    }
}


