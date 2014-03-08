/******************************************************************
 *
 * 所在模块：Dara Access Layer(SQL数据库访问)
 * 类 名 称：DevReservationDAL(DevReservationDAL)
 * 功能描述：
 * 
 * ------------创建信息------------------
 * 作    者：jjf001
 * 日    期：2014-02-15
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
    /// DataProvider DevReservationDAL
    /// </summary>
    public class DevReservationDAL : BaseDAL<DevReservation>
    {
        /// <summary>
        /// DevReservationDAL
        /// </summary>
        public DevReservationDAL()
        {
        }
		
		/// <summary>
        /// 根据数据name属性值获取相关记录
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public DevReservation GetData(string value)
        {
			return GetDataByProperty("Name", value);
        }
    }
}


