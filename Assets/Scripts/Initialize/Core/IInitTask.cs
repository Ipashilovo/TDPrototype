using System;
using Cysharp.Threading.Tasks;

namespace Initialize.Core
{
    public interface IInitTask
    {
        event Action<IInitTask> Completed;
        bool IsDone { get; }
        void Run();
    }
    
    public abstract class AsyncInitTask : IInitTask
    {
        public event Action<IInitTask> Completed;
        public bool IsDone { get; private set; }
        public async void Run()
        {
            await OnExecute();
            IsDone = true;
            Completed?.Invoke(this);
        }

        protected abstract UniTask OnExecute();
    }
    
    public abstract class SyncInitTask : IInitTask
    {
        public event Action<IInitTask> Completed;
        public bool IsDone { get; private set; }
        public void Run()
        {
            OnExecute();
            IsDone = true;
            Completed?.Invoke(this);
        }

        protected abstract void OnExecute();
    }
}