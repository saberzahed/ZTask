using System;
using System.Threading.Tasks;

namespace ZTask
{
    public sealed class FactoryTask
    {
        private FactoryTask()
        {
        }

        internal static FactoryTask CreateInstance()
        {
            return new FactoryTask();
        }

      
        public IntervalJob StartNew(Action execution, TimeSpan interval,Action executionBeforeStart=null, Action executionAfterStop = null)
        {
            var executionAfterStopTask = executionAfterStop is null ? null : new Task(executionAfterStop);
            var task = new IntervalJob(execution,executionBeforeStart,executionAfterStopTask , interval);
            task.Start();
            return task;
        }

        public IntervalJob StartNew(Action execution, int second,Action executionBeforeStart=null, Action executionAfterStop = null) =>
            StartNew(execution, TimeSpan.FromSeconds(second),executionBeforeStart, executionAfterStop);

        public IntervalJob New(Action execution, TimeSpan interval,Action executionBeforeStart=null, Action executionAfterStop = null)
        {
            var executionAfterStopTask = executionAfterStop is null ? null : new Task(executionAfterStop);
            return new IntervalJob(execution, executionBeforeStart,executionAfterStopTask, interval);
        }

        public IntervalJob New(Action execution, int second,Action executionBeforeStart=null, Action executionAfterStop=null) =>
             New(execution,TimeSpan.FromSeconds(second),executionBeforeStart, executionAfterStop);
    }
}