using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Hope.ITMS.Model;
using Hope.ITMS.Model.Enum;

namespace Hope.ITMS.TaskHandler
{
    /// <summary>
    /// 队列任务
    /// </summary>
    public class Task : ITask
    {
        public Task(TaskTask task, Timer timer, Monitor.TimerTaskDelegate taskDelegate) : base(task, timer, taskDelegate)
        {
            this._TaskTask = task;
            this._Timer = timer;
            this._TimerTaskDelegateFun = taskDelegate;
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
