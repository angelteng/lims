
/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：SysTagDAL(SysTagDAL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：wensaint
 * 日    期：2012-06-01
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
using Hope.Util;
using NHibernate;
using System.Collections;

namespace Hope.ITMS.DAL
{
    /// <summary>
    /// DataProvider SysTagDAL
    /// </summary>
    public class SysTagDAL : BaseDAL<SysTag>
    {
        /// <summary>
        /// SysTagDAL
        /// </summary>
        public SysTagDAL()
        {
        }

        /// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public SysTag GetData(string value)
        {
            return GetDataByProperty("Name", value);
        }

        /// <summary>
        /// Get TagCategory List
        /// </summary>
        /// <returns></returns>
        public List<string> GetTagCategoryList()
        {
            string cmdText = string.Format("select Distinct TagCategory from {0} order by TagCategory", typeof(SysTag).FullName);
            ISession session = NHibernateHelper.SessionFactory.OpenSession();

            IList<string> list = session.CreateQuery(cmdText).List<string>();

            List<string> strList = new List<string>();
            strList.AddRange(list);
            return strList;            
        }
    }
}


