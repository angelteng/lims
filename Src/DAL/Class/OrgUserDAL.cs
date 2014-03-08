/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：OrgUserDAL(OrgUserDAL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2013-10-26
 * xingzhewujiang1999@gmail.com
 * QQ:970355214
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
    /// DataProvider OrgUserDAL
    /// </summary>
    public class OrgUserDAL : BaseDAL<OrgUser>
    {
        /// <summary>
        /// OrgUserDAL
        /// </summary>
        public OrgUserDAL()
        {
        }
		
		/// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public OrgUser GetData(string value)
        {
			return GetDataByProperty("Name", value);
        }
    }
}


