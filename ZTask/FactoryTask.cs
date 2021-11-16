using System;

namespace ZTask
{ public sealed class FactoryTask
    {
        private FactoryTask()
        {
                
        }

        internal static FactoryTask CreateInstance()
        {
            return new FactoryTask();
        }

        public IntervalJob StartNew(Action execution, TimeSpan interval)
        {
            var task = new IntervalJob(execution, interval);
            task.Start();
            return task;
        }

        public IntervalJob StartNew(Action execution, int second)
        {
            var task = new IntervalJob(execution, TimeSpan.FromSeconds(second));
            task.Start();
            return task;
        }

        public IntervalJob New(Action execution, TimeSpan interval) =>
            new IntervalJob(execution, interval);

        public IntervalJob New(Action execution, int second) =>
            new IntervalJob(execution, TimeSpan.FromSeconds(second));
    }
  
}