using System;
using System.Collections.Generic;
using System.Text;

namespace Hope.TemplateUtil
{
    /// <summary>
    /// 日期时间类标签
    /// </summary>
    public partial class HopeTag
    {
        #region Fields

        private DateTime _ShowDate = DateTime.Now;

        #endregion

        #region 当前时间

        /// <summary>
        /// 显示日期
        /// </summary>
        public DateTime ShowDate
        {
            get { return _ShowDate; }
            set { _ShowDate = value; }
        }

        #endregion
        
        #region 当前时间

        /// <summary>
        /// 当前时间
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.CurrentDateTime}
        /// </example>
        public DateTime CurrentDateTime
        {
            get { return DateTime.Now; }
        }

        #endregion


        #region 当前年

        /// <summary>
        /// 当前年
        /// </summary>
        /// <example>
        /// 调用：${HopeTag.CurrentYear}
        /// </example>
        public int CurrentYear
        {
            get { return DateTime.Now.Year; }
        }

        #endregion


        #region 返回前、后几天时间

        /// <summary>
        /// 返回前、后几天时间
        /// </summary>
        /// <param name="day">天数，负数为提前，正数为推后</param>
        /// <returns>返回若干天前/后的日期</returns>
        /// <example>
        /// 调用：${HopeTag.LastDateTimeByNow(-7)}
        /// 输出：7天前的时间
        /// </example>
        public DateTime LastDateTimeByNow(int day)
        {
            return CurrentDateTime.AddDays(day);
        }

        #endregion


        #region 获取传入时间与当前时间差值

        /// <summary>
        /// 获取传入时间与当前时间差值
        /// </summary>
        /// <param name="birthday"></param>
        /// <returns></returns>
        public string GetAge(DateTime birthday)
        {
            string result = string.Empty;
            int year = 0;
            int month = 0;

            int oldMonth = birthday.Year*12 + birthday.Month;
            int newMonth = DateTime.Now.Year*12 + DateTime.Now.Month;

            int ts = newMonth - oldMonth;
            if (ts > 12)
            {
                year = ts/12;
                month = ts%12;
            }
            else
            {
                month = ts;
            }

            if (year > 0)
            {
                result += year + "岁";
            }
            if (month > 0)
            {
                result += month + "月";
            }
            return result;
        }

        #endregion

    }
}
