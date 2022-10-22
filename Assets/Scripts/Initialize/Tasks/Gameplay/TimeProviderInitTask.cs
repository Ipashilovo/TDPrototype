using Core.GameplaySystems;
using Initialize.Core;
using Zenject;

namespace Initialize.Tasks.Gameplay
{
    public class TimeProviderInitTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public TimeProviderInitTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            _container.BindInterfacesAndSelfTo<TimeProvider>().FromNew().AsSingle();
        }
    }
}