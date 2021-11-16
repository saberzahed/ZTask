using System;
using System.Threading;
using System.Threading.Tasks;

namespace ZTask
{
    public class IntervalJob
    {
        private readonly Task _afterStop;
        private readonly TimeSpan _interval;
        private readonly Task _task;
        private readonly CancellationTokenSource _token;

        public static FactoryTask Factory => FactoryTask.CreateInstance();

        internal IntervalJob(Action execution, Action beforeStart, Task afterStop, TimeSpan interval)
        {
            _afterStop = afterStop;
            _interval = interval;
            _token = new CancellationTokenSource();
            _task = new Task(() =>
            {
                beforeStart?.Invoke();
                Do(_token.Token, execution);
            }, TaskCreationOptions.LongRunning);
        }

        private void Do(CancellationToken cancellationToken, Action action)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                action();
                Thread.Sleep(_interval);
            }
        }

        public void Start()
        {
            if (_task.Status != TaskStatus.Created)
                return;
            _task.Start();
        }

        public async Task Stop()
        {
            _token?.Cancel();
            await _task;
            _task?.Dispose();
            if (_afterStop != null)
            {
                _afterStop.Start();
                await _afterStop;
            }
        }
    }
}