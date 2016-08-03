using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WindowsFormsApplication1
{
    class MyScheduler : TaskScheduler
    {
        private BlockingCollection< Task> _queue = new BlockingCollection<Task>();
        private Thread _workingThread;
        private object _syncObj = new object();

        public MyScheduler()
        {
            _workingThread = new Thread(Run);
            _workingThread.IsBackground = true;
            _workingThread.Start();
        }

        private void Run()
        {
            while (true)
            {
                var task = _queue.Take();
                base.TryExecuteTask(task);
            }
        }

        protected override void QueueTask(Task task)
        {
            _queue.Add(task);
        }

        

        protected override bool TryExecuteTaskInline(Task task, bool taskWasPreviouslyQueued)
        {
            if(!task.IsCompleted)
            task.RunSynchronously();
            return true;
        }

        protected override IEnumerable<Task> GetScheduledTasks()
        {
            return _queue.ToArray();
        }
    }
}
