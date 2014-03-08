using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hope.ITMS.Model.Enum;

namespace Hope.ITMS.TaskHandler
{
    /// <summary>
    /// 定时信息类
    /// Frequency     类型：Daily(每天),Weekly(每周),PerMonth(每月),PerYear(每年),DesDate(指定日期),LoopDays(循环天数)
    /// DateValue     日期值：TimerType="DayOfWeek"时,值为1-7表示周一到周日;TimerType="DayOfMonth"时,值为1-31表示1号到31号,
    ///               TimerType="LoopDays"时,值为要循环的天数,TimerType为其它值时,此值无效
    /// Year   年：   TimerType="DesDate"时,此值有效
    /// Month  月：   TimerType="DesDate"时,此值有效
    /// Day    日：   TimerType="DesDate"时,此值有效
    /// Hour   时：   设置的执行时间
    /// Minute 分：   设置的执行时间 
    /// Second 秒：   设置的执行时间
    public class Timer
    {
        /// <summary>
        /// 频率，Daily(每天),Weekly(每周),PerMonth(每月),PerYear(每年),DesDate(指定日期),LoopDays(循环天数)
        /// </summary>
        public Frequency Frequency;

        /// <summary>
        /// 日期值：TimerType="DayOfWeek"时,值为1-7表示周一到周日;TimerType="DayOfMonth"时,值为1-31表示1号到31号,
        /// imerType="LoopDays"时,值为要循环的天数,TimerType为其它值时,此值无效
        /// </summary>
        public int DateValue;

        /// <summary>
        /// TimerType="DesDate"时,此值有效
        /// </summary>
        public int Year;

        /// <summary>
        /// TimerType="DesDate"时,此值有效
        /// </summary>
        public int Month;

        /// <summary>
        /// TimerType="DesDate"时,此值有效
        /// </summary>
        public int Day;

        /// <summary>
        /// 时：   设置的执行时间
        /// </summary>
        public int Hour = 00;

        /// <summary>
        /// 分：   设置的执行时间 
        /// </summary>
        public int Minute = 00;

        /// <summary>
        /// 秒：   设置的执行时间
        /// </summary>
        public int Second = 00;
    }
}
