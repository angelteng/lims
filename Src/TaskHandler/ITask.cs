using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hope.ITMS.Model;
using Hope.ITMS.Model.Enum;

namespace Hope.ITMS.TaskHandler
{
    public abstract class ITask
    {
        #region 基本参数

        protected TaskTask _TaskTask; //任务详情

        protected Timer _Timer; //时间信息


        protected DateTime _NextRunTime; 
        /// <summary>
        /// 下一次执行时间
        /// </summary>
        public DateTime NextRunTime
        {
            get { return _NextRunTime; }
        }

        protected Monitor.TimerTaskDelegate _TimerTaskDelegateFun; 
        /// <summary>
        /// 执行具体任务的委托方法
        /// </summary>
        public Monitor.TimerTaskDelegate TimerTaskDelegateFun
        {
            get { return _TimerTaskDelegateFun; }
        }

        protected Monitor.ParmTimerTaskDelegate _ParmTimerTaskDelegateFun; 
        /// <summary>
        ///带参数的执行具体任务的委托方法
        /// </summary>
        public Monitor.ParmTimerTaskDelegate ParmTimerTaskDelegateFun
        {
            get { return _ParmTimerTaskDelegateFun; }
        }


        protected object[] _Parm; 
        /// <summary>
        ///参数
        /// </summary>
        public object[] Parm
        {
            get { return _Parm; }
        }

        #endregion

        protected ITask(TaskTask task, Timer timer, Monitor.TimerTaskDelegate taskDelegate){}

        #region 计算下一次执行时间

        /// <summary>
        /// 计算下一次执行时间
        /// </summary>
        /// <returns></returns>
        public void getNextRunTime()
        {
            DateTime now = DateTime.Now;
            int nowHH = now.Hour;
            int nowMM = now.Minute;
            int nowSS = now.Second;

            int timeHH = _Timer.Hour;
            int timeMM = _Timer.Minute;
            int timeSS = _Timer.Second;

            //设置执行时间对当前时间进行比较
            bool nowTimeComp = nowHH < timeHH || (nowHH <= _Timer.Hour && nowMM < timeMM) ||
                               (nowHH <= _Timer.Hour && nowMM <= timeMM && nowSS < timeSS);


            //每天
            if (_Timer.Frequency == Frequency.Daily)
            {

                if (nowTimeComp)
                {
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS);
                }
                else
                {
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS).AddDays(1);
                }
            }
            //每周一次
            else if (_Timer.Frequency == Frequency.Weekly)
            {
                DayOfWeek ofweek = DateTime.Now.DayOfWeek;

                int dayOfweek = Convert.ToInt32(DateTime.Now.DayOfWeek);

                if (ofweek == DayOfWeek.Sunday) dayOfweek = 7;

                if (dayOfweek < _Timer.DateValue)
                {
                    int addDays = _Timer.DateValue - dayOfweek;
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS).AddDays(addDays);
                }
                else if (dayOfweek == _Timer.DateValue && nowTimeComp)
                {
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS);

                }
                else
                {
                    int addDays = 7 - (dayOfweek - _Timer.DateValue);
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS).AddDays(addDays);
                }
            }
            //每月一次
            else if (_Timer.Frequency == Frequency.PerMonth)
            {
                if (now.Day < _Timer.DateValue)
                {
                    _NextRunTime = new DateTime(now.Year, now.Month, _Timer.DateValue, timeHH, timeMM, timeSS);
                }
                else if (now.Day == _Timer.DateValue && nowTimeComp)
                {
                    _NextRunTime = new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS);
                }
                else
                {
                    _NextRunTime =
                        new DateTime(now.Year, now.Month, _Timer.DateValue, timeHH, timeMM, timeSS).AddMonths(1);
                }
            }
            //指定日期
            else if (_Timer.Frequency == Frequency.DesDate)
            {
                _NextRunTime = new DateTime(_Timer.Year, _Timer.Month, _Timer.Day, timeHH, timeMM, timeSS);
            }
            //循环指定天数
            else if (_Timer.Frequency == Frequency.LoopDays)
            {
                _NextRunTime =
                    new DateTime(now.Year, now.Month, now.Day, timeHH, timeMM, timeSS).AddDays(_Timer.DateValue);
            }
        }

        #endregion

        public abstract void Run();
    }
}
