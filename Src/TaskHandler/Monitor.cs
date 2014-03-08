using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Hope.ITMS.Model;
using Hope.Util;

namespace Hope.ITMS.TaskHandler
{
    /// <summary>
    /// 
    /// </summary>
    public class Monitor 
    {
        private static Queue<ITask> _TaskQueue = new Queue<ITask>();
        /// <summary>
        /// 全局任务队列
        /// </summary>
        public static Queue<ITask> TaskQueue
        {
            get { return _TaskQueue; }
        }


        /// <summary>
        /// 定时任务委托方法
        /// </summary>
        public delegate void TimerTaskDelegate();


        /// <summary>
        /// 有参数的定时任务委托方法
        /// </summary>
        public delegate void ParmTimerTaskDelegate( object[] parm );

        

        /// <summary>
        /// 启动
        /// </summary>
        public void Start()
        {
            while (true)
            {
                LogUtil.Write("thread is running……" + DateTime.Now);
                if (_TaskQueue.Count > 0)
                {
                    lock (_TaskQueue)
                    {
                        ITask task = _TaskQueue.Dequeue();
                        //检查是否到达指定时间
                        if (CheckTimer(task))
                        {
                            //调用执行处理方法
                            if (task.TimerTaskDelegateFun != null)
                            {
                                task.TimerTaskDelegateFun();
                            }
                            else if (task.ParmTimerTaskDelegateFun != null)
                            {
                                task.ParmTimerTaskDelegateFun(task.Parm);
                            }
                            else
                            {
                                task.Run();
                            }
                            //重新计算下次执行时间
                            task.getNextRunTime();
                        }
                        //如果没有到达指定时间，则放回队列中
                        else
                        {
                            _TaskQueue.Enqueue(task);
                        }
                    }
                }

                Thread.Sleep(60000);
            }
// ReSharper disable FunctionNeverReturns
        }
// ReSharper restore FunctionNeverReturns


        #region 检查定时器

        /// <summary>
        /// 检查定时器
        /// </summary>
        /// <param name="task"></param>
        /// <returns></returns>
        private bool CheckTimer(ITask task)
        {

            //计算下次执行时间
            task.getNextRunTime();

            DateTime DateTimeNow = DateTime.Now;

            //时间比较
            bool dateComp = DateTimeNow.Year == task.NextRunTime.Year && DateTimeNow.Month == task.NextRunTime.Month &&
                            DateTimeNow.Day == task.NextRunTime.Day;

            bool timeComp = DateTimeNow.Hour == task.NextRunTime.Hour && DateTimeNow.Minute == task.NextRunTime.Minute &&
                            DateTimeNow.Second == task.NextRunTime.Second;

            return dateComp && timeComp;
        }

        #endregion

    }
}
