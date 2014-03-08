using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hope.ITMS.Model.Enum
{
    /// <summary>
    /// 频率
    /// </summary>
    public enum Frequency
    {
        /// <summary>
        /// Daily
        /// </summary>
        Daily = 1,

        /// <summary>
        /// Weekly
        /// </summary>
        Weekly = 2,

        /// <summary>
        /// Per month
        /// </summary>
        PerMonth = 3,

        /// <summary>
        /// Per year
        /// </summary>
        PerYear = 4,

        /// <summary>
        /// DesDate
        /// </summary>
        DesDate = 5,

        /// <summary>
        /// LoopDays
        /// </summary>
        LoopDays = 6,

        /// <summary>
        /// DayOfWeek
        /// </summary>
        DayOfWeek = 7,

        /// <summary>
        /// DayOfMonth
        /// </summary>
        DayOfMonth = 8,
    }

    public enum FrequencyCN
    {
        /// <summary>
        /// 每天
        /// </summary>
        每天 = 1,

        /// <summary>
        /// 每周
        /// </summary>
        每周 = 2,

        /// <summary>
        /// 每月
        /// </summary>
        每月 = 3,

        /// <summary>
        /// 每年
        /// </summary>
        每年 = 4,

        /// <summary>
        /// 指定日期
        /// </summary>
        指定日期 = 5,

        /// <summary>
        /// 循环指定天数
        /// </summary>
        循环指定天数 = 6,

        /// <summary>
        /// 每周第几天
        /// </summary>
        每周第几天 = 7,

        /// <summary>
        /// 每月第几天
        /// </summary>
        每月第几天 = 8,
    }
}
