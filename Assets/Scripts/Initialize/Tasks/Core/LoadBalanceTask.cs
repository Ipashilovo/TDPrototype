using Balance.BalanceConfig;
using Balance.Data;
using UnityEngine;
using Zenject;

namespace Initialize.Tasks.Core
{
    public class LoadBalanceTask : SyncInitTask
    {
        private DiContainer _container;

        public LoadBalanceTask(DiContainer diContainer)
        {
            _container = diContainer;
        }

        protected override void OnExecute()
        {
            var config = Resources.Load<BalanceConfig>("Config");
            var data = config.Get();
            _container.Bind<BalanceData>().FromInstance(data).AsSingle();
            data.BindFields(_container);
            data.BindProperties(_container);
        }
    }
}