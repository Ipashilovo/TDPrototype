using System.Collections.Generic;
using Balance.Data;
using Core.StatsProviders.Providers;
using Entity;
using Initialize.Core;
using Zenject;

namespace Initialize.Tasks.Core
{
    [RequireTask(typeof(LoadBalanceTask))]
    public class StatsProviderInitTask : SyncInitTask
    {
        private readonly DiContainer _container;

        public StatsProviderInitTask(DiContainer container)
        {
            _container = container;
        }
        
        protected override void OnExecute()
        {
            var balance = _container.Resolve<BalanceData>();
            var stats = new Dictionary<UnitId, MovementStats>
            {
                {balance.PlayerData.Id, balance.PlayerData.MovementData.MovementStats}
            };
            MovementStatsProvider movementStatsProvider = new MovementStatsProvider(stats);
            _container.BindInterfacesTo<MovementStatsProvider>().FromInstance(movementStatsProvider).AsSingle();
        }
    }
}